// <copyright file="IRequestService.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MeshyDB.SDK.Enums;

namespace MeshyDB.SDK.Services
{
    /// <summary>
    /// Defines methods for HTTP requests actions.
    /// </summary>
    internal interface IRequestService
    {
        /// <summary>
        /// Performs HTTP GET request.
        /// </summary>
        /// <typeparam name="T">Type of data to be returned from request.</typeparam>
        /// <param name="path">Endpoint path to request.</param>
        /// <returns>Data result made from request.</returns>
        Task<T> GetRequest<T>(string path);

        /// <summary>
        /// Performs HTTP GET request.
        /// </summary>
        /// <typeparam name="T">Type of data to be returned from request.</typeparam>
        /// <param name="path">Endpoint path to request.</param>
        /// <param name="headers">Custom headers to append with request.</param>
        /// <returns>Data result made from request.</returns>
        Task<T> GetRequest<T>(string path, IDictionary<string, string> headers);

        /// <summary>
        /// Performs HTTP DELETE request.
        /// </summary>
        /// <typeparam name="T">Type of data to be returned from request.</typeparam>
        /// <param name="path">Endpoint path to request.</param>
        /// <returns>Data result made from request.</returns>
        Task<T> DeleteRequest<T>(string path);

        /// <summary>
        /// Performs HTTP DELETE request.
        /// </summary>
        /// <typeparam name="T">Type of data to be returned from request.</typeparam>
        /// <param name="path">Endpoint path to request.</param>
        /// <param name="model">Mesh data to be deleted.</param>
        /// <param name="format">Data format to serialize data for the request.</param>
        /// <returns>Data result made from request.</returns>
        Task<T> DeleteRequest<T>(string path, object model, RequestDataFormat format = RequestDataFormat.Json);

        /// <summary>
        /// Performs HTTP PATCH request.
        /// </summary>
        /// <typeparam name="T">Type of data to be returned from request.</typeparam>
        /// <param name="path">Endpoint path to request.</param>
        /// <param name="model">Mesh data to be deleted.</param>
        /// <param name="format">Data format to serialize data for the request.</param>
        /// <returns>Data result made from request.</returns>
        Task<T> PatchRequest<T>(string path, object model, RequestDataFormat format = RequestDataFormat.Json);

        /// <summary>
        /// Performs HTTP POST request.
        /// </summary>
        /// <typeparam name="T">Type of data to be returned from request.</typeparam>
        /// <param name="path">Endpoint path to request.</param>
        /// <param name="model">Mesh data to be committed.</param>
        /// <param name="format">Data format to serialize data for the request.</param>
        /// <returns>Data result made from request.</returns>
        Task<T> PostRequest<T>(string path, object model, RequestDataFormat format = RequestDataFormat.Json);

        /// <summary>
        /// Performs HTTP PUT request.
        /// </summary>
        /// <typeparam name="T">Type of data to be returned from request.</typeparam>
        /// <param name="path">Endpoint path to request.</param>
        /// <param name="model">Mesh data to be committed.</param>
        /// <returns>Data result made from request.</returns>
        Task<T> PutRequest<T>(string path, object model);
    }
}
