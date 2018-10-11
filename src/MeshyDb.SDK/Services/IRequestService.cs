using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MeshyDB.SDK.Enums;

namespace MeshyDB.SDK.Services
{
    /// <summary>
    /// Defines methods for http requests actions
    /// </summary>
    internal interface IRequestService
    {
        /// <summary>
        /// Performs HTTP GET request
        /// </summary>
        /// <typeparam name="T">Type of data to be returned from request</typeparam>
        /// <param name="path">Endpoint path to request</param>
        /// <returns>Data result made from request</returns>
        Task<T> GetRequest<T>(string path);

        /// <summary>
        /// Performs HTTP DELETE request
        /// </summary>
        /// <typeparam name="T">Type of data to be returned from request</typeparam>
        /// <param name="path">Endpoint path to request</param>
        /// <returns>Data result made from request</returns>
        Task<T> DeleteRequest<T>(string path);

        /// <summary>
        /// Performs HTTP POST request
        /// </summary>
        /// <typeparam name="T">Type of data to be returned from request</typeparam>
        /// <param name="path">Endpoint path to request</param>
        /// <param name="model">Mesh data to be comitted</param>
        /// <param name="format">Data format to serialize data for the request</param>
        /// <returns>Data result made from request</returns>
        Task<T> PostRequest<T>(string path, object model, RequestDataFormat format = RequestDataFormat.Json);

        /// <summary>
        /// Performs HTTP PUT request
        /// </summary>
        /// <typeparam name="T">Type of data to be returned from request</typeparam>
        /// <param name="path">Endpoint path to request</param>
        /// <param name="model">Mesh data to be comitted</param>
        /// <returns>Data result made from request</returns>
        Task<T> PutRequest<T>(string path, object model);
    }
}
