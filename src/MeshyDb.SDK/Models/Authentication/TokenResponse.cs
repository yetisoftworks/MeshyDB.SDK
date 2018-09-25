using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDb.SDK.Models.Authentication
{
    /// <summary>
    /// Class defining elements supplied when a toekn request has been fulfilled
    /// </summary>
    internal class TokenResponse
    {
        /// <summary>
        /// OAuth access token to allow authenticated requests with the api
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; internal set; }

        /// <summary>
        /// Number of seconds until the token expires
        /// </summary>
        [JsonProperty("expires_in")]
        public int Expires { get; internal set; }

        /// <summary>
        /// OAuth token type describing what kind of token was generated
        /// </summary>
        /// <remarks>Most likely this will be "Bearer"</remarks>
        [JsonProperty("token_type")]
        public string TokenType { get; internal set; }
    }
}
