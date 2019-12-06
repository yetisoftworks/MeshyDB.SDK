// <copyright file="MeshyClient.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using MeshyDB.SDK.Models;
using MeshyDB.SDK.Services;

namespace MeshyDB.SDK
{
    /// <summary>
    /// Handles Meshy Client management.
    /// </summary>
    public static class MeshyClient
    {
        /// <summary>
        /// Gets current authenticated Meshy connection.
        /// </summary>
        public static IMeshyConnection CurrentConnection { get; internal set; }

        /// <summary>
        /// Gets or sets current initialized Meshy client.
        /// </summary>
        private static IMeshyClient CurrentClient { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeshyClient"/> class that is used to communicate with the MeshyDB REST API.
        /// </summary>
        /// <param name="accountName">Name of MeshyDB account required for communication.</param>
        /// <param name="publicKey">Public Api credential supplied from MeshyDB to communicate with tenant.</param>
        /// <param name="httpService">HTTP Service to use for making requests.</param>
        /// <returns>Initialized instance of a Meshy Client.</returns>
        public static IMeshyClient Initialize(string accountName, string publicKey, IHttpService httpService = null)
        {
            CurrentClient = new Services.MeshyClient(accountName, publicKey, httpService);
            return CurrentClient;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeshyClient"/> class that is used to communicate with the MeshyDB REST API.
        /// </summary>
        /// <param name="accountName">Name of MeshyDB account required for communication.</param>
        /// <param name="tenant">Tenant of data used for partitioning.</param>
        /// <param name="publicKey">Public Api credential supplied from MeshyDB to communicate with tenant.</param>
        /// <param name="httpService">HTTP Service to use for making requests.</param>
        /// <returns>Initialized instance of a Meshy Client.</returns>
        public static IMeshyClient InitializeWithTenant(string accountName, string tenant, string publicKey, IHttpService httpService = null)
        {
            CurrentClient = new Services.MeshyClient(accountName, tenant, publicKey, httpService);
            return CurrentClient;
        }

        /// <summary>
        /// Log user in with username and password.
        /// </summary>
        /// <param name="username">Unique identifier of user used during login.</param>
        /// <param name="password">Password of user used during login.</param>
        /// <returns>Meshy client for user upon successful login.</returns>
        public static Task<IMeshyConnection> LoginWithPasswordAsync(string username, string password)
        {
            if (CurrentClient == null)
            {
                throw new InvalidOperationException("Client has not been established. Please initialize before continuing.");
            }

            return CurrentClient.LoginWithPasswordAsync(username, password);
        }

        /// <summary>
        /// Log user in with username and password.
        /// </summary>
        /// <param name="username">Unique identifier of user used during login.</param>
        /// <param name="password">Password of user used during login.</param>
        /// <returns>Meshy client for user upon successful login.</returns>
        public static IMeshyConnection LoginWithPassword(string username, string password)
        {
            if (CurrentClient == null)
            {
                throw new InvalidOperationException("Client has not been established. Please initialize before continuing.");
            }

            return CurrentClient.LoginWithPassword(username, password);
        }

        /// <summary>
        /// Login anonymous user.
        /// </summary>
        /// <param name="username">Unique identifier of user used during login, such as device id.</param>
        /// <returns>Meshy client for user upon successful login.</returns>
        public static Task<IMeshyConnection> LoginAnonymouslyAsync(string username)
        {
            if (CurrentClient == null)
            {
                throw new InvalidOperationException("Client has not been established. Please initialize before continuing.");
            }

            return CurrentClient.LoginAnonymouslyAsync(username);
        }

        /// <summary>
        /// Login anonymous user.
        /// </summary>
        /// <param name="username">Unique identifier of user used during login, such as device id.</param>
        /// <returns>Meshy client for user upon successful login.</returns>
        public static IMeshyConnection LoginAnonymously(string username)
        {
            if (CurrentClient == null)
            {
                throw new InvalidOperationException("Client has not been established. Please initialize before continuing.");
            }

            return CurrentClient.LoginAnonymously(username);
        }

        /// <summary>
        /// Register anonymous user.
        /// </summary>
        /// <param name="username">Specify known username for anonymous user. If username is not provided a random username will be generated.</param>
        /// <returns>New anonymous user.</returns>
        public static Task<User> RegisterAnonymousUserAsync(string username = null)
        {
            if (CurrentClient == null)
            {
                throw new InvalidOperationException("Client has not been established. Please initialize before continuing.");
            }

            return CurrentClient.RegisterAnonymousUserAsync(username);
        }

        /// <summary>
        /// Register anonymous user.
        /// </summary>
        /// <param name="username">Specify known username for anonymous user. If username is not provided a random username will be generated.</param>
        /// <returns>New anonymous user.</returns>
        public static User RegisterAnonymousUser(string username = null)
        {
            if (CurrentClient == null)
            {
                throw new InvalidOperationException("Client has not been established. Please initialize before continuing.");
            }

            return CurrentClient.RegisterAnonymousUser(username);
        }

        /// <summary>
        /// Register new user.
        /// </summary>
        /// <param name="user">User to be created with login credentials.</param>
        /// <returns>If verification is required <see cref="UserVerificationHash"/> will be returned. Otherwise nothing will be.</returns>
        public static Task<UserVerificationHash> RegisterUserAsync(RegisterUser user)
        {
            if (CurrentClient == null)
            {
                throw new InvalidOperationException("Client has not been established. Please initialize before continuing.");
            }

            return CurrentClient.RegisterUserAsync(user);
        }

        /// <summary>
        /// Register new user.
        /// </summary>
        /// <param name="user">User to be created with login credentials.</param>
        /// <returns>If verification is required <see cref="UserVerificationHash"/> will be returned. Otherwise nothing will be.</returns>
        public static UserVerificationHash RegisterUser(RegisterUser user)
        {
            if (CurrentClient == null)
            {
                throw new InvalidOperationException("Client has not been established. Please initialize before continuing.");
            }

            return CurrentClient.RegisterUser(user);
        }

        /// <summary>
        /// Request forgot password based on username.
        /// </summary>
        /// <param name="username">Unique identifier of forgotten user.</param>
        /// <param name="attempt">Forgot password attempt.</param>
        /// <returns>Hashed object to ensure forgot password parity request.</returns>
        public static Task<UserVerificationHash> ForgotPasswordAsync(string username, int attempt = 1)
        {
            if (CurrentClient == null)
            {
                throw new InvalidOperationException("Client has not been established. Please initialize before continuing.");
            }

            return CurrentClient.ForgotPasswordAsync(username, attempt);
        }

        /// <summary>
        /// Request forgot password based on username.
        /// </summary>
        /// <param name="username">Username of forgotten user.</param>
        /// <param name="attempt">Forgot password attempt.</param>
        /// <returns>Hashed object to ensure forgot password parity request.</returns>
        public static UserVerificationHash ForgotPassword(string username, int attempt = 1)
        {
            if (CurrentClient == null)
            {
                throw new InvalidOperationException("Client has not been established. Please initialize before continuing.");
            }

            return CurrentClient.ForgotPassword(username);
        }

        /// <summary>
        /// Resets password for forgot password request.
        /// </summary>
        /// <param name="resetPassword">Reset password object to ensure forgot password was requested.</param>
        /// <returns>Task to await completion of reset.</returns>
        public static Task ResetPasswordAsync(ResetPassword resetPassword)
        {
            if (CurrentClient == null)
            {
                throw new InvalidOperationException("Client has not been established. Please initialize before continuing.");
            }

            return CurrentClient.ResetPasswordAsync(resetPassword);
        }

        /// <summary>
        /// Resets password for forgot password request.
        /// </summary>
        /// <param name="resetPassword">Reset password object to ensure forgot password was requested.</param>
        public static void ResetPassword(ResetPassword resetPassword)
        {
            if (CurrentClient == null)
            {
                throw new InvalidOperationException("Client has not been established. Please initialize before continuing.");
            }

            CurrentClient.ResetPassword(resetPassword);
        }

        /// <summary>
        /// Login with refresh token from another session.
        /// </summary>
        /// <param name="refreshToken">Refresh token of previous session for login.</param>
        /// <returns>Meshy client for user upon successful login.</returns>
        public static Task<IMeshyConnection> LoginWithRefreshTokenAsync(string refreshToken)
        {
            if (CurrentClient == null)
            {
                throw new InvalidOperationException("Client has not been established. Please initialize before continuing.");
            }

            return CurrentClient.LoginWithRefreshTokenAsync(refreshToken);
        }

        /// <summary>
        /// Login with refresh token from another session.
        /// </summary>
        /// <param name="refreshToken">Refresh token of previous session for login.</param>
        /// <returns>Meshy client for user upon successful login.</returns>
        public static IMeshyConnection LoginWithRefreshToken(string refreshToken)
        {
            if (CurrentClient == null)
            {
                throw new InvalidOperationException("Client has not been established. Please initialize before continuing.");
            }

            return CurrentClient.LoginWithRefreshToken(refreshToken);
        }

        /// <summary>
        /// Verify user to allow them to log in.
        /// </summary>
        /// <param name="userVerificationCheck">User verification check object to establish authorization.</param>
        public static void Verify(UserVerificationCheck userVerificationCheck)
        {
            if (CurrentClient == null)
            {
                throw new InvalidOperationException("Client has not been established. Please initialize before continuing.");
            }

            CurrentClient.Verify(userVerificationCheck);
        }

        /// <summary>
        /// Verify user to allow them to log in.
        /// </summary>
        /// <param name="userVerificationCheck">User verification check object to establish authorization.</param>
        /// <returns>Task to await completion of verification.</returns>
        public static Task VerifyAsync(UserVerificationCheck userVerificationCheck)
        {
            if (CurrentClient == null)
            {
                throw new InvalidOperationException("Client has not been established. Please initialize before continuing.");
            }

            return CurrentClient.VerifyAsync(userVerificationCheck);
        }

        /// <summary>
        /// Check user hash to verify user request.
        /// </summary>
        /// <param name="userVerificationCheck">User verification check object to establish authorization.</param>
        /// <returns>Whether or not check was successful.</returns>
        public static Valid CheckHash(UserVerificationCheck userVerificationCheck)
        {
            if (CurrentClient == null)
            {
                throw new InvalidOperationException("Client has not been established. Please initialize before continuing.");
            }

            return CurrentClient.CheckHash(userVerificationCheck);
        }

        /// <summary>
        /// Check user hash to verify user request.
        /// </summary>
        /// <param name="userVerificationCheck">User verification check object to establish authorization.</param>
        /// <returns>Whether or not check was successful.</returns>
        public static Task<Valid> CheckHashAsync(UserVerificationCheck userVerificationCheck)
        {
            if (CurrentClient == null)
            {
                throw new InvalidOperationException("Client has not been established. Please initialize before continuing.");
            }

            return CurrentClient.CheckHashAsync(userVerificationCheck);
        }

        /// <summary>
        /// Check whether a user already exists.
        /// </summary>
        /// <param name="username">Unique identifier of user used during login.</param>
        /// <returns>Whether or not the name is already exists.</returns>
        public static Exist CheckUserExist(string username)
        {
            if (CurrentClient == null)
            {
                throw new InvalidOperationException("Client has not been established. Please initialize before continuing.");
            }

            return CurrentClient.CheckUserExist(username);
        }

        /// <summary>
        /// Check whether a user already exists.
        /// </summary>
        /// <param name="username">Unique identifier of user used during login.</param>
        /// <returns>Whether or not the name is already exists.</returns>
        public static Task<Exist> CheckUserExistAsync(string username)
        {
            if (CurrentClient == null)
            {
                throw new InvalidOperationException("Client has not been established. Please initialize before continuing.");
            }

            return CurrentClient.CheckUserExistAsync(username);
        }
    }
}
