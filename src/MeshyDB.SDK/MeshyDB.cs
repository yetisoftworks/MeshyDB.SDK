// <copyright file="MeshyDB.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
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
            var identifier = await this.AuthenticationService.LoginWithPasswordAsync(username, password).ConfigureAwait(true);
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
            var identifier = await this.AuthenticationService.LoginAnonymouslyAsync(username).ConfigureAwait(true);
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
        public Task<UserVerificationHash> RegisterUserAsync(RegisterUser user)
        {
            return this.AuthenticationService.RegisterAsync(user);
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
        /// <param name="attempt">Forgot password attempt.</param>
        /// <returns>Hashed object to ensure forgot password parity request.</returns>
        public Task<UserVerificationHash> ForgotPasswordAsync(string username, int attempt = 1)
        {
            return this.AuthenticationService.ForgotPasswordAsync(username, attempt);
        }

        /// <summary>
        /// Request forgot password based on username.
        /// </summary>
        /// <param name="username">Username of forgotton user.</param>
        /// <param name="attempt">Forgot password attempt.</param>
        /// <returns>Hashed object to ensure forgot password parity request.</returns>
        public UserVerificationHash ForgotPassword(string username, int attempt = 1)
        {
            var t = this.ForgotPasswordAsync(username).ConfigureAwait(true).GetAwaiter();

            return t.GetResult();
        }

        /// <summary>
        /// Resets password for forgot password request.
        /// </summary>
        /// <param name="resetPassword">Reset password object to ensure forgot password was requested.</param>
        /// <returns>Task to await success of reset.</returns>
        public Task ResetPasswordAsync(ResetPassword resetPassword)
        {
            return this.AuthenticationService.ResetPasswordAsync(resetPassword);
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
            var identifier = await this.AuthenticationService.LoginWithPersistanceAsync(persistanceToken).ConfigureAwait(true);
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
        public Task VerifyAsync(UserVerificationCheck userVerificationCheck)
        {
            return this.AuthenticationService.VerifyAsync(userVerificationCheck);
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
        public Task<bool> CheckHashAsync(UserVerificationCheck userVerificationCheck)
        {
            return this.AuthenticationService.CheckHashAsync(userVerificationCheck);
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
