// <copyright file="MeshyDB.cs" company="Yetisoftworks LLC">
// Copyright (c) Yetisoftworks LLC. All rights reserved.
// </copyright>

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MeshyDB.SDK.Models;
using MeshyDB.SDK.Services;

[assembly: InternalsVisibleTo("MeshyDB.SDK.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace MeshyDB.SDK
{
    /// <summary>
    /// MeshyDB Client is used to connect with the MeshyDB REST API.
    /// </summary>
    public class MeshyDB
    {
        private readonly string publicKey;
        private IHttpService httpService;
        private IAuthenticationService authenticationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeshyDB"/> class that is used to communicate with the MeshyDB REST API.
        /// </summary>
        /// <param name="accountName">Name of MeshyDB account required for communication.</param>
        /// <param name="publicKey">Public Api credential supplied from MeshyDB to communicate with tenant.</param>
        /// <param name="httpService">Http Service to use for making requests.</param>
        /// <exception cref="ArgumentException">Thrown if any parameter is not configured.</exception>
        public MeshyDB(string accountName, string publicKey, IHttpService httpService = null)
            : this(accountName, null, publicKey, httpService)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeshyDB"/> class that is used to communicate with the MeshyDB REST API.
        /// </summary>
        /// <param name="accountName">Name of MeshyDB account required for communication.</param>
        /// <param name="tenant">Tenant of data used for partitioning.</param>
        /// <param name="publicKey">Public Api credential supplied from MeshyDB to communicate with tenant.</param>
        /// <param name="httpService">Http Service to use for making requests.</param>
        /// <exception cref="ArgumentException">Thrown if any parameter is not configured.</exception>
        public MeshyDB(string accountName, string tenant, string publicKey, IHttpService httpService = null)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException($"{nameof(accountName)} was not supplied", nameof(accountName));
            }

            if (string.IsNullOrWhiteSpace(publicKey))
            {
                throw new ArgumentException($"{nameof(publicKey)} was not supplied", nameof(publicKey));
            }

            this.AccountName = accountName.Trim();
            this.Tenant = tenant?.Trim();
            this.publicKey = publicKey.Trim();

            this.HttpService = httpService ?? new HttpService();
        }

        /// <summary>
        /// Gets or sets Http Service used for making web request calls.
        /// </summary>
        internal IHttpService HttpService
        {
            get
            {
                return this.httpService;
            }

            set
            {
                if (value != null)
                {
                    this.httpService = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets Authentication Service.
        /// </summary>
        internal IAuthenticationService AuthenticationService
        {
            get
            {
                if (this.authenticationService == null)
                {
                    var authRequestService = new RequestService(this.HttpService, this.GetAuthUrl(), this.Tenant);
                    var tokenService = new TokenService(authRequestService, this.publicKey);
                    var apiRequestService = new RequestService(this.HttpService, this.GetApiUrl(), this.Tenant);

                    this.authenticationService = new AuthenticationService(tokenService, apiRequestService);
                }

                return this.authenticationService;
            }

            set
            {
                this.authenticationService = value;
            }
        }

        /// <summary>
        /// Gets the name of the client key for MeshyDB communication.
        /// </summary>
        internal string AccountName { get; }

        /// <summary>
        /// Gets the name of the tenant for MeshyDB communication.
        /// </summary>
        internal string Tenant { get; }

        /// <summary>
        /// Log user in with username and password.
        /// </summary>
        /// <param name="username">User name of user to login.</param>
        /// <param name="password">Password of user to log in.</param>
        /// <returns>Meshy client for user upon successful login.</returns>
        public async Task<IMeshyClient> LoginWithPasswordAsync(string username, string password)
        {
            var identifier = await this.AuthenticationService.LoginWithPasswordAsync(username, password);
            var services = this.GenerateAPIRequestService(identifier);

            return new MeshyClient(services.Item1, services.Item2, identifier);
        }

        /// <summary>
        /// Log user in with username and password.
        /// </summary>
        /// <param name="username">User name of user to login.</param>
        /// <param name="password">Password of user to log in.</param>
        /// <returns>Meshy client for user upon successful login.</returns>
        public IMeshyClient LoginWithPassword(string username, string password)
        {
            var t = this.LoginWithPasswordAsync(username, password).ConfigureAwait(true).GetAwaiter();

            return t.GetResult();
        }

        /// <summary>
        /// Login anonymously with randomly generated user.
        /// </summary>
        /// <param name="username">Identify anonymous user. If nothing is provided it will be automatically generated.</param>
        /// <returns>Meshy client for user upon successful login.</returns>
        public async Task<IMeshyClient> LoginAnonymouslyAsync(string username = null)
        {
            var identifier = await this.AuthenticationService.LoginAnonymouslyAsync(username);
            var services = this.GenerateAPIRequestService(identifier);

            return new MeshyClient(services.Item1, services.Item2, identifier);
        }

        /// <summary>
        /// Login anonymously with randomly generated user.
        /// </summary>
        /// <param name="username">Identify anonymous user. If nothing is provided it will be automatically generated.</param>
        /// <returns>Meshy client for user upon successful login.</returns>
        public IMeshyClient LoginAnonymously(string username = null)
        {
            var t = this.LoginAnonymouslyAsync(username).ConfigureAwait(true).GetAwaiter();

            return t.GetResult();
        }

        /// <summary>
        /// Register new user.
        /// </summary>
        /// <param name="user">User to be created with login credentials.</param>
        /// <returns>User that was newly created.</returns>
        public async Task<UserVerificationHash> RegisterUserAsync(RegisterUser user)
        {
            if (string.IsNullOrWhiteSpace(user.Username))
            {
                throw new ArgumentException($"Missing required information: {nameof(user.Username)}");
            }

            if (string.IsNullOrWhiteSpace(user.NewPassword))
            {
                throw new ArgumentException($"Missing required information: {nameof(user.Username)}");
            }

            if (string.IsNullOrWhiteSpace(user.PhoneNumber))
            {
                throw new ArgumentException($"Missing required information: {nameof(user.PhoneNumber)}");
            }

            return await this.AuthenticationService.RegisterAsync(user);
        }

        /// <summary>
        /// Register new user.
        /// </summary>
        /// <param name="user">User to be created with login credentials.</param>
        /// <returns>User that was newly created.</returns>
        public UserVerificationHash RegisterUser(RegisterUser user)
        {
            var t = this.RegisterUserAsync(user).ConfigureAwait(true).GetAwaiter();

            return t.GetResult();
        }

        /// <summary>
        /// Request forgot password based on username.
        /// </summary>
        /// <param name="username">Username of forgotton user.</param>
        /// <returns>Hashed object to ensure forgot password parity request.</returns>
        public async Task<UserVerificationHash> ForgotPasswordAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException($"Missing required information: {nameof(username)}");
            }

            return await this.AuthenticationService.ForgotPasswordAsync(username);
        }

        /// <summary>
        /// Request forgot password based on username.
        /// </summary>
        /// <param name="username">Username of forgotton user.</param>
        /// <returns>Hashed object to ensure forgot password parity request.</returns>
        public UserVerificationHash ForgotPassword(string username)
        {
            var t = this.ForgotPasswordAsync(username).ConfigureAwait(true).GetAwaiter();

            return t.GetResult();
        }

        /// <summary>
        /// Resets password for forgot password request.
        /// </summary>
        /// <param name="resetPassword">Reset password object to ensure forgot password was requested.</param>
        /// <returns>Task to await success of reset.</returns>
        public async Task ResetPasswordAsync(ResetPassword resetPassword)
        {
            if (string.IsNullOrWhiteSpace(resetPassword.Username))
            {
                throw new ArgumentException($"Missing required information: {nameof(resetPassword.Username)}");
            }

            if (string.IsNullOrWhiteSpace(resetPassword.Hash))
            {
                throw new ArgumentException($"Missing required information: {nameof(resetPassword.Hash)}");
            }

            if (string.IsNullOrWhiteSpace(resetPassword.NewPassword))
            {
                throw new ArgumentException($"Missing required information: {nameof(resetPassword.NewPassword)}");
            }

            await this.AuthenticationService.ResetPasswordAsync(resetPassword);
        }

        /// <summary>
        /// Resets password for forgot password request.
        /// </summary>
        /// <param name="resetPassword">Reset password object to ensure forgot password was requested.</param>
        public void ResetPassword(ResetPassword resetPassword)
        {
            var t = this.ResetPasswordAsync(resetPassword).ConfigureAwait(true).GetAwaiter();

            t.GetResult();
        }

        /// <summary>
        /// Login with peristance token from another session.
        /// </summary>
        /// <param name="persistanceToken">Persistance token of previous session for login.</param>
        /// <returns>Meshy client for user upon successful login.</returns>
        public async Task<IMeshyClient> LoginWithPersistanceAsync(string persistanceToken)
        {
            var identifier = await this.AuthenticationService.LoginWithPersistanceAsync(persistanceToken);
            var services = this.GenerateAPIRequestService(identifier);

            return new MeshyClient(services.Item1, services.Item2, identifier);
        }

        /// <summary>
        /// Login with peristance token from another session.
        /// </summary>
        /// <param name="persistanceToken">Persistance token of previous session for login.</param>
        /// <returns>Meshy client for user upon successful login.</returns>
        public IMeshyClient LoginWithPersistance(string persistanceToken)
        {
            var t = this.LoginWithPersistanceAsync(persistanceToken).ConfigureAwait(true).GetAwaiter();

            return t.GetResult();
        }

        /// <summary>
        /// Verify user to allow them to log in.
        /// </summary>
        /// <param name="userVerificationCheck">User verification check object to establish authorization.</param>
        public void Verify(UserVerificationCheck userVerificationCheck)
        {
            var t = this.VerifyAsync(userVerificationCheck).ConfigureAwait(true).GetAwaiter();

            t.GetResult();
        }

        /// <summary>
        /// Verify user to allow them to log in.
        /// </summary>
        /// <param name="userVerificationCheck">User verification check object to establish authorization.</param>
        /// <returns>Task to await success of sign out.</returns>
        public async Task VerifyAsync(UserVerificationCheck userVerificationCheck)
        {
            if (string.IsNullOrWhiteSpace(userVerificationCheck.Username))
            {
                throw new ArgumentException($"Missing required information: {nameof(userVerificationCheck.Username)}");
            }

            if (string.IsNullOrWhiteSpace(userVerificationCheck.Hash))
            {
                throw new ArgumentException($"Missing required information: {nameof(userVerificationCheck.Hash)}");
            }

            await this.AuthenticationService.VerifyAsync(userVerificationCheck);
        }

        /// <summary>
        /// Check user hash to verify user request.
        /// </summary>
        /// <param name="userVerificationCheck">User verification check object to establish authorization.</param>
        /// <returns>Whether or not check was successful.</returns>
        public bool CheckHash(UserVerificationCheck userVerificationCheck)
        {
            var t = this.CheckHashAsync(userVerificationCheck).ConfigureAwait(true).GetAwaiter();

            return t.GetResult();
        }

        /// <summary>
        /// Check user hash to verify user request.
        /// </summary>
        /// <param name="userVerificationCheck">User verification check object to establish authorization.</param>
        /// <returns>Whether or not check was successful.</returns>
        public async Task<bool> CheckHashAsync(UserVerificationCheck userVerificationCheck)
        {
            if (string.IsNullOrWhiteSpace(userVerificationCheck.Username))
            {
                throw new ArgumentException($"Missing required information: {nameof(userVerificationCheck.Username)}");
            }

            if (string.IsNullOrWhiteSpace(userVerificationCheck.Hash))
            {
                throw new ArgumentException($"Missing required information: {nameof(userVerificationCheck.Hash)}");
            }

            return await this.AuthenticationService.CheckHashAsync(userVerificationCheck);
        }

        /// <summary>
        /// Gets the Api Url configured for the supplied account.
        /// </summary>
        /// <returns>The configured account Api Url communication.</returns>
        internal string GetApiUrl()
        {
            return Constants.TemplateApiUrl.Replace("{accountName}", this.AccountName);
        }

        /// <summary>
        /// Gets the Auth Url configured for the supplied account.
        /// </summary>
        /// <returns>The configured account Auth Url communication.</returns>
        internal string GetAuthUrl()
        {
            return Constants.TemplateAuthUrl.Replace("{accountName}", this.AccountName);
        }

        /// <summary>
        /// Instantiates services used for api communication with required dependencies.
        /// </summary>
        private Tuple<ITokenService, IRequestService> GenerateAPIRequestService(string identifier)
        {
            var httpService = this.HttpService;
            var authRequestService = new RequestService(httpService, this.GetAuthUrl(), this.Tenant);
            var tokenService = new TokenService(authRequestService, this.publicKey);
            var requestService = new RequestService(httpService, this.GetApiUrl(), this.Tenant, tokenService, identifier);

            return new Tuple<ITokenService, IRequestService>(tokenService, requestService);
        }
    }
}
