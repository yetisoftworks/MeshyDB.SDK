// <copyright file="UpdateManyResult.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class that defines updating many records result.
    /// </summary>
    public class UpdateManyResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether the update was acknowledged.
        /// </summary>
        public bool IsAcknowledged { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the modified count is available.
        /// </summary>
        public bool IsModifiedCountAvailable { get; set; }

        /// <summary>
        /// Gets or sets a count of records matched.
        /// </summary>
        public int MatchedCount { get; set; }

        /// <summary>
        /// Gets or sets a count of records updated.
        /// </summary>
        public int ModifiedCount { get; set; }

        /// <summary>
        /// Gets or sets an identifier if newly inserted records based on update.
        /// </summary>
        public string UpsertedId { get; set; }
    }
}
