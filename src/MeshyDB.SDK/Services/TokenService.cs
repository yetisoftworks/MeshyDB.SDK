using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MeshyDB.SDK.Enums;
using MeshyDB.SDK.Models.Authentication;

namespace MeshyDB.SDK.Services
{
    /// <summary>
    /// Implementation of <see cref="ITokenService"/>
    /// </summary>
    internal class TokenService : ITokenService
    {
        private readonly IRequestService requestService;
        private readonly string publicKey;
        private readonly string privateKey;
        private static Dictionary<string, TokenCacheData> TokenCache = new Dictionary<string, TokenCacheData>();

        /// <summary>
        /// Instantiates an instance of the <see cref="TokenService"/> class.
        /// </summary>
        /// <param name="requestService">Service to make requests against</param>
        /// <param name="publicKey">Public Api credential supplied from MeshyDB to communicate with client</param>
        /// <param name="privateKey">Private Api credential supplied from MeshyDB to communicate with client</param>
        /// <exception cref="ArgumentException">Thrown if any parameter is not configured</exception>
        public TokenService(IRequestService requestService, string publicKey, string privateKey)
        {
            if (string.IsNullOrWhiteSpace(publicKey))
            {
                throw new ArgumentException($"{nameof(publicKey)} was not supplied", nameof(publicKey));
            }

            if (string.IsNullOrWhiteSpace(privateKey))
            {
                throw new ArgumentException($"{nameof(privateKey)} was not supplied", nameof(privateKey));
            }

            this.requestService = requestService ?? throw new ArgumentNullException(nameof(requestService));
            this.publicKey = publicKey;
            this.privateKey = privateKey;
        }

        /// <inheritdoc/>
        public async Task<string> GetOAuthTokenAsync()
        {
            var key = $"{this.publicKey}:{this.privateKey}";
            if (TokenCache.TryGetValue(key, out TokenCacheData value))
            {
                if (value.Expires > DateTimeOffset.UtcNow)
                {
                    return value.Token;
                }
                else
                {
                    TokenCache.Remove(key);
                }
            }

            var response = await this.requestService.PostRequest<TokenResponse>($"connect/token", new TokenRequest()
            {
                ClientId = this.publicKey,
                ClientSecret = this.privateKey
            }, RequestDataFormat.Form);

            TokenCache.Add(key, new TokenCacheData()
            {
                Token = response.AccessToken,
                Expires = DateTimeOffset.UtcNow.AddSeconds(response.Expires)
            });

            return response.AccessToken;
        }
    }
}
