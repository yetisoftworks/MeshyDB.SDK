using MeshyDb.SDK.Enums;
using MeshyDb.SDK.Models.Authentication;
using MeshyDb.SDK.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MeshyDb.SDK.Tests
{
    public class TokenServiceTests
    {
        [Fact]
        public void ShouldCreate()
        {
            var requestService = new Mock<IRequestService>();
            var service = new TokenService(requestService.Object, Generator.RandomString(36), Generator.RandomString(36));

            Assert.NotNull(service);
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithNullRequestService()
        {
            Assert.Throws<ArgumentNullException>(() => new TokenService(null, Generator.RandomString(36), Generator.RandomString(36)));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithNullPublicKey()
        {
            var requestService = new Mock<IRequestService>();

            Assert.Throws<ArgumentException>(() => new TokenService(requestService.Object, null, Generator.RandomString(36)));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithEmptyPublicKey()
        {
            var requestService = new Mock<IRequestService>();

            Assert.Throws<ArgumentException>(() => new TokenService(requestService.Object, string.Empty, Generator.RandomString(36)));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithWhitespacePublicKey()
        {
            var requestService = new Mock<IRequestService>();

            Assert.Throws<ArgumentException>(() => new TokenService(requestService.Object, new string(' ', 5), Generator.RandomString(36)));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithNullPrivateKey()
        {
            var requestService = new Mock<IRequestService>();

            Assert.Throws<ArgumentException>(() => new TokenService(requestService.Object, Generator.RandomString(36), null));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithEmptyPrivateKey()
        {
            var requestService = new Mock<IRequestService>();

            Assert.Throws<ArgumentException>(() => new TokenService(requestService.Object, Generator.RandomString(36), string.Empty));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithWhitespacePrivateKey()
        {
            var requestService = new Mock<IRequestService>();

            Assert.Throws<ArgumentException>(() => new TokenService(requestService.Object, Generator.RandomString(36), new string(' ', 5)));
        }

        [Fact]
        public void ShouldGenerateTokenForNewRequest()
        {
            var requestService = new Mock<IRequestService>();
            var token = Generator.RandomString(25);
            requestService.Setup(x => x.PostRequest<TokenResponse>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>())).Returns(
                () =>
                {
                    return Task.FromResult(new TokenResponse()
                    {
                        AccessToken = token,
                        Expires = 0,
                        TokenType = Generator.RandomString(15)
                    });
                }
                ).Verifiable();
            var service = new TokenService(requestService.Object, Generator.RandomString(36), Generator.RandomString(36));

            Assert.Equal(token, service.GetOAuthTokenAsync().Result);
            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldGenerateTokenIfExpired()
        {
            var requestService = new Mock<IRequestService>();
            var tokens = new List<string>();

            requestService.Setup(x => x.PostRequest<TokenResponse>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>()))
                          .Returns(
                          () =>
                          {
                              var token = Generator.RandomString(25);
                              tokens.Add(token);
                              return Task.FromResult(new TokenResponse()
                              {
                                  AccessToken = token,
                                  Expires = -1,
                                  TokenType = Generator.RandomString(15)
                              });
                          })
                          .Verifiable();

            var service = new TokenService(requestService.Object, Generator.RandomString(36), Generator.RandomString(36));

            var newToken = service.GetOAuthTokenAsync().Result;
            Assert.Equal(tokens[tokens.Count - 1], newToken);

            newToken = service.GetOAuthTokenAsync().Result;
            Assert.Equal(tokens[tokens.Count - 1], newToken);

            requestService.Verify(x => x.PostRequest<TokenResponse>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>()), Times.Exactly(2));
        }

        [Fact]
        public void ShouldGetTokenIfNotExpired()
        {
            var requestService = new Mock<IRequestService>();
            var tokens = new List<string>();

            requestService.Setup(x => x.PostRequest<TokenResponse>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>()))
                          .Returns(
                          () =>
                          {
                              var token = Generator.RandomString(25);
                              tokens.Add(token);
                              return Task.FromResult(new TokenResponse()
                              {
                                  AccessToken = token,
                                  Expires = 500,
                                  TokenType = Generator.RandomString(15)
                              });
                          })
                          .Verifiable();

            var service = new TokenService(requestService.Object, Generator.RandomString(36), Generator.RandomString(36));

            var newToken = service.GetOAuthTokenAsync().Result;
            Assert.Equal(tokens[tokens.Count - 1], newToken);

            newToken = service.GetOAuthTokenAsync().Result;
            Assert.Equal(tokens[tokens.Count - 1], newToken);

            requestService.Verify(x => x.PostRequest<TokenResponse>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>()), Times.Exactly(1));
        }

        [Fact]
        public void ShouldHaveCorrectRequestParameters()
        {
            var requestService = new Mock<IRequestService>();
            var publicKey = Generator.RandomString(36);
            var privateKey = Generator.RandomString(36);

            var request = new TokenRequest()
            {
                ClientId = publicKey,
                ClientSecret = privateKey,
                GrantType = "client_credentials",
                Scope = "meshy.api",
                Username = null,
                Password = null
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

            var service = new TokenService(requestService.Object, publicKey, privateKey);

            var newToken = service.GetOAuthTokenAsync().Result;

            Assert.Equal("connect/token", passedEndpoint);
            Assert.Equal(request.ClientId, passedRequest.ClientId);
            Assert.Equal(request.ClientSecret, passedRequest.ClientSecret);
            Assert.Equal(request.GrantType, passedRequest.GrantType);
            Assert.Equal(request.Scope, passedRequest.Scope);
            Assert.Equal(request.Username, passedRequest.Username);
            Assert.Equal(request.Password, passedRequest.Password);
            Assert.Equal(RequestDataFormat.Form, passedDataFormat);

            requestService.Verify(x => x.PostRequest<TokenResponse>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>()), Times.Exactly(1));
        }
    }
}
