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
        public void ShouldThrowArgumentExceptionWithNullClientKey()
        {
            Assert.Throws<ArgumentException>(() => new MeshyDB(null, Generator.RandomString(36)));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithEmptyClientKey()
        {
            Assert.Throws<ArgumentException>(() => new MeshyDB(string.Empty, Generator.RandomString(36)));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithWhitespaceClientKey()
        {
            Assert.Throws<ArgumentException>(() => new MeshyDB(new string(' ', 5), Generator.RandomString(36)));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithNullPublicKey()
        {
            Assert.Throws<ArgumentException>(() => new MeshyDB(Generator.RandomString(5), null));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithEmptyPublicKey()
        {
            Assert.Throws<ArgumentException>(() => new MeshyDB(Generator.RandomString(5), string.Empty));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithWhitespacePublicKey()
        {
            Assert.Throws<ArgumentException>(() => new MeshyDB(Generator.RandomString(5), new string(' ', 5)));
        }

        [Fact]
        public void ShouldIncludeClientKeyInApiUrl()
        {
            var clientKey = Generator.RandomString(5);
            var client = new MeshyDB(clientKey, Generator.RandomString(36));

            Assert.Equal($"https://api.meshydb.com/{clientKey}".ToLower(), client.GetApiUrl().ToLower());
        }

        [Fact]
        public void ShouldIncludeClientKeyInAuthUrl()
        {
            var clientKey = Generator.RandomString(5);
            var client = new MeshyDB(clientKey, Generator.RandomString(36));

            Assert.Equal($"https://auth.meshydb.com/{clientKey}".ToLower(), client.GetAuthUrl().ToLower());
        }

        [Fact]
        public void ShouldHaveUsersService()
        {
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            authService.Setup(x => x.LoginAnonymouslyAsync())
                       .Returns(() =>
                       {
                           return Task.FromResult(Generator.RandomString(25));
                       });

            database.AuthenticationService = authService.Object;
            var client = database.LoginWithAnonymously();
            Assert.NotNull(client.Users);
        }

        [Fact]
        public void ShouldHaveMeshesService()
        {
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            authService.Setup(x => x.LoginAnonymouslyAsync())
                       .Returns(() =>
                       {
                           return Task.FromResult(Generator.RandomString(25));
                       });

            database.AuthenticationService = authService.Object;
            var client = database.LoginWithAnonymously();
            Assert.NotNull(client.Meshes);
        }

        [Fact]
        public void ShouldLoginAnonymouslyAsyncSuccessfully()
        {
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            var identifier = Generator.RandomString(25);

            authService.Setup(x => x.LoginAnonymouslyAsync())
                       .Returns(() =>
                       {
                           return Task.FromResult(identifier);
                       });

            database.AuthenticationService = authService.Object;
            var client = database.LoginWithAnonymouslyAsync().ConfigureAwait(true).GetAwaiter().GetResult();
            Assert.NotNull(client);
        }

        [Fact]
        public void ShouldLoginAnonymouslySuccessfully()
        {
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();
            var identifier = Generator.RandomString(25);

            authService.Setup(x => x.LoginAnonymouslyAsync())
                       .Returns(() =>
                       {
                           return Task.FromResult(identifier);
                       });

            database.AuthenticationService = authService.Object;
            var client = database.LoginWithAnonymously();
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

        // create user
        [Fact]
        public void ShouldCreateUserAsyncSuccessfully()
        {
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();

            var username = Generator.RandomString(10);
            var expectedUser = new User()
            {
                Username = username,
                IsActive = true,
                Verified = true,
            };
            authService.Setup(x => x.CreateUserAsync(It.IsAny<NewUser>())).Returns(() =>
            {
                return Task.FromResult(expectedUser);
            });

            database.AuthenticationService = authService.Object;

            var actualUser = database.CreateNewUserAsync(new NewUser(username, Generator.RandomString(25))
            {
                IsActive = true,
                Verified = true
            }).ConfigureAwait(true).GetAwaiter().GetResult();

            Assert.Equal(expectedUser, actualUser);
        }

        [Fact]
        public void ShouldCreateUserSuccessfully()
        {
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();

            var username = Generator.RandomString(10);
            var expectedUser = new User()
            {
                Username = username,
                IsActive = true,
                Verified = true,
            };
            authService.Setup(x => x.CreateUserAsync(It.IsAny<NewUser>())).Returns(() =>
            {
                return Task.FromResult(expectedUser);
            });

            database.AuthenticationService = authService.Object;

            var actualUser = database.CreateNewUser(new NewUser(username, Generator.RandomString(25))
            {
                IsActive = true,
                Verified = true
            });

            Assert.Equal(expectedUser, actualUser);
        }

        [Fact]
        public void ShouldCreateUserAsyncShouldThrowArgumentExceptionWithNullUserName()
        {
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();

            database.AuthenticationService = authService.Object;

            Assert.Throws<ArgumentException>(() => database.CreateNewUser(new NewUser(null, Generator.RandomString(25))));
        }

        [Fact]
        public void ShouldCreateUserAsyncShouldThrowArgumentExceptionWithEmptyUserName()
        {
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();

            database.AuthenticationService = authService.Object;

            Assert.Throws<ArgumentException>(() => database.CreateNewUser(new NewUser(new string(' ', 5), Generator.RandomString(25))));
        }

        [Fact]
        public void ShouldCreateUserAsyncShouldThrowArgumentExceptionWithNullPassword()
        {
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();

            database.AuthenticationService = authService.Object;

            Assert.Throws<ArgumentException>(() => database.CreateNewUser(new NewUser(Generator.RandomString(25), null)));
        }

        [Fact]
        public void ShouldCreateUserAsyncShouldThrowArgumentExceptionWithEmptyPassword()
        {
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();

            database.AuthenticationService = authService.Object;

            Assert.Throws<ArgumentException>(() => database.CreateNewUser(new NewUser(Generator.RandomString(25), new string(' ', 5))));
        }

        // forgot password
        [Fact]
        public void ShouldForgotPasswordAsyncSuccessfully()
        {
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();

            var expected = new PasswordResetHash();
            authService.Setup(x => x.ForgotPasswordAsync(It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult(expected);
            });

            database.AuthenticationService = authService.Object;
            var actual = database.ForgotPasswordAsync(Generator.RandomString(10)).ConfigureAwait(true).GetAwaiter().GetResult();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldForgotPasswordSuccessfully()
        {
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();

            var expected = new PasswordResetHash();
            authService.Setup(x => x.ForgotPasswordAsync(It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult(expected);
            });

            database.AuthenticationService = authService.Object;
            var actual = database.ForgotPassword(Generator.RandomString(10));
            Assert.Equal(expected, actual);
        }

        // reset password
        [Fact]
        public void ShouldResetPasswordAsyncSuccessfully()
        {
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();

            authService.Setup(x => x.ResetPasswordAsync(It.IsAny<PasswordResetHash>(), It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult(0);
            });

            database.AuthenticationService = authService.Object;

            database.ResetPasswordAsync(new PasswordResetHash(), Generator.RandomString(10)).ConfigureAwait(true).GetAwaiter().GetResult();
            Assert.True(true);
        }

        [Fact]
        public void ShouldResetPasswordSuccessfully()
        {
            var database = new MeshyDB(Generator.RandomString(5), Generator.RandomString(36));
            var authService = new Mock<IAuthenticationService>();

            authService.Setup(x => x.ResetPasswordAsync(It.IsAny<PasswordResetHash>(), It.IsAny<string>())).Returns(() =>
            {
                return Task.FromResult(0);
            });

            database.AuthenticationService = authService.Object;

            database.ResetPassword(new PasswordResetHash(), Generator.RandomString(10));
            Assert.True(true);
        }
    }
}
