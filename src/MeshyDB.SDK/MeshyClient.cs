using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MeshyDB.SDK.Services;

namespace MeshyDB.SDK
{
    internal class MeshyClient : IMeshyClient
    {
        public MeshyClient(ITokenService tokenService, IRequestService requestService, string authenticationId)
        {
            TokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            RequestService = requestService ?? throw new ArgumentNullException(nameof(requestService));
            AuthenticationId = authenticationId ?? throw new ArgumentNullException(nameof(authenticationId));

            if (string.IsNullOrWhiteSpace(authenticationId))
            {
                throw new ArgumentException(nameof(authenticationId));
            }

            Meshes = new MeshesService(requestService);
            Users = new UsersService(requestService);
            AuthenticationService = new AuthenticationService(tokenService, requestService);
        }

        /// <inheritdoc/>
        public IMeshesService Meshes { get; private set; }

        /// <inheritdoc/>
        public IUsersService Users { get; private set; }

        internal IAuthenticationService AuthenticationService { get; set; }

        internal ITokenService TokenService { get; set; }

        internal IRequestService RequestService { get; set; }

        public string AuthenticationId { get; private set; }

        /// <inheritdoc/>
        public async Task SignoutAsync()
        {
            await this.TokenService.Signout(this.AuthenticationId);
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
