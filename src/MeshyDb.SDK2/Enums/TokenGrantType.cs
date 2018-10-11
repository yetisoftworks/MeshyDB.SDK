using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Enums
{
    /// <summary>
    /// List of supported grant types to be requested for a token
    /// </summary>
    internal static class TokenGrantType
    {
        /// <summary>
        /// Client Credentials. This is used to generate token using client credentials
        /// </summary>
        internal const string ClientCredentials = "client_credentials";
    }
}
