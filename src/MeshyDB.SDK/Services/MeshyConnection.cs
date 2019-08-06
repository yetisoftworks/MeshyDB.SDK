// <copyright file="MeshyConnection.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeshyDB.SDK.Models;
using MeshyDB.SDK.Services;

namespace MeshyDB.SDK.Services
{
    /// <summary>
    /// Implementation of <see cref="IMeshyConnection"/>.
    /// </summary>
    internal class MeshyConnection : IMeshyConnection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeshyConnection"/> class.
        /// </summary>
        /// <param name="tokenService">Service used to make token calls.</param>
        /// <param name="requestService">Service used to make request calls.</param>
        /// <param name="authenticationId">Authentication Id reference of currently logged in user.</param>
        public MeshyConnection(ITokenService tokenService, IRequestService requestService, string authenticationId)
        {
            this.TokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            this.RequestService = requestService ?? throw new ArgumentNullException(nameof(requestService));
            this.AuthenticationId = authenticationId ?? throw new ArgumentNullException(nameof(authenticationId));

            this.Meshes = new MeshesService(requestService);
            this.Users = new UsersService(requestService);
            this.AuthenticationService = new AuthenticationService(tokenService, requestService);
            this.SetCurrentUser();
        }

        /// <inheritdoc/>
        public IMeshesService Meshes { get; private set; }

        /// <summary>
        /// Gets Authentication Id for established user.
        /// </summary>
        public string AuthenticationId { get; private set; }

        /// <inheritdoc/>
        public CurrentUser CurrentUser { get; internal set; }

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
        public Task SignoutAsync()
        {
            SDK.MeshyClient.CurrentConnection = null;
            this.CurrentUser = null;

            return this.TokenService.SignoutAsync(this.AuthenticationId);
        }

        /// <inheritdoc/>
        public void Signout()
        {
            var t = this.SignoutAsync().ConfigureAwait(true).GetAwaiter();

            t.GetResult();
        }

        /// <inheritdoc/>
        public Task UpdatePasswordAsync(string previousPassword, string newPassword)
        {
            return this.AuthenticationService.UpdatePasswordAsync(previousPassword, newPassword);
        }

        /// <inheritdoc/>
        public Task<string> RetrieveRefreshTokenAsync()
        {
            return this.AuthenticationService.RetrieveRefreshTokenAsync(this.AuthenticationId);
        }

        /// <inheritdoc/>
        public void UpdatePassword(string previousPassword, string newPassword)
        {
            var t = this.UpdatePasswordAsync(previousPassword, newPassword).ConfigureAwait(true).GetAwaiter();

            t.GetResult();
        }

        /// <inheritdoc/>
        public string RetrieveRefreshToken()
        {
            var t = this.RetrieveRefreshTokenAsync().ConfigureAwait(true).GetAwaiter();

            return t.GetResult();
        }

        private void SetCurrentUser()
        {
            var accessToken = this.TokenService.GetAccessTokenAsync(this.AuthenticationId).ConfigureAwait(true).GetAwaiter().GetResult();

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                return;
            }

            var handler = new JwtSecurityTokenHandler();
            var parsedToken = handler.ReadToken(accessToken) as JwtSecurityToken;

            this.CurrentUser = new CurrentUser()
            {
                FirstName = parsedToken.Claims.Where(x => x.Type == "given_name").FirstOrDefault()?.Value.ToString(),
                LastName = parsedToken.Claims.Where(x => x.Type == "family_name").FirstOrDefault()?.Value.ToString(),
                Id = parsedToken.Claims.Where(x => x.Type == "sub").FirstOrDefault()?.Value.ToString(),
                UserName = parsedToken.Claims.Where(x => x.Type == "id").FirstOrDefault()?.Value.ToString(),
                Roles = parsedToken.Claims.Where(x => x.Type == "role").Select(x => x.Value),
            };
        }
    }
}
