using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MeshyDb.SDK.Tests
{
    public class MeshyDbClientTests
    {
        [Fact]
        public void ShouldCreate()
        {
            var client = new MeshyDbClient(Generator.RandomString(5), Generator.RandomString(36), Generator.RandomString(36));
            Assert.NotNull(client);
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithNullTenant()
        {
            Assert.Throws<ArgumentException>(() => new MeshyDbClient(null, Generator.RandomString(36), Generator.RandomString(36)));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithEmptyTenant()
        {
            Assert.Throws<ArgumentException>(() => new MeshyDbClient(string.Empty, Generator.RandomString(36), Generator.RandomString(36)));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithWhitespaceTenant()
        {
            Assert.Throws<ArgumentException>(() => new MeshyDbClient(new string(' ', 5), Generator.RandomString(36), Generator.RandomString(36)));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithNullPublicKey()
        {
            Assert.Throws<ArgumentException>(() => new MeshyDbClient(Generator.RandomString(5), null, Generator.RandomString(36)));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithEmptyPublicKey()
        {
            Assert.Throws<ArgumentException>(() => new MeshyDbClient(Generator.RandomString(5), string.Empty, Generator.RandomString(36)));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithWhitespacePublicKey()
        {
            Assert.Throws<ArgumentException>(() => new MeshyDbClient(Generator.RandomString(5), new string(' ', 5), Generator.RandomString(36)));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithNullPrivateKey()
        {
            Assert.Throws<ArgumentException>(() => new MeshyDbClient(Generator.RandomString(5), Generator.RandomString(36), null));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithEmptyPrivateKey()
        {
            Assert.Throws<ArgumentException>(() => new MeshyDbClient(Generator.RandomString(5), Generator.RandomString(36), string.Empty));
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWithWhitespacePrivateKey()
        {
            Assert.Throws<ArgumentException>(() => new MeshyDbClient(Generator.RandomString(5), Generator.RandomString(36), new string(' ', 5)));
        }

        [Fact]
        public void ShouldHaveMeshesService()
        {
            var client = new MeshyDbClient(Generator.RandomString(5), Generator.RandomString(36), Generator.RandomString(36));

            Assert.NotNull(client.Meshes);
        }

        [Fact]
        public void ShouldIncludeTenantInApiUrl()
        {
            var tenant = Generator.RandomString(5);
            var client = new MeshyDbClient(tenant, Generator.RandomString(36), Generator.RandomString(36));
        }

        [Fact]
        public void ShouldIncludeTenantInAuthUrl()
        {
            var tenant = Generator.RandomString(5);
            var client = new MeshyDbClient(tenant, Generator.RandomString(36), Generator.RandomString(36));

            Assert.Equal($"http://auth.meshydb.com/{tenant}".ToLower(), client.GetAuthUrl().ToLower());
        }
    }
}
