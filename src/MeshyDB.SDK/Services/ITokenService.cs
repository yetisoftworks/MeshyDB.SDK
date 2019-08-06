// <copyright file="ITokenService.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeshyDB.SDK.Services
{
    /// <summary>
    /// Defines methods for authentication tokens.
    /// </summary>
    internal interface ITokenService
    {
        /// <summary>
        /// Generates Access token for provided user name and password.
        /// </summary>
        /// <param name="username">Unique identifier of user to generate token for.</param>
        /// <param name="password">Password to generate token with.</param>
        /// <returns>Authentication id upon success.</returns>
        /// <remarks>Generates random authentication id for login.</remarks>
        Task<string> GenerateAccessToken(string username, string password);

        /// <summary>
        /// Generates User token for provided user name and password.
        /// </summary>
        /// <param name="username">Unique identifier of user to generate token for.</param>
        /// <param name="password">Password to generate token with.</param>
        /// <param name="authenticationId">Defined authentication id supplied.</param>
        /// <returns>Authentication id upon success.</returns>
        Task<string> GenerateAccessToken(string username, string password, string authenticationId);

        /// <summary>
        /// Gets access token for specified authentication id.
        /// </summary>
        /// <param name="authenticationId">Authentication id to locate access token for.</param>
        /// <returns>Access token for given authentication id.</returns>
        Task<string> GetAccessTokenAsync(string authenticationId);

        /// <summary>
        /// Gets refresh token for specified authentication id.
        /// </summary>
        /// <param name="authenticationId">Authentication id to locate refresh token for.</param>
        /// <returns>Refresh token for given authentication id.</returns>
        Task<string> GetRefreshTokenAsync(string authenticationId);

        /// <summary>
        /// Generate user token for provided refresh token.
        /// </summary>
        /// <param name="refreshToken">Refresh token to generate new credentials.</param>
        /// <returns>Authentication id upon success.</returns>
        /// <remarks>Generates random authentication id for login.</remarks>
        Task<string> GenerateAccessTokenWithRefreshToken(string refreshToken);

        /// <summary>
        /// Generate user token for provided refresh token.
        /// </summary>
        /// <param name="refreshToken">Refresh token to generate new credentials.</param>
        /// <param name="authenticationId">Defined authentication id supplied.</param>
        /// <returns>Authentication id upon success.</returns>
        Task<string> GenerateAccessTokenWithRefreshToken(string refreshToken, string authenticationId);

        /// <summary>
        /// Sign out of application.
        /// </summary>
        /// <param name="authenticationId">Authentication id to sign out.</param>
        /// <returns>Task indicating when operation is complete.</returns>
        Task SignoutAsync(string authenticationId);
    }
}
