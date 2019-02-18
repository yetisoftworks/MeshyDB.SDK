using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MeshyDB.SDK.Services;

namespace MeshyDB.SDK
{
    internal class MeshyClient : IMeshyClient
    {
        public MeshyClient(ITokenService tokenService, IRequestService requestService, string identifier)
        {
            TokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            RequestService = requestService ?? throw new ArgumentNullException(nameof(requestService));
            Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));

            Meshes = new MeshesService(requestService);
            Users = new UsersService(requestService);
            AuthenticationService = new AuthenticationService(tokenService, requestService);
        }

        /// <inheritdoc/>
        public IMeshesService Meshes { get; private set; }

        /// <inheritdoc/>
        public IUsersService Users { get; private set; }

        internal IAuthenticationService AuthenticationService { get; }

        internal ITokenService TokenService { get; }

        internal IRequestService RequestService { get; }

        public string Identifier { get; private set; }

        /// <inheritdoc/>
        public async Task SignOutAsync()
        {
            await this.TokenService.Signout(this.Identifier);
        }

        /// <inheritdoc/>
        public void SignOut()
        {
            var t = this.SignOutAsync().ConfigureAwait(true).GetAwaiter();

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
            return await this.AuthenticationService.RetrievePersistanceTokenAsync(this.Identifier);
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
    }
}
