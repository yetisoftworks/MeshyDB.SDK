// <copyright file="ForgotPassword.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class defining elements required while forgetting a password.
    /// </summary>
    internal class ForgotPassword
    {
        /// <summary>
        /// Gets or sets unique identifier of user that is requested which a password was forgotten.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets attempt number of forgot password.
        /// </summary>
        public int Attempt { get; set; }
    }
}
