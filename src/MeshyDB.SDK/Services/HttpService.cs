// <copyright file="HttpService.cs" company="Yetisoftworks LLC">
// Copyright (c) Yetisoftworks LLC. All rights reserved.
// </copyright>

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MeshyDB.SDK.Models;
using Newtonsoft.Json;

namespace MeshyDB.SDK.Services
{
    /// <summary>
    /// Implementation of <see cref="IHttpService"/>.
    /// </summary>
    internal class HttpService : IHttpService
    {
        private readonly HttpClient client = new HttpClient();

        /// <inheritdoc/>
        public async Task<T> SendRequestAsync<T>(HttpServiceRequest request)
        {
            var httpClient = this.client;
            var message = new HttpRequestMessage
            {
                Method = request.Method,
            };

            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            message.RequestUri = request.RequestUri;

            if (!string.IsNullOrEmpty(request.Content))
            {
                message.Content = new StringContent(request.Content, Encoding.UTF8, request.ContentType);
            }

            foreach (var item in request.Headers)
            {
                message.Headers.Add(item.Key, item.Value);
            }

            var response = await httpClient.SendAsync(message);
            response = response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseString);
        }
    }
}
