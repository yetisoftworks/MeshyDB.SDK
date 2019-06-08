// <copyright file="MeshyDBJsonContractResolver.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeshyDB.SDK.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MeshyDB.SDK.Resolvers
{
    /// <summary>
    /// Class resolving a json contract for Meshy.
    /// </summary>
    /// <remarks>Used for implementing logic around custom json attributes.</remarks>
    internal class MeshyDBJsonContractResolver : DefaultContractResolver
    {
        /// <inheritdoc/>
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            return base.CreateProperties(type, memberSerialization).Where(x => !x.AttributeProvider.GetAttributes(false)
                                                                                                   .Any(y => y.GetType() == typeof(IgnoreJsonSerializeAttribute)))
                                                                   .ToList();
        }
    }
}
