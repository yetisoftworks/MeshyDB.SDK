// <copyright file="IUsersService.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MeshyDB.SDK.Models;

namespace MeshyDB.SDK.Services
{
    /// <summary>
    /// Defines methods for users.
    /// </summary>
    public interface IUsersService
    {
        /// <summary>
        /// Gets the user information based on the requesting client.
        /// </summary>
        /// <returns>User that made the request.</returns>
        Task<User> GetSelfAsync();

        /// <summary>
        /// Gets the user information based on the requesting client.
        /// </summary>
        /// <returns>User that made the request.</returns>
        User GetSelf();

        /// <summary>
        /// Updates self.
        /// </summary>
        /// <param name="user">Updated user data.</param>
        /// <returns>User with updated information.</returns>
        Task<User> UpdateSelfAsync(User user);

        /// <summary>
        /// Updates self.
        /// </summary>
        /// <param name="user">Updated user data.</param>
        /// <returns>User with updated information.</returns>
        User UpdateSelf(User user);

        /// <summary>
        /// Replaces users security questions with questions provided.
        /// </summary>
        /// <param name="userSecurityQuestionUpdate">Set of data to be updated.</param>
        /// <returns>Task defining whether action was successful.</returns>
        Task UpdateSecurityQuestionsAsync(UserSecurityQuestionUpdate userSecurityQuestionUpdate);

        /// <summary>
        /// Replaces users security questions with questions provided.
        /// </summary>
        /// <param name="userSecurityQuestionUpdate">Set of data to be updated.</param>
        void UpdateSecurityQuestions(UserSecurityQuestionUpdate userSecurityQuestionUpdate);

        /// <summary>
        /// Get user for a given id.
        /// </summary>
        /// <param name="id">Identifier of user to be retrieved.</param>
        /// <returns>User result of request.</returns>
        Task<User> GetAsync(string id);

        /// <summary>
        /// Searches users for a given filter.
        /// </summary>
        /// <param name="name">Contains search of name case-insensitive.</param>
        /// <param name="roleId">Has role assigned.</param>
        /// <param name="orderBy">Order data in Mongo DB sort format.</param>
        /// <param name="activeOnly">Filter users that are active.</param>
        /// <param name="page">Page number to find results on.</param>
        /// <param name="pageSize">Number of items to bring back from search.</param>
        /// <returns>Page result for the given users with applied filter.</returns>
        Task<PageResult<User>> SearchAsync(string name = null, string roleId = null, string orderBy = null, bool activeOnly = true, int page = 1, int pageSize = 25);

        /// <summary>
        /// Searches users for a given filter.
        /// </summary>
        /// <param name="name">Contains search of name case-insensitive.</param>
        /// <param name="roleId">Has role assigned.</param>
        /// <param name="orderBy">Defines order for user.</param>
        /// <param name="activeOnly">Filter users that are active.</param>
        /// <param name="page">Page number to find results on.</param>
        /// <param name="pageSize">Number of items to bring back from search.</param>
        /// <returns>Page result for the given users with applied filter.</returns>
        Task<PageResult<User>> SearchAsync(string name = null, string roleId = null, OrderByDefinition<User> orderBy = null, bool activeOnly = true, int page = 1, int pageSize = 25);

        /// <summary>
        /// Replaces users security questions with questions provided.
        /// </summary>
        /// <param name="id">Identifier of user to be updated.</param>
        /// <param name="userSecurityQuestionUpdate">Set of data to be updated.</param>
        /// <returns>Task defining whether action was successful.</returns>
        Task UpdateSecurityQuestionsAsync(string id, UserSecurityQuestionUpdate userSecurityQuestionUpdate);

        /// <summary>
        /// Create user.
        /// </summary>
        /// <param name="model">User to be committed.</param>
        /// <returns>Result of committed user.</returns>
        Task<User> CreateAsync(NewUser model);

        /// <summary>
        /// Update user.
        /// </summary>
        /// <param name="id">Identifier of user to be updated.</param>
        /// <param name="model">User to be updated.</param>
        /// <returns>Result of updated user.</returns>
        Task<User> UpdateAsync(string id, User model);

        /// <summary>
        /// Delete user for a given id.
        /// </summary>
        /// <param name="id">Identifier of user to be deleted.</param>
        /// <returns>Task indicating when operation is complete.</returns>
        Task DeleteAsync(string id);

        /// <summary>
        /// Get user for a given id.
        /// </summary>
        /// <param name="id">Identifier of user to be retrieved.</param>
        /// <returns>User result of request.</returns>
        User Get(string id);

        /// <summary>
        /// Searches users for a given filter.
        /// </summary>
        /// <param name="name">Contains search of name case-insensitive.</param>
        /// <param name="roleId">Has role assigned.</param>
        /// <param name="orderBy">Order data in Mongo DB sort format.</param>
        /// <param name="activeOnly">Filter users that are active.</param>
        /// <param name="page">Page number to find results on.</param>
        /// <param name="pageSize">Number of items to bring back from search.</param>
        /// <returns>Page result for the given users with applied filter.</returns>
        PageResult<User> Search(string name = null, string roleId = null, string orderBy = null, bool activeOnly = true, int page = 1, int pageSize = 25);

        /// <summary>
        /// Searches users for a given filter.
        /// </summary>
        /// <param name="name">Contains search of name case-insensitive.</param>
        /// <param name="roleId">Has role assigned.</param>
        /// <param name="orderBy">Defines order for user.</param>
        /// <param name="activeOnly">Filter users that are active.</param>
        /// <param name="page">Page number to find results on.</param>
        /// <param name="pageSize">Number of items to bring back from search.</param>
        /// <returns>Page result for the given users with applied filter.</returns>
        PageResult<User> Search(string name = null, string roleId = null, OrderByDefinition<User> orderBy = null, bool activeOnly = true, int page = 1, int pageSize = 25);

        /// <summary>
        /// Replaces users security questions with questions provided.
        /// </summary>
        /// <param name="id">Identifier of user to be updated.</param>
        /// <param name="userSecurityQuestionUpdate">Set of data to be updated.</param>
        void UpdateSecurityQuestions(string id, UserSecurityQuestionUpdate userSecurityQuestionUpdate);

        /// <summary>
        /// Create user.
        /// </summary>
        /// <param name="model">User to be committed.</param>
        /// <returns>Result of committed user.</returns>
        User Create(NewUser model);

        /// <summary>
        /// Update user.
        /// </summary>
        /// <param name="id">Identifier of user to be updated.</param>
        /// <param name="model">User to be updated.</param>
        /// <returns>Result of updated user.</returns>
        User Update(string id, User model);

        /// <summary>
        /// Delete user for a given id.
        /// </summary>
        /// <param name="id">Identifier of user to be deleted.</param>
        void Delete(string id);
    }
}