// <copyright file="MeshesService.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MeshyDB.SDK.Attributes;
using MeshyDB.SDK.Models;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace MeshyDB.SDK.Services
{
    /// <summary>
    /// Implementation of <see cref="IMeshesService"/>.
    /// </summary>
    internal class MeshesService : IMeshesService
    {
        private readonly IRequestService requestService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeshesService"/> class.
        /// </summary>
        /// <param name="requestService">Service used to make request calls.</param>
        internal MeshesService(IRequestService requestService)
        {
            this.requestService = requestService ?? throw new ArgumentNullException(nameof(requestService));
        }

        /// <inheritdoc/>
        public Task<TModel> GetDataAsync<TModel>(string id)
            where TModel : MeshData
        {
            return this.requestService.GetRequest<TModel>($"meshes/{this.GetMeshName<TModel>()}/{id}");
        }

        /// <inheritdoc/>
        public Task<PageResult<TModel>> SearchAsync<TModel>(string filter = null, string orderBy = null, int page = 1, int pageSize = 25)
            where TModel : MeshData
        {
            var encodedUrl = WebUtility.UrlEncode(filter);

            var encodedOrderBy = WebUtility.UrlEncode(orderBy);

            return this.requestService.GetRequest<PageResult<TModel>>($"meshes/{this.GetMeshName<TModel>()}?filter={encodedUrl}&orderby={encodedOrderBy}&page={page}&pageSize={pageSize}");
        }

        /// <inheritdoc/>
        public Task<PageResult<TModel>> SearchAsync<TModel>(Expression<Func<TModel, bool>> filter, OrderByDefinition<TModel> orderBy = null, int page = 1, int pageSize = 25)
            where TModel : MeshData
        {
            var mongoFilter = Builders<TModel>.Filter.Where(filter).Render(BsonSerializer.SerializerRegistry.GetSerializer<TModel>(), BsonSerializer.SerializerRegistry);

            var encodedUrl = WebUtility.UrlEncode(mongoFilter.ToString());

            var encodedOrderBy = string.Empty;

            if (orderBy != null)
            {
                encodedOrderBy = WebUtility.UrlEncode(orderBy.GenerateBsonDocument());
            }

            return this.requestService.GetRequest<PageResult<TModel>>($"meshes/{this.GetMeshName<TModel>()}?filter={encodedUrl}&orderby={encodedOrderBy}&page={page}&pageSize={pageSize}");
        }

        /// <inheritdoc/>
        public Task<PageResult<TModel>> SearchAsync<TModel>(IEnumerable<Expression<Func<TModel, bool>>> filters, OrderByDefinition<TModel> orderBy = null, int page = 1, int pageSize = 25)
            where TModel : MeshData
        {
            var filter = PredicateBuilder.CombineExpressions(filters);

            return this.SearchAsync(filter, orderBy, page, pageSize);
        }

        /// <inheritdoc/>
        public Task<TModel> CreateAsync<TModel>(TModel model)
            where TModel : MeshData
        {
            return this.requestService.PostRequest<TModel>($"meshes/{this.GetMeshName<TModel>()}", model);
        }

        /// <inheritdoc/>
        public Task<TModel> UpdateAsync<TModel>(string id, TModel model)
            where TModel : MeshData
        {
            return this.requestService.PutRequest<TModel>($"meshes/{this.GetMeshName<TModel>()}/{id}", model);
        }

        /// <inheritdoc/>
        public Task<TModel> UpdateAsync<TModel>(TModel model)
            where TModel : MeshData
        {
            return this.requestService.PutRequest<TModel>($"meshes/{this.GetMeshName<TModel>()}/{model.Id}", model);
        }

        /// <inheritdoc/>
        public Task DeleteAsync<TModel>(string id)
            where TModel : MeshData
        {
            return this.requestService.DeleteRequest<object>($"meshes/{this.GetMeshName<TModel>()}/{id}");
        }

        /// <inheritdoc/>
        public TModel GetData<TModel>(string id)
            where TModel : MeshData
        {
            var t = this.GetDataAsync<TModel>(id);
            return t.ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public PageResult<TModel> Search<TModel>(string filter = null, string orderBy = null, int page = 1, int pageSize = 25)
            where TModel : MeshData
        {
            var t = this.SearchAsync<TModel>(filter, orderBy, page, pageSize);
            return t.ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public PageResult<TModel> Search<TModel>(Expression<Func<TModel, bool>> filters, OrderByDefinition<TModel> orderBy = null, int page = 1, int pageSize = 25)
            where TModel : MeshData
        {
            var t = this.SearchAsync<TModel>(filters, orderBy, page, pageSize);
            return t.ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public PageResult<TModel> Search<TModel>(IEnumerable<Expression<Func<TModel, bool>>> filters, OrderByDefinition<TModel> orderBy = null, int page = 1, int pageSize = 25)
            where TModel : MeshData
        {
            var filter = PredicateBuilder.CombineExpressions(filters);
            var t = this.SearchAsync<TModel>(filter, orderBy, page, pageSize);
            return t.ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public TModel Create<TModel>(TModel model)
            where TModel : MeshData
        {
            var t = this.CreateAsync<TModel>(model);
            return t.ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public TModel Update<TModel>(string id, TModel model)
            where TModel : MeshData
        {
            var t = this.UpdateAsync<TModel>(id, model);
            return t.ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public TModel Update<TModel>(TModel model)
            where TModel : MeshData
        {
            var t = this.UpdateAsync<TModel>(model);
            return t.ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public void Delete<TModel>(string id)
            where TModel : MeshData
        {
            var t = this.DeleteAsync<TModel>(id);
            t.ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public DeleteManyResult DeleteMany<TModel>(string filter)
            where TModel : MeshData
        {
            var t = this.DeleteManyAsync<TModel>(filter);
            return t.ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public Task<DeleteManyResult> DeleteManyAsync<TModel>(string filter)
            where TModel : MeshData
        {
            var encodedUrl = WebUtility.UrlEncode(filter);

            return this.requestService.DeleteRequest<DeleteManyResult>($"meshes/{this.GetMeshName<TModel>()}?filter={encodedUrl}");
        }

        /// <inheritdoc/>
        public DeleteManyResult DeleteMany<TModel>(Expression<Func<TModel, bool>> filter)
            where TModel : MeshData
        {
            var t = this.DeleteManyAsync<TModel>(filter);
            return t.ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public Task<DeleteManyResult> DeleteManyAsync<TModel>(Expression<Func<TModel, bool>> filter)
            where TModel : MeshData
        {
            var mongoFilter = Builders<TModel>.Filter.Where(filter).Render(BsonSerializer.SerializerRegistry.GetSerializer<TModel>(), BsonSerializer.SerializerRegistry);

            return this.DeleteManyAsync<TModel>(mongoFilter.ToString());
        }

        /// <inheritdoc/>
        public DeleteManyResult DeleteMany<TModel>(IEnumerable<Expression<Func<TModel, bool>>> filters)
            where TModel : MeshData
        {
            var t = this.DeleteManyAsync<TModel>(filters);
            return t.ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public Task<DeleteManyResult> DeleteManyAsync<TModel>(IEnumerable<Expression<Func<TModel, bool>>> filters)
            where TModel : MeshData
        {
            var filter = PredicateBuilder.CombineExpressions(filters);

            return this.DeleteManyAsync(filter);
        }

        /// <inheritdoc/>
        public Task<UpdateManyResult> UpdateManyAsync<TModel>(string filter, string update)
            where TModel : MeshData
        {
            return this.requestService.PatchRequest<UpdateManyResult>($"meshes/{this.GetMeshName<TModel>()}", new { Filter = filter, Update = update });
        }

        /// <inheritdoc/>
        public Task<UpdateManyResult> UpdateManyAsync<TModel>(IEnumerable<Expression<Func<TModel, bool>>> filters, string update)
            where TModel : MeshData
        {
            var filter = PredicateBuilder.CombineExpressions(filters);

            return this.UpdateManyAsync<TModel>(filter, update);
        }

        /// <inheritdoc/>
        public Task<UpdateManyResult> UpdateManyAsync<TModel>(Expression<Func<TModel, bool>> filter, string update)
            where TModel : MeshData
        {
            var mongoFilter = Builders<TModel>.Filter.Where(filter).Render(BsonSerializer.SerializerRegistry.GetSerializer<TModel>(), BsonSerializer.SerializerRegistry);

            return this.UpdateManyAsync<TModel>(mongoFilter.ToString(), update);
        }

        /// <inheritdoc/>
        public Task<CreateManyResult> CreateManyAsync<TModel>(IEnumerable<TModel> data)
            where TModel : MeshData
        {
            return this.requestService.PostRequest<CreateManyResult>($"meshes/{this.GetMeshName<TModel>()}", data);
        }

        /// <inheritdoc/>
        public CreateManyResult CreateMany<TModel>(IEnumerable<TModel> data)
            where TModel : MeshData
        {
            var t = this.CreateManyAsync<TModel>(data);
            return t.ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public UpdateManyResult UpdateMany<TModel>(string filter, string update)
            where TModel : MeshData
        {
            var t = this.UpdateManyAsync<TModel>(filter, update);
            return t.ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public UpdateManyResult UpdateMany<TModel>(IEnumerable<Expression<Func<TModel, bool>>> filters, string update)
            where TModel : MeshData
        {
            var t = this.UpdateManyAsync<TModel>(filters, update);
            return t.ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public UpdateManyResult UpdateMany<TModel>(Expression<Func<TModel, bool>> filter, string update)
            where TModel : MeshData
        {
            var t = this.UpdateManyAsync<TModel>(filter, update);
            return t.ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private string GetMeshName<TModel>()
        {
            var meshName = (MeshNameAttribute)Attribute.GetCustomAttribute(typeof(TModel), typeof(MeshNameAttribute));
#pragma warning disable CA1308 // Normalize strings to uppercase
            return meshName?.Name.ToLowerInvariant() ?? typeof(TModel).Name.ToLowerInvariant();
#pragma warning restore CA1308 // Normalize strings to uppercase
        }
    }
}
