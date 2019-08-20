using MeshyDB.SDK.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MeshyDB.SDK.Tests
{
    public class MeshyConnectionTests
    {
        [Fact]
        public void ShouldCreate()
        {
            var tokenService = new Mock<ITokenService>();
            var requestService = new Mock<IRequestService>();

            var connection = new MeshyConnection(tokenService.Object, requestService.Object, Generator.RandomString(10));

            Assert.NotNull(connection);
        }

        [Fact]
        public void ShouldMeshesIsNotNull()
        {
            var tokenService = new Mock<ITokenService>();
            var requestService = new Mock<IRequestService>();

            var connection = new MeshyConnection(tokenService.Object, requestService.Object, Generator.RandomString(10));

            Assert.NotNull(connection.Meshes);
        }

        [Fact]
        public void ShouldUsersIsNotNull()
        {
            var tokenService = new Mock<ITokenService>();
            var requestService = new Mock<IRequestService>();

            var connection = new MeshyConnection(tokenService.Object, requestService.Object, Generator.RandomString(10));

            Assert.NotNull(connection.Meshes);
        }

        [Fact]
        public void ShouldThrowExceptionIfTokenServiceIsNull()
        {
            var requestService = new Mock<IRequestService>();

            Assert.Throws<ArgumentNullException>(() => new MeshyConnection(null, requestService.Object, Generator.RandomString(10)));
        }

        [Fact]
        public void ShouldThrowExceptionIfRequestServiceIsNull()
        {
            var tokenService = new Mock<ITokenService>();

            Assert.Throws<ArgumentNullException>(() => new MeshyConnection(tokenService.Object, null, Generator.RandomString(10)));
        }

        [Fact]
        public void ShouldThrowExceptionIfAuthenticationIdIsNull()
        {
            var tokenService = new Mock<ITokenService>();
            var requestService = new Mock<IRequestService>();

            Assert.Throws<ArgumentNullException>(() => new MeshyConnection(tokenService.Object, requestService.Object, null));
        }

        [Fact]
        public void ShouldSetAuthenticationIdDuringCreation()
        {
            var tokenService = new Mock<ITokenService>();
            var requestService = new Mock<IRequestService>();

            var authenticationId = Generator.RandomString(10);
            var connection = new MeshyConnection(tokenService.Object, requestService.Object, authenticationId);

            Assert.Equal(authenticationId, connection.AuthenticationId);
        }

        [Fact]
        public void ShouldUpdatePasswordAsyncSucessfully()
        {
            var tokenService = new Mock<ITokenService>();
            var requestService = new Mock<IRequestService>();
            var authenticationId = Generator.RandomString(10);

            var connection = new MeshyConnection(tokenService.Object, requestService.Object, authenticationId);

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

            connection.AuthenticationService = authenticationService.Object;

            var generatedPreviousPassword = Generator.RandomString(10);
            var generatedNewPassword = Generator.RandomString(10);

            connection.UpdatePasswordAsync(generatedPreviousPassword, generatedNewPassword).ConfigureAwait(false).GetAwaiter().GetResult();

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

            var connection = new MeshyConnection(tokenService.Object, requestService.Object, authenticationId);

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

            connection.AuthenticationService = authenticationService.Object;

            var generatedPreviousPassword = Generator.RandomString(10);
            var generatedNewPassword = Generator.RandomString(10);

            connection.UpdatePassword(generatedPreviousPassword, generatedNewPassword);

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

            var connection = new MeshyConnection(tokenService.Object, requestService.Object, generatedAuthenticationId);

            connection.SignoutAsync().ConfigureAwait(false).GetAwaiter().GetResult();

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

            var connection = new MeshyConnection(tokenService.Object, requestService.Object, generatedAuthenticationId);

            connection.Signout();

            Assert.Equal(generatedAuthenticationId, passedAuthenticationId);
            tokenService.VerifyAll();
        }

        [Fact]
        public void ShouldRetrieveRefreshTokenAsyncSucessfully()
        {
            var tokenService = new Mock<ITokenService>();
            var requestService = new Mock<IRequestService>();
            var generatedAuthenticationId = Generator.RandomString(10);

            var connection = new MeshyConnection(tokenService.Object, requestService.Object, generatedAuthenticationId);

            var passedAuthenticationId = string.Empty;

            var authenticationService = new Mock<IAuthenticationService>();
            authenticationService.Setup(x => x.RetrieveRefreshTokenAsync(It.IsAny<string>()))
                .Callback<string>((authenticationId) =>
                {
                    passedAuthenticationId = authenticationId;
                })
                .Returns(() =>
                {
                    return Task.FromResult(Generator.RandomString(10));
                })
                .Verifiable();

            connection.AuthenticationService = authenticationService.Object;

            connection.RetrieveRefreshTokenAsync().ConfigureAwait(false).GetAwaiter().GetResult();

            Assert.Equal(generatedAuthenticationId, passedAuthenticationId);
            authenticationService.VerifyAll();
        }

        [Fact]
        public void ShouldRetrieveRefreshTokenSucessfully()
        {
            var tokenService = new Mock<ITokenService>();
            var requestService = new Mock<IRequestService>();
            var generatedAuthenticationId = Generator.RandomString(10);

            var connection = new MeshyConnection(tokenService.Object, requestService.Object, generatedAuthenticationId);

            var passedAuthenticationId = string.Empty;

            var authenticationService = new Mock<IAuthenticationService>();
            authenticationService.Setup(x => x.RetrieveRefreshTokenAsync(It.IsAny<string>()))
                .Callback<string>((authenticationId) =>
                {
                    passedAuthenticationId = authenticationId;
                })
                .Returns(() =>
                {
                    return Task.FromResult(Generator.RandomString(10));
                })
                .Verifiable();

            connection.AuthenticationService = authenticationService.Object;

            connection.RetrieveRefreshToken();

            Assert.Equal(generatedAuthenticationId, passedAuthenticationId);
            authenticationService.VerifyAll();
        }
    }
}
