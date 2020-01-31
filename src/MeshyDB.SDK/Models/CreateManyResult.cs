// <copyright file="CreateManyResult.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class that defines creating many records result.
    /// </summary>
    /// <typeparam name="TModel">Type of mesh data to be returned.</typeparam>
    public class CreateManyResult<TModel>
        where TModel : MeshData
    {
        /// <summary>
        /// Gets or sets a count of created records.
        /// </summary>
        public int CreatedCount { get; set; }

        /// <summary>
        /// Gets or sets a collection of created data.
        /// </summary>
        public IEnumerable<TModel> CreatedData { get; set; }
    }
}
