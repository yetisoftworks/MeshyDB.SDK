// <copyright file="RequestService.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
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
        /// <param name="httpService">Service to make HTTP requests against.</param>
        /// <param name="baseUrl">Base Api Url to make requests for.</param>
        /// <param name="tenant">Tenant of data used for partitioning.</param>
        public RequestService(IHttpService httpService, string baseUrl, string tenant = null)
            : this(httpService, baseUrl, tenant, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestService"/> class.
        /// </summary>
        /// <param name="httpService">Service to make HTTP requests against.</param>
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
        public Task<T> GetRequest<T>(string path)
        {
            return this.GetRequest<T>(path, null);
        }

        /// <inheritdoc/>
        public Task<T> GetRequest<T>(string path, IDictionary<string, string> headers)
        {
            var request = this.GetDefaultRequestMessageAsync(path, HttpMethod.Get, headers).ConfigureAwait(false).GetAwaiter().GetResult();

            return this.SendRequest<T>(request);
        }

        /// <inheritdoc/>
        public Task<T> DeleteRequest<T>(string path)
        {
            var request = this.GetDefaultRequestMessageAsync(path, HttpMethod.Delete).ConfigureAwait(false).GetAwaiter().GetResult();

            return this.SendRequest<T>(request);
        }

        /// <inheritdoc/>
        public Task<T> DeleteRequest<T>(string path, object model, RequestDataFormat format = RequestDataFormat.Json)
        {
            var request = this.GetDefaultRequestMessageAsync(path, HttpMethod.Delete).ConfigureAwait(false).GetAwaiter().GetResult();

            request.Content = this.GetContent(model, format).ConfigureAwait(false).GetAwaiter().GetResult();
            request.RequestDataFormat = format;

            if (format == RequestDataFormat.Form)
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }

            return this.SendRequest<T>(request);
        }

        /// <inheritdoc/>
        public Task<T> PatchRequest<T>(string path, object model, RequestDataFormat format = RequestDataFormat.Json)
        {
            var request = this.GetDefaultRequestMessageAsync(path, new HttpMethod("PATCH")).ConfigureAwait(false).GetAwaiter().GetResult();

            request.Content = this.GetContent(model, format).ConfigureAwait(false).GetAwaiter().GetResult();
            request.RequestDataFormat = format;

            if (format == RequestDataFormat.Form)
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }

            return this.SendRequest<T>(request);
        }

        /// <inheritdoc/>
        public Task<T> PostRequest<T>(string path, object model, RequestDataFormat format = RequestDataFormat.Json)
        {
            var request = this.GetDefaultRequestMessageAsync(path, HttpMethod.Post).ConfigureAwait(false).GetAwaiter().GetResult();

            request.Content = this.GetContent(model, format).ConfigureAwait(false).GetAwaiter().GetResult();
            request.RequestDataFormat = format;

            if (format == RequestDataFormat.Form)
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }

            return this.SendRequest<T>(request);
        }

        /// <inheritdoc/>
        public Task<T> PutRequest<T>(string path, object model)
        {
            var request = this.GetDefaultRequestMessageAsync(path, HttpMethod.Put).ConfigureAwait(false).GetAwaiter().GetResult();

            request.Content = JsonConvert.SerializeObject(model, new JsonSerializerSettings()
            {
                ContractResolver = new MeshyDBJsonContractResolver(),
            });

            return this.SendRequest<T>(request);
        }

        private Task<string> GetContent<T>(T model, RequestDataFormat format)
        {
            switch (format)
            {
                case RequestDataFormat.Json:
                    return Task.FromResult(JsonConvert.SerializeObject(model, new JsonSerializerSettings()
                    {
                        ContractResolver = new MeshyDBJsonContractResolver(),
                    }));
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

                    return content.ReadAsStringAsync();
                default:
                    return Task.FromResult<string>(null);
            }
        }

        private Task<HttpServiceRequest> GetDefaultRequestMessageAsync(string path, HttpMethod method = null, IDictionary<string, string> headers = null)
        {
            var request = new HttpServiceRequest();
            this.PopulateHeadersAsync(request.Headers, headers);

            if (!Uri.TryCreate($"{this.baseUrl}/{path}", UriKind.Absolute, out var validatedUri))
            {
                throw new InvalidOperationException($"Unable to create uri for base: {this.baseUrl} and path: {path}");
            }

            request.Method = method ?? HttpMethod.Get;
            request.RequestUri = validatedUri;
            request.ContentType = "application/json";

            return Task.FromResult(request);
        }

        private void PopulateHeadersAsync(IDictionary<string, string> headers, IDictionary<string, string> overrideHeaders)
        {
            if (this.tokenService != null && !string.IsNullOrWhiteSpace(this.authenticationId))
            {
                var token = this.tokenService.GetAccessTokenAsync(this.authenticationId).ConfigureAwait(false).GetAwaiter().GetResult();
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

        private Task<T> SendRequest<T>(HttpServiceRequest requestMessage)
        {
            return this.httpService.SendRequestAsync<T>(requestMessage);
        }
    }
}
