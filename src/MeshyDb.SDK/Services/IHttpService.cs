using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MeshyDB.SDK.Services
{
    /// <summary>
    /// Defines methods for http requests via a service
    /// </summary>
    internal interface IHttpService
    {
        /// <summary>
        /// Sends http request message
        /// </summary>
        /// <typeparam name="T">The type of data to be returned after a request is made</typeparam>
        /// <param name="requestMessage">Message data required to make an http request</param>
        /// <returns>Result of data generated from http request</returns>
        Task<T> SendRequestAsync<T>(HttpRequestMessage requestMessage);
    }
}
