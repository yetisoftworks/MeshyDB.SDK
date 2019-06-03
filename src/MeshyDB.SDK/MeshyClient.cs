// <copyright file="MeshyClient.cs" company="Yetisoftworks LLC">
// Copyright (c) Yetisoftworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MeshyDB.SDK.Services;

namespace MeshyDB.SDK
{
    /// <summary>
    /// Implementation of <see cref="IMeshyClient"/>.
    /// </summary>
    internal class MeshyClient : IMeshyClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeshyClient"/> class.
        /// </summary>
        /// <param name="tokenService">Service used to make token calls.</param>
        /// <param name="requestService">Service used to make request calls.</param>
        /// <param name="authenticationId">Authentication Id reference of currently logged in user.</param>
        public MeshyClient(ITokenService tokenService, IRequestService requestService, string authenticationId)
        {
            this.TokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            this.RequestService = requestService ?? throw new ArgumentNullException(nameof(requestService));
            this.AuthenticationId = authenticationId ?? throw new ArgumentNullException(nameof(authenticationId));

            if (string.IsNullOrWhiteSpace(authenticationId))
            {
                throw new ArgumentException(nameof(authenticationId));
            }

            this.Meshes = new MeshesService(requestService);
            this.Users = new UsersService(requestService);
            this.AuthenticationService = new AuthenticationService(tokenService, requestService);
        }

        /// <inheritdoc/>
        public IMeshesService Meshes { get; private set; }

        /// <summary>
        /// Gets Authentication Id for establisehd user.
        /// </summary>
        public string AuthenticationId { get; private set; }

        /// <inheritdoc/>
        public IUsersService Users { get; private set; }

        /// <summary>
        /// Gets or sets Authentication Service.
        /// </summary>
        internal IAuthenticationService AuthenticationService { get; set; }

        /// <summary>
        /// Gets or sets Token Service.
        /// </summary>
        internal ITokenService TokenService { get; set; }

        /// <summary>
        /// Gets or sets Request Service.
        /// </summary>
        internal IRequestService RequestService { get; set; }

        /// <inheritdoc/>
        public async Task SignoutAsync()
        {
            await this.TokenService.SignoutAsync(this.AuthenticationId);
        }

        /// <inheritdoc/>
        public void Signout()
        {
            var t = this.SignoutAsync().ConfigureAwait(true).GetAwaiter();

            t.GetResult();
        }

        /// <inheritdoc/>
        public async Task UpdatePasswordAsync(string previousPassword, string newPassword)
        {
            await this.AuthenticationService.UpdatePasswordAsync(previousPassword, newPassword);
        }

        /// <inheritdoc/>
        public async Task<string> RetrievePersistanceTokenAsync()
        {
            return await this.AuthenticationService.RetrievePersistanceTokenAsync(this.AuthenticationId);
        }

        /// <inheritdoc/>
        public void UpdatePassword(string previousPassword, string newPassword)
        {
            var t = this.UpdatePasswordAsync(previousPassword, newPassword).ConfigureAwait(true).GetAwaiter();

            t.GetResult();
        }

        /// <inheritdoc/>
        public string RetrievePersistanceToken()
        {
            var t = this.RetrievePersistanceTokenAsync().ConfigureAwait(true).GetAwaiter();

            return t.GetResult();
        }

        /// <inheritdoc/>
        public IDictionary<string, string> GetMyUserInfo()
        {
            var t = this.GetMyUserInfoAsync().ConfigureAwait(true).GetAwaiter();

            return t.GetResult();
        }

        /// <inheritdoc/>
        public async Task<IDictionary<string, string>> GetMyUserInfoAsync()
        {
            return await this.TokenService.GetUserInfoAsync(this.AuthenticationId);
        }
    }
}
