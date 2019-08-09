// <copyright file="SortDefinition.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MeshyDB.SDK.Enums;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class that defines how a sort should be generated for the given type.
    /// </summary>
    /// <typeparam name="T">Type of data that will be sorted.</typeparam>
    public class SortDefinition<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SortDefinition{T}"/> class.
        /// </summary>
        internal SortDefinition()
        {
        }

        /// <summary>
        /// Gets or sets a collection defining sort.
        /// </summary>
        internal ICollection<KeyValuePair<string, SortDirection>> Sort { get; set; } = new List<KeyValuePair<string, SortDirection>>();

        /// <summary>
        /// Sort by given member expression in ascending order.
        /// </summary>
        /// <typeparam name="TKey">Type of member supplied.</typeparam>
        /// <param name="keyExpression">Key expression to derive member name.</param>
        /// <returns>Current Definition of Sort.</returns>
        public static SortDefinition<T> SortBy<TKey>(Expression<Func<T, TKey>> keyExpression)
        {
            return new SortDefinition<T>().ThenBy(keyExpression);
        }

        /// <summary>
        /// Sort by given member expression in descending order.
        /// </summary>
        /// <typeparam name="TKey">Type of member supplied.</typeparam>
        /// <param name="keyExpression">Key expression to derive member name.</param>
        /// <returns>Current Definition of Sort.</returns>
        public static SortDefinition<T> SortByDescending<TKey>(Expression<Func<T, TKey>> keyExpression)
        {
            return new SortDefinition<T>().ThenByDescending(keyExpression);
        }

        /// <summary>
        /// Sort by given member expression in ascending order.
        /// </summary>
        /// <param name="key">Name of member to order.</param>
        /// <returns>Current Definition of Sort.</returns>
        public static SortDefinition<T> SortBy(string key)
        {
            return new SortDefinition<T>().ThenBy(key);
        }

        /// <summary>
        /// Sort by given member expression in descending order.
        /// </summary>
        /// <param name="key">Name of member to order.</param>
        /// <returns>Current Definition of Sort.</returns>
        public static SortDefinition<T> SortByDescending(string key)
        {
            return new SortDefinition<T>().ThenByDescending(key);
        }

        /// <summary>
        /// Sort by given member expression in ascending order.
        /// </summary>
        /// <typeparam name="TKey">Type of member supplied.</typeparam>
        /// <param name="keyExpression">Key expression to derive member name.</param>
        /// <returns>Current Definition of Sort.</returns>
        public SortDefinition<T> ThenBy<TKey>(Expression<Func<T, TKey>> keyExpression)
        {
            var name = this.GetExpressionName(keyExpression);
            return this.ThenBy(name);
        }

        /// <summary>
        /// Sort by given member expression in descending order.
        /// </summary>
        /// <typeparam name="TKey">Type of member supplied.</typeparam>
        /// <param name="keyExpression">Key expression to derive member name.</param>
        /// <returns>Current Definition of Sort.</returns>
        public SortDefinition<T> ThenByDescending<TKey>(Expression<Func<T, TKey>> keyExpression)
        {
            var name = this.GetExpressionName(keyExpression);
            return this.ThenByDescending(name);
        }

        /// <summary>
        /// Sort by given member expression in ascending order.
        /// </summary>
        /// <param name="key">Name of member to order.</param>
        /// <returns>Current Definition of Sort.</returns>
        public SortDefinition<T> ThenBy(string key)
        {
            return this.AddToSort(key, SortDirection.Ascending);
        }

        /// <summary>
        /// Sort by given member expression in descending order.
        /// </summary>
        /// <param name="key">Name of member to order.</param>
        /// <returns>Current Definition of Sort.</returns>
        public SortDefinition<T> ThenByDescending(string key)
        {
            return this.AddToSort(key, SortDirection.Descending);
        }

        /// <summary>
        /// Converts object to a BsonDocument for MongoDB sorting.
        /// </summary>
        /// <returns>Formatted MongoDB sort string.</returns>
        internal string GenerateBsonDocument()
        {
            return $"{{ {string.Join(",", this.Sort.Select(x => $"{x.Key}:{(int)x.Value}"))} }}";
        }

        private string GetExpressionName<TKey>(Expression<Func<T, TKey>> expression)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;

            if (memberExpression != null)
            {
                return memberExpression.Member.Name;
            }

            var constantExpression = expression.Body as ConstantExpression;

            if (constantExpression != null)
            {
                return constantExpression.Value.ToString();
            }

            return null;
        }

        private SortDefinition<T> AddToSort(string key, SortDirection orderDirection)
        {
            var definition = new SortDefinition<T>();
            definition.Sort = this.Sort.Select(x => new KeyValuePair<string, SortDirection>(x.Key, x.Value)).ToList();

            definition.Sort.Add(new KeyValuePair<string, SortDirection>(key, orderDirection));
            return definition;
        }
    }
}
