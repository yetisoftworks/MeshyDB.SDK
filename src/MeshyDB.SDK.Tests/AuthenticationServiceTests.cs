using MeshyDB.SDK.Enums;
using MeshyDB.SDK.Models;
using MeshyDB.SDK.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MeshyDB.SDK.Tests
{
    public class AuthenticationServiceTests
    {
        //Task<string> LoginAnonymouslyAsync();
        [Fact]
        public void ShouldLoginAnonymouslyAsyncSuccessfully()
        {
            var tokenService = new Mock<ITokenService>();

            var passedUsername = string.Empty;
            var passedPassword = string.Empty;
            var authenticationId = Generator.RandomString(10);
            tokenService.Setup(x => x.GenerateAccessToken(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((username, password) =>
                {
                    passedUsername = username;
                    passedPassword = password;
                })
                .Returns(() =>
                {
                    return Task.FromResult(authenticationId);
                });

            var requestService = new Mock<IRequestService>();
            var passedPath = string.Empty;
            var passedModel = default(NewUser);
            var passedFormat = RequestDataFormat.Json;
            var user = new User();

            requestService.Setup(x => x.PostRequest<User>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>()))
                .Callback<string, object, RequestDataFormat>((path, model, format) =>
                {
                    passedPath = path;
                    passedModel = model as NewUser;
                    passedFormat = format;
                }).Returns(() =>
                {
                    return Task.FromResult(user);
                });
            var service = new AuthenticationService(tokenService.Object, requestService.Object);
            var resultId = service.LoginAnonymouslyAsync().Result;

            Assert.NotNull(passedUsername);
            Assert.Equal("nopassword", passedPassword);

            Assert.Equal("users", passedPath);
            Assert.True(passedModel.IsActive);
            Assert.True(passedModel.Verified);
            Assert.Equal(passedUsername, passedModel.Username);
            Assert.Equal(passedPassword, passedModel.NewPassword);

            tokenService.VerifyAll();
            requestService.VerifyAll();
        }

        //Task<string> LoginWithPasswordAsync(string username, string password);
        [Fact]
        public void ShouldLoginWithPasswordAsyncSuccessfully()
        {
            var tokenService = new Mock<ITokenService>();

            var passedUsername = string.Empty;
            var passedPassword = string.Empty;
            var authenticationId = Generator.RandomString(10);

            tokenService.Setup(x => x.GenerateAccessToken(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((username, password) =>
                {
                    passedUsername = username;
                    passedPassword = password;
                })
                .Returns(() =>
                {
                    return Task.FromResult(authenticationId);
                });

            var requestService = new Mock<IRequestService>();

            var service = new AuthenticationService(tokenService.Object, requestService.Object);
            var generatedUsername = Generator.RandomString(10);
            var generatedPassword = Generator.RandomString(10);

            var resultId = service.LoginWithPasswordAsync(generatedUsername, generatedPassword).Result;

            Assert.Equal(generatedUsername, passedUsername);
            Assert.Equal(generatedPassword, passedPassword);

            tokenService.VerifyAll();
            requestService.VerifyAll();
        }
        //Task<string> LoginWithPersistanceAsync(string persistanceToken);
        [Fact]
        public void ShouldLoginWithPersistanceAsyncSuccessfully()
        {
            var tokenService = new Mock<ITokenService>();

            var passedRefreshToken = string.Empty;

            var authenticationId = Generator.RandomString(10);

            tokenService.Setup(x => x.GenerateAccessTokenWithRefreshToken(It.IsAny<string>()))
                .Callback<string>((refreshToken) =>
                {
                    passedRefreshToken = refreshToken;
                })
                .Returns(() =>
                {
                    return Task.FromResult(authenticationId);
                });

            var requestService = new Mock<IRequestService>();

            var service = new AuthenticationService(tokenService.Object, requestService.Object);
            var generatedRefreshToken = Generator.RandomString(10);

            var resultId = service.LoginWithPersistanceAsync(generatedRefreshToken).Result;

            Assert.Equal(generatedRefreshToken, passedRefreshToken);

            tokenService.VerifyAll();
            requestService.VerifyAll();
        }

        //Task<string> RetrievePersistanceTokenAsync(string authenticationId);
        [Fact]
        public void ShouldRetrievePersistanceTokenAsyncSuccessfully()
        {
            var tokenService = new Mock<ITokenService>();

            var passedAuthenticationId = string.Empty;

            var generatedAuthenticationId = Generator.RandomString(10);

            tokenService.Setup(x => x.GetRefreshTokenAsync(It.IsAny<string>()))
                .Callback<string>((authenticationId) =>
                {
                    passedAuthenticationId = authenticationId;
                })
                .Returns(() =>
                {
                    return Task.FromResult(passedAuthenticationId);
                });

            var requestService = new Mock<IRequestService>();

            var service = new AuthenticationService(tokenService.Object, requestService.Object);

            var resultId = service.RetrievePersistanceTokenAsync(generatedAuthenticationId).Result;

            Assert.Equal(generatedAuthenticationId, passedAuthenticationId);

            tokenService.VerifyAll();
            requestService.VerifyAll();
        }
        //Task<PasswordResetHash> ForgotPasswordAsync(string username);
        [Fact]
        public void ShouldForgotPasswordAsyncSuccessfully()
        {
            var tokenService = new Mock<ITokenService>();

            var requestService = new Mock<IRequestService>();
            var passedPath = string.Empty;
            var passedModel = default(ForgotPassword);
            var passedFormat = RequestDataFormat.Json;

            requestService.Setup(x => x.PostRequest<PasswordResetHash>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>()))
                .Callback<string, object, RequestDataFormat>((path, model, format) =>
                {
                    passedPath = path;
                    passedModel = model as ForgotPassword;
                    passedFormat = format;
                })
                .Returns(() =>
                {
                    return Task.FromResult(new PasswordResetHash());
                });

            var service = new AuthenticationService(tokenService.Object, requestService.Object);
            var generatedUserName = Generator.RandomString(10);
            var resultId = service.ForgotPasswordAsync(generatedUserName).Result;

            Assert.Equal("users/forgotpassword", passedPath);
            Assert.Equal(generatedUserName, passedModel.Username);

            tokenService.VerifyAll();
            requestService.VerifyAll();
        }

        //Task ResetPasswordAsync(PasswordResetHash resetHash, string newPassword);
        [Fact]
        public void ShouldResetPasswordAsyncSuccessfully()
        {
            var tokenService = new Mock<ITokenService>();

            var requestService = new Mock<IRequestService>();
            var passedPath = string.Empty;
            var passedModel = default(ResetPassword);
            var passedFormat = RequestDataFormat.Json;

            requestService.Setup(x => x.PostRequest<object>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>()))
                .Callback<string, object, RequestDataFormat>((path, model, format) =>
                {
                    passedPath = path;
                    passedModel = model as ResetPassword;
                    passedFormat = format;
                })
                .Returns(() =>
                {
                    return Task.FromResult<object>(null);
                });

            var service = new AuthenticationService(tokenService.Object, requestService.Object);
            var generatedUsername = Generator.RandomString(10);
            var generatedPassword = Generator.RandomString(10);
            var generatedHash = Generator.RandomString(36);
            var now = DateTimeOffset.Now;

            service.ResetPasswordAsync(new PasswordResetHash()
            {
                Expires = now,
                Username = generatedUsername,
                Hash = generatedHash
            }, generatedPassword).ConfigureAwait(true).GetAwaiter().GetResult();

            Assert.Equal("users/resetpassword", passedPath);
            Assert.Equal(generatedUsername, passedModel.Username);
            Assert.Equal(now, passedModel.Expires);
            Assert.Equal(generatedPassword, passedModel.NewPassword);
            Assert.Equal(generatedHash, passedModel.Hash);

            tokenService.VerifyAll();
            requestService.VerifyAll();
        }

        //Task UpdatePasswordAsync(string previousPassword, string newPassword);
        [Fact]
        public void ShouldUpdatePasswordAsyncSuccessfully()
        {
            var tokenService = new Mock<ITokenService>();

            var requestService = new Mock<IRequestService>();
            var passedPath = string.Empty;
            var passedModel = default(UserPasswordUpdate);
            var passedFormat = RequestDataFormat.Json;

            requestService.Setup(x => x.PostRequest<object>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>()))
                .Callback<string, object, RequestDataFormat>((path, model, format) =>
                {
                    passedPath = path;
                    passedModel = model as UserPasswordUpdate;
                    passedFormat = format;
                })
                .Returns(() =>
                {
                    return Task.FromResult<object>(null);
                });

            var service = new AuthenticationService(tokenService.Object, requestService.Object);
            var generatedPreviousPassword = Generator.RandomString(10);
            var generatedNewPassword = Generator.RandomString(10);

            service.UpdatePasswordAsync(generatedPreviousPassword, generatedNewPassword).ConfigureAwait(true).GetAwaiter().GetResult();

            Assert.Equal("users/me/password", passedPath);
            Assert.Equal(generatedPreviousPassword, passedModel.PreviousPassword);
            Assert.Equal(generatedNewPassword, passedModel.NewPassword);

            tokenService.VerifyAll();
            requestService.VerifyAll();
        }

        //Task<User> CreateUserAsync(NewUser user);
        [Fact]
        public void ShouldCreateUserAsyncSuccessfully()
        {
            var tokenService = new Mock<ITokenService>();

            var requestService = new Mock<IRequestService>();
            var passedPath = string.Empty;
            var passedModel = default(NewUser);
            var passedFormat = RequestDataFormat.Json;
            var user = new User();

            requestService.Setup(x => x.PostRequest<User>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>()))
                .Callback<string, object, RequestDataFormat>((path, model, format) =>
                {
                    passedPath = path;
                    passedModel = model as NewUser;
                    passedFormat = format;
                }).Returns(() =>
                {
                    return Task.FromResult(user);
                });

            var service = new AuthenticationService(tokenService.Object, requestService.Object);

            var generatedUsername = Generator.RandomString(10);
            var generatedPassword = Generator.RandomString(10);
            var resultId = service.CreateUserAsync(new NewUser(generatedUsername, generatedPassword)).Result;

            Assert.Equal("users", passedPath);
            Assert.False(passedModel.IsActive);
            Assert.False(passedModel.Verified);
            Assert.Equal(generatedUsername, passedModel.Username);
            Assert.Equal(generatedPassword, passedModel.NewPassword);

            tokenService.VerifyAll();
            requestService.VerifyAll();
        }

        //Task SignOut(string authenticationId);
        [Fact]
        public void ShouldSignoutSuccessfully()
        {
            var tokenService = new Mock<ITokenService>();
            var passedAuthenticationId = string.Empty;

            tokenService.Setup(x => x.Signout(It.IsAny<string>())).Callback<string>((authenticationId) =>
            {
                passedAuthenticationId = authenticationId;
            })
            .Returns(() =>
            {
                return Task.FromResult<object>(null);
            });

            var requestService = new Mock<IRequestService>();

            var service = new AuthenticationService(tokenService.Object, requestService.Object);

            var generatedAuthenticationId = Generator.RandomString(10);

            service.Signout(generatedAuthenticationId).ConfigureAwait(true).GetAwaiter().GetResult();

            Assert.Equal(generatedAuthenticationId, passedAuthenticationId);

            tokenService.VerifyAll();
            requestService.VerifyAll();
        }
    }
}
