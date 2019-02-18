using MeshyDB.SDK.Enums;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Defines a Http Service Request to abstract away any http specific information
    /// </summary>
    public class HttpServiceRequest
    {
        /// <summary>
        /// Defines Request Uri to make a request for
        /// </summary>
        public Uri RequestUri { get; set; }

        /// <summary>
        /// Defines the HTTP Verb to make the request for the Uri
        /// </summary>
        public HttpMethod Method { get; set; }

        /// <summary>
        /// Defines the Content to send to the request
        /// </summary>
        /// <remarks>
        ///     Primarily used in a POST or PUT request
        /// </remarks>
        public string Content { get; set; }

        /// <summary>
        /// Defines the Content type of the Content supplied
        /// </summary>
        /// <remarks>
        ///     Current examples are application/json and application/x-www-form-urlencoded
        /// </remarks>
        public string ContentType { get; set; }

        /// <summary>
        /// Defines the expected data format coming back from the request
        /// </summary>
        /// <remarks>
        ///     Defaulted to Json
        /// </remarks>
        public RequestDataFormat RequestDataFormat { get; set; } = RequestDataFormat.Json;

        /// <summary>
        /// Defines headers to be added to request
        /// </summary>
        /// <remarks>
        /// Example would be Authentication
        /// </remarks>
        public IDictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    }
}
