using MeshyDB.SDK.Models;
using MeshyDB.SDK.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MeshyDB.SDK.Tests
{
    public class MeshyDBTests
    {
        [Fact]
        public void ShouldCreate()
        {
            var client = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
            Assert.NotNull(client);
        }

        [Fact]
        public void ShouldCreateWithNullTenant()
        {
            var client = new MeshyDB(Generator.RandomString(5), null, Generator.RandomString(36));
            Assert.NotNull(client);
        }

        [Fact]
        public void ShouldIncludeAccountNameInApiUrl()
        {
            var accountName = Generator.RandomString(5);
            var client = new MeshyDB(accountName, Generator.RandomString(36));

            Assert.Equal($"https://api.meshydb.com/{accountName}".ToLowerInvariant(), client.GetApiUrl().ToLowerInvariant());
        }

        [Fact]
        public void ShouldIncludeAccountNameInAuthUrl()
        {
            var accountName = Generator.RandomString(5);
            var client = new MeshyDB(accountName, Generator.RandomString(36));

            Assert.Equal($"https://auth.meshydb.com/{accountName}".ToLowerInvariant(), client.GetAuthUrl().ToLowerInvariant());
        }

        [Fact]
        public void ShouldHaveTenantWhenCreated()
        {
            var tenant = Generator.RandomString(5);
            var client = new MeshyDB(Generator.RandomString(5), tenant, Generator.RandomString(36));
            Assert.Equal(tenant, client.Tenant);
        }

        [Fact]
        public void ShouldHaveTenantTrimmedWhenCreated()
        {
            var tenant = Generator.RandomString(5);
            var client = new MeshyDB(Generator.RandomString(5), $" {tenant} ", Generator.RandomString(36));
            Assert.Equal(tenant, client.Tenant);
        }

        [Fact]
        public void ShouldHaveUsersService()
        {
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            authService.Setup(x => x.LoginAnonymouslyAsync(It.IsAny<string>()))
                       .Returns(() =>
                       {
                           return Task.FromResult(Generator.RandomString(25));
                       });

            database.AuthenticationService = authService.Object;
            var client = database.LoginAnonymously();
            Assert.NotNull(client.Users);
        }

        [Fact]
        public void ShouldHaveMeshesService()
        {
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            authService.Setup(x => x.LoginAnonymouslyAsync(It.IsAny<string>()))
                       .Returns(() =>
                       {
                           return Task.FromResult(Generator.RandomString(25));
                       });

            database.AuthenticationService = authService.Object;
            var client = database.LoginAnonymously();
            Assert.NotNull(client.Meshes);
        }

        [Fact]
        public void ShouldLoginAnonymouslyAsyncSuccessfully()
        {
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            var identifier = Generator.RandomString(25);

            authService.Setup(x => x.LoginAnonymouslyAsync(It.IsAny<string>()))
                       .Returns(() =>
                       {
                           return Task.FromResult(identifier);
                       });

            database.AuthenticationService = authService.Object;
            var client = database.LoginAnonymouslyAsync().ConfigureAwait(true).GetAwaiter().GetResult();
            Assert.NotNull(client);
        }

        [Fact]
        public void ShouldLoginAnonymouslySuccessfully()
        {
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            var identifier = Generator.RandomString(25);

            authService.Setup(x => x.LoginAnonymouslyAsync(It.IsAny<string>()))
                       .Returns(() =>
                       {
                           return Task.FromResult(identifier);
                       });

            database.AuthenticationService = authService.Object;
            var client = database.LoginAnonymously();
            Assert.NotNull(client);
        }

        [Fact]
        public void ShouldLoginWithPasswordAsyncSuccessfully()
        {
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            var identifier = Generator.RandomString(25);

            authService.Setup(x => x.LoginWithPasswordAsync(It.IsAny<string>(), It.IsAny<string>()))
                       .Returns(() =>
                       {
                           return Task.FromResult(identifier);
                       });

            database.AuthenticationService = authService.Object;
            var client = database.LoginWithPasswordAsync(Generator.RandomString(10), Generator.RandomString(10)).ConfigureAwait(true).GetAwaiter().GetResult();
            Assert.NotNull(client);
        }

        [Fact]
        public void ShouldLoginWithPasswordSuccessfully()
        {
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            var identifier = Generator.RandomString(25);

            authService.Setup(x => x.LoginWithPasswordAsync(It.IsAny<string>(), It.IsAny<string>()))
                       .Returns(() =>
                       {
                           return Task.FromResult(identifier);
                       });

            database.AuthenticationService = authService.Object;
            var client = database.LoginWithPassword(Generator.RandomString(10), Generator.RandomString(10));
            Assert.NotNull(client);
        }

        [Fact]
        public void ShouldLoginWithPersistanceAsyncSuccessfully()
        {
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            var identifier = Generator.RandomString(25);

            authService.Setup(x => x.LoginWithPersistanceAsync(It.IsAny<string>()))
                       .Returns(() =>
                       {
                           return Task.FromResult(identifier);
                       });

            database.AuthenticationService = authService.Object;
            var client = database.LoginWithPersistanceAsync(Generator.RandomString(10)).ConfigureAwait(true).GetAwaiter().GetResult();
            Assert.NotNull(client);
        }

        [Fact]
        public void ShouldLoginWithPersistanceSuccessfully()
        {
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            var identifier = Generator.RandomString(25);

            authService.Setup(x => x.LoginWithPersistanceAsync(It.IsAny<string>()))
                       .Returns(() =>
                       {
                           return Task.FromResult(identifier);
                       });

            database.AuthenticationService = authService.Object;
            var client = database.LoginWithPersistance(Generator.RandomString(10));
            Assert.NotNull(client);
        }

        #region RegisterUserAsync

        [Fact]
        public void ShouldRegisterUserAsyncSuccessfully()
        {
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            var expected = new UserVerificationHash();
            authService.Setup(x => x.RegisterAsync(It.IsAny<RegisterUser>())).Returns(() =>
            {
                return Task.FromResult(expected);
            });

            database.AuthenticationService = authService.Object;

            var actual = database.RegisterUserAsync(new RegisterUser(Generator.RandomString(5), Generator.RandomString(5))).ConfigureAwait(true).GetAwaiter().GetResult();
            Assert.Equal(expected, actual);
        }

        #endregion

        #region RegisterUser
        [Fact]
        public void ShouldRegisterUserSuccessfully()
        {
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            var expected = new UserVerificationHash();
            authService.Setup(x => x.RegisterAsync(It.IsAny<RegisterUser>())).Returns(() =>
            {
                return Task.FromResult(expected);
            });

            database.AuthenticationService = authService.Object;

            var actual = database.RegisterUser(new RegisterUser(Generator.RandomString(5), Generator.RandomString(5)));
            Assert.Equal(expected, actual);
        }

        #endregion RegisterUser

        #region ForgotPasswordAsync

        [Fact]
        public void ShouldForgotPasswordAsyncSuccessfully()
        {
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
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

            database.AuthenticationService = authService.Object;

            var username = Generator.RandomString(5);
            var actual = database.ForgotPasswordAsync(username).ConfigureAwait(true).GetAwaiter().GetResult();
            Assert.Equal(expected, actual);
            Assert.Equal(username, passedUsername);
        }

        #endregion

        #region ForgotPassword

        [Fact]
        public void ShouldForgotPasswordSuccessfully()
        {
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
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

            database.AuthenticationService = authService.Object;

            var username = Generator.RandomString(5);
            var actual = database.ForgotPassword(username);
            Assert.Equal(expected, actual);
            Assert.Equal(username, passedUsername);
        }

        #endregion

        #region ResetPasswordAsync

        [Fact]
        public void ShouldResetPasswordAsyncSuccessfully()
        {
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
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

            database.AuthenticationService = authService.Object;

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

            database.ResetPasswordAsync(resetPassword).ConfigureAwait(true).GetAwaiter().GetResult();
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
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
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

            database.AuthenticationService = authService.Object;

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

            database.ResetPassword(resetPassword);
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
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
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

            database.AuthenticationService = authService.Object;

            var now = DateTimeOffset.Now;
            var userVerificationCheck = new UserVerificationCheck()
            {
                Expires = now,
                Hash = Generator.RandomString(5),
                Hint = Generator.RandomString(5),
                Username = Generator.RandomString(5),
                VerificationCode = Generator.RandomString(5)
            };

            database.VerifyAsync(userVerificationCheck).ConfigureAwait(true).GetAwaiter().GetResult();
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
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
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

            database.AuthenticationService = authService.Object;

            var now = DateTimeOffset.Now;
            var userVerificationCheck = new UserVerificationCheck()
            {
                Expires = now,
                Hash = Generator.RandomString(5),
                Hint = Generator.RandomString(5),
                Username = Generator.RandomString(5),
                VerificationCode = Generator.RandomString(5)
            };

            database.Verify(userVerificationCheck);
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
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
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

            database.AuthenticationService = authService.Object;

            var now = DateTimeOffset.Now;
            var userVerificationCheck = new UserVerificationCheck()
            {
                Expires = now,
                Hash = Generator.RandomString(5),
                Hint = Generator.RandomString(5),
                Username = Generator.RandomString(5),
                VerificationCode = Generator.RandomString(5)
            };

            var result = database.CheckHashAsync(userVerificationCheck).ConfigureAwait(true).GetAwaiter().GetResult();
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
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
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

            database.AuthenticationService = authService.Object;

            var now = DateTimeOffset.Now;
            var userVerificationCheck = new UserVerificationCheck()
            {
                Expires = now,
                Hash = Generator.RandomString(5),
                Hint = Generator.RandomString(5),
                Username = Generator.RandomString(5),
                VerificationCode = Generator.RandomString(5)
            };

            var result = database.CheckHash(userVerificationCheck);
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
