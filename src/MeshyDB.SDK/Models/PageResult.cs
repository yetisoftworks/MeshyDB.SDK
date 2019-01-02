using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class getting paged result of mesh data
    /// </summary>
    /// <typeparam name="TMeshData">The type of data returned from the mesh</typeparam>
    public class PageResult<TMeshData> where TMeshData : MeshData
    {
        /// <summary>
        /// Page number of the data retrieved
        /// </summary>
        [JsonProperty("page")]
        public int Page { get; internal set; }

        /// <summary>
        /// Page size of the data retrieved
        /// </summary>
        [JsonProperty("pageSize")]
        public int PageSize { get; internal set; }

        /// <summary>
        /// Collection of data results for the given page and page size of the mesh data type
        /// </summary>
        [JsonProperty("results")]
        public IEnumerable<TMeshData> Results { get; internal set; } = new List<TMeshData>();

        /// <summary>
        /// Total number of records of the data retrieved
        /// </summary>
        [JsonProperty("totalRecords")]
        public int TotalRecords { get; internal set; }
    }
}
