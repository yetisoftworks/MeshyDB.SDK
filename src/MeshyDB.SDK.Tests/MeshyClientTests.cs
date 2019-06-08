using MeshyDB.SDK.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MeshyDB.SDK.Tests
{
    public class MeshyClientTests
    {
        [Fact]
        public void ShouldCreate()
        {
            var tokenService = new Mock<ITokenService>();
            var requestService = new Mock<IRequestService>();

            var client = new MeshyClient(tokenService.Object, requestService.Object, Generator.RandomString(10));

            Assert.NotNull(client);
        }

        [Fact]
        public void ShouldMeshesIsNotNull()
        {
            var tokenService = new Mock<ITokenService>();
            var requestService = new Mock<IRequestService>();

            var client = new MeshyClient(tokenService.Object, requestService.Object, Generator.RandomString(10));

            Assert.NotNull(client.Meshes);
        }

        [Fact]
        public void ShouldUsersIsNotNull()
        {
            var tokenService = new Mock<ITokenService>();
            var requestService = new Mock<IRequestService>();

            var client = new MeshyClient(tokenService.Object, requestService.Object, Generator.RandomString(10));

            Assert.NotNull(client.Meshes);
        }

        [Fact]
        public void ShouldThrowExceptionIfTokenServiceIsNull()
        {
            var requestService = new Mock<IRequestService>();

            Assert.Throws<ArgumentNullException>(() => new MeshyClient(null, requestService.Object, Generator.RandomString(10)));
        }

        [Fact]
        public void ShouldThrowExceptionIfRequestServiceIsNull()
        {
            var tokenService = new Mock<ITokenService>();

            Assert.Throws<ArgumentNullException>(() => new MeshyClient(tokenService.Object, null, Generator.RandomString(10)));
        }

        [Fact]
        public void ShouldThrowExceptionIfAuthenticationIdIsNull()
        {
            var tokenService = new Mock<ITokenService>();
            var requestService = new Mock<IRequestService>();

            Assert.Throws<ArgumentNullException>(() => new MeshyClient(tokenService.Object, requestService.Object, null));
        }

        [Fact]
        public void ShouldSetAuthenticationIdDuringCreation()
        {
            var tokenService = new Mock<ITokenService>();
            var requestService = new Mock<IRequestService>();

            var authenticationId = Generator.RandomString(10);
            var client = new MeshyClient(tokenService.Object, requestService.Object, authenticationId);

            Assert.Equal(authenticationId, client.AuthenticationId);
        }

        [Fact]
        public void ShouldUpdatePasswordAsyncSucessfully()
        {
            var tokenService = new Mock<ITokenService>();
            var requestService = new Mock<IRequestService>();
            var authenticationId = Generator.RandomString(10);

            var client = new MeshyClient(tokenService.Object, requestService.Object, authenticationId);

            var passedPreviousPassword = string.Empty;
            var passedNewPassword = string.Empty;
            var authenticationService = new Mock<IAuthenticationService>();
            authenticationService.Setup(x => x.UpdatePasswordAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((previousPassword, newPassword) =>
                {
                    passedPreviousPassword = previousPassword;
                    passedNewPassword = newPassword;
                })
                .Returns(() =>
                {
                    return Task.FromResult<object>(null);
                })
                .Verifiable();

            client.AuthenticationService = authenticationService.Object;

            var generatedPreviousPassword = Generator.RandomString(10);
            var generatedNewPassword = Generator.RandomString(10);

            client.UpdatePasswordAsync(generatedPreviousPassword, generatedNewPassword).ConfigureAwait(true).GetAwaiter().GetResult();

            Assert.Equal(generatedPreviousPassword, passedPreviousPassword);
            Assert.Equal(generatedNewPassword, passedNewPassword);
            authenticationService.VerifyAll();
        }

        [Fact]
        public void ShouldUpdatePasswordSucessfully()
        {
            var tokenService = new Mock<ITokenService>();
            var requestService = new Mock<IRequestService>();
            var authenticationId = Generator.RandomString(10);

            var client = new MeshyClient(tokenService.Object, requestService.Object, authenticationId);

            var passedPreviousPassword = string.Empty;
            var passedNewPassword = string.Empty;
            var authenticationService = new Mock<IAuthenticationService>();
            authenticationService.Setup(x => x.UpdatePasswordAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((previousPassword, newPassword) =>
                {
                    passedPreviousPassword = previousPassword;
                    passedNewPassword = newPassword;
                })
                .Returns(() =>
                {
                    return Task.FromResult<object>(null);
                })
                .Verifiable();

            client.AuthenticationService = authenticationService.Object;

            var generatedPreviousPassword = Generator.RandomString(10);
            var generatedNewPassword = Generator.RandomString(10);

            client.UpdatePassword(generatedPreviousPassword, generatedNewPassword);

            Assert.Equal(generatedPreviousPassword, passedPreviousPassword);
            Assert.Equal(generatedNewPassword, passedNewPassword);
            authenticationService.VerifyAll();
        }

        [Fact]
        public void ShouldSignoutAsyncSucessfully()
        {
            var tokenService = new Mock<ITokenService>();
            var requestService = new Mock<IRequestService>();
            var generatedAuthenticationId = Generator.RandomString(10);

            var passedAuthenticationId = string.Empty;

            tokenService.Setup(x => x.SignoutAsync(It.IsAny<string>()))
                .Callback<string>((authenticationId) =>
                {
                    passedAuthenticationId = authenticationId;
                })
                .Returns(() =>
                {
                    return Task.FromResult<object>(null);
                })
                .Verifiable();

            var client = new MeshyClient(tokenService.Object, requestService.Object, generatedAuthenticationId);

            client.SignoutAsync().ConfigureAwait(true).GetAwaiter().GetResult();

            Assert.Equal(generatedAuthenticationId, passedAuthenticationId);
            tokenService.VerifyAll();
        }

        [Fact]
        public void ShouldSignoutSucessfully()
        {
            var tokenService = new Mock<ITokenService>();
            var requestService = new Mock<IRequestService>();
            var generatedAuthenticationId = Generator.RandomString(10);

            var passedAuthenticationId = string.Empty;

            tokenService.Setup(x => x.SignoutAsync(It.IsAny<string>()))
                .Callback<string>((authenticationId) =>
                {
                    passedAuthenticationId = authenticationId;
                })
                .Returns(() =>
                {
                    return Task.FromResult<object>(null);
                })
                .Verifiable();

            var client = new MeshyClient(tokenService.Object, requestService.Object, generatedAuthenticationId);

            client.Signout();

            Assert.Equal(generatedAuthenticationId, passedAuthenticationId);
            tokenService.VerifyAll();
        }

        [Fact]
        public void ShouldRetrievePersistanceTokenAsyncSucessfully()
        {
            var tokenService = new Mock<ITokenService>();
            var requestService = new Mock<IRequestService>();
            var generatedAuthenticationId = Generator.RandomString(10);

            var client = new MeshyClient(tokenService.Object, requestService.Object, generatedAuthenticationId);

            var passedAuthenticationId = string.Empty;

            var authenticationService = new Mock<IAuthenticationService>();
            authenticationService.Setup(x => x.RetrievePersistanceTokenAsync(It.IsAny<string>()))
                .Callback<string>((authenticationId) =>
                {
                    passedAuthenticationId = authenticationId;
                })
                .Returns(() =>
                {
                    return Task.FromResult(Generator.RandomString(10));
                })
                .Verifiable();

            client.AuthenticationService = authenticationService.Object;

            client.RetrievePersistanceTokenAsync().ConfigureAwait(true).GetAwaiter().GetResult();

            Assert.Equal(generatedAuthenticationId, passedAuthenticationId);
            authenticationService.VerifyAll();
        }

        [Fact]
        public void ShouldRetrievePersistanceTokenSucessfully()
        {
            var tokenService = new Mock<ITokenService>();
            var requestService = new Mock<IRequestService>();
            var generatedAuthenticationId = Generator.RandomString(10);

            var client = new MeshyClient(tokenService.Object, requestService.Object, generatedAuthenticationId);

            var passedAuthenticationId = string.Empty;

            var authenticationService = new Mock<IAuthenticationService>();
            authenticationService.Setup(x => x.RetrievePersistanceTokenAsync(It.IsAny<string>()))
                .Callback<string>((authenticationId) =>
                {
                    passedAuthenticationId = authenticationId;
                })
                .Returns(() =>
                {
                    return Task.FromResult(Generator.RandomString(10));
                })
                .Verifiable();

            client.AuthenticationService = authenticationService.Object;

            client.RetrievePersistanceToken();

            Assert.Equal(generatedAuthenticationId, passedAuthenticationId);
            authenticationService.VerifyAll();
        }

        [Fact]
        public void ShouldGetMyUserInfoSuccessfully()
        {
            var tokenService = new Mock<ITokenService>();
            var requestService = new Mock<IRequestService>();
            var generatedAuthenticationId = Generator.RandomString(10);

            var passedAuthenticationId = string.Empty;

            tokenService.Setup(x => x.GetUserInfoAsync(It.IsAny<string>()))
                        .Callback<string>((authenticationId) => {
                            passedAuthenticationId = authenticationId;
                        })
                        .Returns(() => {
                            return Task.FromResult<IDictionary<string, string>>(null);
                        })
                        .Verifiable();

            var client = new MeshyClient(tokenService.Object, requestService.Object, generatedAuthenticationId);

            client.GetMyUserInfo();

            Assert.Equal(generatedAuthenticationId, passedAuthenticationId);
            tokenService.VerifyAll();
        }

        [Fact]
        public void ShouldGetMyUserInfoAsyncSuccessfully()
        {
            var tokenService = new Mock<ITokenService>();
            var requestService = new Mock<IRequestService>();
            var generatedAuthenticationId = Generator.RandomString(10);

            var passedAuthenticationId = string.Empty;

            tokenService.Setup(x => x.GetUserInfoAsync(It.IsAny<string>()))
                        .Callback<string>((authenticationId) => {
                            passedAuthenticationId = authenticationId;
                        })
                        .Returns(() => {
                            return Task.FromResult<IDictionary<string, string>>(null);
                        })
                        .Verifiable();

            var client = new MeshyClient(tokenService.Object, requestService.Object, generatedAuthenticationId);

            client.GetMyUserInfoAsync().ConfigureAwait(true).GetAwaiter().GetResult();

            Assert.Equal(generatedAuthenticationId, passedAuthenticationId);
            tokenService.VerifyAll();
        }
    }
}
