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
    public class UsersServiceTests
    {
        [Fact]
        public void ShouldGetLogggedInUserAsyncSuccessfully()
        {
            var requestService = new Mock<IRequestService>();
            var passedPath = string.Empty;
            requestService.Setup(x => x.GetRequest<User>(It.IsAny<string>())).Callback<string>((path) =>
            {
                passedPath = path;
            }).Returns(() =>
            {
                return Task.FromResult(new User());
            });

            var service = new UsersService(requestService.Object);
            var user = service.GetLoggedInUserAsync().Result;
            Assert.NotNull(user);
            Assert.Equal("users/me", passedPath);
            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldGetLogggedInUserSuccessfully()
        {
            var requestService = new Mock<IRequestService>();
            var passedPath = string.Empty;
            requestService.Setup(x => x.GetRequest<User>(It.IsAny<string>())).Callback<string>((path) =>
            {
                passedPath = path;
            }).Returns(() =>
            {
                return Task.FromResult(new User());
            });

            var service = new UsersService(requestService.Object);
            var user = service.GetLoggedInUser();
            Assert.NotNull(user);
            Assert.Equal("users/me", passedPath);
            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldGetUserAsyncSuccessfully()
        {
            var requestService = new Mock<IRequestService>();
            var passedPath = string.Empty;
            requestService.Setup(x => x.GetRequest<User>(It.IsAny<string>())).Callback<string>((path) =>
            {
                passedPath = path;
            }).Returns(() =>
            {
                return Task.FromResult(new User());
            });

            var service = new UsersService(requestService.Object);
            var userId = Generator.RandomString(10);
            var user = service.GetUserAsync(userId).Result;

            Assert.NotNull(user);
            Assert.Equal($"users/{userId}", passedPath);

            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldGetUserSuccessfully()
        {
            var requestService = new Mock<IRequestService>();
            var passedPath = string.Empty;
            requestService.Setup(x => x.GetRequest<User>(It.IsAny<string>())).Callback<string>((path) =>
            {
                passedPath = path;
            }).Returns(() =>
            {
                return Task.FromResult(new User());
            });

            var service = new UsersService(requestService.Object);
            var userId = Generator.RandomString(10);
            var user = service.GetUser(userId);

            Assert.NotNull(user);
            Assert.Equal($"users/{userId}", passedPath);

            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldGetUsersAsyncSuccessfully()
        {
            var requestService = new Mock<IRequestService>();
            var passedPath = string.Empty;
            requestService.Setup(x => x.GetRequest<PageResult<User>>(It.IsAny<string>())).Callback<string>((path) =>
            {
                passedPath = path;
            }).Returns(() =>
            {
                return Task.FromResult(new PageResult<User>());
            });

            var service = new UsersService(requestService.Object);
            var userId = Generator.RandomString(10);
            var user = service.GetUsersAsync(new List<string>() { "a","b","c" },new List<string> { "admin", "user" }).Result;
            
            Assert.Equal($"users?query=a+b+c&roles=admin&roles=user&activeOnly=True&page=1&pageSize=200", passedPath);

            requestService.VerifyAll();
        }
        
        [Fact]
        public void ShouldGetUsersSuccessfully()
        {
            var requestService = new Mock<IRequestService>();
            var passedPath = string.Empty;
            requestService.Setup(x => x.GetRequest<PageResult<User>>(It.IsAny<string>())).Callback<string>((path) =>
            {
                passedPath = path;
            }).Returns(() =>
            {
                return Task.FromResult(new PageResult<User>());
            });

            var service = new UsersService(requestService.Object);
            var userId = Generator.RandomString(10);
            var user = service.GetUsers(new List<string>() { "a", "b", "c" }, new List<string> { "admin", "user" });

            Assert.Equal($"users?query=a+b+c&roles=admin&roles=user&activeOnly=True&page=1&pageSize=200", passedPath);

            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldUpdateUserAsyncSuccessfully()
        {
            var requestService = new Mock<IRequestService>();
            var passedPath = string.Empty;
            var passedModel = default(User);
            requestService.Setup(x => x.PutRequest<User>(It.IsAny<string>(), It.IsAny<object>()))
               .Callback<string, object>((path, model) =>
              {
                  passedPath = path;
                  passedModel = model as User;
              })
              .Returns(() =>
              {
                  return Task.FromResult(new User());
              });

            var service = new UsersService(requestService.Object);
            var userId = Generator.RandomString(10);

            var updatedUser = new User()
            {
                Id = userId
            };

            var user = service.UpdateUserAsync(updatedUser).Result;

            Assert.Equal(passedModel, updatedUser);
            Assert.Equal($"users/{userId}", passedPath);

            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldUpdateUserSuccessfully()
        {
            var requestService = new Mock<IRequestService>();
            var passedPath = string.Empty;
            var passedModel = default(User);
            requestService.Setup(x => x.PutRequest<User>(It.IsAny<string>(), It.IsAny<object>()))
               .Callback<string, object>((path, model) =>
               {
                   passedPath = path;
                   passedModel = model as User;
               })
              .Returns(() =>
              {
                  return Task.FromResult(new User());
              });

            var service = new UsersService(requestService.Object);
            var userId = Generator.RandomString(10);

            var updatedUser = new User()
            {
                Id = userId
            };

            var user = service.UpdateUser(updatedUser);

            Assert.Equal(passedModel, updatedUser);
            Assert.Equal($"users/{userId}", passedPath);

            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldUpdateUserAsyncByIdSuccessfully()
        {
            var requestService = new Mock<IRequestService>();
            var passedPath = string.Empty;
            var passedModel = default(User);
            requestService.Setup(x => x.PutRequest<User>(It.IsAny<string>(), It.IsAny<object>()))
               .Callback<string, object>((path, model) =>
               {
                   passedPath = path;
                   passedModel = model as User;
               })
              .Returns(() =>
              {
                  return Task.FromResult(new User());
              });

            var service = new UsersService(requestService.Object);
            var userId = Generator.RandomString(10);

            var updatedUser = new User()
            {
                Id = Generator.RandomString(10)
            };

            var user = service.UpdateUserAsync(userId, updatedUser).Result;

            Assert.Equal(passedModel, updatedUser);
            Assert.Equal($"users/{userId}", passedPath);

            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldUpdateUserByIdSuccessfully()
        {
            var requestService = new Mock<IRequestService>();
            var passedPath = string.Empty;
            var passedModel = default(User);
            requestService.Setup(x => x.PutRequest<User>(It.IsAny<string>(), It.IsAny<object>()))
               .Callback<string, object>((path, model) =>
               {
                   passedPath = path;
                   passedModel = model as User;
               })
              .Returns(() =>
              {
                  return Task.FromResult(new User());
              });

            var service = new UsersService(requestService.Object);
            var userId = Generator.RandomString(10);

            var updatedUser = new User()
            {
                Id = Generator.RandomString(10)
            };

            var user = service.UpdateUser(userId, updatedUser);

            Assert.Equal(passedModel, updatedUser);
            Assert.Equal($"users/{userId}", passedPath);

            requestService.VerifyAll();
        }
        
        [Fact]
        public void ShouldDeleteUserAsyncSuccessfully()
        {
            var requestService = new Mock<IRequestService>();
            var passedPath = string.Empty;

            requestService.Setup(x => x.DeleteRequest<object>(It.IsAny<string>()))
               .Callback<string>((path) =>
               {
                   passedPath = path;
               })
              .Returns(() =>
              {
                  return Task.FromResult<object>(null);
              });

            var service = new UsersService(requestService.Object);
            var userId = Generator.RandomString(10);

            service.DeleteUserAsync(userId).ConfigureAwait(true).GetAwaiter().GetResult();
            
            Assert.Equal($"users/{userId}", passedPath);

            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldDeleteUserSuccessfully()
        {
            var requestService = new Mock<IRequestService>();
            var passedPath = string.Empty;

            requestService.Setup(x => x.DeleteRequest<object>(It.IsAny<string>()))
               .Callback<string>((path) =>
               {
                   passedPath = path;
               })
              .Returns(() =>
              {
                  return Task.FromResult<object>(null);
              });

            var service = new UsersService(requestService.Object);
            var userId = Generator.RandomString(10);

            service.DeleteUser(userId);

            Assert.Equal($"users/{userId}", passedPath);

            requestService.VerifyAll();
        }
    }
}
