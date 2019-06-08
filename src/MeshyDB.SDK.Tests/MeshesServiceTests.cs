using MeshyDB.SDK.Attributes;
using MeshyDB.SDK.Enums;
using MeshyDB.SDK.Models;
using MeshyDB.SDK.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace MeshyDB.SDK.Tests
{
    public class MeshesServiceTests
    {
        [Fact]
        public void ShouldCreate()
        {
            var requestService = new Mock<IRequestService>();

            var service = new MeshesService(requestService.Object);

            Assert.NotNull(service);
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithNullRequestService()
        {
            Assert.Throws<ArgumentNullException>(() => new MeshesService(null));
        }

        #region Get Request

        [Fact]
        public void ShouldGetRequestHaveCorrectMeshNameFromClassAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;

            requestService.Setup(x => x.GetRequest<TestMeshNameFromClass>(It.IsAny<string>())).Callback((string url) =>
            {
                passedUrl = url;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<TestMeshNameFromClass>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);
            var id = Generator.RandomString(25);
            service.GetData<TestMeshNameFromClass>(id);

            Assert.Equal($"meshes/testmeshnamefromclass/{id}", passedUrl);
            requestService.VerifyAll();
        }

        [Fact]
        public async Task ShouldGetAsyncRequestHaveCorrectMeshNameFromClassAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;

            requestService.Setup(x => x.GetRequest<TestMeshNameFromClass>(It.IsAny<string>())).Callback((string url) =>
            {
                passedUrl = url;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<TestMeshNameFromClass>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);
            var id = Generator.RandomString(25);
            await service.GetDataAsync<TestMeshNameFromClass>(id).ConfigureAwait(true);

            Assert.Equal($"meshes/testmeshnamefromclass/{id}", passedUrl);
            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldGetRequestHaveCorrectMeshNameFromAttributeAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;

            requestService.Setup(x => x.GetRequest<MeshNameAttributeClassTest>(It.IsAny<string>())).Callback((string url) =>
            {
                passedUrl = url;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<MeshNameAttributeClassTest>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);
            var id = Generator.RandomString(25);
            service.GetData<MeshNameAttributeClassTest>(id);

            Assert.Equal($"meshes/{MeshName.ToLowerInvariant()}/{id}", passedUrl);
            requestService.VerifyAll();
        }

        [Fact]
        public async Task ShouldGetAsyncRequestHaveCorrectMeshNameFromAttributeAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;

            requestService.Setup(x => x.GetRequest<MeshNameAttributeClassTest>(It.IsAny<string>())).Callback((string url) =>
            {
                passedUrl = url;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<MeshNameAttributeClassTest>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);
            var id = Generator.RandomString(25);
            await service.GetDataAsync<MeshNameAttributeClassTest>(id).ConfigureAwait(true);

            Assert.Equal($"meshes/{MeshName.ToLowerInvariant()}/{id}", passedUrl);
            requestService.VerifyAll();
        }

        #endregion

        #region Create Request

        [Fact]
        public void ShouldCreateRequestHaveCorrectMeshNameFromClassAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;
            var passedModel = default(TestMeshNameFromClass);
            var passedFormat = default(RequestDataFormat);

            var expectedModel = new TestMeshNameFromClass();
            requestService.Setup(x => x.PostRequest<TestMeshNameFromClass>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>())).Callback((string url, object model, RequestDataFormat format) =>
             {
                 passedUrl = url;
                 passedModel = model as TestMeshNameFromClass;
                 passedFormat = format;
             }).Returns(() =>
             {
                 return Task.FromResult(It.IsAny<TestMeshNameFromClass>());
             }).Verifiable();

            var service = new MeshesService(requestService.Object);
            service.Create(expectedModel);

            Assert.Equal($"meshes/testmeshnamefromclass", passedUrl);
            Assert.Equal(expectedModel, passedModel);
            Assert.Equal(RequestDataFormat.Json, passedFormat);

            requestService.VerifyAll();
        }

        [Fact]
        public async Task ShouldCreateAsyncRequestHaveCorrectMeshNameFromClassAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;
            var passedModel = default(TestMeshNameFromClass);
            var passedFormat = default(RequestDataFormat);

            var expectedModel = new TestMeshNameFromClass();
            requestService.Setup(x => x.PostRequest<TestMeshNameFromClass>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>())).Callback((string url, object model, RequestDataFormat format) =>
            {
                passedUrl = url;
                passedModel = model as TestMeshNameFromClass;
                passedFormat = format;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<TestMeshNameFromClass>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);
            await service.CreateAsync(expectedModel).ConfigureAwait(true);

            Assert.Equal($"meshes/testmeshnamefromclass", passedUrl);
            Assert.Equal(expectedModel, passedModel);
            Assert.Equal(RequestDataFormat.Json, passedFormat);

            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldCreateRequestHaveCorrectMeshNameFromAttributeAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;
            var passedModel = default(MeshNameAttributeClassTest);
            var passedFormat = default(RequestDataFormat);

            var expectedModel = new MeshNameAttributeClassTest();
            requestService.Setup(x => x.PostRequest<MeshNameAttributeClassTest>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>())).Callback((string url, object model, RequestDataFormat format) =>
            {
                passedUrl = url;
                passedModel = model as MeshNameAttributeClassTest;
                passedFormat = format;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<MeshNameAttributeClassTest>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);
            service.Create(expectedModel);

            Assert.Equal($"meshes/{MeshName.ToLowerInvariant()}", passedUrl);
            Assert.Equal(expectedModel, passedModel);
            Assert.Equal(RequestDataFormat.Json, passedFormat);

            requestService.VerifyAll();
        }

        [Fact]
        public async Task ShouldCreateAsyncRequestHaveCorrectMeshNameFromAttributeAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;
            var passedModel = default(MeshNameAttributeClassTest);
            var passedFormat = default(RequestDataFormat);

            var expectedModel = new MeshNameAttributeClassTest();
            requestService.Setup(x => x.PostRequest<MeshNameAttributeClassTest>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<RequestDataFormat>())).Callback((string url, object model, RequestDataFormat format) =>
            {
                passedUrl = url;
                passedModel = model as MeshNameAttributeClassTest;
                passedFormat = format;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<MeshNameAttributeClassTest>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);
            await service.CreateAsync(expectedModel).ConfigureAwait(true);

            Assert.Equal($"meshes/{MeshName.ToLowerInvariant()}", passedUrl);
            Assert.Equal(expectedModel, passedModel);
            Assert.Equal(RequestDataFormat.Json, passedFormat);

            requestService.VerifyAll();
        }

        #endregion

        #region Update Request

        [Fact]
        public void ShouldUpdateRequestHaveCorrectMeshNameFromClassAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;
            var passedModel = default(TestMeshNameFromClass);

            var expectedModel = new TestMeshNameFromClass();
            requestService.Setup(x => x.PutRequest<TestMeshNameFromClass>(It.IsAny<string>(), It.IsAny<object>())).Callback((string url, object model) =>
            {
                passedUrl = url;
                passedModel = model as TestMeshNameFromClass;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<TestMeshNameFromClass>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);
            var id = Generator.RandomString(25);

            service.Update(id, expectedModel);

            Assert.Equal($"meshes/testmeshnamefromclass/{id}", passedUrl);
            Assert.Equal(expectedModel, passedModel);

            requestService.VerifyAll();
        }

        [Fact]
        public async Task ShouldUpdateAsyncRequestHaveCorrectMeshNameFromClassAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;
            var passedModel = default(TestMeshNameFromClass);

            var expectedModel = new TestMeshNameFromClass();
            requestService.Setup(x => x.PutRequest<TestMeshNameFromClass>(It.IsAny<string>(), It.IsAny<object>())).Callback((string url, object model) =>
            {
                passedUrl = url;
                passedModel = model as TestMeshNameFromClass;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<TestMeshNameFromClass>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);
            var id = Generator.RandomString(25);

            await service.UpdateAsync(id, expectedModel).ConfigureAwait(true);

            Assert.Equal($"meshes/testmeshnamefromclass/{id}", passedUrl);
            Assert.Equal(expectedModel, passedModel);

            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldUpdateRequestHaveCorrectMeshNameFromAttributeAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;
            var passedModel = default(MeshNameAttributeClassTest);

            var expectedModel = new MeshNameAttributeClassTest();
            requestService.Setup(x => x.PutRequest<MeshNameAttributeClassTest>(It.IsAny<string>(), It.IsAny<object>())).Callback((string url, object model) =>
            {
                passedUrl = url;
                passedModel = model as MeshNameAttributeClassTest;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<MeshNameAttributeClassTest>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);
            var id = Generator.RandomString(25);

            service.Update(id, expectedModel);

            Assert.Equal($"meshes/{MeshName.ToLowerInvariant()}/{id}", passedUrl);
            Assert.Equal(expectedModel, passedModel);

            requestService.VerifyAll();
        }

        [Fact]
        public async Task ShouldUpdateAsyncRequestHaveCorrectMeshNameFromAttributeAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;
            var passedModel = default(MeshNameAttributeClassTest);

            var expectedModel = new MeshNameAttributeClassTest();
            requestService.Setup(x => x.PutRequest<MeshNameAttributeClassTest>(It.IsAny<string>(), It.IsAny<object>())).Callback((string url, object model) =>
            {
                passedUrl = url;
                passedModel = model as MeshNameAttributeClassTest;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<MeshNameAttributeClassTest>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);
            var id = Generator.RandomString(25);

            await service.UpdateAsync(id, expectedModel).ConfigureAwait(true);

            Assert.Equal($"meshes/{MeshName.ToLowerInvariant()}/{id}", passedUrl);
            Assert.Equal(expectedModel, passedModel);

            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldUpdateFromModelRequestHaveCorrectMeshNameFromClassAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;
            var passedModel = default(TestMeshNameFromClass);

            var expectedModel = new TestMeshNameFromClass();
            requestService.Setup(x => x.PutRequest<TestMeshNameFromClass>(It.IsAny<string>(), It.IsAny<object>())).Callback((string url, object model) =>
            {
                passedUrl = url;
                passedModel = model as TestMeshNameFromClass;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<TestMeshNameFromClass>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);
            var id = Generator.RandomString(25);

            expectedModel.Id = id;

            service.Update(expectedModel);

            Assert.Equal($"meshes/testmeshnamefromclass/{id}", passedUrl);
            Assert.Equal(expectedModel, passedModel);

            requestService.VerifyAll();
        }

        [Fact]
        public async Task ShouldUpdateAsyncFromModelRequestHaveCorrectMeshNameFromClassAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;
            var passedModel = default(TestMeshNameFromClass);

            var expectedModel = new TestMeshNameFromClass();
            requestService.Setup(x => x.PutRequest<TestMeshNameFromClass>(It.IsAny<string>(), It.IsAny<object>())).Callback((string url, object model) =>
            {
                passedUrl = url;
                passedModel = model as TestMeshNameFromClass;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<TestMeshNameFromClass>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);
            var id = Generator.RandomString(25);

            expectedModel.Id = id;

            await service.UpdateAsync(expectedModel).ConfigureAwait(true);

            Assert.Equal($"meshes/testmeshnamefromclass/{id}", passedUrl);
            Assert.Equal(expectedModel, passedModel);

            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldUpdateFromModelRequestHaveCorrectMeshNameFromAttributeAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;
            var passedModel = default(MeshNameAttributeClassTest);

            var expectedModel = new MeshNameAttributeClassTest();
            requestService.Setup(x => x.PutRequest<MeshNameAttributeClassTest>(It.IsAny<string>(), It.IsAny<object>())).Callback((string url, object model) =>
            {
                passedUrl = url;
                passedModel = model as MeshNameAttributeClassTest;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<MeshNameAttributeClassTest>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);
            var id = Generator.RandomString(25);

            expectedModel.Id = id;

            service.Update(expectedModel);

            Assert.Equal($"meshes/{MeshName.ToLowerInvariant()}/{id}", passedUrl);
            Assert.Equal(expectedModel, passedModel);

            requestService.VerifyAll();
        }

        [Fact]
        public async Task ShouldUpdateAsyncFromModelRequestHaveCorrectMeshNameFromAttributeAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;
            var passedModel = default(MeshNameAttributeClassTest);

            var expectedModel = new MeshNameAttributeClassTest();
            requestService.Setup(x => x.PutRequest<MeshNameAttributeClassTest>(It.IsAny<string>(), It.IsAny<object>())).Callback((string url, object model) =>
            {
                passedUrl = url;
                passedModel = model as MeshNameAttributeClassTest;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<MeshNameAttributeClassTest>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);
            var id = Generator.RandomString(25);

            expectedModel.Id = id;

            await service.UpdateAsync(expectedModel).ConfigureAwait(true);

            Assert.Equal($"meshes/{MeshName.ToLowerInvariant()}/{id}", passedUrl);
            Assert.Equal(expectedModel, passedModel);

            requestService.VerifyAll();
        }

        #endregion

        #region Delete Request

        [Fact]
        public void ShouldDeleteRequestHaveCorrectMeshNameFromClassAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;

            requestService.Setup(x => x.DeleteRequest<object>(It.IsAny<string>())).Callback((string url) =>
            {
                passedUrl = url;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<object>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);
            var id = Generator.RandomString(25);

            service.Delete<TestMeshNameFromClass>(id);

            Assert.Equal($"meshes/testmeshnamefromclass/{id}", passedUrl);

            requestService.VerifyAll();
        }

        [Fact]
        public async Task ShouldDeleteAsyncRequestHaveCorrectMeshNameFromClassAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;

            requestService.Setup(x => x.DeleteRequest<object>(It.IsAny<string>())).Callback((string url) =>
            {
                passedUrl = url;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<object>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);
            var id = Generator.RandomString(25);

            await service.DeleteAsync<TestMeshNameFromClass>(id).ConfigureAwait(true);

            Assert.Equal($"meshes/testmeshnamefromclass/{id}", passedUrl);

            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldDeleteRequestHaveCorrectMeshNameFromAttributeAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;

            requestService.Setup(x => x.DeleteRequest<object>(It.IsAny<string>())).Callback((string url) =>
            {
                passedUrl = url;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<object>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);
            var id = Generator.RandomString(25);

            service.Delete<MeshNameAttributeClassTest>(id);

            Assert.Equal($"meshes/{MeshName.ToLowerInvariant()}/{id}", passedUrl);

            requestService.VerifyAll();
        }

        [Fact]
        public async Task ShouldDeleteAsyncRequestHaveCorrectMeshNameFromAttributeAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;

            requestService.Setup(x => x.DeleteRequest<object>(It.IsAny<string>())).Callback((string url) =>
            {
                passedUrl = url;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<object>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);
            var id = Generator.RandomString(25);

            await service.DeleteAsync<MeshNameAttributeClassTest>(id).ConfigureAwait(true);

            Assert.Equal($"meshes/{MeshName.ToLowerInvariant()}/{id}", passedUrl);

            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldDeleteFromModelRequestHaveCorrectMeshNameFromClassAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;

            requestService.Setup(x => x.DeleteRequest<object>(It.IsAny<string>())).Callback((string url) =>
            {
                passedUrl = url;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<object>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);
            var id = Generator.RandomString(25);
            var data = new TestMeshNameFromClass();

            data.Id = id;

            service.Delete<TestMeshNameFromClass>(data);

            Assert.Equal($"meshes/testmeshnamefromclass/{id}", passedUrl);

            requestService.VerifyAll();
        }

        [Fact]
        public async Task ShouldDeleteAsyncFromModelRequestHaveCorrectMeshNameFromClassAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;

            requestService.Setup(x => x.DeleteRequest<object>(It.IsAny<string>())).Callback((string url) =>
            {
                passedUrl = url;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<object>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);
            var id = Generator.RandomString(25);
            var data = new TestMeshNameFromClass();

            data.Id = id;

            await service.DeleteAsync<TestMeshNameFromClass>(data).ConfigureAwait(true);

            Assert.Equal($"meshes/testmeshnamefromclass/{id}", passedUrl);

            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldDeleteFromModelRequestHaveCorrectMeshNameFromAttributeAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;

            requestService.Setup(x => x.DeleteRequest<object>(It.IsAny<string>())).Callback((string url) =>
            {
                passedUrl = url;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<object>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);
            var id = Generator.RandomString(25);
            var data = new MeshNameAttributeClassTest();

            data.Id = id;

            service.Delete<MeshNameAttributeClassTest>(data);

            Assert.Equal($"meshes/{MeshName.ToLowerInvariant()}/{id}", passedUrl);

            requestService.VerifyAll();
        }

        [Fact]
        public async Task ShouldDeleteFromModelAsyncRequestHaveCorrectMeshNameFromAttributeAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;

            requestService.Setup(x => x.DeleteRequest<object>(It.IsAny<string>())).Callback((string url) =>
            {
                passedUrl = url;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<object>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);
            var id = Generator.RandomString(25);
            var data = new MeshNameAttributeClassTest();

            data.Id = id;

            await service.DeleteAsync<MeshNameAttributeClassTest>(data).ConfigureAwait(true);

            Assert.Equal($"meshes/{MeshName.ToLowerInvariant()}/{id}", passedUrl);

            requestService.VerifyAll();
        }


        #endregion

        #region Search Manual Request

        [Fact]
        public void ShouldSearchManualRequestHaveCorrectMeshNameFromClassAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;

            requestService.Setup(x => x.GetRequest<PageResult<TestMeshNameFromClass>>(It.IsAny<string>())).Callback((string url) =>
           {
               passedUrl = url;
           }).Returns(() =>
           {
               return Task.FromResult(It.IsAny<PageResult<TestMeshNameFromClass>>());
           }).Verifiable();

            var service = new MeshesService(requestService.Object);

            service.Search<TestMeshNameFromClass>();

            Assert.Equal($"meshes/testmeshnamefromclass?filter=&orderby=&page=1&pageSize=200", passedUrl);

            requestService.VerifyAll();
        }

        [Fact]
        public async Task ShouldSearchManualAsyncRequestHaveCorrectMeshNameFromClassAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;

            requestService.Setup(x => x.GetRequest<PageResult<TestMeshNameFromClass>>(It.IsAny<string>())).Callback((string url) =>
           {
               passedUrl = url;
           }).Returns(() =>
           {
               return Task.FromResult(It.IsAny<PageResult<TestMeshNameFromClass>>());
           }).Verifiable();

            var service = new MeshesService(requestService.Object);

            await service.SearchAsync<TestMeshNameFromClass>().ConfigureAwait(true);

            Assert.Equal($"meshes/testmeshnamefromclass?filter=&orderby=&page=1&pageSize=200", passedUrl);

            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldSearchManualRequestHaveCorrectMeshNameFromAttributeAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;

            requestService.Setup(x => x.GetRequest<PageResult<MeshNameAttributeClassTest>>(It.IsAny<string>())).Callback((string url) =>
            {
                passedUrl = url;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<PageResult<MeshNameAttributeClassTest>>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);

            service.Search<MeshNameAttributeClassTest>();

            Assert.Equal($"meshes/{MeshName.ToLowerInvariant()}?filter=&orderby=&page=1&pageSize=200", passedUrl);

            requestService.VerifyAll();
        }

        [Fact]
        public async Task ShouldSearchManualAsyncRequestHaveCorrectMeshNameFromAttributeAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;

            requestService.Setup(x => x.GetRequest<PageResult<MeshNameAttributeClassTest>>(It.IsAny<string>())).Callback((string url) =>
           {
               passedUrl = url;
           }).Returns(() =>
           {
               return Task.FromResult(It.IsAny<PageResult<MeshNameAttributeClassTest>>());
           }).Verifiable();

            var service = new MeshesService(requestService.Object);

            await service.SearchAsync<MeshNameAttributeClassTest>().ConfigureAwait(true);

            Assert.Equal($"meshes/{MeshName.ToLowerInvariant()}?filter=&orderby=&page=1&pageSize=200", passedUrl);

            requestService.VerifyAll();
        }

        #endregion

        #region Search Compiled Request

        [Fact]
        public void ShouldSearchCompiledRequestHaveCorrectMeshNameFromClassAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;

            requestService.Setup(x => x.GetRequest<PageResult<TestMeshNameFromClass>>(It.IsAny<string>())).Callback((string url) =>
            {
                passedUrl = url;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<PageResult<TestMeshNameFromClass>>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);

            service.Search<TestMeshNameFromClass>(x => true);

            var encodedFilter = WebUtility.UrlEncode("{ }");

            Assert.Equal($"meshes/testmeshnamefromclass?filter={encodedFilter}&orderby=&page=1&pageSize=200", passedUrl);

            requestService.VerifyAll();
        }

        [Fact]
        public async Task ShouldSearchCompiledAsyncRequestHaveCorrectMeshNameFromClassAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;

            requestService.Setup(x => x.GetRequest<PageResult<TestMeshNameFromClass>>(It.IsAny<string>())).Callback((string url) =>
            {
                passedUrl = url;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<PageResult<TestMeshNameFromClass>>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);

            await service.SearchAsync<TestMeshNameFromClass>(x => true).ConfigureAwait(true);
            var encodedFilter = WebUtility.UrlEncode("{ }");

            Assert.Equal($"meshes/testmeshnamefromclass?filter={encodedFilter}&orderby=&page=1&pageSize=200", passedUrl);

            requestService.VerifyAll();
        }

        [Fact]
        public void ShouldSearchCompiledRequestHaveCorrectMeshNameFromAttributeAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;

            requestService.Setup(x => x.GetRequest<PageResult<MeshNameAttributeClassTest>>(It.IsAny<string>())).Callback((string url) =>
            {
                passedUrl = url;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<PageResult<MeshNameAttributeClassTest>>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);

            service.Search<MeshNameAttributeClassTest>(x => true);
            var encodedFilter = WebUtility.UrlEncode("{ }");

            Assert.Equal($"meshes/{MeshName.ToLowerInvariant()}?filter={encodedFilter}&orderby=&page=1&pageSize=200", passedUrl);

            requestService.VerifyAll();
        }

        [Fact]
        public async Task ShouldSearchCompiledAsyncRequestHaveCorrectMeshNameFromAttributeAndUrl()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;

            requestService.Setup(x => x.GetRequest<PageResult<MeshNameAttributeClassTest>>(It.IsAny<string>())).Callback((string url) =>
            {
                passedUrl = url;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<PageResult<MeshNameAttributeClassTest>>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);

            await service.SearchAsync<MeshNameAttributeClassTest>(x => true).ConfigureAwait(true);
            var encodedFilter = WebUtility.UrlEncode("{ }");

            Assert.Equal($"meshes/{MeshName.ToLowerInvariant()}?filter={encodedFilter}&orderby=&page=1&pageSize=200", passedUrl);

            requestService.VerifyAll();
        }

        #endregion

        #region Search Parameters

        [Fact]
        public void ShouldSearchManualRequestHasCorrectQueryString()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;

            requestService.Setup(x => x.GetRequest<PageResult<MeshNameAttributeClassTest>>(It.IsAny<string>())).Callback((string url) =>
            {
                passedUrl = url;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<PageResult<MeshNameAttributeClassTest>>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);
            var pageNumber = new Random().Next();
            var pageSize = new Random().Next();

            var searchFilter = "{ 'FavoriteNumber': { '$gt': 5000 } }";
            var sort = "{ FavoriteNumber: -1 }";

            service.Search<MeshNameAttributeClassTest>(searchFilter, sort, pageNumber, pageSize);
            var encodedFilter = WebUtility.UrlEncode(searchFilter);
            var encodedSort = WebUtility.UrlEncode("{ FavoriteNumber: -1 }");

            Assert.Equal($"meshes/{MeshName.ToLowerInvariant()}?filter={encodedFilter}&orderby={encodedSort}&page={pageNumber}&pageSize={pageSize}", passedUrl);

            requestService.VerifyAll();
        }

        [Fact]
        public async Task ShouldSearchCompiledRequestHasCorrectQueryString()
        {
            var requestService = new Mock<IRequestService>();

            var passedUrl = string.Empty;

            requestService.Setup(x => x.GetRequest<PageResult<MeshNameAttributeClassTest>>(It.IsAny<string>())).Callback((string url) =>
            {
                passedUrl = url;
            }).Returns(() =>
            {
                return Task.FromResult(It.IsAny<PageResult<MeshNameAttributeClassTest>>());
            }).Verifiable();

            var service = new MeshesService(requestService.Object);
            var pageNumber = new Random().Next();
            var pageSize = new Random().Next();


            await service.SearchAsync<MeshNameAttributeClassTest>(x => x.FavoriteNumber > 5000, new List<KeyValuePair<string, SortDirection>>() { new KeyValuePair<string, SortDirection>("FavoriteNumber", SortDirection.Desending) }, pageNumber, pageSize).ConfigureAwait(true);
            var encodedFilter = WebUtility.UrlEncode("{ \"FavoriteNumber\" : { \"$gt\" : 5000 } }");
            var encodedSort = WebUtility.UrlEncode("{ FavoriteNumber:-1 }");

            Assert.Equal($"meshes/{MeshName.ToLowerInvariant()}?filter={encodedFilter}&orderby={encodedSort}&page={pageNumber}&pageSize={pageSize}", passedUrl);

            requestService.VerifyAll();
        }

        #endregion

        #region TestModels
        internal class TestMeshNameFromClass : MeshData
        {
            public int FavoriteNumber { get; set; }
        }

        private const string MeshName = "ThisIsTheTest";

        [MeshName(MeshName)]
        internal class MeshNameAttributeClassTest : MeshData
        {
            public int FavoriteNumber { get; set; }
        }
        #endregion
    }
}
