// <copyright file="IMeshyClient.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MeshyDB.SDK.Services;

namespace MeshyDB.SDK
{
    /// <summary>
    /// Represents Meshy Client.
    /// </summary>
    public interface IMeshyClient
    {
        /// <summary>
        /// Gets service to interact with Meshes for the logged in user.
        /// </summary>
        IMeshesService Meshes { get; }

        /// <summary>
        /// Gets service to interact with Users for the logged in user.
        /// </summary>
        IUsersService Users { get; }

        /// <summary>
        /// Updates password for logged in user.
        /// </summary>
        /// <param name="previousPassword">Previous password of user to change.</param>
        /// <param name="newPassword">New password of user to log in with next.</param>
        /// <returns>Task to await password success.</returns>
        Task UpdatePasswordAsync(string previousPassword, string newPassword);

        /// <summary>
        /// Updates password for logged in user.
        /// </summary>
        /// <param name="previousPassword">Previous password of user to change.</param>
        /// <param name="newPassword">New password of user to log in with next.</param>
        void UpdatePassword(string previousPassword, string newPassword);

        /// <summary>
        /// Sign out currently logged in user.
        /// </summary>
        /// <returns>Task to await success of sign out.</returns>
        Task SignoutAsync();

        /// <summary>
        /// Sign out currently logged in user.
        /// </summary>
        void Signout();

        /// <summary>
        /// Retrieves persistance token of logged in user to refresh their session at a later time.
        /// </summary>
        /// <returns>Persistance token to be used for a later login.</returns>
        Task<string> RetrievePersistanceTokenAsync();

        /// <summary>
        /// Retrieves persistance token of logged in user to refresh their session at a later time.
        /// </summary>
        /// <returns>Persistance token to be used for a later login.</returns>
        string RetrievePersistanceToken();

        /// <summary>
        /// Gets my user information.
        /// </summary>
        /// <returns>Dictionary of user claim info.</returns>
        IDictionary<string, string> GetMyUserInfo();

        /// <summary>
        /// Gets my user information.
        /// </summary>
        /// <returns>Dictionary of user claim info.</returns>
        Task<IDictionary<string, string>> GetMyUserInfoAsync();
    }
}
