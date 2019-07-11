// <copyright file="IMeshyClient.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using MeshyDB.SDK.Models;

namespace MeshyDB.SDK
{
    public interface IMeshyClient
    {
        /// <summary>
        /// Log user in with username and password.
        /// </summary>
        /// <param name="username">User name of user used during login.</param>
        /// <param name="password">Password of user used during login.</param>
        /// <returns>Meshy client for user upon successful login.</returns>
        Task<IMeshyConnection> LoginWithPasswordAsync(string username, string password);

        /// <summary>
        /// Log user in with username and password.
        /// </summary>
        /// <param name="username">User name of user used during login.</param>
        /// <param name="password">Password of user used during login.</param>
        /// <returns>Meshy client for user upon successful login.</returns>
        IMeshyConnection LoginWithPassword(string username, string password);

        /// <summary>
        /// Login anonymous user.
        /// </summary>
        /// <param name="username">User name of user used during login.</param>
        /// <returns>Meshy client for user upon successful login.</returns>
        Task<IMeshyConnection> LoginAnonymouslyAsync(string username);

        /// <summary>
        /// Login anonymous user.
        /// </summary>
        /// <param name="username">User name of user used during login.</param>
        /// <returns>Meshy client for user upon successful login.</returns>
        IMeshyConnection LoginAnonymously(string username);

        /// <summary>
        /// Register anonymous user.
        /// </summary>
        /// <param name="username">Specify known username for anonymous user. If username is not provided a random username will be generated.</param>
        /// <returns>New anonymous user.</returns>
        Task<User> RegisterAnonymousUserAsync(string username = null);

        /// <summary>
        /// Register anonymous user.
        /// </summary>
        /// <param name="username">Specify known username for anonymous user. If username is not provided a random username will be generated.</param>
        /// <returns>New anonymous user.</returns>
        User RegisterAnonymousUser(string username = null);

        /// <summary>
        /// Register new user.
        /// </summary>
        /// <param name="user">User to be created with login credentials.</param>
        /// <returns>If verification is required <see cref="UserVerificationHash"/> will be returned. Otherwise nothing will be.</returns>
        Task<UserVerificationHash> RegisterUserAsync(RegisterUser user);

        /// <summary>
        /// Register new user.
        /// </summary>
        /// <param name="user">User to be created with login credentials.</param>
        /// <returns>If verification is required <see cref="UserVerificationHash"/> will be returned. Otherwise nothing will be.</returns>
        UserVerificationHash RegisterUser(RegisterUser user);

        /// <summary>
        /// Request forgot password based on username.
        /// </summary>
        /// <param name="username">Username of forgotton user.</param>
        /// <param name="attempt">Forgot password attempt.</param>
        /// <returns>Hashed object to ensure forgot password parity request.</returns>
        Task<UserVerificationHash> ForgotPasswordAsync(string username, int attempt = 1);

        /// <summary>
        /// Request forgot password based on username.
        /// </summary>
        /// <param name="username">Username of forgotton user.</param>
        /// <param name="attempt">Forgot password attempt.</param>
        /// <returns>Hashed object to ensure forgot password parity request.</returns>
        UserVerificationHash ForgotPassword(string username, int attempt = 1);

        /// <summary>
        /// Resets password for forgot password request.
        /// </summary>
        /// <param name="resetPassword">Reset password object to ensure forgot password was requested.</param>
        /// <returns>Task to await completion of reset.</returns>
        Task ResetPasswordAsync(ResetPassword resetPassword);

        /// <summary>
        /// Resets password for forgot password request.
        /// </summary>
        /// <param name="resetPassword">Reset password object to ensure forgot password was requested.</param>
        void ResetPassword(ResetPassword resetPassword);

        /// <summary>
        /// Login with peristance token from another session.
        /// </summary>
        /// <param name="persistanceToken">Persistance token of previous session for login.</param>
        /// <returns>Meshy client for user upon successful login.</returns>
        Task<IMeshyConnection> LoginWithPersistanceAsync(string persistanceToken);

        /// <summary>
        /// Login with peristance token from another session.
        /// </summary>
        /// <param name="persistanceToken">Persistance token of previous session for login.</param>
        /// <returns>Meshy client for user upon successful login.</returns>
        IMeshyConnection LoginWithPersistance(string persistanceToken);

        /// <summary>
        /// Verify user to allow them to log in.
        /// </summary>
        /// <param name="userVerificationCheck">User verification check object to establish authorization.</param>
        void Verify(UserVerificationCheck userVerificationCheck);

        /// <summary>
        /// Verify user to allow them to log in.
        /// </summary>
        /// <param name="userVerificationCheck">User verification check object to establish authorization.</param>
        /// <returns>Task to await completion of verification.</returns>
        Task VerifyAsync(UserVerificationCheck userVerificationCheck);

        /// <summary>
        /// Check user hash to verify user request.
        /// </summary>
        /// <param name="userVerificationCheck">User verification check object to establish authorization.</param>
        /// <returns>Whether or not check was successful.</returns>
        bool CheckHash(UserVerificationCheck userVerificationCheck);

        /// <summary>
        /// Check user hash to verify user request.
        /// </summary>
        /// <param name="userVerificationCheck">User verification check object to establish authorization.</param>
        /// <returns>Whether or not check was successful.</returns>
        Task<bool> CheckHashAsync(UserVerificationCheck userVerificationCheck);
    }
}
