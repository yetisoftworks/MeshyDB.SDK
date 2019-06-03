// <copyright file="UsersService.cs" company="Yetisoftworks LLC">
// Copyright (c) Yetisoftworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MeshyDB.SDK.Models;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace MeshyDB.SDK.Services
{
    /// <summary>
    /// Implementation of <see cref="IUsersService"/>.
    /// </summary>
    internal class UsersService : IUsersService
    {
        private readonly IRequestService requestService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersService"/> class.
        /// </summary>
        /// <param name="requestService">Service used to make request calls.</param>
        public UsersService(IRequestService requestService)
        {
            this.requestService = requestService ?? throw new ArgumentNullException(nameof(requestService));
        }

        /// <inheritdoc/>
        public async Task<User> GetSelfAsync()
        {
            return await this.requestService.GetRequest<User>("users/me");
        }

        /// <inheritdoc/>
        public async Task<User> UpdateSelfAsync(User user)
        {
            return await this.requestService.PutRequest<User>($"users/me", user);
        }

        /// <inheritdoc/>
        public User GetSelf()
        {
            var t = this.GetSelfAsync().ConfigureAwait(true).GetAwaiter();
            return t.GetResult();
        }

        /// <inheritdoc/>
        public User UpdateSelf(User user)
        {
            var t = this.UpdateSelfAsync(user).ConfigureAwait(true).GetAwaiter();
            return t.GetResult();
        }
    }
}
