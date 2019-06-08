// <copyright file="TokenResponse.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MeshyDB.SDK.Models.Authentication
{
    /// <summary>
    /// Class defining elements supplied when a token request has been fulfilled.
    /// </summary>
    internal class TokenResponse
    {
        /// <summary>
        /// Gets or sets OAuth access token to allow authenticated requests with the api.
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; internal set; }

        /// <summary>
        /// Gets or sets number of seconds until the token expires.
        /// </summary>
        [JsonProperty("expires_in")]
        public int Expires { get; internal set; }

        /// <summary>
        /// Gets or sets OAuth token type describing what kind of token was generated.
        /// </summary>
        /// <remarks>Most likely this will be "Bearer".</remarks>
        [JsonProperty("token_type")]
        public string TokenType { get; internal set; }

        /// <summary>
        /// Gets or sets OAuth refresh token to allow reauthentication of a user.
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; internal set; }
    }
}
