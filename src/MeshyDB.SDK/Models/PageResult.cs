// <copyright file="PageResult.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class getting paged result of mesh data.
    /// </summary>
    /// <typeparam name="TData">The type of data returned result.</typeparam>
    public class PageResult<TData>
        where TData : class
    {
        /// <summary>
        /// Gets page number of the data retrieved.
        /// </summary>
        [JsonProperty("page")]
        public int Page { get; internal set; }

        /// <summary>
        /// Gets page size of the data retrieved.
        /// </summary>
        [JsonProperty("pageSize")]
        public int PageSize { get; internal set; }

        /// <summary>
        /// Gets collection of data results for the given page and page size of the mesh data type.
        /// </summary>
        [JsonProperty("results")]
        public IEnumerable<TData> Results { get; internal set; } = new List<TData>();

        /// <summary>
        /// Gets total number of records of the data retrieved.
        /// </summary>
        [JsonProperty("totalRecords")]
        public int TotalRecords { get; internal set; }
    }
}
