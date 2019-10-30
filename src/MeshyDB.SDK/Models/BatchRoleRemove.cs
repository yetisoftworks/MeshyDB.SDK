// <copyright file="BatchRoleRemove.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class that defines removing a role to a set of users.
    /// </summary>
    public class BatchRoleRemove
    {
        /// <summary>
        /// Gets or sets an enumeration of users to be removed from a role.
        /// </summary>
        public IEnumerable<UserRoleRemove> Users { get; set; }
    }
}
