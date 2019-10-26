// <copyright file="Permissible.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class that defines a permissible.
    /// </summary>
    public class Permissible
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the permissible supports create.
        /// </summary>
        public bool CanCreate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the permissible supports update.
        /// </summary>
        public bool CanUpdate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the permissible supports read.
        /// </summary>
        public bool CanRead { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the permissible supports delete.
        /// </summary>
        public bool CanDelete { get; set; }
    }
}
