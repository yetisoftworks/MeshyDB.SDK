using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using MeshyDb.SDK.Attributes;

namespace MeshyDb.SDK.Models
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
        [JsonProperty("_id")]
        [IgnoreJsonSerialize]
        public string Id { get; internal set; }
    }
}
