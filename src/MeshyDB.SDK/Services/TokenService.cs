// <copyright file="TokenService.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MeshyDB.SDK.Enums;
using MeshyDB.SDK.Models.Authentication;
using Newtonsoft.Json;

namespace MeshyDB.SDK.Services
{
    /// <summary>
    /// Implementation of <see cref="ITokenService"/>.
    /// </summary>
    internal class TokenService : ITokenService
    {
        private static readonly Dictionary<string, TokenCacheData> TokenCache = new Dictionary<string, TokenCacheData>();
        private readonly IRequestService requestService;
        private readonly string publicKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenService"/> class.
        /// </summary>
        /// <param name="requestService">Service to make requests against.</param>
        /// <param name="publicKey">Public Api credential supplied from MeshyDB to communicate with client.</param>
        /// <exception cref="ArgumentException">Thrown if any parameter is not configured.</exception>
        public TokenService(IRequestService requestService, string publicKey)
        {
            this.requestService = requestService ?? throw new ArgumentNullException(nameof(requestService));
            this.publicKey = publicKey;
        }

        /// <inheritdoc/>
        public Task<string> GenerateAccessToken(string username, string password)
        {
            return this.GenerateAccessToken(username, password, Guid.NewGuid().ToString());
        }

        /// <inheritdoc/>
        public async Task<string> GenerateAccessToken(string username, string password, string authenticationId)
        {
            var response = await this.requestService.PostRequest<TokenResponse>(
                $"connect/token",
                new TokenRequest()
                {
                    ClientId = this.publicKey,
                    Username = username,
                    Password = password,
                }, RequestDataFormat.Form).ConfigureAwait(true);

            TokenCache.Add(authenticationId, new TokenCacheData()
            {
                Token = response.AccessToken,
                RefreshToken = response.RefreshToken,
                Expires = DateTimeOffset.UtcNow.AddSeconds(response.Expires),
            });

            return authenticationId;
        }

        /// <inheritdoc/>
        public async Task<string> GetAccessTokenAsync(string authenticationId)
        {
            if (TokenCache.TryGetValue(authenticationId, out TokenCacheData value))
            {
                if (value.Expires > DateTimeOffset.UtcNow)
                {
                    return value.Token;
                }
                else
                {
                    value = await this.RefreshUserToken(value.RefreshToken).ConfigureAwait(true);
                    TokenCache.Remove(authenticationId);
                    TokenCache.Add(authenticationId, value);
                }
            }
            else
            {
                return null;
            }

            return value.Token;
        }

        /// <inheritdoc/>
        public async Task SignoutAsync(string authenticationId)
        {
            if (!TokenCache.TryGetValue(authenticationId, out TokenCacheData value))
            {
                return;
            }

            await this.requestService.PostRequest<object>(
                $"/connect/revocation",
                new TokenRevocation()
                {
                    Token = value.RefreshToken,
                    TokenTypeHint = "refresh_token",
                    ClientId = this.publicKey,
                }, RequestDataFormat.Form)
                .ConfigureAwait(true);

            TokenCache.Remove(authenticationId);
        }

        /// <inheritdoc/>
        public Task<string> GetRefreshTokenAsync(string authenticationId)
        {
            if (!string.IsNullOrWhiteSpace(authenticationId) && TokenCache.TryGetValue(authenticationId, out TokenCacheData value))
            {
                return Task.FromResult(value.RefreshToken);
            }

            return Task.FromResult(null as string);
        }

        /// <inheritdoc/>
        public Task<string> GenerateAccessTokenWithRefreshToken(string refreshToken)
        {
            return this.GenerateAccessTokenWithRefreshToken(refreshToken, Guid.NewGuid().ToString());
        }

        /// <inheritdoc/>
        public async Task<string> GenerateAccessTokenWithRefreshToken(string refreshToken, string authenticationId)
        {
            var tokenCacheData = await this.RefreshUserToken(refreshToken).ConfigureAwait(true);
            TokenCache.Add(authenticationId, tokenCacheData);
            return authenticationId;
        }

        /// <inheritdoc/>
        public Task<IDictionary<string, string>> GetUserInfoAsync(string authenticationId)
        {
            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {TokenCache[authenticationId].Token}" },
            };

            return this.requestService.GetRequest<IDictionary<string, string>>($"/connect/userinfo", headers);
        }

        private async Task<TokenCacheData> RefreshUserToken(string refreshToken)
        {
            var response = await this.requestService.PostRequest<TokenResponse>(
                $"connect/token",
                new TokenRequest()
                {
                    ClientId = this.publicKey,
                    GrantType = "refresh_token",
                    RefreshToken = refreshToken,
                }, RequestDataFormat.Form)
                .ConfigureAwait(true);

            return new TokenCacheData()
            {
                Token = response.AccessToken,
                RefreshToken = response.RefreshToken,
                Expires = DateTimeOffset.UtcNow.AddSeconds(response.Expires),
            };
        }
    }
}
