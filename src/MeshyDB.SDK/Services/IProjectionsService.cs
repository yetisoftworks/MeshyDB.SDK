// <copyright file="IProjectionsService.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using MeshyDB.SDK.Models;

namespace MeshyDB.SDK.Services
{
    /// <summary>
    /// Defines methods for projections.
    /// </summary>
    public interface IProjectionsService
    {
        /// <summary>
        /// Gets data for a given projection name.
        /// </summary>
        /// <typeparam name="TData">Type of data to be returned.</typeparam>
        /// <param name="projectionName">Name of projection to retrieve.</param>
        /// <param name="sort">Sort data in Mongo DB sort format.</param>
        /// <param name="page">Page number to find results on.</param>
        /// <param name="pageSize">Number of items to bring back from search.</param>
        /// <returns>Page result data for the given projection.</returns>
        /// <remarks>
        /// The maximum page size is 200.
        /// </remarks>
        Task<PageResult<TData>> GetAsync<TData>(string projectionName, SortDefinition<TData> sort, int page = 1, int pageSize = 25)
            where TData : class;

        /// <summary>
        /// Gets data for a given projection name.
        /// </summary>
        /// <typeparam name="TData">Type of data to be returned.</typeparam>
        /// <param name="projectionName">Name of projection to retrieve.</param>
        /// <param name="page">Page number to find results on.</param>
        /// <param name="pageSize">Number of items to bring back from search.</param>
        /// <returns>Page result data for the given projection.</returns>
        /// <remarks>
        /// The maximum page size is 200.
        /// </remarks>
        Task<PageResult<TData>> GetAsync<TData>(string projectionName, int page = 1, int pageSize = 25)
            where TData : class;

        /// <summary>
        /// Gets data for a given projection name.
        /// </summary>
        /// <typeparam name="TData">Type of data to be returned.</typeparam>
        /// <param name="projectionName">Name of projection to retrieve.</param>
        /// <param name="sort">Sort data in Mongo DB sort format.</param>
        /// <param name="page">Page number to find results on.</param>
        /// <param name="pageSize">Number of items to bring back from search.</param>
        /// <returns>Page result data for the given projection.</returns>
        /// <remarks>
        /// The maximum page size is 200.
        /// </remarks>
        Task<PageResult<TData>> GetAsync<TData>(string projectionName, string sort, int page = 1, int pageSize = 25)
            where TData : class;

        /// <summary>
        /// Gets data for a given projection name.
        /// </summary>
        /// <typeparam name="TData">Type of data to be returned.</typeparam>
        /// <param name="projectionName">Name of projection to retrieve.</param>
        /// <returns>Page result data for the given projection.</returns>
        /// <remarks>
        /// The maximum page size is 200.
        /// </remarks>
        Task<PageResult<TData>> GetAsync<TData>(string projectionName)
            where TData : class;

        /// <summary>
        /// Gets data for a given projection name.
        /// </summary>
        /// <typeparam name="TData">Type of data to be returned.</typeparam>
        /// <param name="projectionName">Name of projection to retrieve.</param>
        /// <param name="sort">Sort data in Mongo DB sort format.</param>
        /// <param name="page">Page number to find results on.</param>
        /// <param name="pageSize">Number of items to bring back from search.</param>
        /// <returns>Page result data for the given projection.</returns>
        /// <remarks>
        /// The maximum page size is 200.
        /// </remarks>
        PageResult<TData> Get<TData>(string projectionName, SortDefinition<TData> sort = null, int page = 1, int pageSize = 25)
            where TData : class;

        /// <summary>
        /// Gets data for a given projection name.
        /// </summary>
        /// <typeparam name="TData">Type of data to be returned.</typeparam>
        /// <param name="projectionName">Name of projection to retrieve.</param>
        /// <param name="page">Page number to find results on.</param>
        /// <param name="pageSize">Number of items to bring back from search.</param>
        /// <returns>Page result data for the given projection.</returns>
        /// <remarks>
        /// The maximum page size is 200.
        /// </remarks>
        PageResult<TData> Get<TData>(string projectionName, int page = 1, int pageSize = 25)
            where TData : class;

        /// <summary>
        /// Gets data for a given projection name.
        /// </summary>
        /// <typeparam name="TData">Type of data to be returned.</typeparam>
        /// <param name="projectionName">Name of projection to retrieve.</param>
        /// <returns>Page result data for the given projection.</returns>
        /// <remarks>
        /// The maximum page size is 200.
        /// </remarks>
        PageResult<TData> Get<TData>(string projectionName)
            where TData : class;

        /// <summary>
        /// Gets data for a given projection name.
        /// </summary>
        /// <typeparam name="TData">Type of data to be returned.</typeparam>
        /// <param name="projectionName">Name of projection to retrieve.</param>
        /// <param name="sort">Sort data in Mongo DB sort format.</param>
        /// <param name="page">Page number to find results on.</param>
        /// <param name="pageSize">Number of items to bring back from search.</param>
        /// <returns>Page result data for the given projection.</returns>
        /// <remarks>
        /// The maximum page size is 200.
        /// </remarks>
        PageResult<TData> Get<TData>(string projectionName, string sort, int page = 1, int pageSize = 25)
            where TData : class;
    }
}
