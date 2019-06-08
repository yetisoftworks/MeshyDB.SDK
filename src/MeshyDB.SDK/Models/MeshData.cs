// <copyright file="MeshData.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using MeshyDB.SDK.Attributes;
using Newtonsoft.Json;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Abstract class defining required data pieces and format for web requests.
    /// </summary>
    public abstract class MeshData
    {
        /// <summary>
        /// Gets identifier of the mesh data.
        /// </summary>
        /// <remarks>This field is auto generated when a mesh is created.</remarks>
        [JsonProperty("_id")]
        [IgnoreJsonSerialize]
        public string Id { get; internal set; }

        /// <summary>
        /// Gets reference identifier of the mesh.
        /// </summary>
        /// <remarks>
        /// This will be a url reference to this object.
        /// </remarks>
        [JsonProperty("_rid")]
        [IgnoreJsonSerialize]
        public string ReferenceId { get; internal set; }
    }
}
