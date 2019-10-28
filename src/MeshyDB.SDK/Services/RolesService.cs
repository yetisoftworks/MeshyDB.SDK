// <copyright file="RolesService.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using MeshyDB.SDK.Models;

namespace MeshyDB.SDK.Services
{
    /// <summary>
    /// Implementation of <see cref="IRolesService"/>.
    /// </summary>
    internal class RolesService : IRolesService
    {
        private readonly IRequestService requestService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RolesService"/> class.
        /// </summary>
        /// <param name="requestService">Service used to make request calls.</param>
        public RolesService(IRequestService requestService)
        {
            this.requestService = requestService;
        }

        /// <inheritdoc/>
        public void AddUsers(string id, UserRoleAdd model)
        {
            var t = this.AddUsersAsync(id, model).ConfigureAwait(false).GetAwaiter();
            t.GetResult();
        }

        /// <inheritdoc/>
        public Task AddUsersAsync(string id, UserRoleAdd model)
        {
            return this.requestService.PostRequest<object>($"roles/{id}/users", model);
        }

        /// <inheritdoc/>
        public Role Create(Role model)
        {
            var t = this.CreateAsync(model).ConfigureAwait(false).GetAwaiter();
            return t.GetResult();
        }

        /// <inheritdoc/>
        public Task<Role> CreateAsync(Role model)
        {
            return this.requestService.PostRequest<Role>($"roles", model);
        }

        /// <inheritdoc/>
        public Permission CreatePermission(string roleId, Permission model)
        {
            var t = this.CreatePermissionAsync(roleId, model).ConfigureAwait(false).GetAwaiter();
            return t.GetResult();
        }

        /// <inheritdoc/>
        public Task<Permission> CreatePermissionAsync(string roleId, Permission model)
        {
            return this.requestService.PostRequest<Permission>($"roles/{roleId}/permissions", model);
        }

        /// <inheritdoc/>
        public void Delete(string id)
        {
            var t = this.DeleteAsync(id).ConfigureAwait(false).GetAwaiter();
            t.GetResult();
        }

        /// <inheritdoc/>
        public Task DeleteAsync(string id)
        {
            return this.requestService.DeleteRequest<object>($"roles/{id}");
        }

        /// <inheritdoc/>
        public void DeletePermission(string roleId, string permissionId)
        {
            var t = this.DeletePermissionAsync(roleId, permissionId).ConfigureAwait(false).GetAwaiter();
            t.GetResult();
        }

        /// <inheritdoc/>
        public Task DeletePermissionAsync(string roleId, string permissionId)
        {
            return this.requestService.DeleteRequest<object>($"roles/{roleId}/permissions/{permissionId}");
        }

        /// <inheritdoc/>
        public Role Get(string id)
        {
            var t = this.GetAsync(id).ConfigureAwait(false).GetAwaiter();
            return t.GetResult();
        }

        /// <inheritdoc/>
        public Task<Role> GetAsync(string id)
        {
            return this.requestService.GetRequest<Role>($"roles/{id}");
        }

        /// <inheritdoc/>
        public Permission GetPermission(string roleId, string permissionId)
        {
            var t = this.GetPermissionAsync(roleId, permissionId).ConfigureAwait(false).GetAwaiter();
            return t.GetResult();
        }

        /// <inheritdoc/>
        public Task<Permission> GetPermissionAsync(string roleId, string permissionId)
        {
            return this.requestService.GetRequest<Permission>($"roles/{roleId}/permissions/{permissionId}");
        }

        /// <inheritdoc/>
        public void RemoveUsers(string id, UserRoleRemove model)
        {
            var t = this.RemoveUsersAsync(id, model).ConfigureAwait(false).GetAwaiter();
            t.GetResult();
        }

        /// <inheritdoc/>
        public Task RemoveUsersAsync(string id, UserRoleRemove model)
        {
            return this.requestService.DeleteRequest<object>($"roles/{id}/users", model);
        }

        /// <inheritdoc/>
        public PageResult<Role> Search(string name = null, int page = 1, int pageSize = 25)
        {
            var t = this.SearchAsync(name, page, pageSize).ConfigureAwait(false).GetAwaiter();
            return t.GetResult();
        }

        /// <inheritdoc/>
        public Task<PageResult<Role>> SearchAsync(string name = null, int page = 1, int pageSize = 25)
        {
            return this.requestService.GetRequest<PageResult<Role>>($"roles?name={name}&page={page}&pageSize={pageSize}");
        }

        /// <inheritdoc/>
        public PageResult<Permissible> SearchPermissible(string name = null, int page = 1, int pageSize = 25)
        {
            var t = this.SearchPermissibleAsync(name, page, pageSize).ConfigureAwait(false).GetAwaiter();
            return t.GetResult();
        }

        /// <inheritdoc/>
        public Task<PageResult<Permissible>> SearchPermissibleAsync(string name = null, int page = 1, int pageSize = 25)
        {
            return this.requestService.GetRequest<PageResult<Permissible>>($"permissibles?name={name}&page={page}&pageSize={pageSize}");
        }

        /// <inheritdoc/>
        public PageResult<Permission> SearchPermissions(string roleId, string permissibleName = null, int page = 1, int pageSize = 25)
        {
            var t = this.SearchPermissionsAsync(roleId, permissibleName, page, pageSize).ConfigureAwait(false).GetAwaiter();
            return t.GetResult();
        }

        /// <inheritdoc/>
        public Task<PageResult<Permission>> SearchPermissionsAsync(string roleId, string permissibleName = null, int page = 1, int pageSize = 25)
        {
            return this.requestService.GetRequest<PageResult<Permission>>($"roles/{roleId}/permissions?permissibleName={permissibleName}&page={page}&pageSize={pageSize}");
        }

        /// <inheritdoc/>
        public Role Update(string id, Role model)
        {
            var t = this.UpdateAsync(id, model).ConfigureAwait(false).GetAwaiter();
            return t.GetResult();
        }

        /// <inheritdoc/>
        public Task<Role> UpdateAsync(string id, Role model)
        {
            return this.requestService.PutRequest<Role>($"roles/{id}", model);
        }

        /// <inheritdoc/>
        public Permission UpdatePermission(string roleId, string permissionId, Permission model)
        {
            var t = this.UpdatePermissionAsync(roleId, permissionId, model).ConfigureAwait(false).GetAwaiter();
            return t.GetResult();
        }

        /// <inheritdoc/>
        public Task<Permission> UpdatePermissionAsync(string roleId, string permissionId, Permission model)
        {
            return this.requestService.PutRequest<Permission>($"roles/{roleId}/permissions/{permissionId}", model);
        }
    }
}
