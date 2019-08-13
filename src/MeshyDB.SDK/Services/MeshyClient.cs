// <copyright file="MeshyClient.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MeshyDB.SDK.Models;
using MeshyDB.SDK.Resolvers;
using MeshyDB.SDK.Services;

[assembly: InternalsVisibleTo("MeshyDB.SDK.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace MeshyDB.SDK.Services
{
    /// <summary>
    /// Meshy Client is used to connect with the MeshyDB REST API.
    /// </summary>
    internal class MeshyClient : IMeshyClient
    {
        private readonly string publicKey;
        private IHttpService httpService;
        private IAuthenticationService authenticationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeshyClient"/> class that is used to communicate with the MeshyDB REST API.
        /// </summary>
        /// <param name="accountName">Name of MeshyDB account required for communication.</param>
        /// <param name="publicKey">Public Api credential supplied from MeshyDB to communicate with tenant.</param>
        /// <param name="httpService">HTTP Service to use for making requests.</param>
        public MeshyClient(string accountName, string publicKey, IHttpService httpService = null)
            : this(accountName, null, publicKey, httpService)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeshyClient"/> class that is used to communicate with the MeshyDB REST API.
        /// </summary>
        /// <param name="accountName">Name of MeshyDB account required for communication.</param>
        /// <param name="tenant">Tenant of data used for partitioning.</param>
        /// <param name="publicKey">Public Api credential supplied from MeshyDB to communicate with tenant.</param>
        /// <param name="httpService">HTTP Service to use for making requests.</param>
        public MeshyClient(string accountName, string tenant, string publicKey, IHttpService httpService = null)
        {
            this.AccountName = accountName.Trim();
            this.Tenant = tenant?.Trim();
            this.publicKey = publicKey.Trim();

            this.HttpService = httpService ?? new HttpService();
            CustomDateTimeOffsetSerializer.AddSeralizer();
        }

        /// <summary>
        /// Gets or sets HTTP Service used for making web request calls.
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

        /// <inheritdoc/>
        public async Task<IMeshyConnection> LoginWithPasswordAsync(string username, string password)
        {
            if (SDK.MeshyClient.CurrentConnection != null)
            {
                throw new InvalidOperationException("Connection has already been established. Please sign out before switching");
            }

            var identifier = await this.AuthenticationService.LoginWithPasswordAsync(username, password).ConfigureAwait(true);
            var services = this.GenerateAPIRequestService(identifier);

            SDK.MeshyClient.CurrentConnection = new MeshyConnection(services.Item1, services.Item2, identifier);

            return SDK.MeshyClient.CurrentConnection;
        }

        /// <inheritdoc/>
        public IMeshyConnection LoginWithPassword(string username, string password)
        {
            var t = this.LoginWithPasswordAsync(username, password).ConfigureAwait(true).GetAwaiter();

            return t.GetResult();
        }

        /// <inheritdoc/>
        public async Task<IMeshyConnection> LoginAnonymouslyAsync(string username)
        {
            if (SDK.MeshyClient.CurrentConnection != null)
            {
                throw new InvalidOperationException("Connection has already been established. Please sign out before switching");
            }

            var identifier = await this.AuthenticationService.LoginAnonymouslyAsync(username).ConfigureAwait(true);
            var services = this.GenerateAPIRequestService(identifier);

            SDK.MeshyClient.CurrentConnection = new MeshyConnection(services.Item1, services.Item2, identifier);

            return SDK.MeshyClient.CurrentConnection;
        }

        /// <inheritdoc/>
        public IMeshyConnection LoginAnonymously(string username)
        {
            var t = this.LoginAnonymouslyAsync(username).ConfigureAwait(true).GetAwaiter();

            return t.GetResult();
        }

        /// <inheritdoc/>
        public Task<User> RegisterAnonymousUserAsync(string username = null)
        {
            return this.AuthenticationService.RegisterAnonymousUserAsync(username);
        }

        /// <inheritdoc/>
        public User RegisterAnonymousUser(string username = null)
        {
            var t = this.RegisterAnonymousUserAsync(username).ConfigureAwait(true).GetAwaiter();

            return t.GetResult();
        }

        /// <inheritdoc/>
        public Task<UserVerificationHash> RegisterUserAsync(RegisterUser user)
        {
            return this.AuthenticationService.RegisterAsync(user);
        }

        /// <inheritdoc/>
        public UserVerificationHash RegisterUser(RegisterUser user)
        {
            var t = this.RegisterUserAsync(user).ConfigureAwait(true).GetAwaiter();

            return t.GetResult();
        }

        /// <inheritdoc/>
        public Task<UserVerificationHash> ForgotPasswordAsync(string username, int attempt = 1)
        {
            return this.AuthenticationService.ForgotPasswordAsync(username, attempt);
        }

        /// <inheritdoc/>
        public UserVerificationHash ForgotPassword(string username, int attempt = 1)
        {
            var t = this.ForgotPasswordAsync(username).ConfigureAwait(true).GetAwaiter();

            return t.GetResult();
        }

        /// <inheritdoc/>
        public Task ResetPasswordAsync(ResetPassword resetPassword)
        {
            return this.AuthenticationService.ResetPasswordAsync(resetPassword);
        }

        /// <inheritdoc/>
        public void ResetPassword(ResetPassword resetPassword)
        {
            var t = this.ResetPasswordAsync(resetPassword).ConfigureAwait(true).GetAwaiter();

            t.GetResult();
        }

        /// <inheritdoc/>
        public async Task<IMeshyConnection> LoginWithRefreshTokenAsync(string refreshToken)
        {
            if (SDK.MeshyClient.CurrentConnection != null)
            {
                throw new InvalidOperationException("Connection has already been established. Please sign out before switching");
            }

            var identifier = await this.AuthenticationService.LoginWithRefreshTokenAsync(refreshToken).ConfigureAwait(true);
            var services = this.GenerateAPIRequestService(identifier);

            SDK.MeshyClient.CurrentConnection = new MeshyConnection(services.Item1, services.Item2, identifier);

            return SDK.MeshyClient.CurrentConnection;
        }

        /// <inheritdoc/>
        public IMeshyConnection LoginWithRefreshToken(string refreshToken)
        {
            var t = this.LoginWithRefreshTokenAsync(refreshToken).ConfigureAwait(true).GetAwaiter();

            return t.GetResult();
        }

        /// <inheritdoc/>
        public void Verify(UserVerificationCheck userVerificationCheck)
        {
            var t = this.VerifyAsync(userVerificationCheck).ConfigureAwait(true).GetAwaiter();

            t.GetResult();
        }

        /// <inheritdoc/>
        public Task VerifyAsync(UserVerificationCheck userVerificationCheck)
        {
            return this.AuthenticationService.VerifyAsync(userVerificationCheck);
        }

        /// <inheritdoc/>
        public Valid CheckHash(UserVerificationCheck userVerificationCheck)
        {
            var t = this.CheckHashAsync(userVerificationCheck).ConfigureAwait(true).GetAwaiter();

            return t.GetResult();
        }

        /// <inheritdoc/>
        public Task<Valid> CheckHashAsync(UserVerificationCheck userVerificationCheck)
        {
            return this.AuthenticationService.CheckHashAsync(userVerificationCheck);
        }

        /// <inheritdoc/>
        public Exist CheckUserExist(string username)
        {
            var t = this.CheckUserExistAsync(username).ConfigureAwait(true).GetAwaiter();

            return t.GetResult();
        }

        /// <inheritdoc/>
        public Task<Exist> CheckUserExistAsync(string username)
        {
            return this.AuthenticationService.CheckUserExistAsync(username);
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
