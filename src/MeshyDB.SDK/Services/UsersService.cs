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
    internal class UsersService : IUsersService
    {
        private readonly IRequestService requestService;

        public UsersService(IRequestService requestService)
        {
            this.requestService = requestService ?? throw new ArgumentNullException(nameof(requestService));
        }

        /// <inheritdoc/>
        public async Task<User> GetLoggedInUserAsync()
        {
            return await this.requestService.GetRequest<User>("users/me");
        }

        /// <inheritdoc/>
        public async Task<User> GetUserAsync(string id)
        {
            return await this.requestService.GetRequest<User>($"users/{id}");
        }

        /// <inheritdoc/>
        public async Task<PageResult<User>> GetUsersAsync(IEnumerable<string> nameParts = null, IEnumerable<string> roles = null, bool activeOnly = true, int page = 1, int pageSize = 200)
        {
            var encodedUrl = string.Empty;
            encodedUrl = WebUtility.UrlEncode(string.Join(" ", nameParts));
            var encodedRoles = string.Empty;

            foreach (var item in roles)
            {
                encodedRoles += $"&roles=" + WebUtility.UrlEncode(item);
            }
            return await this.requestService.GetRequest<PageResult<User>>($"users?query={encodedUrl}{encodedRoles}&activeOnly={activeOnly}&page={page}&pageSize={pageSize}");
        }

        /// <inheritdoc/>
        public async Task<User> UpdateUserAsync(User user)
        {
            return await this.UpdateUserAsync(user.Id, user);
        }

        /// <inheritdoc/>
        public async Task<User> UpdateUserAsync(string id, User user)
        {
            return await this.requestService.PutRequest<User>($"users/{id}", user);
        }

        /// <inheritdoc/>
        public async Task DeleteUserAsync(string id)
        {
            await this.requestService.DeleteRequest<object>($"users/{id}");
        }

        /// <inheritdoc/>
        public User GetLoggedInUser()
        {
            var t = this.GetLoggedInUserAsync().ConfigureAwait(true).GetAwaiter();
            return t.GetResult();
        }

        /// <inheritdoc/>
        public User GetUser(string id)
        {
            var t = this.GetUserAsync(id).ConfigureAwait(true).GetAwaiter();
            return t.GetResult();
        }

        /// <inheritdoc/>
        public PageResult<User> GetUsers(IEnumerable<string> nameParts = null, IEnumerable<string> roles = null, bool activeOnly = true, int page = 1, int pageSize = 200)
        {
            var t = this.GetUsersAsync(nameParts, roles, activeOnly, page, pageSize).ConfigureAwait(true).GetAwaiter();
            return t.GetResult();
        }

        /// <inheritdoc/>
        public User UpdateUser(User user)
        {
            var t = this.UpdateUserAsync(user).ConfigureAwait(true).GetAwaiter();
            return t.GetResult();
        }

        /// <inheritdoc/>
        public User UpdateUser(string id, User user)
        {
            var t = this.UpdateUserAsync(id, user).ConfigureAwait(true).GetAwaiter();
            return t.GetResult();
        }

        /// <inheritdoc/>
        public void DeleteUser(string id)
        {
            var t = this.DeleteUserAsync(id).ConfigureAwait(true).GetAwaiter();
            t.GetResult();
        }
    }
}
