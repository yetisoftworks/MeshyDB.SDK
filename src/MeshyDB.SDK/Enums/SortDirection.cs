// <copyright file="SortDirection.cs" company="Yetisoftworks LLC">
// Copyright (c) Yetisoftworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Enums
{
    /// <summary>
    /// List of sort directions used for MongoDB.
    /// </summary>
    public enum SortDirection
    {
        /// <summary>
        /// This shoiuld be used when wanting to sort in ascending order.
        /// </summary>
        Ascending = 1,

        /// <summary>
        /// This shoiuld be used when wanting to sort in desending order.
        /// </summary>
        Desending = -1,
    }
}
