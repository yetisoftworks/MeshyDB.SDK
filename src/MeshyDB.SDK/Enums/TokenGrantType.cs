// <copyright file="TokenGrantType.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Enums
{
    /// <summary>
    /// List of supported grant types to be requested for a token.
    /// </summary>
    internal static class TokenGrantType
    {
        /// <summary>
        /// Password. This is used to generate token using password.
        /// </summary>
        internal const string Password = "password";
    }
}
