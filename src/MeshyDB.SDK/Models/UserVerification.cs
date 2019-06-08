// <copyright file="UserVerification.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class defining elements for user verification.
    /// </summary>
    public class UserVerification
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password reset attempt.
        /// </summary>
        public int Attempt { get; set; }
    }
}
