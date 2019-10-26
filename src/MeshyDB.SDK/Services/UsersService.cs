// <copyright file="UsersService.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Net;
using System.Threading.Tasks;
using MeshyDB.SDK.Models;

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

        /// <inheritdoc/>
        public Task<User> GetAsync(string id)
        {
            return this.requestService.GetRequest<User>($"users/{id}");
        }

        /// <inheritdoc/>
        public Task<PageResult<User>> SearchAsync(string name = null, string orderBy = null, bool activeOnly = true, int page = 1, int pageSize = 25)
        {
            var encodedFilter = string.Empty;

            if (!string.IsNullOrWhiteSpace(name))
            {
                encodedFilter = WebUtility.UrlEncode(name);
            }

            var encodedOrderBy = string.Empty;

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                encodedOrderBy = WebUtility.UrlEncode(orderBy);
            }

            return this.requestService.GetRequest<PageResult<User>>($"users?name={encodedFilter}&orderBy={encodedOrderBy}&activeOnly={activeOnly}&page={page}&pageSize={pageSize}");
        }

        /// <inheritdoc/>
        public Task<PageResult<User>> SearchAsync(string name = null, OrderByDefinition<User> orderBy = null, bool activeOnly = true, int page = 1, int pageSize = 25)
        {
            var parsedOrderBy = string.Empty;

            if (orderBy != null)
            {
                parsedOrderBy = orderBy.GenerateBsonDocument();
            }

            return this.SearchAsync(name, parsedOrderBy, activeOnly, page, pageSize);
        }

        /// <inheritdoc/>
        public Task UpdateSecurityQuestionsAsync(string id, UserSecurityQuestionUpdate userSecurityQuestionUpdate)
        {
            return this.requestService.PostRequest<User>($"users/{id}/questions", userSecurityQuestionUpdate);
        }

        /// <inheritdoc/>
        public Task<User> CreateAsync(NewUser model)
        {
            return this.requestService.PostRequest<User>($"users", model);
        }

        /// <inheritdoc/>
        public Task<User> UpdateAsync(string id, User model)
        {
            return this.requestService.PutRequest<User>($"users/{id}", model);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(string id)
        {
            return this.requestService.DeleteRequest<object>($"users/{id}");
        }

        /// <inheritdoc/>
        public User Get(string id)
        {
            var t = this.GetAsync(id).ConfigureAwait(false).GetAwaiter();
            return t.GetResult();
        }

        /// <inheritdoc/>
        public PageResult<User> Search(string name = null, string orderBy = null, bool activeOnly = true, int page = 1, int pageSize = 25)
        {
            var t = this.SearchAsync(name, orderBy, activeOnly, page, pageSize).ConfigureAwait(false).GetAwaiter();
            return t.GetResult();
        }

        /// <inheritdoc/>
        public PageResult<User> Search(string name = null, OrderByDefinition<User> orderBy = null, bool activeOnly = true, int page = 1, int pageSize = 25)
        {
            var t = this.SearchAsync(name, orderBy, activeOnly, page, pageSize).ConfigureAwait(false).GetAwaiter();
            return t.GetResult();
        }

        /// <inheritdoc/>
        public void UpdateSecurityQuestions(string id, UserSecurityQuestionUpdate userSecurityQuestionUpdate)
        {
            var t = this.UpdateSecurityQuestionsAsync(id, userSecurityQuestionUpdate).ConfigureAwait(false).GetAwaiter();
            t.GetResult();
        }

        /// <inheritdoc/>
        public User Create(NewUser model)
        {
            var t = this.CreateAsync(model).ConfigureAwait(false).GetAwaiter();
            return t.GetResult();
        }

        /// <inheritdoc/>
        public User Update(string id, User model)
        {
            var t = this.UpdateAsync(id, model).ConfigureAwait(false).GetAwaiter();
            return t.GetResult();
        }

        /// <inheritdoc/>
        public void Delete(string id)
        {
            var t = this.DeleteAsync(id).ConfigureAwait(false).GetAwaiter();
            t.GetResult();
        }
    }
}
