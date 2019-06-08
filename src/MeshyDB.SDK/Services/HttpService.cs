// <copyright file="HttpService.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
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
        /// <inheritdoc/>
        public async Task<T> SendRequestAsync<T>(HttpServiceRequest request)
        {
            using (var httpClient = new HttpClient())
            {
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

                var response = await httpClient.SendAsync(message).ConfigureAwait(true);
                response = response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                return JsonConvert.DeserializeObject<T>(responseString);
            }
        }
    }
}
