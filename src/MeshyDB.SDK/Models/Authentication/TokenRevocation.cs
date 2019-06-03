// <copyright file="TokenRevocation.cs" company="Yetisoftworks LLC">
// Copyright (c) Yetisoftworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MeshyDB.SDK.Models.Authentication
{
    /// <summary>
    /// Class to identifiy what token to revoke.
    /// </summary>
    internal class TokenRevocation
    {
        /// <summary>
        /// Gets or sets token to revoke.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets type of token to revoke.
        /// </summary>
        /// <remarks>Revoking refresh token.</remarks>
        [JsonProperty("token_type_hint")]
        public string TokenTypeHint { get; set; }

        /// <summary>
        /// Gets or sets public identifier of client to revoke on behalf of.
        /// </summary>
        [JsonProperty("client_id")]
        public string ClientId { get; set; }
    }
}
