using System;
using Xunit;

namespace MeshyDB.SDK.Tests
{
    public class MeshyDBClientTests
    {
        [Fact]
        public void ShouldCreate()
        {
            var client = new MeshyDBClient(Generator.RandomString(5), Generator.RandomString(36), Generator.RandomString(36));
            Assert.NotNull(client);
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithNullClientKey()
        {
            Assert.Throws<ArgumentException>(() => new MeshyDBClient(null, Generator.RandomString(36), Generator.RandomString(36)));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithEmptyClientKey()
        {
            Assert.Throws<ArgumentException>(() => new MeshyDBClient(string.Empty, Generator.RandomString(36), Generator.RandomString(36)));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithWhitespaceClientKey()
        {
            Assert.Throws<ArgumentException>(() => new MeshyDBClient(new string(' ', 5), Generator.RandomString(36), Generator.RandomString(36)));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithNullPublicKey()
        {
            Assert.Throws<ArgumentException>(() => new MeshyDBClient(Generator.RandomString(5), null, Generator.RandomString(36)));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithEmptyPublicKey()
        {
            Assert.Throws<ArgumentException>(() => new MeshyDBClient(Generator.RandomString(5), string.Empty, Generator.RandomString(36)));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithWhitespacePublicKey()
        {
            Assert.Throws<ArgumentException>(() => new MeshyDBClient(Generator.RandomString(5), new string(' ', 5), Generator.RandomString(36)));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithNullPrivateKey()
        {
            Assert.Throws<ArgumentException>(() => new MeshyDBClient(Generator.RandomString(5), Generator.RandomString(36), null));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithEmptyPrivateKey()
        {
            Assert.Throws<ArgumentException>(() => new MeshyDBClient(Generator.RandomString(5), Generator.RandomString(36), string.Empty));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithWhitespacePrivateKey()
        {
            Assert.Throws<ArgumentException>(() => new MeshyDBClient(Generator.RandomString(5), Generator.RandomString(36), new string(' ', 5)));
        }

        [Fact]
        public void ShouldHaveMeshesService()
        {
            var client = new MeshyDBClient(Generator.RandomString(5), Generator.RandomString(36), Generator.RandomString(36));

            Assert.NotNull(client.Meshes);
        }

        [Fact]
        public void ShouldIncludeClientKeyInApiUrl()
        {
            var clientKey = Generator.RandomString(5);
            var client = new MeshyDBClient(clientKey, Generator.RandomString(36), Generator.RandomString(36));

            Assert.Equal($"https://api.meshydb.com/{clientKey}".ToLower(), client.GetApiUrl().ToLower());
        }

        [Fact]
        public void ShouldIncludeClientKeyInAuthUrl()
        {
            var clientKey = Generator.RandomString(5);
            var client = new MeshyDBClient(clientKey, Generator.RandomString(36), Generator.RandomString(36));

            Assert.Equal($"https://auth.meshydb.com/{clientKey}".ToLower(), client.GetAuthUrl().ToLower());
        }
    }
}
