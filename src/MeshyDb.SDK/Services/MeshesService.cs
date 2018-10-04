using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MeshyDb.SDK.Models;
using System.Linq;
using MeshyDb.SDK.Attributes;

namespace MeshyDb.SDK.Services
{
    /// <summary>
    /// Implementation of <see cref="IMeshesService"/>
    /// </summary>
    internal class MeshesService : IMeshesService
    {
        private readonly IRequestService requestService;

        internal MeshesService(IRequestService requestService)
        {
            this.requestService = requestService ?? throw new ArgumentNullException(nameof(requestService));
        }

        /// <inheritdoc/>
        public async Task<TModel> GetAsync<TModel>(string id) where TModel : MeshData
        {
            return await requestService.GetRequest<TModel>($"meshes/{GetMeshName<TModel>()}/{id}");
        }

        /// <inheritdoc/>
        public async Task<PageResult<TModel>> SearchAsync<TModel>(string filter = null, int page = 1, int pageSize = 200) where TModel : MeshData
        {
            var encodedUrl = string.Empty;
            encodedUrl = WebUtility.UrlEncode(filter);

            return await requestService.GetRequest<PageResult<TModel>>($"meshes/{GetMeshName<TModel>()}?filter={encodedUrl}&page={page}&pageSize={pageSize}");
        }

        /// <inheritdoc/>
        public async Task<PageResult<TModel>> SearchAsync<TModel>(Expression<Func<TModel, bool>> filter, int page = 1, int pageSize = 200) where TModel : MeshData
        {
            var mongoFilter = Builders<TModel>.Filter.Where(filter).Render(BsonSerializer.SerializerRegistry.GetSerializer<TModel>(), BsonSerializer.SerializerRegistry);

            var encodedUrl = WebUtility.UrlEncode(mongoFilter.ToString());

            return await requestService.GetRequest<PageResult<TModel>>($"meshes/{GetMeshName<TModel>()}?filter={encodedUrl}&page={page}&pageSize={pageSize}");
        }

        /// <inheritdoc/>
        public async Task<TModel> CreateAsync<TModel>(TModel model) where TModel : MeshData
        {
            return await requestService.PostRequest<TModel>($"meshes/{GetMeshName<TModel>()}", model);
        }

        /// <inheritdoc/>
        public async Task<TModel> UpdateAsync<TModel>(string id, TModel model) where TModel : MeshData
        {
            return await requestService.PutRequest<TModel>($"meshes/{GetMeshName<TModel>()}/{id}", model);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync<TModel>(string id) where TModel : MeshData
        {
            await requestService.DeleteRequest<object>($"meshes/{GetMeshName<TModel>()}/{id}");
        }

        /// <inheritdoc/>
        public TModel Get<TModel>(string id) where TModel : MeshData
        {
            var t = this.GetAsync<TModel>(id);
            return t.ConfigureAwait(true).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public PageResult<TModel> Search<TModel>(string filter = null, int page = 1, int pageSize = 200) where TModel : MeshData
        {
            var t = this.SearchAsync<TModel>(filter, page, pageSize);
            return t.ConfigureAwait(true).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public PageResult<TModel> Search<TModel>(Expression<Func<TModel, bool>> filter, int page = 1, int pageSize = 200) where TModel : MeshData
        {
            var t = this.SearchAsync<TModel>(filter, page, pageSize);
            return t.ConfigureAwait(true).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public TModel Create<TModel>(TModel model) where TModel : MeshData
        {
            var t = this.CreateAsync<TModel>(model);
            return t.ConfigureAwait(true).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public TModel Update<TModel>(string id, TModel model) where TModel : MeshData
        {
            var t = this.UpdateAsync<TModel>(id, model);
            return t.ConfigureAwait(true).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public void Delete<TModel>(string id) where TModel : MeshData
        {
            var t = this.DeleteAsync<TModel>(id);
            t.ConfigureAwait(true).GetAwaiter().GetResult();
        }

        private string GetMeshName<TModel>()
        {
            var meshName = (MeshNameAttribute)Attribute.GetCustomAttribute(typeof(TModel), typeof(MeshNameAttribute));
            return meshName?.Name.ToLower() ?? typeof(TModel).Name.ToLower();
        }
    }
}
