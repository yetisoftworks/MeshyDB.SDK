using MeshyDB.SDK.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MeshyDB.SDK.Services
{
    public interface IUsersService
    {
        /// <summary>
        /// Gets the user information based on the requesting client
        /// </summary>
        /// <returns>User that made the request</returns>
        Task<User> GetSelfAsync();

        /// <summary>
        /// Gets the user information based on the requesting client
        /// </summary>
        /// <returns>User that made the request</returns>
        User GetSelf();

        /// <summary>
        /// Updates self
        /// </summary>
        /// <param name="user">Updated user data</param>
        /// <returns>User with updated information</returns>
        Task<User> UpdateSelfAsync(User user);

        /// <summary>
        /// Updates self
        /// </summary>
        /// <param name="user">Updated user data</param>
        /// <returns>User with updated information</returns>
        User UpdateSelf(User user);
    }
}