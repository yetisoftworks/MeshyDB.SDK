// <copyright file="HttpServiceRequest.cs" company="Yetisoftworks LLC">
// Copyright (c) Yetisoftworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using MeshyDB.SDK.Enums;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class defining elements of a Http Service Request to abstract away any http specific information.
    /// </summary>
    public class HttpServiceRequest
    {
        /// <summary>
        /// Gets or sets request Uri to make a request for.
        /// </summary>
        public Uri RequestUri { get; set; }

        /// <summary>
        /// Gets or sets the HTTP Verb to make the request for the Uri.
        /// </summary>
        public HttpMethod Method { get; set; }

        /// <summary>
        /// Gets or sets the Content to send to the request.
        /// </summary>
        /// <remarks>
        ///     Primarily used in a POST or PUT request.
        /// </remarks>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the Content type of the Content supplied.
        /// </summary>
        /// <remarks>
        ///     Current examples are application/json and application/x-www-form-urlencoded.
        /// </remarks>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the expected data format coming back from the request.
        /// </summary>
        /// <remarks>
        ///     Defaulted to Json.
        /// </remarks>
        public RequestDataFormat RequestDataFormat { get; set; } = RequestDataFormat.Json;

        /// <summary>
        /// Gets or sets headers to be added to request.
        /// </summary>
        /// <remarks>
        /// Example would be Authentication.
        /// </remarks>
        public IDictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    }
}
