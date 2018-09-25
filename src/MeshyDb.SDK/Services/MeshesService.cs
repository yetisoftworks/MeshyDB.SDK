using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MeshyDb.SDK.Models;

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
        public async Task<TModel> GetAsync<TModel>(string mesh, string id) where TModel : MeshData
        {
            return await requestService.GetRequest<TModel>($"meshes/{mesh}/{id}");
        }

        /// <inheritdoc/>
        public async Task<PageResult<TModel>> SearchAsync<TModel>(string mesh, string filter = null, int page = 1, int pageSize = 200) where TModel : MeshData
        {
            var encodedUrl = string.Empty;
            encodedUrl = WebUtility.UrlEncode(filter);

            return await requestService.GetRequest<PageResult<TModel>>($"meshes/{mesh}?filter={encodedUrl}&page={page}&pageSize={pageSize}");
        }

        /// <inheritdoc/>
        public async Task<PageResult<TModel>> SearchAsync<TModel>(string mesh, Expression<Func<TModel, bool>> filter, int page = 1, int pageSize = 200) where TModel : MeshData
        {
            var mongoFilter = Builders<TModel>.Filter.Where(filter).Render(BsonSerializer.SerializerRegistry.GetSerializer<TModel>(), BsonSerializer.SerializerRegistry);

            var encodedUrl = WebUtility.UrlEncode(mongoFilter.ToString());

            return await requestService.GetRequest<PageResult<TModel>>($"meshes/{mesh}?filter={encodedUrl}&page={page}&pageSize={pageSize}");
        }

        /// <inheritdoc/>
        public async Task<TModel> CreateAsync<TModel>(string mesh, TModel model) where TModel : MeshData
        {
            return await requestService.PostRequest<TModel>($"meshes/{mesh}", model);
        }

        /// <inheritdoc/>
        public async Task<TModel> UpdateAsync<TModel>(string mesh, string id, TModel model) where TModel : MeshData
        {
            return await requestService.PutRequest<TModel>($"meshes/{mesh}/{id}", model);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(string mesh, string id)
        {
            await requestService.DeleteRequest<object>($"meshes/{mesh}/{id}");
        }

        /// <inheritdoc/>
        public TModel Get<TModel>(string mesh, string id) where TModel : MeshData
        {
            var t = this.GetAsync<TModel>(mesh, id);
            return t.ConfigureAwait(true).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public PageResult<TModel> Search<TModel>(string mesh, string filter = null, int page = 1, int pageSize = 200) where TModel : MeshData
        {
            var t = this.SearchAsync<TModel>(mesh, filter, page, pageSize);
            return t.ConfigureAwait(true).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public PageResult<TModel> Search<TModel>(string mesh, Expression<Func<TModel, bool>> filter, int page = 1, int pageSize = 200) where TModel : MeshData
        {
            var t = this.SearchAsync<TModel>(mesh, filter, page, pageSize);
            return t.ConfigureAwait(true).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public TModel Create<TModel>(string mesh, TModel model) where TModel : MeshData
        {
            var t = this.CreateAsync<TModel>(mesh, model);
            return t.ConfigureAwait(true).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public TModel Update<TModel>(string mesh, string id, TModel model) where TModel : MeshData
        {
            var t = this.UpdateAsync<TModel>(mesh, id, model);
            return t.ConfigureAwait(true).GetAwaiter().GetResult();
        }

        /// <inheritdoc/>
        public void Delete(string mesh, string id)
        {
            var t = this.DeleteAsync(mesh, id);
            t.ConfigureAwait(true).GetAwaiter().GetResult();
        }
    }
}
