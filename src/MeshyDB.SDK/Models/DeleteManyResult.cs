// <copyright file="DeleteManyResult.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using Newtonsoft.Json;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class defining elements for a delete many result.
    /// </summary>
    public class DeleteManyResult
    {
        /// <summary>
        /// Gets the deleted count. If IsAcknowledged is false, this will throw an exception.
        /// </summary>
        [JsonProperty("deletedCount")]
        public long DeletedCount { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the result is acknowledged.
        /// </summary>
        [JsonProperty("isAcknowledged")]
        public bool IsAcknowledged { get; internal set; }
    }
}
