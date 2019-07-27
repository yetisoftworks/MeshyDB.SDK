using MeshyDB.SDK.Models;
using MeshyDB.SDK.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MeshyDB.SDK.Tests
{
    public class MeshyClientTests
    {
        public MeshyClientTests()
        {
            MeshyClient.CurrentConnection = null;
        }

        [Fact]
        public void ShouldCreate()
        {
            var client = new Services.MeshyClient(Generator.RandomString(5), Generator.RandomString(36));
            Assert.NotNull(client);
        }

        [Fact]
        public void ShouldCreateWithNullTenant()
        {
            var client = new Services.MeshyClient(Generator.RandomString(5), null, Generator.RandomString(36));
            Assert.NotNull(client);
        }

        [Fact]
        public void ShouldIncludeAccountNameInApiUrl()
        {
            var accountName = Generator.RandomString(5);
            var client = new Services.MeshyClient(accountName, Generator.RandomString(36));

            Assert.Equal($"https://api.meshydb.com/{accountName}".ToLowerInvariant(), client.GetApiUrl().ToLowerInvariant());
        }

        [Fact]
        public void ShouldIncludeAccountNameInAuthUrl()
        {
            var accountName = Generator.RandomString(5);
            var client = new Services.MeshyClient(accountName, Generator.RandomString(36));

            Assert.Equal($"https://auth.meshydb.com/{accountName}".ToLowerInvariant(), client.GetAuthUrl().ToLowerInvariant());
        }

        [Fact]
        public void ShouldHaveTenantWhenCreated()
        {
            var tenant = Generator.RandomString(5);
            var client = new Services.MeshyClient(Generator.RandomString(5), tenant, Generator.RandomString(36));
            Assert.Equal(tenant, client.Tenant);
        }

        [Fact]
        public void ShouldHaveTenantTrimmedWhenCreated()
        {
            var tenant = Generator.RandomString(5);
            var client = new Services.MeshyClient(Generator.RandomString(5), $" {tenant} ", Generator.RandomString(36));
            Assert.Equal(tenant, client.Tenant);
        }

        [Fact]
        public void ShouldHaveUsersService()
        {
            var client = new Services.MeshyClient(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            authService.Setup(x => x.LoginAnonymouslyAsync(It.IsAny<string>()))
                       .Returns(() =>
                       {
                           return Task.FromResult(Generator.RandomString(25));
                       });

            client.AuthenticationService = authService.Object;
            var connection = client.LoginAnonymously(Generator.RandomString(5));
            Assert.NotNull(connection.Users);
        }

        [Fact]
        public void ShouldHaveMeshesService()
        {
            var client = new Services.MeshyClient(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            authService.Setup(x => x.LoginAnonymouslyAsync(It.IsAny<string>()))
                       .Returns(() =>
                       {
                           return Task.FromResult(Generator.RandomString(25));
                       });

            client.AuthenticationService = authService.Object;
            var connection = client.LoginAnonymously(Generator.RandomString(5));
            Assert.NotNull(connection.Meshes);
        }

        [Fact]
        public void ShouldLoginAnonymouslyAsyncSuccessfully()
        {
            var client = new Services.MeshyClient(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            var identifier = Generator.RandomString(25);

            authService.Setup(x => x.LoginAnonymouslyAsync(It.IsAny<string>()))
                       .Returns(() =>
                       {
                           return Task.FromResult(identifier);
                       });

            client.AuthenticationService = authService.Object;
            var connection = client.LoginAnonymouslyAsync(Generator.RandomString(5)).ConfigureAwait(true).GetAwaiter().GetResult();
            Assert.NotNull(connection);
        }

        [Fact]
        public void ShouldLoginAnonymouslySuccessfully()
        {
            var client = new Services.MeshyClient(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            var identifier = Generator.RandomString(25);

            authService.Setup(x => x.LoginAnonymouslyAsync(It.IsAny<string>()))
                       .Returns(() =>
                       {
                           return Task.FromResult(identifier);
                       });

            client.AuthenticationService = authService.Object;
            var connection = client.LoginAnonymously(Generator.RandomString(5));
            Assert.NotNull(connection);
        }

        [Fact]
        public void ShouldLoginWithPasswordAsyncSuccessfully()
        {
            var client = new Services.MeshyClient(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            var identifier = Generator.RandomString(25);

            authService.Setup(x => x.LoginWithPasswordAsync(It.IsAny<string>(), It.IsAny<string>()))
                       .Returns(() =>
                       {
                           return Task.FromResult(identifier);
                       });

            client.AuthenticationService = authService.Object;
            var connection = client.LoginWithPasswordAsync(Generator.RandomString(10), Generator.RandomString(10)).ConfigureAwait(true).GetAwaiter().GetResult();
            Assert.NotNull(connection);
        }

        [Fact]
        public void ShouldLoginWithPasswordSuccessfully()
        {
            var client = new Services.MeshyClient(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            var identifier = Generator.RandomString(25);

            authService.Setup(x => x.LoginWithPasswordAsync(It.IsAny<string>(), It.IsAny<string>()))
                       .Returns(() =>
                       {
                           return Task.FromResult(identifier);
                       });

            client.AuthenticationService = authService.Object;
            var connection = client.LoginWithPassword(Generator.RandomString(10), Generator.RandomString(10));
            Assert.NotNull(connection);
        }

        [Fact]
        public void ShouldLoginWithPersistenceAsyncSuccessfully()
        {
            var client = new Services.MeshyClient(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            var identifier = Generator.RandomString(25);

            authService.Setup(x => x.LoginWithPersistenceAsync(It.IsAny<string>()))
                       .Returns(() =>
                       {
                           return Task.FromResult(identifier);
                       });

            client.AuthenticationService = authService.Object;
            var connection = client.LoginWithPersistenceAsync(Generator.RandomString(10)).ConfigureAwait(true).GetAwaiter().GetResult();
            Assert.NotNull(connection);
        }

        [Fact]
        public void ShouldLoginWithPersistenceSuccessfully()
        {
            var client = new Services.MeshyClient(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            var identifier = Generator.RandomString(25);

            authService.Setup(x => x.LoginWithPersistenceAsync(It.IsAny<string>()))
                       .Returns(() =>
                       {
                           return Task.FromResult(identifier);
                       });

            client.AuthenticationService = authService.Object;
            var connection = client.LoginWithPersistence(Generator.RandomString(10));
            Assert.NotNull(connection);
        }

        #region RegisterUserAsync

        [Fact]
        public void ShouldRegisterUserAsyncSuccessfully()
        {
            var client = new Services.MeshyClient(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            var expected = new UserVerificationHash();
            authService.Setup(x => x.RegisterAsync(It.IsAny<RegisterUser>())).Returns(() =>
            {
                return Task.FromResult(expected);
            });

            client.AuthenticationService = authService.Object;

            var actual = client.RegisterUserAsync(new RegisterUser(Generator.RandomString(5), Generator.RandomString(5))).ConfigureAwait(true).GetAwaiter().GetResult();
            Assert.Equal(expected, actual);
        }

        #endregion

        #region RegisterUser
        [Fact]
        public void ShouldRegisterUserSuccessfully()
        {
            var client = new Services.MeshyClient(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            var expected = new UserVerificationHash();
            authService.Setup(x => x.RegisterAsync(It.IsAny<RegisterUser>())).Returns(() =>
            {
                return Task.FromResult(expected);
            });

            client.AuthenticationService = authService.Object;

            var actual = client.RegisterUser(new RegisterUser(Generator.RandomString(5), Generator.RandomString(5)));
            Assert.Equal(expected, actual);
        }

        #endregion RegisterUser

        #region ForgotPasswordAsync

        [Fact]
        public void ShouldForgotPasswordAsyncSuccessfully()
        {
            var client = new Services.MeshyClient(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            var expected = new UserVerificationHash();
            var passedUsername = string.Empty;

            authService.Setup(x => x.ForgotPasswordAsync(It.IsAny<string>(), It.IsAny<int>()))
                .Callback<string, int>((sentUsername, sentAttempt) =>
                 {
                     passedUsername = sentUsername;
                 })
                .Returns(() =>
                {
                    return Task.FromResult(expected);
                });

            client.AuthenticationService = authService.Object;

            var username = Generator.RandomString(5);
            var actual = client.ForgotPasswordAsync(username).ConfigureAwait(true).GetAwaiter().GetResult();
            Assert.Equal(expected, actual);
            Assert.Equal(username, passedUsername);
        }

        #endregion

        #region ForgotPassword

        [Fact]
        public void ShouldForgotPasswordSuccessfully()
        {
            var client = new Services.MeshyClient(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            var expected = new UserVerificationHash();
            var passedUsername = string.Empty;

            authService.Setup(x => x.ForgotPasswordAsync(It.IsAny<string>(), It.IsAny<int>()))
                .Callback<string, int>((sentUsername, sentAttempt) =>
                 {
                     passedUsername = sentUsername;
                 })
                .Returns(() =>
                {
                    return Task.FromResult(expected);
                });

            client.AuthenticationService = authService.Object;

            var username = Generator.RandomString(5);
            var actual = client.ForgotPassword(username);
            Assert.Equal(expected, actual);
            Assert.Equal(username, passedUsername);
        }

        #endregion

        #region ResetPasswordAsync

        [Fact]
        public void ShouldResetPasswordAsyncSuccessfully()
        {
            var client = new Services.MeshyClient(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            var passedResetPassword = default(ResetPassword);

            authService.Setup(x => x.ResetPasswordAsync(It.IsAny<ResetPassword>()))
                .Callback<ResetPassword>((sentPasswordReset) =>
                {
                    passedResetPassword = sentPasswordReset;
                })
                .Returns(() =>
                {
                    return Task.FromResult<object>(null);
                });

            client.AuthenticationService = authService.Object;

            var now = DateTimeOffset.Now;
            var resetPassword = new ResetPassword()
            {
                Expires = now,
                Hash = Generator.RandomString(5),
                Hint = Generator.RandomString(5),
                NewPassword = Generator.RandomString(5),
                Username = Generator.RandomString(5),
                VerificationCode = Generator.RandomString(5)
            };

            client.ResetPasswordAsync(resetPassword).ConfigureAwait(true).GetAwaiter().GetResult();
            Assert.Equal(resetPassword.Hash, passedResetPassword.Hash);
            Assert.Equal(resetPassword.Expires, passedResetPassword.Expires);
            Assert.Equal(resetPassword.Hint, passedResetPassword.Hint);
            Assert.Equal(resetPassword.NewPassword, passedResetPassword.NewPassword);
            Assert.Equal(resetPassword.Username, passedResetPassword.Username);
            Assert.Equal(resetPassword.VerificationCode, passedResetPassword.VerificationCode);
        }

        #endregion

        #region ResetPassword

        [Fact]
        public void ShouldResetPasswordSuccessfully()
        {
            var client = new Services.MeshyClient(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            var passedResetPassword = default(ResetPassword);

            authService.Setup(x => x.ResetPasswordAsync(It.IsAny<ResetPassword>()))
                .Callback<ResetPassword>((sentPasswordReset) =>
                {
                    passedResetPassword = sentPasswordReset;
                })
                .Returns(() =>
                {
                    return Task.FromResult<object>(null);
                });

            client.AuthenticationService = authService.Object;

            var now = DateTimeOffset.Now;
            var resetPassword = new ResetPassword()
            {
                Expires = now,
                Hash = Generator.RandomString(5),
                Hint = Generator.RandomString(5),
                NewPassword = Generator.RandomString(5),
                Username = Generator.RandomString(5),
                VerificationCode = Generator.RandomString(5)
            };

            client.ResetPassword(resetPassword);
            Assert.Equal(resetPassword.Hash, passedResetPassword.Hash);
            Assert.Equal(resetPassword.Expires, passedResetPassword.Expires);
            Assert.Equal(resetPassword.Hint, passedResetPassword.Hint);
            Assert.Equal(resetPassword.NewPassword, passedResetPassword.NewPassword);
            Assert.Equal(resetPassword.Username, passedResetPassword.Username);
            Assert.Equal(resetPassword.VerificationCode, passedResetPassword.VerificationCode);
        }

        #endregion

        #region VerifyAsync

        [Fact]
        public void ShouldVerifyAsyncSuccessfully()
        {
            var client = new Services.MeshyClient(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            var passedUserVerificationCheck = default(UserVerificationCheck);

            authService.Setup(x => x.VerifyAsync(It.IsAny<UserVerificationCheck>()))
                .Callback<UserVerificationCheck>((sentUserVerificationCheck) =>
                {
                    passedUserVerificationCheck = sentUserVerificationCheck;
                })
                .Returns(() =>
                {
                    return Task.FromResult<object>(null);
                });

            client.AuthenticationService = authService.Object;

            var now = DateTimeOffset.Now;
            var userVerificationCheck = new UserVerificationCheck()
            {
                Expires = now,
                Hash = Generator.RandomString(5),
                Hint = Generator.RandomString(5),
                Username = Generator.RandomString(5),
                VerificationCode = Generator.RandomString(5)
            };

            client.VerifyAsync(userVerificationCheck).ConfigureAwait(true).GetAwaiter().GetResult();
            Assert.Equal(userVerificationCheck.Hash, passedUserVerificationCheck.Hash);
            Assert.Equal(userVerificationCheck.Expires, passedUserVerificationCheck.Expires);
            Assert.Equal(userVerificationCheck.Hint, passedUserVerificationCheck.Hint);
            Assert.Equal(userVerificationCheck.Username, passedUserVerificationCheck.Username);
            Assert.Equal(userVerificationCheck.VerificationCode, passedUserVerificationCheck.VerificationCode);
        }

        #endregion

        #region Verify

        [Fact]
        public void ShouldVerifySuccessfully()
        {
            var client = new Services.MeshyClient(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            var passedUserVerificationCheck = default(UserVerificationCheck);

            authService.Setup(x => x.VerifyAsync(It.IsAny<UserVerificationCheck>()))
                .Callback<UserVerificationCheck>((sentUserVerificationCheck) =>
                {
                    passedUserVerificationCheck = sentUserVerificationCheck;
                })
                .Returns(() =>
                {
                    return Task.FromResult<object>(null);
                });

            client.AuthenticationService = authService.Object;

            var now = DateTimeOffset.Now;
            var userVerificationCheck = new UserVerificationCheck()
            {
                Expires = now,
                Hash = Generator.RandomString(5),
                Hint = Generator.RandomString(5),
                Username = Generator.RandomString(5),
                VerificationCode = Generator.RandomString(5)
            };

            client.Verify(userVerificationCheck);
            Assert.Equal(userVerificationCheck.Hash, passedUserVerificationCheck.Hash);
            Assert.Equal(userVerificationCheck.Expires, passedUserVerificationCheck.Expires);
            Assert.Equal(userVerificationCheck.Hint, passedUserVerificationCheck.Hint);
            Assert.Equal(userVerificationCheck.Username, passedUserVerificationCheck.Username);
            Assert.Equal(userVerificationCheck.VerificationCode, passedUserVerificationCheck.VerificationCode);
        }

        #endregion

        #region CheckHashAsync

        [Fact]
        public void ShouldCheckHashAsyncSuccessfully()
        {
            var client = new Services.MeshyClient(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            var passedUserVerificationCheck = default(UserVerificationCheck);

            authService.Setup(x => x.CheckHashAsync(It.IsAny<UserVerificationCheck>()))
                .Callback<UserVerificationCheck>((sentUserVerificationCheck) =>
                {
                    passedUserVerificationCheck = sentUserVerificationCheck;
                })
                .Returns(() =>
                {
                    return Task.FromResult(true);
                });

            client.AuthenticationService = authService.Object;

            var now = DateTimeOffset.Now;
            var userVerificationCheck = new UserVerificationCheck()
            {
                Expires = now,
                Hash = Generator.RandomString(5),
                Hint = Generator.RandomString(5),
                Username = Generator.RandomString(5),
                VerificationCode = Generator.RandomString(5)
            };

            var result = client.CheckHashAsync(userVerificationCheck).ConfigureAwait(true).GetAwaiter().GetResult();
            Assert.True(result);
            Assert.Equal(userVerificationCheck.Hash, passedUserVerificationCheck.Hash);
            Assert.Equal(userVerificationCheck.Expires, passedUserVerificationCheck.Expires);
            Assert.Equal(userVerificationCheck.Hint, passedUserVerificationCheck.Hint);
            Assert.Equal(userVerificationCheck.Username, passedUserVerificationCheck.Username);
            Assert.Equal(userVerificationCheck.VerificationCode, passedUserVerificationCheck.VerificationCode);
        }

        #endregion

        #region CheckHashAsync

        [Fact]
        public void ShouldCheckHashSuccessfully()
        {
            var client = new Services.MeshyClient(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            var passedUserVerificationCheck = default(UserVerificationCheck);

            authService.Setup(x => x.CheckHashAsync(It.IsAny<UserVerificationCheck>()))
                .Callback<UserVerificationCheck>((sentUserVerificationCheck) =>
                {
                    passedUserVerificationCheck = sentUserVerificationCheck;
                })
                .Returns(() =>
                {
                    return Task.FromResult(true);
                });

            client.AuthenticationService = authService.Object;

            var now = DateTimeOffset.Now;
            var userVerificationCheck = new UserVerificationCheck()
            {
                Expires = now,
                Hash = Generator.RandomString(5),
                Hint = Generator.RandomString(5),
                Username = Generator.RandomString(5),
                VerificationCode = Generator.RandomString(5)
            };

            var result = client.CheckHash(userVerificationCheck);
            Assert.True(result);
            Assert.Equal(userVerificationCheck.Hash, passedUserVerificationCheck.Hash);
            Assert.Equal(userVerificationCheck.Expires, passedUserVerificationCheck.Expires);
            Assert.Equal(userVerificationCheck.Hint, passedUserVerificationCheck.Hint);
            Assert.Equal(userVerificationCheck.Username, passedUserVerificationCheck.Username);
            Assert.Equal(userVerificationCheck.VerificationCode, passedUserVerificationCheck.VerificationCode);
        }

        #endregion
    }
}
