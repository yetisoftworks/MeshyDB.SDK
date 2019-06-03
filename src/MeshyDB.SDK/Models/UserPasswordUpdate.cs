// <copyright file="UserPasswordUpdate.cs" company="Yetisoftworks LLC">
// Copyright (c) Yetisoftworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class defining the ability to update the users password.
    /// </summary>
    internal class UserPasswordUpdate
    {
        /// <summary>
        /// Gets or sets the new password to use.
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the previous password to valdiate it was correct.
        /// </summary>
        public string PreviousPassword { get; set; }
    }
}
