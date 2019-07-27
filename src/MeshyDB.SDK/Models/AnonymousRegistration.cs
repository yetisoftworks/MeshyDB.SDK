// <copyright file="AnonymousRegistration.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class defining elements for allowing anonymous registration.
    /// </summary>
    public class AnonymousRegistration
    {
        /// <summary>
        /// Gets or sets unique identifier of anonymous user, such as a device id.
        /// </summary>
        public string Username { get; set; }
    }
}
