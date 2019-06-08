// <copyright file="IHttpService.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using MeshyDB.SDK.Models;

namespace MeshyDB.SDK.Services
{
    /// <summary>
    /// Defines methods for http requests via a service.
    /// </summary>
    public interface IHttpService
    {
        /// <summary>
        /// Sends http request message.
        /// </summary>
        /// <typeparam name="T">The type of data to be returned after a request is made.</typeparam>
        /// <param name="request">Message data required to make an http request.</param>
        /// <returns>Result of data generated from http request.</returns>
        Task<T> SendRequestAsync<T>(HttpServiceRequest request);
    }
}
