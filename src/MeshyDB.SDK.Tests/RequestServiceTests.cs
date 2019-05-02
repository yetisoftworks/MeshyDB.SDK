using MeshyDB.SDK.Models;
using MeshyDB.SDK.Services;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MeshyDB.SDK.Tests
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

        #region Get Request

        [Fact]
        public async void ShouldSendGetRequest()
        {
            var httpService = new Mock<IHttpService>();
            var tokenService = new Mock<ITokenService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var authenticationId = Generator.RandomString(10);
            var service = new RequestService(httpService.Object, baseUrl, null, tokenService.Object, authenticationId);

            var passedRequest = default(HttpServiceRequest);

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpServiceRequest>())).Callback((HttpServiceRequest request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(new TestData());
            }).Verifiable();

            tokenService.Setup(x => x.GetAccessTokenAsync(It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult(Generator.RandomString(25));
            }).Verifiable();

            await service.GetRequest<TestData>("test/path");
            httpService.VerifyAll();
            tokenService.VerifyAll();
            Assert.Equal("GET", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(passedRequest.Headers.ContainsKey("Authorization"));
        }

        [Fact]
        public async void ShouldRequireValidUrlForGetRequest()
        {
            var httpService = new Mock<IHttpService>();
            var tokenService = new Mock<ITokenService>();

            // Forcing bad base url for error
            var baseUrl = $"http{Generator.RandomString(25)}";

            var service = new RequestService(httpService.Object, baseUrl, null, tokenService.Object, null);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await service.GetRequest<TestData>("test/path"));
        }

        [Fact]
        public async void ShouldNotAddAuthorizationWithNullTokenServiceForGetRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var service = new RequestService(httpService.Object, baseUrl, null);

            var passedRequest = default(HttpServiceRequest);

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpServiceRequest>())).Callback((HttpServiceRequest request) =>
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
            Assert.True(!passedRequest.Headers.ContainsKey("Authorization"));
        }

        [Fact]
        public async void ShouldNotAddAuthorizationWithNullTokenFromTokenServiceForGetRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var tokenService = new Mock<ITokenService>();
            var authenticationId = Generator.RandomString(10);
            var service = new RequestService(httpService.Object, baseUrl, null, tokenService.Object, authenticationId);

            var passedRequest = default(HttpServiceRequest);

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpServiceRequest>())).Callback((HttpServiceRequest request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(new TestData());
            }).Verifiable();

            tokenService.Setup(x => x.GetAccessTokenAsync(It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult((string)null);
            }).Verifiable();

            await service.GetRequest<TestData>("test/path");
            httpService.VerifyAll();
            Assert.Equal("GET", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(!passedRequest.Headers.ContainsKey("Authorization"));
        }

        [Fact]
        public async void ShouldNotAddAuthorizationWithEmptyTokenFromTokenServiceForGetRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var tokenService = new Mock<ITokenService>();
            var authenticationId = Generator.RandomString(10);
            var service = new RequestService(httpService.Object, baseUrl, null, tokenService.Object, authenticationId);

            var passedRequest = default(HttpServiceRequest);

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpServiceRequest>())).Callback((HttpServiceRequest request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(new TestData());
            }).Verifiable();

            tokenService.Setup(x => x.GetAccessTokenAsync(It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult(string.Empty);
            }).Verifiable();

            await service.GetRequest<TestData>("test/path");
            httpService.VerifyAll();
            Assert.Equal("GET", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(!passedRequest.Headers.ContainsKey("Authorization"));
        }

        [Fact]
        public async void ShouldSendGetRequestWithNoExtraHeaders()
        {
            var httpService = new Mock<IHttpService>();
            var tokenService = new Mock<ITokenService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var authenticationId = Generator.RandomString(10);
            var service = new RequestService(httpService.Object, baseUrl, null, tokenService.Object, authenticationId);

            var passedRequest = default(HttpServiceRequest);

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpServiceRequest>())).Callback((HttpServiceRequest request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(new TestData());
            }).Verifiable();

            tokenService.Setup(x => x.GetAccessTokenAsync(It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult(Generator.RandomString(25));
            }).Verifiable();

            await service.GetRequest<TestData>("test/path", null);
            httpService.VerifyAll();
            tokenService.VerifyAll();
            Assert.Equal("GET", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(passedRequest.Headers.ContainsKey("Authorization"));
            Assert.Equal(1, passedRequest.Headers.Count);
        }

        [Fact]
        public async void ShouldSendGetRequestWithOneExtraHeaders()
        {
            var httpService = new Mock<IHttpService>();
            var tokenService = new Mock<ITokenService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var authenticationId = Generator.RandomString(10);
            var service = new RequestService(httpService.Object, baseUrl, null, tokenService.Object, authenticationId);

            var passedRequest = default(HttpServiceRequest);

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpServiceRequest>())).Callback((HttpServiceRequest request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(new TestData());
            }).Verifiable();

            tokenService.Setup(x => x.GetAccessTokenAsync(It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult(Generator.RandomString(25));
            }).Verifiable();

            var headers = new Dictionary<string, string>();
            headers.Add("Test", "Test");

            await service.GetRequest<TestData>("test/path", headers);
            httpService.VerifyAll();
            tokenService.VerifyAll();
            Assert.Equal("GET", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(passedRequest.Headers.ContainsKey("Authorization"));
            Assert.True(passedRequest.Headers.ContainsKey("Test"));
            Assert.Equal(2, passedRequest.Headers.Count);
        }

        [Fact]
        public async void ShouldSendGetRequestAndOverwriteAuthorizationWithSameHeaderKey()
        {
            var httpService = new Mock<IHttpService>();
            var tokenService = new Mock<ITokenService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var authenticationId = Generator.RandomString(10);
            var service = new RequestService(httpService.Object, baseUrl, null, tokenService.Object, authenticationId);

            var passedRequest = default(HttpServiceRequest);

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpServiceRequest>())).Callback((HttpServiceRequest request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(new TestData());
            }).Verifiable();

            tokenService.Setup(x => x.GetAccessTokenAsync(It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult(Generator.RandomString(25));
            }).Verifiable();

            var headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Test");

            await service.GetRequest<TestData>("test/path", headers);
            httpService.VerifyAll();
            tokenService.VerifyAll();
            Assert.Equal("GET", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(passedRequest.Headers.ContainsKey("Authorization"));
            Assert.Equal("Test", passedRequest.Headers["Authorization"]);
            Assert.Equal(1, passedRequest.Headers.Count);
        }
        
        [Fact]
        public async void ShouldNotAddTenantWithNullTenantForGetRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var service = new RequestService(httpService.Object, baseUrl, null);

            var passedRequest = default(HttpServiceRequest);

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpServiceRequest>())).Callback((HttpServiceRequest request) =>
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
            Assert.True(!passedRequest.Headers.ContainsKey("tenant"));
            Assert.Equal(0, passedRequest.Headers.Count);
        }

        [Fact]
        public async void ShouldAddTenantWithTenantForGetRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var tenant = Generator.RandomString(25);
            var service = new RequestService(httpService.Object, baseUrl, tenant);

            var passedRequest = default(HttpServiceRequest);

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpServiceRequest>())).Callback((HttpServiceRequest request) =>
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
            Assert.True(passedRequest.Headers.ContainsKey("tenant"));
            Assert.Equal(tenant, passedRequest.Headers["tenant"]);
            Assert.Equal(1, passedRequest.Headers.Count);
        }

        [Fact]
        public async void ShouldOverwriteTenantWithTenantForGetRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var tenant = Generator.RandomString(25);
            var service = new RequestService(httpService.Object, baseUrl, tenant);

            var passedRequest = default(HttpServiceRequest);

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpServiceRequest>())).Callback((HttpServiceRequest request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(new TestData());
            }).Verifiable();
            var tenantOverwrite = Generator.RandomString(25);
            await service.GetRequest<TestData>("test/path", new Dictionary<string, string>() { { "tenant", tenantOverwrite } });
            httpService.VerifyAll();
            Assert.Equal("GET", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(passedRequest.Headers.ContainsKey("tenant"));
            Assert.Equal(tenantOverwrite, passedRequest.Headers["tenant"]);
            Assert.Equal(1, passedRequest.Headers.Count);
        }

        #endregion

        #region Delete Request

        [Fact]
        public async void ShouldSendDeleteRequest()
        {
            var httpService = new Mock<IHttpService>();
            var tokenService = new Mock<ITokenService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var authenticationId = Generator.RandomString(10);
            var service = new RequestService(httpService.Object, baseUrl, null, tokenService.Object, authenticationId);

            var passedRequest = default(HttpServiceRequest);

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpServiceRequest>())).Callback((HttpServiceRequest request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(new TestData());
            }).Verifiable();

            tokenService.Setup(x => x.GetAccessTokenAsync(It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult(Generator.RandomString(25));
            }).Verifiable();

            await service.DeleteRequest<TestData>("test/path");
            httpService.VerifyAll();
            tokenService.VerifyAll();
            Assert.Equal("DELETE", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(passedRequest.Headers.ContainsKey("Authorization"));
        }

        [Fact]
        public async void ShouldRequireValidUrlForDeleteRequest()
        {
            var httpService = new Mock<IHttpService>();
            var tokenService = new Mock<ITokenService>();

            // Forcing bad base url for error
            var baseUrl = $"http{Generator.RandomString(25)}";

            var service = new RequestService(httpService.Object, baseUrl, null, tokenService.Object, null);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await service.DeleteRequest<TestData>("test/path"));
        }

        [Fact]
        public async void ShouldNotAddAuthorizationWithNullTokenServiceForDeleteRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var service = new RequestService(httpService.Object, baseUrl, null);

            var passedRequest = default(HttpServiceRequest);

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpServiceRequest>())).Callback((HttpServiceRequest request) =>
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
            Assert.True(!passedRequest.Headers.ContainsKey("Authorization"));
        }

        [Fact]
        public async void ShouldNotAddAuthorizationWithNullTokenFromTokenServiceForDeleteRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var tokenService = new Mock<ITokenService>();
            var authenticationId = Generator.RandomString(10);
            var service = new RequestService(httpService.Object, baseUrl, null, tokenService.Object, authenticationId);

            var passedRequest = default(HttpServiceRequest);

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpServiceRequest>())).Callback((HttpServiceRequest request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(new TestData());
            }).Verifiable();

            tokenService.Setup(x => x.GetAccessTokenAsync(It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult((string)null);
            }).Verifiable();

            await service.DeleteRequest<TestData>("test/path");
            httpService.VerifyAll();
            Assert.Equal("DELETE", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(!passedRequest.Headers.ContainsKey("Authorization"));
        }

        [Fact]
        public async void ShouldNotAddAuthorizationWithEmptyTokenFromTokenServiceForDeleteRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var tokenService = new Mock<ITokenService>();
            var authenticationId = Generator.RandomString(10);
            var service = new RequestService(httpService.Object, baseUrl, null, tokenService.Object, authenticationId);

            var passedRequest = default(HttpServiceRequest);

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpServiceRequest>())).Callback((HttpServiceRequest request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(new TestData());
            }).Verifiable();

            tokenService.Setup(x => x.GetAccessTokenAsync(authenticationId)).Returns(() =>
            {
                return Task.FromResult(string.Empty);
            }).Verifiable();

            await service.DeleteRequest<TestData>("test/path");
            httpService.VerifyAll();
            Assert.Equal("DELETE", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(!passedRequest.Headers.ContainsKey("Authorization"));
        }

        #endregion

        #region Put Data

        [Fact]
        public async void ShouldSendPutRequest()
        {
            var httpService = new Mock<IHttpService>();
            var tokenService = new Mock<ITokenService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var authenticationId = Generator.RandomString(10);
            var service = new RequestService(httpService.Object, baseUrl, null, tokenService.Object, authenticationId);

            var passedRequest = default(HttpServiceRequest);

            var data = new TestData()
            {
                Id = 5,
                Data = Generator.RandomString(15)
            };

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpServiceRequest>())).Callback((HttpServiceRequest request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(data);
            }).Verifiable();

            tokenService.Setup(x => x.GetAccessTokenAsync(It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult(Generator.RandomString(25));
            }).Verifiable();

            await service.PutRequest<TestData>("test/path", data);
            httpService.VerifyAll();
            tokenService.VerifyAll();
            Assert.Equal("PUT", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(passedRequest.Headers.ContainsKey("Authorization"));
            Assert.Equal("application/json", passedRequest.ContentType);
            Assert.Equal(JsonConvert.SerializeObject(data), passedRequest.Content);
        }

        [Fact]
        public async void ShouldRequireValidUrlForPutRequest()
        {
            var httpService = new Mock<IHttpService>();
            var tokenService = new Mock<ITokenService>();

            // Forcing bad base url for error
            var baseUrl = $"http{Generator.RandomString(25)}";

            var service = new RequestService(httpService.Object, baseUrl, null, tokenService.Object, null);
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

            var passedRequest = default(HttpServiceRequest);
            var data = new TestData()
            {
                Id = 5,
                Data = Generator.RandomString(15)
            };

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpServiceRequest>())).Callback((HttpServiceRequest request) =>
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
            Assert.True(!passedRequest.Headers.ContainsKey("Authorization"));
            Assert.Equal("application/json", passedRequest.ContentType);
            Assert.Equal(JsonConvert.SerializeObject(data), passedRequest.Content);
        }

        [Fact]
        public async void ShouldNotAddAuthorizationWithNullTokenFromTokenServiceForPutRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var tokenService = new Mock<ITokenService>();
            var authenticationId = Generator.RandomString(10);
            var service = new RequestService(httpService.Object, baseUrl, null, tokenService.Object, authenticationId);

            var passedRequest = default(HttpServiceRequest);
            var data = new TestData()
            {
                Id = 5,
                Data = Generator.RandomString(15)
            };

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpServiceRequest>())).Callback((HttpServiceRequest request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(data);
            }).Verifiable();

            tokenService.Setup(x => x.GetAccessTokenAsync(It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult((string)null);
            }).Verifiable();

            await service.PutRequest<TestData>("test/path", data);
            httpService.VerifyAll();
            Assert.Equal("PUT", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(!passedRequest.Headers.ContainsKey("Authorization"));
            Assert.Equal("application/json", passedRequest.ContentType);
            Assert.Equal(JsonConvert.SerializeObject(data), passedRequest.Content);
        }

        [Fact]
        public async void ShouldNotAddAuthorizationWithEmptyTokenFromTokenServiceForPutRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var tokenService = new Mock<ITokenService>();
            var authenticationId = Generator.RandomString(10);
            var service = new RequestService(httpService.Object, baseUrl, null, tokenService.Object, authenticationId);

            var passedRequest = default(HttpServiceRequest);
            var data = new TestData()
            {
                Id = 5,
                Data = Generator.RandomString(15)
            };

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpServiceRequest>())).Callback((HttpServiceRequest request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(data);
            }).Verifiable();

            tokenService.Setup(x => x.GetAccessTokenAsync(It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult(string.Empty);
            }).Verifiable();

            await service.PutRequest<TestData>("test/path", data);
            httpService.VerifyAll();
            Assert.Equal("PUT", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(!passedRequest.Headers.ContainsKey("Authorization"));
            Assert.Equal("application/json", passedRequest.ContentType);
            Assert.Equal(JsonConvert.SerializeObject(data), passedRequest.Content);
        }

        [Fact]
        public async void ShouldAllowNullDataForPutRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var tokenService = new Mock<ITokenService>();
            var authenticationId = Generator.RandomString(10);
            var service = new RequestService(httpService.Object, baseUrl, null, tokenService.Object, authenticationId);

            var passedRequest = default(HttpServiceRequest);
            var data = default(TestData);

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpServiceRequest>())).Callback((HttpServiceRequest request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(data);
            }).Verifiable();

            tokenService.Setup(x => x.GetAccessTokenAsync(It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult(string.Empty);
            }).Verifiable();

            await service.PutRequest<TestData>("test/path", data);
            httpService.VerifyAll();
            Assert.Equal("PUT", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(!passedRequest.Headers.ContainsKey("Authorization"));
            Assert.Equal("application/json", passedRequest.ContentType);
            Assert.Equal(JsonConvert.SerializeObject(data), passedRequest.Content);
        }

        #endregion

        #region Post Data

        [Fact]
        public async void ShouldSendPostRequest()
        {
            var httpService = new Mock<IHttpService>();
            var tokenService = new Mock<ITokenService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var authenticationId = Generator.RandomString(10);
            var service = new RequestService(httpService.Object, baseUrl, null, tokenService.Object, authenticationId);

            var passedRequest = default(HttpServiceRequest);

            var data = new TestData()
            {
                Id = 5,
                Data = Generator.RandomString(15)
            };

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpServiceRequest>())).Callback((HttpServiceRequest request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(data);
            }).Verifiable();

            tokenService.Setup(x => x.GetAccessTokenAsync(It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult(Generator.RandomString(25));
            }).Verifiable();

            await service.PostRequest<TestData>("test/path", data);
            httpService.VerifyAll();
            tokenService.VerifyAll();
            Assert.Equal("POST", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(passedRequest.Headers.ContainsKey("Authorization"));
            Assert.Equal("application/json", passedRequest.ContentType);
            Assert.Equal(JsonConvert.SerializeObject(data), passedRequest.Content);
        }

        [Fact]
        public async void ShouldSendPostRequestWithJsonData()
        {
            var httpService = new Mock<IHttpService>();
            var tokenService = new Mock<ITokenService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var authenticationId = Generator.RandomString(10);
            var service = new RequestService(httpService.Object, baseUrl, null, tokenService.Object, authenticationId);

            var passedRequest = default(HttpServiceRequest);

            var data = new TestData()
            {
                Id = 5,
                Data = Generator.RandomString(15)
            };

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpServiceRequest>())).Callback((HttpServiceRequest request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(data);
            }).Verifiable();

            tokenService.Setup(x => x.GetAccessTokenAsync(It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult(Generator.RandomString(25));
            }).Verifiable();

            await service.PostRequest<TestData>("test/path", data, Enums.RequestDataFormat.Json);
            httpService.VerifyAll();
            tokenService.VerifyAll();
            Assert.Equal("POST", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(passedRequest.Headers.ContainsKey("Authorization"));
            Assert.Equal("application/json", passedRequest.ContentType);
            Assert.Equal(JsonConvert.SerializeObject(data), passedRequest.Content);
        }

        [Fact]
        public async void ShouldSendPostRequestWithFormData()
        {
            var httpService = new Mock<IHttpService>();
            var tokenService = new Mock<ITokenService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var authenticationId = Generator.RandomString(10);
            var service = new RequestService(httpService.Object, baseUrl, null, tokenService.Object, authenticationId);

            var passedRequest = default(HttpServiceRequest);

            var data = new TestData()
            {
                Id = 5,
                Data = Generator.RandomString(15)
            };

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpServiceRequest>())).Callback((HttpServiceRequest request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(data);
            }).Verifiable();

            tokenService.Setup(x => x.GetAccessTokenAsync(It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult(Generator.RandomString(25));
            }).Verifiable();

            await service.PostRequest<TestData>("test/path", data, Enums.RequestDataFormat.Form);
            httpService.VerifyAll();
            tokenService.VerifyAll();
            Assert.Equal("POST", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(passedRequest.Headers.ContainsKey("Authorization"));
            Assert.Equal("application/x-www-form-urlencoded", passedRequest.ContentType);
            Assert.Equal($"Id={data.Id}&Data={data.Data}", passedRequest.Content);
        }

        [Fact]
        public async void ShouldRequireValidUrlForPostRequest()
        {
            var httpService = new Mock<IHttpService>();
            var tokenService = new Mock<ITokenService>();

            // Forcing bad base url for error
            var baseUrl = $"http{Generator.RandomString(25)}";

            var service = new RequestService(httpService.Object, baseUrl, null, tokenService.Object, null);
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

            var passedRequest = default(HttpServiceRequest);
            var data = new TestData()
            {
                Id = 5,
                Data = Generator.RandomString(15)
            };

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpServiceRequest>())).Callback((HttpServiceRequest request) =>
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
            Assert.True(!passedRequest.Headers.ContainsKey("Authorization"));
            Assert.Equal("application/json", passedRequest.ContentType);
            Assert.Equal(JsonConvert.SerializeObject(data), passedRequest.Content);
        }

        [Fact]
        public async void ShouldNotAddAuthorizationWithNullTokenFromTokenServiceForPostRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var tokenService = new Mock<ITokenService>();
            var authenticationId = Generator.RandomString(10);
            var service = new RequestService(httpService.Object, baseUrl, null, tokenService.Object, authenticationId);

            var passedRequest = default(HttpServiceRequest);
            var data = new TestData()
            {
                Id = 5,
                Data = Generator.RandomString(15)
            };

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpServiceRequest>())).Callback((HttpServiceRequest request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(data);
            }).Verifiable();

            tokenService.Setup(x => x.GetAccessTokenAsync(It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult((string)null);
            }).Verifiable();

            await service.PostRequest<TestData>("test/path", data);
            httpService.VerifyAll();
            Assert.Equal("POST", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(!passedRequest.Headers.ContainsKey("Authorization"));
            Assert.Equal("application/json", passedRequest.ContentType);
            Assert.Equal(JsonConvert.SerializeObject(data), passedRequest.Content);
        }

        [Fact]
        public async void ShouldNotAddAuthorizationWithEmptyTokenFromTokenServiceForPostRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var tokenService = new Mock<ITokenService>();
            var authenticationId = Generator.RandomString(10);
            var service = new RequestService(httpService.Object, baseUrl, null, tokenService.Object, authenticationId);

            var passedRequest = default(HttpServiceRequest);
            var data = new TestData()
            {
                Id = 5,
                Data = Generator.RandomString(15)
            };

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpServiceRequest>())).Callback((HttpServiceRequest request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(data);
            }).Verifiable();

            tokenService.Setup(x => x.GetAccessTokenAsync(It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult(string.Empty);
            }).Verifiable();

            await service.PostRequest<TestData>("test/path", data);
            httpService.VerifyAll();
            Assert.Equal("POST", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(!passedRequest.Headers.ContainsKey("Authorization"));
            Assert.Equal("application/json", passedRequest.ContentType);
            Assert.Equal(JsonConvert.SerializeObject(data), passedRequest.Content);
        }

        [Fact]
        public async void ShouldAllowNullDataForPostRequest()
        {
            var httpService = new Mock<IHttpService>();
            var baseUrl = $"http://{Generator.RandomString(25)}";
            var tokenService = new Mock<ITokenService>();
            var authenticationId = Generator.RandomString(10);
            var service = new RequestService(httpService.Object, baseUrl, null, tokenService.Object, authenticationId);

            var passedRequest = default(HttpServiceRequest);
            var data = default(TestData);

            httpService.Setup(x => x.SendRequestAsync<TestData>(It.IsAny<HttpServiceRequest>())).Callback((HttpServiceRequest request) =>
            {
                passedRequest = request;
            }).Returns(() =>
            {
                return Task.FromResult(data);
            }).Verifiable();

            tokenService.Setup(x => x.GetAccessTokenAsync(It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult(string.Empty);
            }).Verifiable();

            await service.PostRequest<TestData>("test/path", data);

            httpService.VerifyAll();
            Assert.Equal("POST", passedRequest.Method.Method);
            Assert.Equal($"{baseUrl}/test/path".ToLower(), passedRequest.RequestUri.ToString());
            Assert.True(!passedRequest.Headers.ContainsKey("Authorization"));
            Assert.Equal("application/json", passedRequest.ContentType);
            Assert.Equal(JsonConvert.SerializeObject(data), passedRequest.Content);
        }

        #endregion

        public class TestData
        {
            public int Id { get; set; }
            public string Data { get; set; }
        }
    }
}
