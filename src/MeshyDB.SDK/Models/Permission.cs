// <copyright file="Permission.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class that defines a permission.
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// Gets or sets the id of the permission.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets name reference of permissible.
        /// </summary>
        public string PermissibleName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether create permission is granted.
        /// </summary>
        public bool Create { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether update permission is granted.
        /// </summary>
        public bool Update { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether read permission is granted.
        /// </summary>
        public bool Read { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether delete permission is granted.
        /// </summary>
        public bool Delete { get; set; }
    }
}