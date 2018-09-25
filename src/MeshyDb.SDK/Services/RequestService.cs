using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MeshyDb.SDK;
using MeshyDb.SDK.Enums;
using MeshyDb.SDK.Models;
using MeshyDb.SDK.Resolvers;

namespace MeshyDb.SDK.Services
{
    /// <summary>
    /// Implementation of <see cref="IRequestService"/>
    /// </summary>
    internal class RequestService : IRequestService
    {
        /// <summary>
        /// Instantiates an instance of the <see cref="RequestService"/> class.
        /// </summary>
        /// <param name="httpService">Service to make http requests against</param>
        /// <param name="baseUrl">Base Api Url to make requests for</param>
        public RequestService(IHttpService httpService, string baseUrl) : this(httpService, baseUrl,null) { }

        /// <summary>
        /// Instantiates an instance of the <see cref="RequestService"/> class.
        /// </summary>
        /// <param name="httpService">Service to make http requests against</param>
        /// <param name="baseUrl">Base Api Url to make requests for</param>
        /// <param name="tokenService">Service to get token to add authentication to endpoint</param>
        public RequestService(IHttpService httpService, string baseUrl, ITokenService tokenService)
        {
            this.tokenService = tokenService;
            this.httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
            this.baseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
        }

        private readonly ITokenService tokenService;
        private readonly IHttpService httpService;
        private readonly string baseUrl;

        /// <inheritdoc/>
        public async Task<T> GetRequest<T>(string path)
        {
            var request = await GetDefaultRequestMessageAsync(path, HttpMethod.Get);

            return await SendRequest<T>(request);
        }

        /// <inheritdoc/>
        public async Task<T> DeleteRequest<T>(string path)
        {
            var request = await GetDefaultRequestMessageAsync(path, HttpMethod.Delete);

            return await SendRequest<T>(request);
        }

        /// <inheritdoc/>
        public async Task<T> PostRequest<T>(string path, object model, RequestDataFormat format = RequestDataFormat.Json)
        {
            var request = await GetDefaultRequestMessageAsync(path, HttpMethod.Post);

            request.Content = GetContent(model, format);

            return await SendRequest<T>(request);
        }

        private HttpContent GetContent<T>(T model, RequestDataFormat format)
        {
            switch (format)
            {
                case RequestDataFormat.Json:
                    return new StringContent(JsonConvert.SerializeObject(model, new JsonSerializerSettings()
                    {
                        ContractResolver = new MeshyDbJsonContractResolver()
                    }),
                                                    Encoding.UTF8,
                                                    "application/json");
                case RequestDataFormat.Form:
                    return new FormUrlEncodedContent(model.GetType()
                                         .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                              .ToDictionary(prop =>
                                              {
                                                  var jsonProp = prop.GetCustomAttributes().FirstOrDefault(x => x.GetType() == typeof(JsonPropertyAttribute)) as JsonPropertyAttribute;

                                                  return jsonProp?.PropertyName ?? prop.Name;
                                              },
                                              prop => prop.GetValue(model, null)?.ToString()));
                default:
                    return null;
            }

        }

        /// <inheritdoc/>
        public async Task<T> PutRequest<T>(string path, object model)
        {
            var request = await GetDefaultRequestMessageAsync(path, HttpMethod.Put);

            request.Content = new StringContent(JsonConvert.SerializeObject(model, new JsonSerializerSettings()
            {
                ContractResolver = new MeshyDbJsonContractResolver()
            }),
                                    Encoding.UTF8,
                                    "application/json");

            return await SendRequest<T>(request);
        }

        private async Task<HttpRequestMessage> GetDefaultRequestMessageAsync(string path, HttpMethod method = null)
        {
            var request = new HttpRequestMessage();
            await PopulateHeadersAsync(request.Headers);

            if (!Uri.TryCreate($"{baseUrl}/{path}", UriKind.Absolute, out var validatedUri))
            {
                throw new InvalidOperationException($"Unable to create uri for base: {baseUrl} and path: {path}");
            }

            request.Method = method ?? HttpMethod.Get;
            request.RequestUri = validatedUri;

            return request;
        }

        private async Task PopulateHeadersAsync(HttpRequestHeaders headers)
        {
            if (this.tokenService != null)
            {
                var token = await this.tokenService.GetOAuthTokenAsync();
                if (!string.IsNullOrWhiteSpace(token))
                {
                    headers.Add("Authorization", $"Bearer {token}");
                }
            }
        }

        private async Task<T> SendRequest<T>(HttpRequestMessage requestMessage)
        {
            return await this.httpService.SendRequestAsync<T>(requestMessage);
        }
    }
}
