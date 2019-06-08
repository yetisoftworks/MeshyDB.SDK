// <copyright file="TokenCacheData.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Models.Authentication
{
    /// <summary>
    /// Class used for storing token data used in a cache.
    /// </summary>
    internal class TokenCacheData
    {
        /// <summary>
        /// Gets or sets token generated from the auth url.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets refresh token from the auth url.
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets date and time the token will expire.
        /// </summary>
        public DateTimeOffset Expires { get; set; }
    }
}
