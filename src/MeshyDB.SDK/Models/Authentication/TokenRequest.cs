using MeshyDB.SDK.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Models.Authentication
{
    /// <summary>
    /// Class defining elements required for generating a token request with the auth url
    /// </summary>
    internal class TokenRequest
    {
        /// <summary>
        /// Public identifier of client to authenticate on behalf of
        /// </summary>
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        /// <summary>
        /// Private secret of client to authenticate on behalf of
        /// </summary>
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        /// <summary>
        /// OAuth grant type to request authentication
        /// </summary>
        /// <remarks>Client Credentials is currently the only supported grant type</remarks>
        [JsonProperty("grant_type")]
        public string GrantType { get; set; } = TokenGrantType.Password;

        /// <summary>
        /// User name registered with the supplied client id to authenticate on behalf of
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// Password of user registered with the supplied client id to authenticate on behalf of
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// Scope(s) to request access to communicate with apis
        /// </summary>
        [JsonProperty("scope")]
        public string Scope { get; set; } = Constants.ApiScopes;

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
