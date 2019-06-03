// <copyright file="RequestService.cs" company="Yetisoftworks LLC">
// Copyright (c) Yetisoftworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MeshyDB.SDK.Enums;
using MeshyDB.SDK.Models;
using MeshyDB.SDK.Resolvers;
using Newtonsoft.Json;

namespace MeshyDB.SDK.Services
{
    /// <summary>
    /// Implementation of <see cref="IRequestService"/>.
    /// </summary>
    internal class RequestService : IRequestService
    {
        private readonly ITokenService tokenService;
        private readonly IHttpService httpService;
        private readonly string baseUrl;
        private readonly string authenticationId;
        private readonly string tenant;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestService"/> class.
        /// </summary>
        /// <param name="httpService">Service to make http requests against.</param>
        /// <param name="baseUrl">Base Api Url to make requests for.</param>
        /// <param name="tenant">Tenant of data used for partitioning.</param>
        public RequestService(IHttpService httpService, string baseUrl, string tenant = null)
            : this(httpService, baseUrl, tenant, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestService"/> class.
        /// </summary>
        /// <param name="httpService">Service to make http requests against.</param>
        /// <param name="baseUrl">Base Api Url to make requests for.</param>
        /// <param name="tenant">Tenant of data used for partitioning.</param>
        /// <param name="tokenService">Service to get token to add authentication to endpoint.</param>
        /// <param name="authenticationId">Internal identifier provided from login process.</param>
        public RequestService(IHttpService httpService, string baseUrl, string tenant, ITokenService tokenService, string authenticationId)
        {
            this.tokenService = tokenService;
            this.httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
            this.baseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
            this.authenticationId = authenticationId;
            this.tenant = tenant;
        }

        /// <inheritdoc/>
        public async Task<T> GetRequest<T>(string path)
        {
            return await this.GetRequest<T>(path, null);
        }

        /// <inheritdoc/>
        public async Task<T> GetRequest<T>(string path, IDictionary<string, string> headers)
        {
            var request = await this.GetDefaultRequestMessageAsync(path, HttpMethod.Get, headers);

            return await this.SendRequest<T>(request);
        }

        /// <inheritdoc/>
        public async Task<T> DeleteRequest<T>(string path)
        {
            var request = await this.GetDefaultRequestMessageAsync(path, HttpMethod.Delete);

            return await this.SendRequest<T>(request);
        }

        /// <inheritdoc/>
        public async Task<T> PostRequest<T>(string path, object model, RequestDataFormat format = RequestDataFormat.Json)
        {
            var request = await this.GetDefaultRequestMessageAsync(path, HttpMethod.Post);

            request.Content = await this.GetContent(model, format);
            request.RequestDataFormat = format;

            if (format == RequestDataFormat.Form)
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }

            return await this.SendRequest<T>(request);
        }

        /// <inheritdoc/>
        public async Task<T> PutRequest<T>(string path, object model)
        {
            var request = await this.GetDefaultRequestMessageAsync(path, HttpMethod.Put);

            request.Content = JsonConvert.SerializeObject(model, new JsonSerializerSettings()
            {
                ContractResolver = new MeshyDBJsonContractResolver(),
            });

            return await this.SendRequest<T>(request);
        }

        private async Task<string> GetContent<T>(T model, RequestDataFormat format)
        {
            switch (format)
            {
                case RequestDataFormat.Json:
                    return JsonConvert.SerializeObject(model, new JsonSerializerSettings()
                    {
                        ContractResolver = new MeshyDBJsonContractResolver(),
                    });
                case RequestDataFormat.Form:
                    var content = new FormUrlEncodedContent(model.GetType()
                                         .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                              .ToDictionary(
                                                  prop =>
                                                  {
                                                      var jsonProp = prop.GetCustomAttributes().FirstOrDefault(x => x.GetType() == typeof(JsonPropertyAttribute)) as JsonPropertyAttribute;

                                                      return jsonProp?.PropertyName ?? prop.Name;
                                                  }, prop => prop.GetValue(model, null)?.ToString()));

                    content.Headers.ContentType.CharSet = Encoding.UTF8.BodyName;

                    return await content.ReadAsStringAsync();
                default:
                    return null;
            }
        }

        private async Task<HttpServiceRequest> GetDefaultRequestMessageAsync(string path, HttpMethod method = null, IDictionary<string, string> headers = null)
        {
            var request = new HttpServiceRequest();
            await this.PopulateHeadersAsync(request.Headers, headers);

            if (!Uri.TryCreate($"{this.baseUrl}/{path}", UriKind.Absolute, out var validatedUri))
            {
                throw new InvalidOperationException($"Unable to create uri for base: {this.baseUrl} and path: {path}");
            }

            request.Method = method ?? HttpMethod.Get;
            request.RequestUri = validatedUri;
            request.ContentType = "application/json";

            return request;
        }

        private async Task PopulateHeadersAsync(IDictionary<string, string> headers, IDictionary<string, string> overrideHeaders)
        {
            if (this.tokenService != null && !string.IsNullOrWhiteSpace(this.authenticationId))
            {
                var token = await this.tokenService.GetAccessTokenAsync(this.authenticationId);
                if (!string.IsNullOrWhiteSpace(token))
                {
                    headers.Add("Authorization", $"Bearer {token}");
                }
            }

            if (!string.IsNullOrEmpty(this.tenant))
            {
                headers.Add("tenant", this.tenant);
            }

            if (overrideHeaders != null)
            {
                foreach (var item in overrideHeaders)
                {
                    if (headers.ContainsKey(item.Key))
                    {
                        headers[item.Key] = item.Value;
                    }
                    else
                    {
                        headers.Add(item.Key, item.Value);
                    }
                }
            }
        }

        private async Task<T> SendRequest<T>(HttpServiceRequest requestMessage)
        {
            return await this.httpService.SendRequestAsync<T>(requestMessage);
        }
    }
}
