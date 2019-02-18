using MeshyDB.SDK.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MeshyDB.SDK.Services
{
    /// <summary>
    /// Implementation of <see cref="IAuthenticationService"/>
    /// </summary>
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService tokenService;
        private readonly IRequestService requestService;

        public AuthenticationService(ITokenService tokenService, IRequestService requestService)
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
        public async Task<User> CreateUserAsync(NewUser user)
        {
            return await this.requestService.PostRequest<User>("users", user);
        }

        /// <inheritdoc/>
        public async Task<PasswordResetHash> ForgotPasswordAsync(string username)
        {
            var forgotPassword = new ForgotPassword { Username = username };
            var resetHash = await this.requestService.PostRequest<PasswordResetHash>("users/forgotpassword", forgotPassword);
            return resetHash;
        }

        /// <inheritdoc/>
        public async Task ResetPasswordAsync(PasswordResetHash resetHash, string newPassword)
        {
            var reset = new ResetPassword
            {
                Expires = resetHash.Expires,
                Hash = resetHash.Hash,
                Username = resetHash.Username,
                NewPassword = newPassword,
            };

            await this.requestService.PostRequest<object>("users/resetpassword", reset);
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
        public async Task Signout(string identifier)
        {
            await this.tokenService.Signout(identifier);
        }

        /// <inheritdoc/>
        public async Task<string> LoginAnonymouslyAsync()
        {
            var username = Guid.NewGuid().ToString();
            var user = await this.CreateUserAsync(new NewUser()
            {
                IsActive = true,
                Verified = true,
                NewPassword = "nopassword",
                Username = username
            });

            return await this.tokenService.GenerateAccessToken(username, "nopassword");
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
    }
}
