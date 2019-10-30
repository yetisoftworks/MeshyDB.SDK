// <copyright file="BatchRoleAdd.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class that defines adding a role to a set of users.
    /// </summary>
    public class BatchRoleAdd
    {
        /// <summary>
        /// Gets or sets an enumeration of users to be added to a role.
        /// </summary>
        public IEnumerable<UserRoleAdd> Users { get; set; }
    }
}
