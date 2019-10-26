// <copyright file="ProjectionsService.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using MeshyDB.SDK.Models;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace MeshyDB.SDK.Services
{
    /// <summary>
    /// Implementation of <see cref="IProjectionsService"/>.
    /// </summary>
    internal class ProjectionsService : IProjectionsService
    {
        private readonly IRequestService requestService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectionsService"/> class.
        /// </summary>
        /// <param name="requestService">Service used to make request calls.</param>
        internal ProjectionsService(IRequestService requestService)
        {
            this.requestService = requestService ?? throw new ArgumentNullException(nameof(requestService));
        }

        /// <inheritdoc/>
        public PageResult<TData> Get<TData>(string projectionName, Expression<Func<TData, bool>> filter, OrderByDefinition<TData> orderBy, int page = 1, int pageSize = 25)
            where TData : class
        {
            var mongoFilter = Builders<TData>.Filter.Where(filter).Render(BsonSerializer.SerializerRegistry.GetSerializer<TData>(), BsonSerializer.SerializerRegistry);

            var parsedFilter = mongoFilter.ToString();

            return this.Get<TData>(projectionName, parsedFilter, orderBy.GenerateBsonDocument(), page, pageSize);
        }

        /// <inheritdoc/>
        public PageResult<TData> Get<TData>(string projectionName, int page = 1, int pageSize = 25)
            where TData : class
        {
            return this.Get<TData>(projectionName, string.Empty, string.Empty, page, pageSize);
        }

        /// <inheritdoc/>
        public PageResult<TData> Get<TData>(string projectionName, string filter, string orderBy, int page = 1, int pageSize = 25)
            where TData : class
        {
            var t = this.GetAsync<TData>(projectionName, filter, orderBy, page, pageSize);

            return t.ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public PageResult<TData> Get<TData>(string projectionName)
            where TData : class
        {
            return this.Get<TData>(projectionName, string.Empty, string.Empty);
        }

        /// <inheritdoc/>
        public PageResult<TData> Get<TData>(string projectionName, IEnumerable<Expression<Func<TData, bool>>> filters, OrderByDefinition<TData> orderBy = null, int page = 1, int pageSize = 25)
            where TData : class
        {
            var filter = PredicateBuilder.CombineExpressions(filters);

            return this.Get<TData>(projectionName, filter, orderBy, page, pageSize);
        }

        /// <inheritdoc/>
        public PageResult<TData> Get<TData>(string projectionName, OrderByDefinition<TData> orderBy = null, int page = 1, int pageSize = 25)
            where TData : class
        {
            return this.Get<TData>(projectionName, (TData _) => true, orderBy, page, pageSize);
        }

        /// <inheritdoc/>
        public Task<PageResult<TData>> GetAsync<TData>(string projectionName, Expression<Func<TData, bool>> filter, OrderByDefinition<TData> orderBy, int page = 1, int pageSize = 25)
            where TData : class
        {
            var mongoFilter = Builders<TData>.Filter.Where(filter).Render(BsonSerializer.SerializerRegistry.GetSerializer<TData>(), BsonSerializer.SerializerRegistry);

            var parsedFilter = mongoFilter.ToString();

            return this.GetAsync<TData>(projectionName, parsedFilter, orderBy.GenerateBsonDocument(), page, pageSize);
        }

        /// <inheritdoc/>
        public Task<PageResult<TData>> GetAsync<TData>(string projectionName, int page = 1, int pageSize = 25)
            where TData : class
        {
            return this.GetAsync<TData>(projectionName, string.Empty, string.Empty, page, pageSize);
        }

        /// <inheritdoc/>
        public Task<PageResult<TData>> GetAsync<TData>(string projectionName, string filter, string orderBy, int page = 1, int pageSize = 25)
            where TData : class
        {
            var encodedOrderBy = string.Empty;

            if (orderBy != null)
            {
                encodedOrderBy = WebUtility.UrlEncode(orderBy);
            }

            var encodedFilter = string.Empty;

            if (filter != null)
            {
                encodedFilter = WebUtility.UrlEncode(filter);
            }

            return this.requestService.GetRequest<PageResult<TData>>($"projections/{projectionName}?filter={encodedFilter}&orderby={encodedOrderBy}&page={page}&pageSize={pageSize}");
        }

        /// <inheritdoc/>
        public Task<PageResult<TData>> GetAsync<TData>(string projectionName)
            where TData : class
        {
            return this.GetAsync<TData>(projectionName, string.Empty, string.Empty);
        }

        /// <inheritdoc/>
        public Task<PageResult<TData>> GetAsync<TData>(string projectionName, IEnumerable<Expression<Func<TData, bool>>> filters, OrderByDefinition<TData> orderBy, int page = 1, int pageSize = 25)
            where TData : class
        {
            var filter = PredicateBuilder.CombineExpressions(filters);

            return this.GetAsync<TData>(projectionName, filter, orderBy, page, pageSize);
        }

        /// <inheritdoc/>
        public Task<PageResult<TData>> GetAsync<TData>(string projectionName, OrderByDefinition<TData> orderBy = null, int page = 1, int pageSize = 25)
            where TData : class
        {
            return this.GetAsync<TData>(projectionName, (TData _) => true, orderBy, page, pageSize);
        }
    }
}
