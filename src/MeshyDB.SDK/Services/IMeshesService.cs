// <copyright file="IMeshesService.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MeshyDB.SDK.Enums;
using MeshyDB.SDK.Models;

namespace MeshyDB.SDK.Services
{
    /// <summary>
    /// Defines methods for meshes.
    /// </summary>
    public interface IMeshesService
    {
        /// <summary>
        /// Get mesh data for a given id.
        /// </summary>
        /// <typeparam name="TModel">Type of mesh data to be returned.</typeparam>
        /// <param name="id">Identifier of mesh record to be retrieved.</param>
        /// <returns>Data result of request.</returns>
        TModel GetData<TModel>(string id)
            where TModel : MeshData;

        /// <summary>
        /// Get mesh data for a given id.
        /// </summary>
        /// <typeparam name="TModel">Type of mesh data to be returned.</typeparam>
        /// <param name="id">Identifier of mesh record to be retrieved.</param>
        /// <returns>Data result of request.</returns>
        Task<TModel> GetDataAsync<TModel>(string id)
            where TModel : MeshData;

        /// <summary>
        /// Searches mesh data for a given filter.
        /// </summary>
        /// <typeparam name="TModel">Type of mesh data to be returned.</typeparam>
        /// <param name="filter">Filter of data in Mongo DB filter format.</param>
        /// <param name="sort">Sort data in Mongo DB sort format.</param>
        /// <param name="page">Page number to find results on.</param>
        /// <param name="pageSize">Number of items to bring back from search.</param>
        /// <returns>Page result data for the given mesh with applied filter.</returns>
        /// <remarks>
        /// The maximum page size is 200.
        /// </remarks>
        PageResult<TModel> Search<TModel>(string filter = null, string sort = null, int page = 1, int pageSize = 200)
            where TModel : MeshData;

        /// <summary>
        /// Searches mesh data for a given filter.
        /// </summary>
        /// <typeparam name="TModel">Type of mesh data to be returned.</typeparam>
        /// <param name="filter">Filter of data in Mongo DB filter format.</param>
        /// <param name="sort">Sort data in Mongo DB sort format.</param>
        /// <param name="page">Page number to find results on.</param>
        /// <param name="pageSize">Number of items to bring back from search.</param>
        /// <returns>Page result data for the given mesh with applied filter.</returns>
        /// <remarks>
        /// The maximum page size is 200.
        /// </remarks>
        Task<PageResult<TModel>> SearchAsync<TModel>(string filter = null, string sort = null, int page = 1, int pageSize = 200)
            where TModel : MeshData;

        /// <summary>
        /// Searches mesh data for a given filter.
        /// </summary>
        /// <typeparam name="TModel">Type of mesh data to be returned.</typeparam>
        /// <param name="filter">Filter predicate for data.</param>
        /// <param name="sort">Collection of sort for data.</param>
        /// <param name="page">Page number to find results on.</param>
        /// <param name="pageSize">Number of items to bring back from search.</param>
        /// <returns>Page result data for the given mesh with applied filter.</returns>
        /// <remarks>
        /// The maximum page size is 200.
        /// </remarks>
        PageResult<TModel> Search<TModel>(Expression<Func<TModel, bool>> filter, IEnumerable<KeyValuePair<string, SortDirection>> sort = null, int page = 1, int pageSize = 200)
            where TModel : MeshData;

        /// <summary>
        /// Searches mesh data for a given filter.
        /// </summary>
        /// <typeparam name="TModel">Type of mesh data to be returned.</typeparam>
        /// <param name="filter">Filter predicate for data.</param>
        /// <param name="sort">Collection of sort for data.</param>
        /// <param name="page">Page number to find results on.</param>
        /// <param name="pageSize">Number of items to bring back from search.</param>
        /// <returns>Page result data for the given mesh with applied filter.</returns>
        /// <remarks>
        /// The maximum page size is 200.
        /// </remarks>
        Task<PageResult<TModel>> SearchAsync<TModel>(Expression<Func<TModel, bool>> filter, IEnumerable<KeyValuePair<string, SortDirection>> sort = null, int page = 1, int pageSize = 200)
            where TModel : MeshData;

        /// <summary>
        /// Searches mesh data for a given filter.
        /// </summary>
        /// <typeparam name="TModel">Type of mesh data to be returned.</typeparam>
        /// <param name="filters">Enumerable of filters that all must be met for data.</param>
        /// <param name="sort">Collection of sort for data.</param>
        /// <param name="page">Page number to find results on.</param>
        /// <param name="pageSize">Number of items to bring back from search.</param>
        /// <returns>Page result data for the given mesh with applied filter.</returns>
        /// <remarks>
        /// The maximum page size is 200.
        /// </remarks>
        PageResult<TModel> Search<TModel>(IEnumerable<Expression<Func<TModel, bool>>> filters, IEnumerable<KeyValuePair<string, SortDirection>> sort = null, int page = 1, int pageSize = 200)
            where TModel : MeshData;

        /// <summary>
        /// Searches mesh data for a given filter.
        /// </summary>
        /// <typeparam name="TModel">Type of mesh data to be returned.</typeparam>
        /// <param name="filters">Enumerable of filters that all must be met for data.</param>
        /// <param name="sort">Collection of sort for data.</param>
        /// <param name="page">Page number to find results on.</param>
        /// <param name="pageSize">Number of items to bring back from search.</param>
        /// <returns>Page result data for the given mesh with applied filter.</returns>
        /// <remarks>
        /// The maximum page size is 200.
        /// </remarks>
        Task<PageResult<TModel>> SearchAsync<TModel>(IEnumerable<Expression<Func<TModel, bool>>> filters, IEnumerable<KeyValuePair<string, SortDirection>> sort = null, int page = 1, int pageSize = 200)
            where TModel : MeshData;

        /// <summary>
        /// Create mesh data.
        /// </summary>
        /// <typeparam name="TModel">Type of mesh data to be returned and created.</typeparam>
        /// <param name="model">Mesh data to be committed.</param>
        /// <returns>Result of committed mesh data.</returns>
        TModel Create<TModel>(TModel model)
            where TModel : MeshData;

        /// <summary>
        /// Create mesh data.
        /// </summary>
        /// <typeparam name="TModel">Type of mesh data to be returned and created.</typeparam>
        /// <param name="model">Mesh data to be committed.</param>
        /// <returns>Result of committed mesh data.</returns>
        Task<TModel> CreateAsync<TModel>(TModel model)
            where TModel : MeshData;

        /// <summary>
        /// Update mesh data for a given id.
        /// </summary>
        /// <typeparam name="TModel">Type of mesh data to be returned and updated.</typeparam>
        /// <param name="id">Identifier of mesh record to be updated.</param>
        /// <param name="model">Mesh data to be updated.</param>
        /// <returns>Result of updated mesh data.</returns>
        TModel Update<TModel>(string id, TModel model)
            where TModel : MeshData;

        /// <summary>
        /// Update mesh data for a given id.
        /// </summary>
        /// <typeparam name="TModel">Type of mesh data to be returned and updated.</typeparam>
        /// <param name="id">Identifier of mesh record to be updated.</param>
        /// <param name="model">Mesh data to be updated.</param>
        /// <returns>Result of updated mesh data.</returns>
        Task<TModel> UpdateAsync<TModel>(string id, TModel model)
            where TModel : MeshData;

        /// <summary>
        /// Update mesh data for a given id.
        /// </summary>
        /// <typeparam name="TModel">Type of mesh data to be returned and updated.</typeparam>
        /// <param name="model">Mesh data to be updated.</param>
        /// <returns>Result of updated mesh data.</returns>
        TModel Update<TModel>(TModel model)
            where TModel : MeshData;

        /// <summary>
        /// Update mesh data for a given id.
        /// </summary>
        /// <typeparam name="TModel">Type of mesh data to be returned and updated.</typeparam>
        /// <param name="model">Mesh data to be updated.</param>
        /// <returns>Result of updated mesh data.</returns>
        Task<TModel> UpdateAsync<TModel>(TModel model)
            where TModel : MeshData;

        /// <summary>
        /// Get mesh data for a given id.
        /// </summary>
        /// <typeparam name="TModel">Type of mesh data to be deleted.</typeparam>
        /// <param name="id">Identifier of mesh record to be deleted.</param>
        void Delete<TModel>(string id)
            where TModel : MeshData;

        /// <summary>
        /// Get mesh data for a given id.
        /// </summary>
        /// <typeparam name="TModel">Type of mesh data to be deleted.</typeparam>
        /// <param name="id">Identifier of mesh record to be deleted.</param>
        /// <returns>Task indicating when operation is complete.</returns>
        Task DeleteAsync<TModel>(string id)
            where TModel : MeshData;

        /// <summary>
        /// Remove entire mesh collection.
        /// </summary>
        /// <typeparam name="TModel">Type of mesh data to be deleted.</typeparam>
        void Delete<TModel>()
            where TModel : MeshData;

        /// <summary>
        /// Remove entire mesh collection.
        /// </summary>
        /// <typeparam name="TModel">Type of mesh data to be deleted.</typeparam>
        /// <returns>Task indicating when operation is complete.</returns>
        Task DeleteAsync<TModel>()
            where TModel : MeshData;
    }
}
