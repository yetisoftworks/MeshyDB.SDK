// <copyright file="UserRole.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class that defines a role assigned to a user.
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the date the role was added to the user.
        /// </summary>
        public DateTimeOffset AddedDate { get; set; }
    }
}
