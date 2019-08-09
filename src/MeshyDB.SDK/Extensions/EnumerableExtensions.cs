// <copyright file="EnumerableExtensions.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MeshyDB.SDK.Models;

namespace MeshyDB.SDK
{
    /// <summary>
    /// Defines extensions for Enumerables for MeshyDB.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Sort by given member expression in ascending order.
        /// </summary>
        /// <typeparam name="T">Type of data that will be sorted.</typeparam>
        /// <typeparam name="TKey">Type of member supplied.</typeparam>
        /// <param name="collection">Collection of data to be sorted.</param>
        /// <param name="keyExpression">Key expression to derive member name.</param>
        /// <returns>Current Definition of Sort.</returns>
        public static SortDefinition<T> SortBy<T, TKey>(this IEnumerable<T> collection, Expression<Func<T, TKey>> keyExpression)
        {
            return new SortDefinition<T>().ThenBy(keyExpression);
        }

        /// <summary>
        /// Sort by given member expression in descending order.
        /// </summary>
        /// <typeparam name="T">Type of data that will be sorted.</typeparam>
        /// <typeparam name="TKey">Type of member supplied.</typeparam>
        /// <param name="collection">Collection of data to be sorted.</param>
        /// <param name="keyExpression">Key expression to derive member name.</param>
        /// <returns>Current Definition of Sort.</returns>
        public static SortDefinition<T> SortByDescending<T, TKey>(this IEnumerable<T> collection, Expression<Func<T, TKey>> keyExpression)
        {
            return new SortDefinition<T>().ThenByDescending(keyExpression);
        }

        /// <summary>
        /// Sort by given member expression in ascending order.
        /// </summary>
        /// <typeparam name="T">Type of data that will be sorted.</typeparam>
        /// <param name="collection">Collection of data to be sorted.</param>
        /// <param name="key">Name of member to order.</param>
        /// <returns>Current Definition of Sort.</returns>
        public static SortDefinition<T> SortBy<T>(this IEnumerable<T> collection, string key)
        {
            return new SortDefinition<T>().ThenBy(key);
        }

        /// <summary>
        /// Sort by given member expression in descending order.
        /// </summary>
        /// <typeparam name="T">Type of data that will be sorted.</typeparam>
        /// <param name="collection">Collection of data to be sorted.</param>
        /// <param name="key">Name of member to order.</param>
        /// <returns>Current Definition of Sort.</returns>
        public static SortDefinition<T> SortByDescending<T>(this IEnumerable<T> collection, string key)
        {
            return new SortDefinition<T>().ThenByDescending(key);
        }
    }
}
