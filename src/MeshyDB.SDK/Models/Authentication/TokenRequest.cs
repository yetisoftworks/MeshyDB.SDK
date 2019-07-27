// <copyright file="TokenRequest.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;
using MeshyDB.SDK.Enums;
using Newtonsoft.Json;

namespace MeshyDB.SDK.Models.Authentication
{
    /// <summary>
    /// Class defining elements required for generating a token request with the auth url.
    /// </summary>
    internal class TokenRequest
    {
        /// <summary>
        /// Gets or sets public identifier of client to authenticate on behalf of.
        /// </summary>
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets private secret of client to authenticate on behalf of.
        /// </summary>
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets OAuth grant type to request authentication.
        /// </summary>
        /// <remarks>Password is currently the only supported grant type.</remarks>
        [JsonProperty("grant_type")]
        public string GrantType { get; set; } = TokenGrantType.Password;

        /// <summary>
        /// Gets or sets username registered with the supplied client id to authenticate on behalf of.
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets password of user registered with the supplied client id to authenticate on behalf of.
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets scope(s) to request access to communicate with apis.
        /// </summary>
        [JsonProperty("scope")]
        public string Scope { get; set; } = Constants.ApiScopes;

        /// <summary>
        /// Gets or sets OAuth refresh token to allow re-authentication of a user.
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
