// <copyright file="IRolesService.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using MeshyDB.SDK.Models;

namespace MeshyDB.SDK.Services
{
    /// <summary>
    /// Defines methods for roles.
    /// </summary>
    public interface IRolesService
    {
        /// <summary>
        /// Get role for a given id.
        /// </summary>
        /// <param name="id">Identifier of role to be retrieved.</param>
        /// <returns>Role result of request.</returns>
        Task<Role> GetAsync(string id);

        /// <summary>
        /// Searches roles for a given filter.
        /// </summary>
        /// <param name="name">Contains search of name case-insensitive.</param>
        /// <param name="page">Page number to find results on.</param>
        /// <param name="pageSize">Number of items to bring back from search.</param>
        /// <returns>Page result for the given role with applied filter.</returns>
        Task<PageResult<Role>> SearchAsync(string name = null, int page = 1, int pageSize = 25);

        /// <summary>
        /// Create role.
        /// </summary>
        /// <param name="model">Role to be committed.</param>
        /// <returns>Result of committed role.</returns>
        Task<Role> CreateAsync(Role model);

        /// <summary>
        /// Update role for a given id.
        /// </summary>
        /// <param name="id">Identifier of role to be updated.</param>
        /// <param name="model">Role to be updated.</param>
        /// <returns>Result of updated role.</returns>
        Task<Role> UpdateAsync(string id, Role model);

        /// <summary>
        /// Delete role for a given id.
        /// </summary>
        /// <param name="id">Identifier of role to be deleted.</param>
        /// <returns>Task indicating when operation is complete.</returns>
        Task DeleteAsync(string id);

        /// <summary>
        /// Get permission for a role and permission id.
        /// </summary>
        /// <param name="roleId">Identifier of role to be scoped.</param>
        /// <param name="permissionId">Identifier of the permission to be retrieved.</param>
        /// <returns>Result of retrieved permission.</returns>
        Task<Permission> GetPermissionAsync(string roleId, string permissionId);

        /// <summary>
        /// Searches permissions for a role and given filters.
        /// </summary>
        /// <param name="roleId">Identifier of role to be scoped.</param>
        /// <param name="permissibleName">Name of permissible to find case-insensitive.</param>
        /// <param name="page">Page number to find results on.</param>
        /// <param name="pageSize">Number of items to bring back from search.</param>
        /// <returns>Page result for the given permissions with applied filter.</returns>
        Task<PageResult<Permission>> SearchPermissionsAsync(string roleId, string permissibleName = null, int page = 1, int pageSize = 25);

        /// <summary>
        /// Create permission for a specified role.
        /// </summary>
        /// <param name="roleId">Identifier of role to be scoped.</param>
        /// <param name="model">Permission to be committed.</param>
        /// <returns>Result of committed permission.</returns>
        Task<Permission> CreatePermissionAsync(string roleId, Permission model);

        /// <summary>
        /// Update permission for a specified role.
        /// </summary>
        /// <param name="roleId">Identifier of role to be scoped.</param>
        /// <param name="permissionId">Identifier of permission to be updated.</param>
        /// <param name="model">Permission to be updated.</param>
        /// <returns>Result of updated permission.</returns>
        Task<Permission> UpdatePermissionAsync(string roleId, string permissionId, Permission model);

        /// <summary>
        /// Delete permission for a specified role.
        /// </summary>
        /// <param name="roleId">Identifier of role to be scoped.</param>
        /// <param name="permissionId">Identifier of permission to be deleted.</param>
        /// <returns>Task indicating when operation is complete.</returns>
        Task DeletePermissionAsync(string roleId, string permissionId);

        /// <summary>
        /// Get role for a given id.
        /// </summary>
        /// <param name="id">Identifier of role to be retrieved.</param>
        /// <returns>Role result of request.</returns>
        Role Get(string id);

        /// <summary>
        /// Searches roles for a given filter.
        /// </summary>
        /// <param name="name">Contains search of name case-insensitive.</param>
        /// <param name="page">Page number to find results on.</param>
        /// <param name="pageSize">Number of items to bring back from search.</param>
        /// <returns>Page result for the given  role with applied filter.</returns>
        PageResult<Role> Search(string name = null, int page = 1, int pageSize = 25);

        /// <summary>
        /// Create role.
        /// </summary>
        /// <param name="model">Role to be committed.</param>
        /// <returns>Result of committed role.</returns>
        Role Create(Role model);

        /// <summary>
        /// Update role for a given id.
        /// </summary>
        /// <param name="id">Identifier of role to be updated.</param>
        /// <param name="model">Role to be updated.</param>
        /// <returns>Result of updated role.</returns>
        Role Update(string id, Role model);

        /// <summary>
        /// Delete role for a given id.
        /// </summary>
        /// <param name="id">Identifier of role to be deleted.</param>
        void Delete(string id);

        /// <summary>
        /// Get permission for a role and permission id.
        /// </summary>
        /// <param name="roleId">Identifier of role to be scoped.</param>
        /// <param name="permissionId">Identifier of the permission to be retrieved.</param>
        /// <returns>Result of retrieved permission.</returns>
        Permission GetPermission(string roleId, string permissionId);

        /// <summary>
        /// Searches permissions for a role and given filters.
        /// </summary>
        /// <param name="roleId">Identifier of role to be scoped.</param>
        /// <param name="permissibleName">Name of permissible to find case-insensitive.</param>
        /// <param name="page">Page number to find results on.</param>
        /// <param name="pageSize">Number of items to bring back from search.</param>
        /// <returns>Page result for the given permissions with applied filter.</returns>
        PageResult<Permission> SearchPermissions(string roleId, string permissibleName = null, int page = 1, int pageSize = 25);

        /// <summary>
        /// Create permission for a specified role.
        /// </summary>
        /// <param name="roleId">Identifier of role to be scoped.</param>
        /// <param name="model">Permission to be committed.</param>
        /// <returns>Result of committed permission.</returns>
        Permission CreatePermission(string roleId, Permission model);

        /// <summary>
        /// Update permission for a specified role.
        /// </summary>
        /// <param name="roleId">Identifier of role to be scoped.</param>
        /// <param name="permissionId">Identifier of permission to be updated.</param>
        /// <param name="model">Permission to be updated.</param>
        /// <returns>Result of updated permission.</returns>
        Permission UpdatePermission(string roleId, string permissionId, Permission model);

        /// <summary>
        /// Delete permission for a specified role.
        /// </summary>
        /// <param name="roleId">Identifier of role to be scoped.</param>
        /// <param name="permissionId">Identifier of permission to be deleted.</param>
        void DeletePermission(string roleId, string permissionId);

        /// <summary>
        /// Searches permissibles for the given filters.
        /// </summary>
        /// <param name="name">Name of permissible to filter.</param>
        /// <param name="page">Page number to find results on.</param>
        /// <param name="pageSize">Number of items to bring back from search.</param>
        /// <returns>Page result for permissibles with applied filter.</returns>
        Task<PageResult<Permissible>> SearchPermissibleAsync(string name = null, int page = 1, int pageSize = 25);

        /// <summary>
        /// Searches permissibles for the given filters.
        /// </summary>
        /// <param name="name">Name of permissible to filter.</param>
        /// <param name="page">Page number to find results on.</param>
        /// <param name="pageSize">Number of items to bring back from search.</param>
        /// <returns>Page result for permissibles with applied filter.</returns>
        PageResult<Permissible> SearchPermissible(string name = null, int page = 1, int pageSize = 25);

        /// <summary>
        /// Add set of users to specified role.
        /// </summary>
        /// <param name="id">Identifier of role.</param>
        /// <param name="model">Users to be added.</param>
        /// <returns>Task indicating when operation is complete.</returns>
        Task AddUsersAsync(string id, UserRoleAdd model);

        /// <summary>
        /// Remove set of users to specified role.
        /// </summary>
        /// <param name="id">Identifier of role.</param>
        /// <param name="model">Users to be removed.</param>
        /// <returns>Task indicating when operation is complete.</returns>
        Task RemoveUsersAsync(string id, UserRoleRemove model);

        /// <summary>
        /// Add set of users to specified role.
        /// </summary>
        /// <param name="id">Identifier of role.</param>
        /// <param name="model">Users to be added.</param>
        void AddUsers(string id, UserRoleAdd model);

        /// <summary>
        /// Remove set of users to specified role.
        /// </summary>
        /// <param name="id">Identifier of role.</param>
        /// <param name="model">Users to be removed.</param>
        void RemoveUsers(string id, UserRoleRemove model);
    }
}
