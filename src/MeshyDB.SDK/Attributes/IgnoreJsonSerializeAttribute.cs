// <copyright file="IgnoreJsonSerializeAttribute.cs" company="Yetisoftworks LLC">
// Copyright (c) Yetisoftworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Attributes
{
    /// <summary>
    /// Class used to mark a property that should not be serialized during Json Serialization process.
    /// </summary>
    internal sealed class IgnoreJsonSerializeAttribute : Attribute
    {
    }
}
