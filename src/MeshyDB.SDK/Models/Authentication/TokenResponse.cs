using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Models.Authentication
{
    /// <summary>
    /// Class defining elements supplied when a token request has been fulfilled
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

        /// <summary>
        /// OAuth refresh token to allow reauthentication of a user
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; internal set; }
    }
}
