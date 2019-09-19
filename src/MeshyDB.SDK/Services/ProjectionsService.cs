// <copyright file="ProjectionsService.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MeshyDB.SDK.Models;

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
        public PageResult<TData> Get<TData>(string projectionName, OrderByDefinition<TData> orderBy, int page = 1, int pageSize = 25)
            where TData : class
        {
            return this.Get<TData>(projectionName, orderBy.GenerateBsonDocument(), page, pageSize);
        }

        /// <inheritdoc/>
        public PageResult<TData> Get<TData>(string projectionName, int page = 1, int pageSize = 25)
            where TData : class
        {
            return this.Get<TData>(projectionName, string.Empty, page, pageSize);
        }

        /// <inheritdoc/>
        public PageResult<TData> Get<TData>(string projectionName, string orderBy, int page = 1, int pageSize = 25)
            where TData : class
        {
            var t = this.GetAsync<TData>(projectionName, orderBy, page, pageSize);

            return t.ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public PageResult<TData> Get<TData>(string projectionName)
            where TData : class
        {
            return this.Get<TData>(projectionName, string.Empty);
        }

        /// <inheritdoc/>
        public Task<PageResult<TData>> GetAsync<TData>(string projectionName, OrderByDefinition<TData> orderBy, int page = 1, int pageSize = 25)
            where TData : class
        {
            return this.GetAsync<TData>(projectionName, orderBy.GenerateBsonDocument(), page, pageSize);
        }

        /// <inheritdoc/>
        public Task<PageResult<TData>> GetAsync<TData>(string projectionName, int page = 1, int pageSize = 25)
            where TData : class
        {
            return this.GetAsync<TData>(projectionName, string.Empty, page, pageSize);
        }

        /// <inheritdoc/>
        public Task<PageResult<TData>> GetAsync<TData>(string projectionName, string orderBy, int page = 1, int pageSize = 25)
            where TData : class
        {
            var encodedOrderBy = string.Empty;

            if (orderBy != null)
            {
                encodedOrderBy = WebUtility.UrlEncode(orderBy);
            }

            return this.requestService.GetRequest<PageResult<TData>>($"projections/{projectionName}?orderby={encodedOrderBy}&page={page}&pageSize={pageSize}");
        }

        /// <inheritdoc/>
        public Task<PageResult<TData>> GetAsync<TData>(string projectionName)
            where TData : class
        {
            return this.GetAsync<TData>(projectionName, string.Empty);
        }
    }
}
