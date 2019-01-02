using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using MeshyDB.SDK.Attributes;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Abstract class defining required data pieces and format for web requests
    /// </summary>
    public abstract class MeshData
    {
        /// <summary>
        /// Identifier of the mesh data
        /// </summary>
        /// <remarks>This field is auto generated when a mesh is created</remarks>
        public string Id { get; internal set; }

        [JsonProperty("_id")]
        [IgnoreJsonSerialize]
        private string ApiId { set { Id = value; } }
    }
}
