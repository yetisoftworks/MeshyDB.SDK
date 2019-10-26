// <copyright file="Role.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class that defines a role.
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Gets or sets the id of the role.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets name of the role.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the role.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets an enumeration of permissions assigned to the role.
        /// </summary>
        public IEnumerable<Permission> Permissions { get; set; }

        /// <summary>
        /// Gets or sets number of users assigned to the role.
        /// </summary>
        public int NumberOfUsers { get; set; }
    }
}
