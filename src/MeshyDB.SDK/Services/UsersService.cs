// <copyright file="UsersService.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
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
        public Task<User> GetSelfAsync()
        {
            return this.requestService.GetRequest<User>("users/me");
        }

        /// <inheritdoc/>
        public Task<User> UpdateSelfAsync(User user)
        {
            return this.requestService.PutRequest<User>($"users/me", user);
        }

        /// <inheritdoc/>
        public User GetSelf()
        {
            var t = this.GetSelfAsync().ConfigureAwait(false).GetAwaiter();
            return t.GetResult();
        }

        /// <inheritdoc/>
        public User UpdateSelf(User user)
        {
            var t = this.UpdateSelfAsync(user).ConfigureAwait(false).GetAwaiter();
            return t.GetResult();
        }

        /// <inheritdoc/>
        public Task UpdateSecurityQuestionsAsync(UserSecurityQuestionUpdate userSecurityQuestionUpdate)
        {
            return this.requestService.PostRequest<object>($"users/me/questions", userSecurityQuestionUpdate);
        }

        /// <inheritdoc/>
        public void UpdateSecurityQuestions(UserSecurityQuestionUpdate userSecurityQuestionUpdate)
        {
            var t = this.UpdateSecurityQuestionsAsync(userSecurityQuestionUpdate).ConfigureAwait(false).GetAwaiter();
            t.GetResult();
        }
    }
}
