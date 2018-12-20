using MeshyDB.SDK.Models;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace MeshyDB.SDK.Services
{
    /// <summary>
    /// Implementation of <see cref="IHttpService"/>
    /// </summary>
    internal class HttpService : IHttpService
    {
        /// <inheritdoc/>
        public async Task<T> SendRequestAsync<T>(HttpServiceRequest request)
        {
            var webRequest = WebRequest.CreateHttp(request.RequestUri);
            webRequest.Method = request.Method.Method;

            webRequest.Accept = "application/json";

            if (request.Headers.TryGetValue("Authorization", out var token))
            {
                webRequest.UseDefaultCredentials = true;
                webRequest.PreAuthenticate = true;
                webRequest.Credentials = CredentialCache.DefaultCredentials;
                webRequest.Headers.Add(HttpRequestHeader.Authorization, token);
            }

            if (!string.IsNullOrWhiteSpace(request.Content))
            {
                webRequest.ContentLength = request.Content.Length;
                webRequest.ContentType = request.ContentType;

                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    streamWriter.Write(request.Content);
                    streamWriter.Flush();
                }

            }

            var response = await webRequest.GetResponseAsync();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var content = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
        }
    }
}
