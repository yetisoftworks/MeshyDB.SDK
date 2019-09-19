// <copyright file="OrderByDefinition.cs" company="Yeti Softworks LLC">
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
    /// Class that defines how a order by should be generated for the given type.
    /// </summary>
    /// <typeparam name="T">Type of data that will be ordered.</typeparam>
    public class OrderByDefinition<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderByDefinition{T}"/> class.
        /// </summary>
        internal OrderByDefinition()
        {
        }

        /// <summary>
        /// Gets or sets a collection defining order.
        /// </summary>
        internal ICollection<KeyValuePair<string, OrderByDirection>> Order { get; set; } = new List<KeyValuePair<string, OrderByDirection>>();

        /// <summary>
        /// Order by given member expression in ascending order.
        /// </summary>
        /// <typeparam name="TKey">Type of member supplied.</typeparam>
        /// <param name="keyExpression">Key expression to derive member name.</param>
        /// <returns>Current Definition of Order.</returns>
        public static OrderByDefinition<T> OrderBy<TKey>(Expression<Func<T, TKey>> keyExpression)
        {
            return new OrderByDefinition<T>().ThenBy(keyExpression);
        }

        /// <summary>
        /// Order by given member expression in descending order.
        /// </summary>
        /// <typeparam name="TKey">Type of member supplied.</typeparam>
        /// <param name="keyExpression">Key expression to derive member name.</param>
        /// <returns>Current Definition of Order.</returns>
        public static OrderByDefinition<T> OrderByDescending<TKey>(Expression<Func<T, TKey>> keyExpression)
        {
            return new OrderByDefinition<T>().ThenByDescending(keyExpression);
        }

        /// <summary>
        /// Order by given member expression in ascending order.
        /// </summary>
        /// <param name="key">Name of member to order.</param>
        /// <returns>Current Definition of Order.</returns>
        public static OrderByDefinition<T> OrderBy(string key)
        {
            return new OrderByDefinition<T>().ThenBy(key);
        }

        /// <summary>
        /// Order by given member expression in descending order.
        /// </summary>
        /// <param name="key">Name of member to order.</param>
        /// <returns>Current Definition of Order.</returns>
        public static OrderByDefinition<T> OrderByDescending(string key)
        {
            return new OrderByDefinition<T>().ThenByDescending(key);
        }

        /// <summary>
        /// Order by given member expression in ascending order.
        /// </summary>
        /// <typeparam name="TKey">Type of member supplied.</typeparam>
        /// <param name="keyExpression">Key expression to derive member name.</param>
        /// <returns>Current Definition of Order.</returns>
        public OrderByDefinition<T> ThenBy<TKey>(Expression<Func<T, TKey>> keyExpression)
        {
            var name = this.GetExpressionName(keyExpression);
            return this.ThenBy(name);
        }

        /// <summary>
        /// Order by given member expression in descending order.
        /// </summary>
        /// <typeparam name="TKey">Type of member supplied.</typeparam>
        /// <param name="keyExpression">Key expression to derive member name.</param>
        /// <returns>Current Definition of Order.</returns>
        public OrderByDefinition<T> ThenByDescending<TKey>(Expression<Func<T, TKey>> keyExpression)
        {
            var name = this.GetExpressionName(keyExpression);
            return this.ThenByDescending(name);
        }

        /// <summary>
        /// Order by given member expression in ascending order.
        /// </summary>
        /// <param name="key">Name of member to order.</param>
        /// <returns>Current Definition of Order.</returns>
        public OrderByDefinition<T> ThenBy(string key)
        {
            return this.AddToOrder(key, OrderByDirection.Ascending);
        }

        /// <summary>
        /// Order by given member expression in descending order.
        /// </summary>
        /// <param name="key">Name of member to order.</param>
        /// <returns>Current Definition of Order.</returns>
        public OrderByDefinition<T> ThenByDescending(string key)
        {
            return this.AddToOrder(key, OrderByDirection.Descending);
        }

        /// <summary>
        /// Converts object to a BsonDocument for MongoDB ordering.
        /// </summary>
        /// <returns>Formatted MongoDB order string.</returns>
        internal string GenerateBsonDocument()
        {
            return $"{{ {string.Join(",", this.Order.Select(x => $"{x.Key}:{(int)x.Value}"))} }}";
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

        private OrderByDefinition<T> AddToOrder(string key, OrderByDirection orderDirection)
        {
            var definition = new OrderByDefinition<T>();
            definition.Order = this.Order.Select(x => new KeyValuePair<string, OrderByDirection>(x.Key, x.Value)).ToList();

            definition.Order.Add(new KeyValuePair<string, OrderByDirection>(key, orderDirection));
            return definition;
        }
    }
}
