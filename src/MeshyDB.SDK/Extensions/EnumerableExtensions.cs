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
        /// Order by given member expression in ascending order.
        /// </summary>
        /// <typeparam name="T">Type of data that will be ordered.</typeparam>
        /// <typeparam name="TKey">Type of member supplied.</typeparam>
        /// <param name="collection">Collection of data to be ordered.</param>
        /// <param name="keyExpression">Key expression to derive member name.</param>
        /// <returns>Current Definition of Order.</returns>
        public static OrderByDefinition<T> OrderBy<T, TKey>(this IEnumerable<T> collection, Expression<Func<T, TKey>> keyExpression)
        {
            return new OrderByDefinition<T>().ThenBy(keyExpression);
        }

        /// <summary>
        /// Order by given member expression in descending order.
        /// </summary>
        /// <typeparam name="T">Type of data that will be ordered.</typeparam>
        /// <typeparam name="TKey">Type of member supplied.</typeparam>
        /// <param name="collection">Collection of data to be ordered.</param>
        /// <param name="keyExpression">Key expression to derive member name.</param>
        /// <returns>Current Definition of Order.</returns>
        public static OrderByDefinition<T> OrderByDescending<T, TKey>(this IEnumerable<T> collection, Expression<Func<T, TKey>> keyExpression)
        {
            return new OrderByDefinition<T>().ThenByDescending(keyExpression);
        }

        /// <summary>
        /// Order by given member expression in ascending order.
        /// </summary>
        /// <typeparam name="T">Type of data that will be ordered.</typeparam>
        /// <param name="collection">Collection of data to be ordered.</param>
        /// <param name="key">Name of member to order.</param>
        /// <returns>Current Definition of Order.</returns>
        public static OrderByDefinition<T> OrderBy<T>(this IEnumerable<T> collection, string key)
        {
            return new OrderByDefinition<T>().ThenBy(key);
        }

        /// <summary>
        /// Order by given member expression in descending order.
        /// </summary>
        /// <typeparam name="T">Type of data that will be ordered.</typeparam>
        /// <param name="collection">Collection of data to be ordered.</param>
        /// <param name="key">Name of member to order.</param>
        /// <returns>Current Definition of Order.</returns>
        public static OrderByDefinition<T> OrderByDescending<T>(this IEnumerable<T> collection, string key)
        {
            return new OrderByDefinition<T>().ThenByDescending(key);
        }
    }
}
