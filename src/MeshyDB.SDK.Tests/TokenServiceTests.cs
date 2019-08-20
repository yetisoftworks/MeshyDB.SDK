using MeshyDB.SDK.Enums;
using MeshyDB.SDK.Models.Authentication;
using MeshyDB.SDK.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MeshyDB.SDK.Tests
{
    public class TokenServiceTests
    {
        [Fact]
        public void ShouldCreate()
        {
            var requestService = new Mock<IRequestService>();
            var service = new TokenService(requestService.Object, Generator.RandomString(36));

            Assert.NotNull(service);
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithNullRequestService()
        {
            Assert.Throws<ArgumentNullException>(() => new TokenService(null, Generator.RandomString(36)));
        }

        [Fact]
        public void ShouldGenerateTokenWithRandomIdentifierSuccessfully()
        {
            var requestService = new Mock<IRequestService>();
            var token = Generator.RandomString(25);
            requestService.Setup(x => x.PostRequest<TokenResponse>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>())).Returns(
                () =>
                {
                    return Task.FromResult(new TokenResponse()
                    {
                        AccessToken = token,
                        Expires = 500,
                        TokenType = Generator.RandomString(15),
                    });
                }
                ).Verifiable();

            var service = new TokenService(requestService.Object, Generator.RandomString(36));

            Assert.True(Guid.TryParse(service.GenerateAccessToken(Generator.RandomString(10), Generator.RandomString(10)).Result, out var _));
            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldGenerateTokenWithKnownIdentifierSuccessfully()
        {
            var requestService = new Mock<IRequestService>();
            var token = Generator.RandomString(25);
            var authenticationId = Generator.RandomString(10);
            requestService.Setup(x => x.PostRequest<TokenResponse>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>())).Returns(
                () =>
                {
                    return Task.FromResult(new TokenResponse()
                    {
                        AccessToken = token,
                        Expires = 500,
                        TokenType = Generator.RandomString(15),
                    });
                }
                ).Verifiable();

            var service = new TokenService(requestService.Object, Generator.RandomString(36));

            Assert.Equal(authenticationId, service.GenerateAccessToken(Generator.RandomString(10), Generator.RandomString(10), authenticationId).Result);
            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldGetAccessTokenSuccessfully()
        {
            var requestService = new Mock<IRequestService>();
            var token = Generator.RandomString(25);
            var authenticationId = Generator.RandomString(10);
            requestService.Setup(x => x.PostRequest<TokenResponse>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>())).Returns(
                () =>
                {
                    return Task.FromResult(new TokenResponse()
                    {
                        AccessToken = token,
                        Expires = 500,
                        TokenType = Generator.RandomString(15),
                    });
                }
                ).Verifiable();

            var service = new TokenService(requestService.Object, Generator.RandomString(36));

            Assert.Equal(authenticationId, service.GenerateAccessToken(Generator.RandomString(10), Generator.RandomString(10), authenticationId).Result);
            Assert.Equal(token, service.GetAccessTokenAsync(authenticationId).Result);
            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldGetAccessTokenBeNullIfNotFound()
        {
            var requestService = new Mock<IRequestService>();
            var authenticationId = Generator.RandomString(10);

            var service = new TokenService(requestService.Object, Generator.RandomString(36));

            Assert.Null(service.GetAccessTokenAsync(authenticationId).Result);
        }

        [Fact]
        public void ShouldGetAccessTokenShouldRefreshIfExpired()
        {
            var requestService = new Mock<IRequestService>();
            var token = Generator.RandomString(25);
            var authenticationId = Generator.RandomString(10);
            var models = new List<TokenRequest>();
            requestService.Setup(x => x.PostRequest<TokenResponse>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>()))
                .Callback<string, object, RequestDataFormat>((path, model, format) =>
                {
                    models.Add(model as TokenRequest);
                })
                .Returns(
                () =>
                {
                    return Task.FromResult(new TokenResponse()
                    {
                        AccessToken = token,
                        Expires = -1,
                        TokenType = Generator.RandomString(15),
                    });
                }
                ).Verifiable();

            var service = new TokenService(requestService.Object, Generator.RandomString(36));

            Assert.Equal(authenticationId, service.GenerateAccessToken(Generator.RandomString(10), Generator.RandomString(10), authenticationId).Result);
            Assert.Equal(token, service.GetAccessTokenAsync(authenticationId).Result);
            Assert.Contains(models, x => x.GrantType == "refresh_token");
            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldHaveCorrectRequestParametersDuringRefresh()
        {
            var requestService = new Mock<IRequestService>();
            var publicKey = Generator.RandomString(36); ;
            var username = Generator.RandomString(10);
            var password = Generator.RandomString(10);
            var refreshToken = Generator.RandomString(25);
            var request = new TokenRequest()
            {
                ClientId = publicKey,
                GrantType = "refresh_token",
                RefreshToken = refreshToken
            };

            var passedEndpoint = default(string);
            var passedRequest = default(TokenRequest);
            var passedDataFormat = default(RequestDataFormat);

            requestService.Setup(x => x.PostRequest<TokenResponse>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>()))
                          .Callback<string, object, RequestDataFormat>((endpoint, requestData, format) =>
                          {
                              passedEndpoint = endpoint;
                              passedRequest = requestData as TokenRequest;
                              passedDataFormat = format;
                          })
                          .Returns(
                          () =>
                          {
                              return Task.FromResult(new TokenResponse()
                              {
                                  AccessToken = Generator.RandomString(25),
                                  Expires = -1,
                                  TokenType = Generator.RandomString(15),
                                  RefreshToken = refreshToken
                              });
                          })
                          .Verifiable();

            var service = new TokenService(requestService.Object, publicKey);
            var authenticationId = Generator.RandomString(10);
            var newToken = service.GenerateAccessToken(username, password, authenticationId).Result;
            service.GetAccessTokenAsync(authenticationId).ConfigureAwait(false).GetAwaiter().GetResult();

            Assert.Equal("connect/token", passedEndpoint);
            Assert.Equal(request.ClientId, passedRequest.ClientId);
            Assert.Equal(request.GrantType, passedRequest.GrantType);
            Assert.Equal(request.RefreshToken, passedRequest.RefreshToken);
            Assert.Equal(request.Username, passedRequest.Username);
            Assert.Equal(request.Password, passedRequest.Password);
            Assert.Equal(RequestDataFormat.Form, passedDataFormat);

            requestService.Verify(x => x.PostRequest<TokenResponse>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>()), Times.Exactly(2));
        }

        [Fact]
        public void ShouldHaveCorrectRequestParametersDuringCreate()
        {
            var requestService = new Mock<IRequestService>();
            var publicKey = Generator.RandomString(36); ;
            var username = Generator.RandomString(10);
            var password = Generator.RandomString(10);

            var request = new TokenRequest()
            {
                ClientId = publicKey,
                GrantType = "password",
                Scope = "meshy.api offline_access openid",
                Username = username,
                Password = password
            };
            var passedEndpoint = default(string);
            var passedRequest = default(TokenRequest);
            var passedDataFormat = default(RequestDataFormat);
            requestService.Setup(x => x.PostRequest<TokenResponse>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>()))
                          .Callback<string, object, RequestDataFormat>((endpoint, requestData, format) =>
                          {
                              passedEndpoint = endpoint;
                              passedRequest = requestData as TokenRequest;
                              passedDataFormat = format;
                          })
                          .Returns(
                          () =>
                          {
                              return Task.FromResult(new TokenResponse()
                              {
                                  AccessToken = Generator.RandomString(25),
                                  Expires = 500,
                                  TokenType = Generator.RandomString(15)
                              });
                          })
                          .Verifiable();

            var service = new TokenService(requestService.Object, publicKey);

            var newToken = service.GenerateAccessToken(username, password).Result;

            Assert.Equal("connect/token", passedEndpoint);
            Assert.Equal(request.ClientId, passedRequest.ClientId);
            Assert.Equal(request.GrantType, passedRequest.GrantType);
            Assert.Equal(request.Scope, passedRequest.Scope);
            Assert.Equal(request.RefreshToken, passedRequest.RefreshToken);
            Assert.Equal(request.Username, passedRequest.Username);
            Assert.Equal(request.Password, passedRequest.Password);
            Assert.Equal(RequestDataFormat.Form, passedDataFormat);

            requestService.Verify(x => x.PostRequest<TokenResponse>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>()), Times.Exactly(1));
        }

        [Fact]
        public void ShouldGetRefreshTokenAsyncSuccessfully()
        {
            var requestService = new Mock<IRequestService>();
            var token = Generator.RandomString(25);
            var refreshToken = Generator.RandomString(25);

            requestService.Setup(x => x.PostRequest<TokenResponse>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>())).Returns(
                () =>
                {
                    return Task.FromResult(new TokenResponse()
                    {
                        AccessToken = token,
                        Expires = 500,
                        TokenType = Generator.RandomString(15),
                        RefreshToken = refreshToken
                    });
                }
                ).Verifiable();

            var service = new TokenService(requestService.Object, Generator.RandomString(36));

            var authenticationId = service.GenerateAccessToken(Generator.RandomString(10), Generator.RandomString(10)).Result;

            Assert.Equal(refreshToken, service.GetRefreshTokenAsync(authenticationId).Result);

            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldGetRefreshTokenAsyncBeNullWhenAuthenticationIdIsNull()
        {
            var requestService = new Mock<IRequestService>();

            var service = new TokenService(requestService.Object, Generator.RandomString(36));

            Assert.Null(service.GetRefreshTokenAsync(null).Result);

            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldGetRefreshTokenAsyncBeNullWhenAuthenticationIdIsEmpty()
        {
            var requestService = new Mock<IRequestService>();

            var service = new TokenService(requestService.Object, Generator.RandomString(36));

            Assert.Null(service.GetRefreshTokenAsync(new string(' ', 5)).Result);

            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldGetRefreshTokenAsyncBeNullWhenAuthenticationIdIsNotFound()
        {
            var requestService = new Mock<IRequestService>();

            var service = new TokenService(requestService.Object, Generator.RandomString(36));

            Assert.Null(service.GetRefreshTokenAsync(Generator.RandomString(10)).Result);

            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldGenerateAccessTokenWithRefreshToken()
        {
            var requestService = new Mock<IRequestService>();
            var token = Generator.RandomString(25);
            var refreshToken = Generator.RandomString(25);

            requestService.Setup(x => x.PostRequest<TokenResponse>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>())).Returns(
                () =>
                {
                    return Task.FromResult(new TokenResponse()
                    {
                        AccessToken = token,
                        Expires = 500,
                        TokenType = Generator.RandomString(15),
                        RefreshToken = refreshToken
                    });
                }
                ).Verifiable();

            var service = new TokenService(requestService.Object, Generator.RandomString(36));

            var authenticationId = service.GenerateAccessTokenWithRefreshToken(Generator.RandomString(10)).Result;
            Assert.NotNull(authenticationId);

            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldGenerateAccessTokenWithRefreshTokenAndAuthenticationId()
        {
            var requestService = new Mock<IRequestService>();
            var token = Generator.RandomString(25);
            var refreshToken = Generator.RandomString(25);

            requestService.Setup(x => x.PostRequest<TokenResponse>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>())).Returns(
                () =>
                {
                    return Task.FromResult(new TokenResponse()
                    {
                        AccessToken = token,
                        Expires = 500,
                        TokenType = Generator.RandomString(15),
                        RefreshToken = refreshToken
                    });
                }
                ).Verifiable();

            var service = new TokenService(requestService.Object, Generator.RandomString(36));
            var authenticationId = Generator.RandomString(10);
            var resultId = service.GenerateAccessTokenWithRefreshToken(Generator.RandomString(10), authenticationId).Result;
            Assert.Equal(authenticationId, resultId);

            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldSignoutSuccessfully()
        {
            var requestService = new Mock<IRequestService>();
            var token = Generator.RandomString(25);
            var refreshToken = Generator.RandomString(25);

            var passedPath = string.Empty;
            var passedModel = default(TokenRevocation);
            var passedFormat = RequestDataFormat.Json;

            requestService.Setup(x => x.PostRequest<TokenResponse>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>()))
                .Returns(
                () =>
                {
                    return Task.FromResult(new TokenResponse()
                    {
                        AccessToken = token,
                        Expires = 500,
                        TokenType = Generator.RandomString(15),
                        RefreshToken = refreshToken
                    });
                })
                .Verifiable();

            requestService.Setup(x => x.PostRequest<object>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>()))
                .Callback<string, object, RequestDataFormat>((path, model, format) =>
                {
                    passedPath = path;
                    passedModel = model as TokenRevocation;
                    passedFormat = format;
                })
                .Returns(() =>
                {
                    return Task.FromResult<object>(null);
                })
                .Verifiable();

            var publicKey = Generator.RandomString(36);
            var service = new TokenService(requestService.Object, publicKey);
            var authenticationId = Generator.RandomString(10);
            var resultId = service.GenerateAccessTokenWithRefreshToken(Generator.RandomString(10), authenticationId).Result;

            service.SignoutAsync(resultId).ConfigureAwait(false).GetAwaiter().GetResult();

            var signedoutToken = service.GetAccessTokenAsync(resultId).Result;

            Assert.Null(signedoutToken);
            Assert.Contains("/connect/revocation", passedPath, StringComparison.InvariantCultureIgnoreCase);
            Assert.Equal(publicKey, passedModel.ClientId);
            Assert.Equal(refreshToken, passedModel.Token);
            Assert.Equal("refresh_token", passedModel.TokenTypeHint);

            requestService.VerifyAll();
            requestService.Verify(x => x.PostRequest<object>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>()), Times.Exactly(1));
        }
    }
}
