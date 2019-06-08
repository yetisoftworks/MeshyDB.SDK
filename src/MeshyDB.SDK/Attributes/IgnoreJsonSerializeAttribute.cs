// <copyright file="IgnoreJsonSerializeAttribute.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Attributes
{
    /// <summary>
    /// Class used to mark a property that should not be serialized during Json Serialization process.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    internal sealed class IgnoreJsonSerializeAttribute : Attribute
    {
    }
}
