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
        Task<User> GetLoggedInUserAsync();

        /// <summary>
        /// Gets the user information based on the requesting client
        /// </summary>
        /// <returns>User that made the request</returns>
        User GetLoggedInUser();

        /// <summary>
        /// Gets user based on user id
        /// </summary>
        /// <param name="id">Id of user to retrieve</param>
        /// <returns>User from id requested</returns>
        Task<User> GetUserAsync(string id);

        /// <summary>
        /// Gets user based on user id
        /// </summary>
        /// <param name="id">Id of user to retrieve</param>
        /// <returns>User from id requested</returns>
        User GetUser(string id);

        /// <summary>
        /// Gets users based on criteria provided
        /// </summary>
        /// <param name="nameParts">All parts must be contained in the user with a case insensitive match</param>
        /// <param name="roles">All roles that a user must match to be found</param>
        /// <param name="activeOnly">Only include active users</param>
        /// <param name="page">Page of results to return</param>
        /// <param name="pageSize">Number of results per page to return</param>
        /// <returns><see cref="PageResult{TData}"/> of users found based on provided criteria</returns>
        Task<PageResult<User>> GetUsersAsync(IEnumerable<string> nameParts = null, IEnumerable<string> roles = null, bool activeOnly = true, int page = 1, int pageSize = 200);
        
        /// <summary>
        /// Gets users based on criteria provided
        /// </summary>
        /// <param name="nameParts">All parts must be contained in the user with a case insensitive match</param>
        /// <param name="roles">All roles that a user must match to be found</param>
        /// <param name="activeOnly">Only include active users</param>
        /// <param name="page">Page of results to return</param>
        /// <param name="pageSize">Number of results per page to return</param>
        /// <returns><see cref="PageResult{TData}"/> of users found based on provided criteria</returns>
        PageResult<User> GetUsers(IEnumerable<string> nameParts = null, IEnumerable<string> roles = null, bool activeOnly = true, int page = 1, int pageSize = 200);

        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="user">Updated user data</param>
        /// <returns>User with updated information</returns>
        Task<User> UpdateUserAsync(User user);

        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="user">Updated user data</param>
        /// <returns>User with updated information</returns>
        User UpdateUser(User user);

        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="id">Id of user to update</param>
        /// <param name="user">Updated user data</param>
        /// <returns>User with updated information</returns>
        Task<User> UpdateUserAsync(string id, User user);

        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="id">Id of user to update</param>
        /// <param name="user">Updated user data</param>
        /// <returns>User with updated information</returns>
        User UpdateUser(string id, User user);

        /// <summary>
        /// Deletes user
        /// </summary>
        /// <param name="id">Id of user to delete</param>
        /// <returns>Task to await deletion</returns>
        Task DeleteUserAsync(string id);
        
        /// <summary>
        /// Deletes user
        /// </summary>
        /// <param name="id">Id of user to delete</param>
        void DeleteUser(string id);
    }
}