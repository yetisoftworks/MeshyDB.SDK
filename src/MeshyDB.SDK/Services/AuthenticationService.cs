// <copyright file="AuthenticationService.cs" company="Yetisoftworks LLC">
// Copyright (c) Yetisoftworks LLC. All rights reserved.
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
        public async Task<string> LoginWithPasswordAsync(string username, string password)
        {
            return await this.tokenService.GenerateAccessToken(username, password);
        }

        /// <inheritdoc/>
        public async Task<UserVerificationHash> RegisterAsync(RegisterUser user)
        {
            return await this.requestService.PostRequest<UserVerificationHash>("users/register", user);
        }

        /// <inheritdoc/>
        public async Task<UserVerificationHash> ForgotPasswordAsync(string username)
        {
            var forgotPassword = new ForgotPassword { Username = username };
            var resetHash = await this.requestService.PostRequest<UserVerificationHash>("users/forgotpassword", forgotPassword);
            return resetHash;
        }

        /// <inheritdoc/>
        public async Task ResetPasswordAsync(ResetPassword resetPassword)
        {
            await this.requestService.PostRequest<object>("users/resetpassword", resetPassword);
        }

        /// <inheritdoc/>
        public async Task UpdatePasswordAsync(string previousPassword, string newPassword)
        {
            var update = new UserPasswordUpdate()
            {
                NewPassword = newPassword,
                PreviousPassword = previousPassword,
            };

            await this.requestService.PostRequest<object>("users/me/password", update);
        }

        /// <inheritdoc/>
        public async Task SignoutAsync(string identifier)
        {
            await this.tokenService.SignoutAsync(identifier);
        }

        /// <inheritdoc/>
        public async Task<string> LoginAnonymouslyAsync(string username = null)
        {
            var generatedUsername = username ?? Guid.NewGuid().ToString();
            var anonymousUser = new AnonymousRegistration()
            {
                Username = generatedUsername,
            };

            await this.requestService.PostRequest<User>("users/register/anonymous", anonymousUser);

            return await this.LoginWithPasswordAsync(generatedUsername, "nopassword");
        }

        /// <inheritdoc/>
        public async Task<string> LoginWithPersistanceAsync(string persistanceToken)
        {
            return await this.tokenService.GenerateAccessTokenWithRefreshToken(persistanceToken);
        }

        /// <inheritdoc/>
        public Task<string> RetrievePersistanceTokenAsync(string authenticationId)
        {
            return this.tokenService.GetRefreshTokenAsync(authenticationId);
        }

        /// <inheritdoc/>
        public async Task VerifyAsync(UserVerificationCheck userVerificationCheck)
        {
            await this.requestService.PostRequest<object>("users/verify", userVerificationCheck);
        }

        /// <inheritdoc/>
        public async Task<bool> CheckHashAsync(UserVerificationCheck userVerificationCheck)
        {
            return await this.requestService.PostRequest<bool>("users/checkhash", userVerificationCheck);
        }
    }
}
