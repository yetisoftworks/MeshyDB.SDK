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

        //Task SignOut(string authenticationId);
        [Fact]
        public void ShouldSignoutSuccessfully()
        {
            var tokenService = new Mock<ITokenService>();
            var passedAuthenticationId = string.Empty;

            tokenService.Setup(x => x.SignoutAsync(It.IsAny<string>())).Callback<string>((authenticationId) =>
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

            service.SignoutAsync(generatedAuthenticationId).ConfigureAwait(true).GetAwaiter().GetResult();

            Assert.Equal(generatedAuthenticationId, passedAuthenticationId);

            tokenService.VerifyAll();
            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldRegisterSuccessfully()
        {
            var tokenService = new Mock<ITokenService>();

            var requestService = new Mock<IRequestService>();
            var passedPath = string.Empty;
            var passedModel = default(RegisterUser);
            var passedFormat = RequestDataFormat.Json;

            requestService.Setup(x => x.PostRequest<UserVerificationHash>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>()))
                .Callback<string, object, RequestDataFormat>((path, model, format) =>
                {
                    passedPath = path;
                    passedModel = model as RegisterUser;
                    passedFormat = format;
                })
                .Returns(() =>
                {
                    return Task.FromResult(It.IsAny<UserVerificationHash>());
                });

            var service = new AuthenticationService(tokenService.Object, requestService.Object);

            var user = new RegisterUser(Generator.RandomString(5), Generator.RandomString(5));

            service.RegisterAsync(user).ConfigureAwait(true).GetAwaiter().GetResult();

            Assert.Equal("users/register", passedPath);
            Assert.Equal(user.Username, passedModel.Username);
            Assert.Equal(user.PhoneNumber, passedModel.PhoneNumber);

            tokenService.VerifyAll();
            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldForgotPasswordSuccessfully()
        {
            var tokenService = new Mock<ITokenService>();

            var requestService = new Mock<IRequestService>();
            var passedPath = string.Empty;
            var passedModel = default(ForgotPassword);
            var passedFormat = RequestDataFormat.Json;

            requestService.Setup(x => x.PostRequest<UserVerificationHash>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>()))
                .Callback<string, object, RequestDataFormat>((path, model, format) =>
                {
                    passedPath = path;
                    passedModel = model as ForgotPassword;
                    passedFormat = format;
                })
                .Returns(() =>
                {
                    return Task.FromResult(It.IsAny<UserVerificationHash>());
                });

            var service = new AuthenticationService(tokenService.Object, requestService.Object);

            var username = Generator.RandomString(5);

            service.ForgotPasswordAsync(username).ConfigureAwait(true).GetAwaiter().GetResult();

            Assert.Equal("users/forgotpassword", passedPath);
            Assert.Equal(username, passedModel.Username);

            tokenService.VerifyAll();
            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldResetPasswordSuccessfully()
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
                    return Task.FromResult(It.IsAny<object>());
                });

            var service = new AuthenticationService(tokenService.Object, requestService.Object);

            var data = new ResetPassword()
            {
                Expires = DateTimeOffset.Now,
                Hash = Generator.RandomString(5),
                Hint = Generator.RandomString(5),
                NewPassword = Generator.RandomString(5),
                Username = Generator.RandomString(5),
                VerificationCode = Generator.RandomString(5)
            };

            service.ResetPasswordAsync(data).ConfigureAwait(true).GetAwaiter().GetResult();

            Assert.Equal("users/resetpassword", passedPath);
            Assert.Equal(data.Username, passedModel.Username);
            Assert.Equal(data.Expires, passedModel.Expires);
            Assert.Equal(data.Hash, passedModel.Hash);
            Assert.Equal(data.Hint, passedModel.Hint);
            Assert.Equal(data.NewPassword, passedModel.NewPassword);

            tokenService.VerifyAll();
            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldVerifySuccessfully()
        {
            var tokenService = new Mock<ITokenService>();

            var requestService = new Mock<IRequestService>();
            var passedPath = string.Empty;
            var passedModel = default(UserVerificationCheck);
            var passedFormat = RequestDataFormat.Json;

            requestService.Setup(x => x.PostRequest<object>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>()))
                .Callback<string, object, RequestDataFormat>((path, model, format) =>
                {
                    passedPath = path;
                    passedModel = model as UserVerificationCheck;
                    passedFormat = format;
                })
                .Returns(() =>
                {
                    return Task.FromResult(It.IsAny<object>());
                });

            var service = new AuthenticationService(tokenService.Object, requestService.Object);

            var data = new UserVerificationCheck()
            {
                Expires = DateTimeOffset.Now,
                Hash = Generator.RandomString(5),
                Hint = Generator.RandomString(5),
                Username = Generator.RandomString(5),
                VerificationCode = Generator.RandomString(5)
            };

            service.VerifyAsync(data).ConfigureAwait(true).GetAwaiter().GetResult();

            Assert.Equal("users/verify", passedPath);
            Assert.Equal(data.Username, passedModel.Username);
            Assert.Equal(data.Expires, passedModel.Expires);
            Assert.Equal(data.Hash, passedModel.Hash);
            Assert.Equal(data.Hint, passedModel.Hint);

            tokenService.VerifyAll();
            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldCheckHashSuccessfully()
        {
            var tokenService = new Mock<ITokenService>();

            var requestService = new Mock<IRequestService>();
            var passedPath = string.Empty;
            var passedModel = default(UserVerificationCheck);
            var passedFormat = RequestDataFormat.Json;

            requestService.Setup(x => x.PostRequest<bool>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>()))
                .Callback<string, object, RequestDataFormat>((path, model, format) =>
                {
                    passedPath = path;
                    passedModel = model as UserVerificationCheck;
                    passedFormat = format;
                })
                .Returns(() =>
                {
                    return Task.FromResult(It.IsAny<bool>());
                });

            var service = new AuthenticationService(tokenService.Object, requestService.Object);

            var data = new UserVerificationCheck()
            {
                Expires = DateTimeOffset.Now,
                Hash = Generator.RandomString(5),
                Hint = Generator.RandomString(5),
                Username = Generator.RandomString(5),
                VerificationCode = Generator.RandomString(5)
            };

            service.CheckHashAsync(data).ConfigureAwait(true).GetAwaiter().GetResult();

            Assert.Equal("users/checkhash", passedPath);
            Assert.Equal(data.Username, passedModel.Username);
            Assert.Equal(data.Expires, passedModel.Expires);
            Assert.Equal(data.Hash, passedModel.Hash);
            Assert.Equal(data.Hint, passedModel.Hint);

            tokenService.VerifyAll();
            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldLoginAnonymouslySuccessfully()
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
            var passedModel = default(AnonymousRegistration);
            var passedFormat = RequestDataFormat.Json;

            requestService.Setup(x => x.PostRequest<User>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>()))
                .Callback<string, object, RequestDataFormat>((path, model, format) =>
                {
                    passedPath = path;
                    passedModel = model as AnonymousRegistration;
                    passedFormat = format;
                })
                .Returns(() =>
                {
                    return Task.FromResult(It.IsAny<User>());
                });

            var service = new AuthenticationService(tokenService.Object, requestService.Object);

            var generatedUsername = Generator.RandomString(5);

            service.LoginAnonymouslyAsync(generatedUsername).ConfigureAwait(true).GetAwaiter().GetResult();

            Assert.Equal("users/register/anonymous", passedPath);
            Assert.Equal(generatedUsername, passedModel.Username);
            Assert.Equal(generatedUsername, passedUsername);
            Assert.Equal("nopassword", passedPassword);

            tokenService.VerifyAll();
            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldLoginAnonymouslyAutomaticallyGenerateUsername()
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
            var passedModel = default(AnonymousRegistration);
            var passedFormat = RequestDataFormat.Json;

            requestService.Setup(x => x.PostRequest<User>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>()))
                .Callback<string, object, RequestDataFormat>((path, model, format) =>
                {
                    passedPath = path;
                    passedModel = model as AnonymousRegistration;
                    passedFormat = format;
                })
                .Returns(() =>
                {
                    return Task.FromResult(It.IsAny<User>());
                });

            var service = new AuthenticationService(tokenService.Object, requestService.Object);

            service.LoginAnonymouslyAsync().ConfigureAwait(true).GetAwaiter().GetResult();

            Assert.Equal("users/register/anonymous", passedPath);
            Assert.True(Guid.TryParse(passedModel.Username, out var _));
            Assert.Equal(passedModel.Username, passedUsername);
            Assert.Equal("nopassword", passedPassword);

            tokenService.VerifyAll();
            requestService.VerifyAll();
        }
    }
}
