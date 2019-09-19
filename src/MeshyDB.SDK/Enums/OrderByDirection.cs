// <copyright file="OrderByDirection.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Enums
{
    /// <summary>
    /// List of order by directions used for MongoDB.
    /// </summary>
    public enum OrderByDirection
    {
        /// <summary>
        /// This should be used when wanting to order in ascending order.
        /// </summary>
        Ascending = 1,

        /// <summary>
        /// This should be used when wanting to order in descending order.
        /// </summary>
        Descending = -1,
    }
}
