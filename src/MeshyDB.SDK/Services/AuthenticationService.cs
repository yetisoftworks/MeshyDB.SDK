// <copyright file="AuthenticationService.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MeshyDB.SDK.Models;

namespace MeshyDB.SDK.Services
{
    /// <summary>
    /// Implementation of <see cref="IAuthenticationService"/>.
    /// </summary>
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService tokenService;
        private readonly IRequestService requestService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
        /// </summary>
        /// <param name="tokenService">Service used to make token calls.</param>
        /// <param name="requestService">Service used to make request calls.</param>
        internal AuthenticationService(ITokenService tokenService, IRequestService requestService)
        {
            this.tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            this.requestService = requestService ?? throw new ArgumentNullException(nameof(requestService));
        }

        /// <inheritdoc/>
        public Task<string> LoginWithPasswordAsync(string username, string password)
        {
            return this.tokenService.GenerateAccessToken(username, password);
        }

        /// <inheritdoc/>
        public Task<UserVerificationHash> RegisterAsync(RegisterUser user)
        {
            return this.requestService.PostRequest<UserVerificationHash>("users/register", user);
        }

        /// <inheritdoc/>
        public Task<UserVerificationHash> ForgotPasswordAsync(string username, int attempt = 1)
        {
            var forgotPassword = new ForgotPassword { Username = username, Attempt = attempt };
            return this.requestService.PostRequest<UserVerificationHash>("users/forgotpassword", forgotPassword);
        }

        /// <inheritdoc/>
        public Task ResetPasswordAsync(ResetPassword resetPassword)
        {
            return this.requestService.PostRequest<object>("users/resetpassword", resetPassword);
        }

        /// <inheritdoc/>
        public Task UpdatePasswordAsync(string previousPassword, string newPassword)
        {
            var update = new UserPasswordUpdate()
            {
                NewPassword = newPassword,
                PreviousPassword = previousPassword,
            };

            return this.requestService.PostRequest<object>("users/me/password", update);
        }

        /// <inheritdoc/>
        public Task SignoutAsync(string identifier)
        {
            return this.tokenService.SignoutAsync(identifier);
        }

        /// <inheritdoc/>
        public Task<string> LoginAnonymouslyAsync(string username = null)
        {
            var generatedUsername = username ?? Guid.NewGuid().ToString();
            var anonymousUser = new AnonymousRegistration()
            {
                Username = generatedUsername,
            };

            this.requestService.PostRequest<User>("users/register/anonymous", anonymousUser).ConfigureAwait(false).GetAwaiter().GetResult();

            return this.LoginWithPasswordAsync(generatedUsername, "nopassword");
        }

        /// <inheritdoc/>
        public Task<string> LoginWithPersistanceAsync(string persistanceToken)
        {
            return this.tokenService.GenerateAccessTokenWithRefreshToken(persistanceToken);
        }

        /// <inheritdoc/>
        public Task<string> RetrievePersistanceTokenAsync(string authenticationId)
        {
            return this.tokenService.GetRefreshTokenAsync(authenticationId);
        }

        /// <inheritdoc/>
        public Task VerifyAsync(UserVerificationCheck userVerificationCheck)
        {
            return this.requestService.PostRequest<object>("users/verify", userVerificationCheck);
        }

        /// <inheritdoc/>
        public Task<bool> CheckHashAsync(UserVerificationCheck userVerificationCheck)
        {
            return this.requestService.PostRequest<bool>("users/checkhash", userVerificationCheck);
        }
    }
}
