using MeshyDb.SDK.Services;
using Moq;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MeshyDb.SDK.Tests
{
    public class RequestServiceTests
    {
        [Fact]
        public void ShouldCreate()
        {
            var httpService = new Mock<IHttpService>();
            var service = new RequestService(httpService.Object, Generator.RandomString(25));
            Assert.NotNull(service);
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithNullRequestService()
        {
            Assert.Throws<ArgumentNullException>(() => new RequestService(null, Generator.RandomString(25)));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithNullBaseUrl()
        {
            var httpService = new Mock<IHttpService>();
            Assert.Throws<ArgumentNullException>(() => new RequestService(httpService.Object, null));
        }

        [Fact]
        public void ShouldAllowOptionalTokenService()
        {
            var httpService = new Mock<IHttpService>();
            var tokenService = new Mock<ITokenService>();
            var service = new RequestService(httpService.Object, Generator.RandomString(25), tokenService.Object);
            Assert.NotNull(service);
        }

        #region Get Request

        [Fact]
        public async void ShouldSendGetRequest()
        {
            var httpService = new Mock<IHttpService>();
            var tokenService = new Mock<ITokenService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var service = new RequestService(httpService.Object, baseUrl, tokenService.Object);

            var passedRequest = default(HttpRequestMessage);

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpRequestMessage>())).Callback((HttpRequestMessage request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(new TestData());
            }).Verifiable();

            tokenService.Setup(x => x.GetOAuthTokenAsync()).Returns(() =>
            {
                return Task.FromResult(Generator.RandomString(25));
            }).Verifiable();

            await service.GetRequest<TestData>("test/path");
            httpService.VerifyAll();
            tokenService.VerifyAll();
            Assert.Equal("GET", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(passedRequest.Headers.Contains("Authorization"));
        }

        [Fact]
        public async void ShouldRequireValidUrlForGetRequest()
        {
            var httpService = new Mock<IHttpService>();
            var tokenService = new Mock<ITokenService>();

            // Forcing bad base url for error
            var baseUrl = $"http{Generator.RandomString(25)}";

            var service = new RequestService(httpService.Object, baseUrl, tokenService.Object);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await service.GetRequest<TestData>("test/path"));
        }

        [Fact]
        public async void ShouldNotAddAuthorizationWithNullTokenServiceForGetRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var service = new RequestService(httpService.Object, baseUrl, null);

            var passedRequest = default(HttpRequestMessage);

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpRequestMessage>())).Callback((HttpRequestMessage request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(new TestData());
            }).Verifiable();

            await service.GetRequest<TestData>("test/path");
            httpService.VerifyAll();
            Assert.Equal("GET", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(!passedRequest.Headers.Contains("Authorization"));
        }

        [Fact]
        public async void ShouldNotAddAuthorizationWithNullTokenFromTokenServiceForGetRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var tokenService = new Mock<ITokenService>();
            var service = new RequestService(httpService.Object, baseUrl, tokenService.Object);

            var passedRequest = default(HttpRequestMessage);

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpRequestMessage>())).Callback((HttpRequestMessage request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(new TestData());
            }).Verifiable();

            tokenService.Setup(x => x.GetOAuthTokenAsync()).Returns(() =>
            {
                return Task.FromResult((string)null);
            }).Verifiable();

            await service.GetRequest<TestData>("test/path");
            httpService.VerifyAll();
            Assert.Equal("GET", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(!passedRequest.Headers.Contains("Authorization"));
        }

        [Fact]
        public async void ShouldNotAddAuthorizationWithEmptyTokenFromTokenServiceForGetRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var tokenService = new Mock<ITokenService>();
            var service = new RequestService(httpService.Object, baseUrl, tokenService.Object);

            var passedRequest = default(HttpRequestMessage);

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpRequestMessage>())).Callback((HttpRequestMessage request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(new TestData());
            }).Verifiable();

            tokenService.Setup(x => x.GetOAuthTokenAsync()).Returns(() =>
            {
                return Task.FromResult(string.Empty);
            }).Verifiable();

            await service.GetRequest<TestData>("test/path");
            httpService.VerifyAll();
            Assert.Equal("GET", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(!passedRequest.Headers.Contains("Authorization"));
        }

        #endregion

        #region Delete Request

        [Fact]
        public async void ShouldSendDeleteRequest()
        {
            var httpService = new Mock<IHttpService>();
            var tokenService = new Mock<ITokenService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var service = new RequestService(httpService.Object, baseUrl, tokenService.Object);

            var passedRequest = default(HttpRequestMessage);

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpRequestMessage>())).Callback((HttpRequestMessage request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(new TestData());
            }).Verifiable();

            tokenService.Setup(x => x.GetOAuthTokenAsync()).Returns(() =>
            {
                return Task.FromResult(Generator.RandomString(25));
            }).Verifiable();

            await service.DeleteRequest<TestData>("test/path");
            httpService.VerifyAll();
            tokenService.VerifyAll();
            Assert.Equal("DELETE", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(passedRequest.Headers.Contains("Authorization"));
        }

        [Fact]
        public async void ShouldRequireValidUrlForDeleteRequest()
        {
            var httpService = new Mock<IHttpService>();
            var tokenService = new Mock<ITokenService>();

            // Forcing bad base url for error
            var baseUrl = $"http{Generator.RandomString(25)}";

            var service = new RequestService(httpService.Object, baseUrl, tokenService.Object);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await service.DeleteRequest<TestData>("test/path"));
        }

        [Fact]
        public async void ShouldNotAddAuthorizationWithNullTokenServiceForDeleteRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var service = new RequestService(httpService.Object, baseUrl, null);

            var passedRequest = default(HttpRequestMessage);

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpRequestMessage>())).Callback((HttpRequestMessage request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(new TestData());
            }).Verifiable();

            await service.DeleteRequest<TestData>("test/path");
            httpService.VerifyAll();
            Assert.Equal("DELETE", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(!passedRequest.Headers.Contains("Authorization"));
        }

        [Fact]
        public async void ShouldNotAddAuthorizationWithNullTokenFromTokenServiceForDeleteRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var tokenService = new Mock<ITokenService>();
            var service = new RequestService(httpService.Object, baseUrl, tokenService.Object);

            var passedRequest = default(HttpRequestMessage);

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpRequestMessage>())).Callback((HttpRequestMessage request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(new TestData());
            }).Verifiable();

            tokenService.Setup(x => x.GetOAuthTokenAsync()).Returns(() =>
            {
                return Task.FromResult((string)null);
            }).Verifiable();

            await service.DeleteRequest<TestData>("test/path");
            httpService.VerifyAll();
            Assert.Equal("DELETE", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(!passedRequest.Headers.Contains("Authorization"));
        }

        [Fact]
        public async void ShouldNotAddAuthorizationWithEmptyTokenFromTokenServiceForDeleteRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var tokenService = new Mock<ITokenService>();
            var service = new RequestService(httpService.Object, baseUrl, tokenService.Object);

            var passedRequest = default(HttpRequestMessage);

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpRequestMessage>())).Callback((HttpRequestMessage request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(new TestData());
            }).Verifiable();

            tokenService.Setup(x => x.GetOAuthTokenAsync()).Returns(() =>
            {
                return Task.FromResult(string.Empty);
            }).Verifiable();

            await service.DeleteRequest<TestData>("test/path");
            httpService.VerifyAll();
            Assert.Equal("DELETE", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(!passedRequest.Headers.Contains("Authorization"));
        }

        #endregion

        #region Put Data

        [Fact]
        public async void ShouldSendPutRequest()
        {
            var httpService = new Mock<IHttpService>();
            var tokenService = new Mock<ITokenService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var service = new RequestService(httpService.Object, baseUrl, tokenService.Object);

            var passedRequest = default(HttpRequestMessage);

            var data = new TestData()
            {
                Id = 5,
                Data = Generator.RandomString(15)
            };

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpRequestMessage>())).Callback((HttpRequestMessage request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(data);
            }).Verifiable();

            tokenService.Setup(x => x.GetOAuthTokenAsync()).Returns(() =>
            {
                return Task.FromResult(Generator.RandomString(25));
            }).Verifiable();

            await service.PutRequest<TestData>("test/path", data);
            httpService.VerifyAll();
            tokenService.VerifyAll();
            Assert.Equal("PUT", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(passedRequest.Headers.Contains("Authorization"));
            Assert.Equal("application/json", passedRequest.Content.Headers.ContentType.MediaType);
            Assert.Equal("utf-8", passedRequest.Content.Headers.ContentType.CharSet);
            Assert.Equal(JsonConvert.SerializeObject(data), await passedRequest.Content.ReadAsStringAsync());
            Assert.Equal(33, passedRequest.Content.Headers.ContentLength);
        }

        [Fact]
        public async void ShouldRequireValidUrlForPutRequest()
        {
            var httpService = new Mock<IHttpService>();
            var tokenService = new Mock<ITokenService>();

            // Forcing bad base url for error
            var baseUrl = $"http{Generator.RandomString(25)}";

            var service = new RequestService(httpService.Object, baseUrl, tokenService.Object);
            var data = new TestData()
            {
                Id = 5,
                Data = Generator.RandomString(15)
            };

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await service.PutRequest<TestData>("test/path", data));
        }

        [Fact]
        public async void ShouldNotAddAuthorizationWithNullTokenServiceForPutRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var service = new RequestService(httpService.Object, baseUrl, null);

            var passedRequest = default(HttpRequestMessage);
            var data = new TestData()
            {
                Id = 5,
                Data = Generator.RandomString(15)
            };

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpRequestMessage>())).Callback((HttpRequestMessage request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(data);
            }).Verifiable();

            await service.PutRequest<TestData>("test/path", data);

            httpService.VerifyAll();
            Assert.Equal("PUT", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(!passedRequest.Headers.Contains("Authorization"));
            Assert.Equal("application/json", passedRequest.Content.Headers.ContentType.MediaType);
            Assert.Equal("utf-8", passedRequest.Content.Headers.ContentType.CharSet);
            Assert.Equal(JsonConvert.SerializeObject(data), await passedRequest.Content.ReadAsStringAsync());
            Assert.Equal(33, passedRequest.Content.Headers.ContentLength);
        }

        [Fact]
        public async void ShouldNotAddAuthorizationWithNullTokenFromTokenServiceForPutRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var tokenService = new Mock<ITokenService>();
            var service = new RequestService(httpService.Object, baseUrl, tokenService.Object);

            var passedRequest = default(HttpRequestMessage);
            var data = new TestData()
            {
                Id = 5,
                Data = Generator.RandomString(15)
            };

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpRequestMessage>())).Callback((HttpRequestMessage request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(data);
            }).Verifiable();

            tokenService.Setup(x => x.GetOAuthTokenAsync()).Returns(() =>
            {
                return Task.FromResult((string)null);
            }).Verifiable();

            await service.PutRequest<TestData>("test/path", data);
            httpService.VerifyAll();
            Assert.Equal("PUT", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(!passedRequest.Headers.Contains("Authorization"));
            Assert.Equal("application/json", passedRequest.Content.Headers.ContentType.MediaType);
            Assert.Equal("utf-8", passedRequest.Content.Headers.ContentType.CharSet);
            Assert.Equal(JsonConvert.SerializeObject(data), await passedRequest.Content.ReadAsStringAsync());
            Assert.Equal(33, passedRequest.Content.Headers.ContentLength);
        }

        [Fact]
        public async void ShouldNotAddAuthorizationWithEmptyTokenFromTokenServiceForPutRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var tokenService = new Mock<ITokenService>();
            var service = new RequestService(httpService.Object, baseUrl, tokenService.Object);

            var passedRequest = default(HttpRequestMessage);
            var data = new TestData()
            {
                Id = 5,
                Data = Generator.RandomString(15)
            };

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpRequestMessage>())).Callback((HttpRequestMessage request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(data);
            }).Verifiable();

            tokenService.Setup(x => x.GetOAuthTokenAsync()).Returns(() =>
            {
                return Task.FromResult(string.Empty);
            }).Verifiable();

            await service.PutRequest<TestData>("test/path", data);
            httpService.VerifyAll();
            Assert.Equal("PUT", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(!passedRequest.Headers.Contains("Authorization"));
            Assert.Equal("application/json", passedRequest.Content.Headers.ContentType.MediaType);
            Assert.Equal("utf-8", passedRequest.Content.Headers.ContentType.CharSet);
            Assert.Equal(JsonConvert.SerializeObject(data), await passedRequest.Content.ReadAsStringAsync());
            Assert.Equal(33, passedRequest.Content.Headers.ContentLength);
        }

        [Fact]
        public async void ShouldAllowNullDataForPutRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var tokenService = new Mock<ITokenService>();
            var service = new RequestService(httpService.Object, baseUrl, tokenService.Object);

            var passedRequest = default(HttpRequestMessage);
            var data = default(TestData);

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpRequestMessage>())).Callback((HttpRequestMessage request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(data);
            }).Verifiable();

            tokenService.Setup(x => x.GetOAuthTokenAsync()).Returns(() =>
            {
                return Task.FromResult(string.Empty);
            }).Verifiable();

            await service.PutRequest<TestData>("test/path", data);
            httpService.VerifyAll();
            Assert.Equal("PUT", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(!passedRequest.Headers.Contains("Authorization"));
            Assert.Equal("application/json", passedRequest.Content.Headers.ContentType.MediaType);
            Assert.Equal("utf-8", passedRequest.Content.Headers.ContentType.CharSet);
            Assert.Equal(JsonConvert.SerializeObject(data), await passedRequest.Content.ReadAsStringAsync());
            Assert.Equal(4, passedRequest.Content.Headers.ContentLength);
        }

        #endregion

        #region Post Data

        [Fact]
        public async void ShouldSendPostRequest()
        {
            var httpService = new Mock<IHttpService>();
            var tokenService = new Mock<ITokenService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var service = new RequestService(httpService.Object, baseUrl, tokenService.Object);

            var passedRequest = default(HttpRequestMessage);

            var data = new TestData()
            {
                Id = 5,
                Data = Generator.RandomString(15)
            };

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpRequestMessage>())).Callback((HttpRequestMessage request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(data);
            }).Verifiable();

            tokenService.Setup(x => x.GetOAuthTokenAsync()).Returns(() =>
            {
                return Task.FromResult(Generator.RandomString(25));
            }).Verifiable();

            await service.PostRequest<TestData>("test/path", data);
            httpService.VerifyAll();
            tokenService.VerifyAll();
            Assert.Equal("POST", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(passedRequest.Headers.Contains("Authorization"));
            Assert.Equal("application/json", passedRequest.Content.Headers.ContentType.MediaType);
            Assert.Equal("utf-8", passedRequest.Content.Headers.ContentType.CharSet);
            Assert.Equal(JsonConvert.SerializeObject(data), await passedRequest.Content.ReadAsStringAsync());
            Assert.Equal(33, passedRequest.Content.Headers.ContentLength);
        }

        [Fact]
        public async void ShouldSendPostRequestWithJsonData()
        {
            var httpService = new Mock<IHttpService>();
            var tokenService = new Mock<ITokenService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var service = new RequestService(httpService.Object, baseUrl, tokenService.Object);

            var passedRequest = default(HttpRequestMessage);

            var data = new TestData()
            {
                Id = 5,
                Data = Generator.RandomString(15)
            };

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpRequestMessage>())).Callback((HttpRequestMessage request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(data);
            }).Verifiable();

            tokenService.Setup(x => x.GetOAuthTokenAsync()).Returns(() =>
            {
                return Task.FromResult(Generator.RandomString(25));
            }).Verifiable();

            await service.PostRequest<TestData>("test/path", data, Enums.RequestDataFormat.Json);
            httpService.VerifyAll();
            tokenService.VerifyAll();
            Assert.Equal("POST", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(passedRequest.Headers.Contains("Authorization"));
            Assert.Equal("application/json", passedRequest.Content.Headers.ContentType.MediaType);
            Assert.Equal("utf-8", passedRequest.Content.Headers.ContentType.CharSet);
            Assert.Equal(JsonConvert.SerializeObject(data), await passedRequest.Content.ReadAsStringAsync());
            Assert.Equal(33, passedRequest.Content.Headers.ContentLength);
        }

        [Fact]
        public async void ShouldSendPostRequestWithFormData()
        {
            var httpService = new Mock<IHttpService>();
            var tokenService = new Mock<ITokenService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var service = new RequestService(httpService.Object, baseUrl, tokenService.Object);

            var passedRequest = default(HttpRequestMessage);

            var data = new TestData()
            {
                Id = 5,
                Data = Generator.RandomString(15)
            };

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpRequestMessage>())).Callback((HttpRequestMessage request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(data);
            }).Verifiable();

            tokenService.Setup(x => x.GetOAuthTokenAsync()).Returns(() =>
            {
                return Task.FromResult(Generator.RandomString(25));
            }).Verifiable();

            await service.PostRequest<TestData>("test/path", data, Enums.RequestDataFormat.Form);
            httpService.VerifyAll();
            tokenService.VerifyAll();
            Assert.Equal("POST", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(passedRequest.Headers.Contains("Authorization"));
            Assert.Equal("application/x-www-form-urlencoded", passedRequest.Content.Headers.ContentType.MediaType);
            Assert.Equal("utf-8", passedRequest.Content.Headers.ContentType.CharSet);
            Assert.Equal($"Id={data.Id}&Data={data.Data}", await passedRequest.Content.ReadAsStringAsync());
            Assert.Equal(25, passedRequest.Content.Headers.ContentLength);
        }

        [Fact]
        public async void ShouldRequireValidUrlForPostRequest()
        {
            var httpService = new Mock<IHttpService>();
            var tokenService = new Mock<ITokenService>();

            // Forcing bad base url for error
            var baseUrl = $"http{Generator.RandomString(25)}";

            var service = new RequestService(httpService.Object, baseUrl, tokenService.Object);
            var data = new TestData()
            {
                Id = 5,
                Data = Generator.RandomString(15)
            };

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await service.PostRequest<TestData>("test/path", data));
        }

        [Fact]
        public async void ShouldNotAddAuthorizationWithNullTokenServiceForPostRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var service = new RequestService(httpService.Object, baseUrl, null);

            var passedRequest = default(HttpRequestMessage);
            var data = new TestData()
            {
                Id = 5,
                Data = Generator.RandomString(15)
            };

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpRequestMessage>())).Callback((HttpRequestMessage request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(data);
            }).Verifiable();

            await service.PostRequest<TestData>("test/path", data);

            httpService.VerifyAll();
            Assert.Equal("POST", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(!passedRequest.Headers.Contains("Authorization"));
            Assert.Equal("application/json", passedRequest.Content.Headers.ContentType.MediaType);
            Assert.Equal("utf-8", passedRequest.Content.Headers.ContentType.CharSet);
            Assert.Equal(JsonConvert.SerializeObject(data), await passedRequest.Content.ReadAsStringAsync());
            Assert.Equal(33, passedRequest.Content.Headers.ContentLength);
        }

        [Fact]
        public async void ShouldNotAddAuthorizationWithNullTokenFromTokenServiceForPostRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var tokenService = new Mock<ITokenService>();
            var service = new RequestService(httpService.Object, baseUrl, tokenService.Object);

            var passedRequest = default(HttpRequestMessage);
            var data = new TestData()
            {
                Id = 5,
                Data = Generator.RandomString(15)
            };

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpRequestMessage>())).Callback((HttpRequestMessage request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(data);
            }).Verifiable();

            tokenService.Setup(x => x.GetOAuthTokenAsync()).Returns(() =>
            {
                return Task.FromResult((string)null);
            }).Verifiable();

            await service.PostRequest<TestData>("test/path", data);
            httpService.VerifyAll();
            Assert.Equal("POST", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(!passedRequest.Headers.Contains("Authorization"));
            Assert.Equal("application/json", passedRequest.Content.Headers.ContentType.MediaType);
            Assert.Equal("utf-8", passedRequest.Content.Headers.ContentType.CharSet);
            Assert.Equal(JsonConvert.SerializeObject(data), await passedRequest.Content.ReadAsStringAsync());
            Assert.Equal(33, passedRequest.Content.Headers.ContentLength);
        }

        [Fact]
        public async void ShouldNotAddAuthorizationWithEmptyTokenFromTokenServiceForPostRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var tokenService = new Mock<ITokenService>();
            var service = new RequestService(httpService.Object, baseUrl, tokenService.Object);

            var passedRequest = default(HttpRequestMessage);
            var data = new TestData()
            {
                Id = 5,
                Data = Generator.RandomString(15)
            };

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpRequestMessage>())).Callback((HttpRequestMessage request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(data);
            }).Verifiable();

            tokenService.Setup(x => x.GetOAuthTokenAsync()).Returns(() =>
            {
                return Task.FromResult(string.Empty);
            }).Verifiable();

            await service.PostRequest<TestData>("test/path", data);
            httpService.VerifyAll();
            Assert.Equal("POST", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(!passedRequest.Headers.Contains("Authorization"));
            Assert.Equal("application/json", passedRequest.Content.Headers.ContentType.MediaType);
            Assert.Equal("utf-8", passedRequest.Content.Headers.ContentType.CharSet);
            Assert.Equal(JsonConvert.SerializeObject(data), await passedRequest.Content.ReadAsStringAsync());
            Assert.Equal(33, passedRequest.Content.Headers.ContentLength);
        }

        [Fact]
        public async void ShouldAllowNullDataForPostRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var tokenService = new Mock<ITokenService>();
            var service = new RequestService(httpService.Object, baseUrl, tokenService.Object);

            var passedRequest = default(HttpRequestMessage);
            var data = default(TestData);

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpRequestMessage>())).Callback((HttpRequestMessage request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(data);
            }).Verifiable();

            tokenService.Setup(x => x.GetOAuthTokenAsync()).Returns(() =>
            {
                return Task.FromResult(string.Empty);
            }).Verifiable();

            await service.PostRequest<TestData>("test/path", data);

            httpService.VerifyAll();
            Assert.Equal("POST", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(!passedRequest.Headers.Contains("Authorization"));
            Assert.Equal("application/json", passedRequest.Content.Headers.ContentType.MediaType);
            Assert.Equal("utf-8", passedRequest.Content.Headers.ContentType.CharSet);
            Assert.Equal(JsonConvert.SerializeObject(data), await passedRequest.Content.ReadAsStringAsync());
            Assert.Equal(4, passedRequest.Content.Headers.ContentLength);
        }

        #endregion

        public class TestData
        {
            public int Id { get; set; }
            public string Data { get; set; }
        }
    }
}
