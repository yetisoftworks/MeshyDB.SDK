// <copyright file="MeshyClient.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using MeshyDB.SDK.Services;

namespace MeshyDB.SDK
{
    /// <summary>
    /// Handles Meshy Client management.
    /// </summary>
    public static class MeshyClient
    {
        /// <summary>
        /// Gets current authenticated Meshy connection.
        /// </summary>
        public static IMeshyConnection CurrentConnection { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeshyClient"/> class that is used to communicate with the MeshyDB REST API.
        /// </summary>
        /// <param name="accountName">Name of MeshyDB account required for communication.</param>
        /// <param name="publicKey">Public Api credential supplied from MeshyDB to communicate with tenant.</param>
        /// <param name="httpService">Http Service to use for making requests.</param>
        /// <returns>Initialized instance of a Meshy Client.</returns>
        public static IMeshyClient Initialize(string accountName, string publicKey, IHttpService httpService = null)
        {
            return new Services.MeshyClient(accountName, publicKey, httpService);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeshyClient"/> class that is used to communicate with the MeshyDB REST API.
        /// </summary>
        /// <param name="accountName">Name of MeshyDB account required for communication.</param>
        /// <param name="tenant">Tenant of data used for partitioning.</param>
        /// <param name="publicKey">Public Api credential supplied from MeshyDB to communicate with tenant.</param>
        /// <param name="httpService">Http Service to use for making requests.</param>
        /// <returns>Initialized instance of a Meshy Client.</returns>
        public static IMeshyClient InitializeWithTenant(string accountName, string tenant, string publicKey, IHttpService httpService = null)
        {
            return new Services.MeshyClient(accountName, tenant, publicKey, httpService);
        }
    }
}
