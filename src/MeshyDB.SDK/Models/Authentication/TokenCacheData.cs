using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Models.Authentication
{
    /// <summary>
    /// Class used for storing token data used in a cache
    /// </summary>
    internal class TokenCacheData
    {
        /// <summary>
        /// Token generated from the auth url
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Refresh token from the auth url
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Date and time the token will expire
        /// </summary>
        public DateTimeOffset Expires { get; set; }

    }
}
