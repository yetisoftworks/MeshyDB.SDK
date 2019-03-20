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
            var user = service.GetSelfAsync().Result;
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
            var user = service.GetSelf();
            Assert.NotNull(user);
            Assert.Equal("users/me", passedPath);
            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldUpdateSelfAsyncSuccessfully()
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

            var user = service.UpdateSelfAsync(updatedUser).Result;

            Assert.Equal(passedModel, updatedUser);
            Assert.Equal($"users/me", passedPath);

            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldUpdateSelfSuccessfully()
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

            var user = service.UpdateSelf(updatedUser);

            Assert.Equal(passedModel, updatedUser);
            Assert.Equal($"users/me", passedPath);

            requestService.VerifyAll();
        }
    }
}
