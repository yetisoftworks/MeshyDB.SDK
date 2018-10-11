using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MeshyDB.SDK.Services
{
    /// <summary>
    /// Implementation of <see cref="IHttpService"/>
    /// </summary>
    internal class HttpService : IHttpService
    {
        /// <summary>
        /// Http client to make requests against
        /// </summary>
        private static readonly HttpClient HttpClient = new HttpClient();

        /// <summary>
        /// Instantiates static instance of class <see cref="HttpService"/>
        /// </summary>
        static HttpService()
        {
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <inheritdoc/>
        public async Task<T> SendRequestAsync<T>(HttpRequestMessage requestMessage)
        {
            var response = await HttpClient.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
