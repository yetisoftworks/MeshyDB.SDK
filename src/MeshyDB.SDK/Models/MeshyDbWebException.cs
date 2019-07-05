// <copyright file="MeshyDbWebException.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System.Net.Http;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class to provide more web error details.
    /// </summary>
    public class MeshyDbWebException : HttpRequestException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeshyDbWebException"/> class.
        /// </summary>
        /// <param name="message">Message object to be parsed from web response error.</param>
        /// <param name="exception">Defining exception from web request.</param>
        public MeshyDbWebException(string message, HttpRequestException exception)
            : base(message, exception)
        {
        }
    }
}
