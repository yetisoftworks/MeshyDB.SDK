// <copyright file="MeshNameAttribute.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Attributes
{
    /// <summary>
    /// Attribute used to override the name of the mesh.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class MeshNameAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeshNameAttribute"/> class.
        /// </summary>
        /// <param name="name">Name of the mesh.</param>
        public MeshNameAttribute(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets mesh name.
        /// </summary>
        internal string Name { get; set; }
    }
}
