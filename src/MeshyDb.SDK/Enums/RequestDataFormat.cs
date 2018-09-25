using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDb.SDK.Enums
{
    /// <summary>
    /// List of supported data formats to be sent as part of a HTTP request
    /// </summary>
    internal enum RequestDataFormat
    {
        /// <summary>
        /// Json format. This is used when the API should serialize as json data to be sent in the request
        /// </summary>
        Json = 0,

        /// <summary>
        /// Form format. This is used when the API should serialize as form data to be sent in the request
        /// </summary>
        Form = 1
    }
}
