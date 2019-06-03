// <copyright file="TokenService.cs" company="Yetisoftworks LLC">
// Copyright (c) Yetisoftworks LLC. All rights reserved.
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
            if (string.IsNullOrWhiteSpace(publicKey))
            {
                throw new ArgumentException($"{nameof(publicKey)} was not supplied", nameof(publicKey));
            }

            this.requestService = requestService ?? throw new ArgumentNullException(nameof(requestService));
            this.publicKey = publicKey;
        }

        /// <inheritdoc/>
        public async Task<string> GenerateAccessToken(string username, string password)
        {
            return await this.GenerateAccessToken(username, password, Guid.NewGuid().ToString());
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
                }, RequestDataFormat.Form);

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
                    value = await this.RefreshUserToken(value.RefreshToken);
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
                }, RequestDataFormat.Form);

            TokenCache.Remove(authenticationId);
        }

        /// <inheritdoc/>
        public async Task<string> GetRefreshTokenAsync(string authenticationId)
        {
            if (!string.IsNullOrWhiteSpace(authenticationId) && TokenCache.TryGetValue(authenticationId, out TokenCacheData value))
            {
                return await Task.FromResult(value.RefreshToken);
            }

            return await Task.FromResult(null as string);
        }

        /// <inheritdoc/>
        public async Task<string> GenerateAccessTokenWithRefreshToken(string refreshToken)
        {
            return await this.GenerateAccessTokenWithRefreshToken(refreshToken, Guid.NewGuid().ToString());
        }

        /// <inheritdoc/>
        public async Task<string> GenerateAccessTokenWithRefreshToken(string refreshToken, string authenticationId)
        {
            var tokenCacheData = await this.RefreshUserToken(refreshToken);
            TokenCache.Add(authenticationId, tokenCacheData);
            return authenticationId;
        }

        /// <inheritdoc/>
        public async Task<IDictionary<string, string>> GetUserInfoAsync(string authenticationId)
        {
            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {TokenCache[authenticationId].Token}" },
            };

            var response = await this.requestService.GetRequest<Dictionary<string, string>>($"/connect/userinfo", headers);
            return response;
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
                }, RequestDataFormat.Form);

            return new TokenCacheData()
            {
                Token = response.AccessToken,
                RefreshToken = response.RefreshToken,
                Expires = DateTimeOffset.UtcNow.AddSeconds(response.Expires),
            };
        }
    }
}
