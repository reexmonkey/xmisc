using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using reexmonkey.xmisc.backbone.identifiers.tests.fixtures;
using Xunit;
using Xunit.Abstractions;

namespace xmisc.backbone.identity.tests.guids
{
    public class Sha1GuidTests
    {
        private readonly ITestOutputHelper console;

        public Sha1GuidTests(ITestOutputHelper console)
        {
            this.console = console ?? throw new System.ArgumentNullException(nameof(console));
        }

        [Theory]
        [InlineData("test", "test")]
        [InlineData("unique", "unique")]
        [InlineData("guid", "guid")]
        [InlineData("md5", "md5")]
        public void TestUniqueness(string x, string y)
        {
            //act
            var first = Sha1Guid.NewGuid(Sha1Guid.DnsNamespaceId, x, Fixture.Encoding);
            var second = Sha1Guid.NewGuid(Sha1Guid.DnsNamespaceId, y, Fixture.Encoding);

            //assert
            Assert.Equal(first, second);
        }

        [Theory]
        [InlineData("test")]
        [InlineData("unique")]
        [InlineData("guid")]
        [InlineData("md5")]
        public void TestVersionNumber(string name)
        {
            //arrange
            var guid = Sha1Guid.NewGuid(Sha1Guid.DnsNamespaceId.ToByteArray(), Fixture.Encoding.GetBytes(name));
            var bytes = guid.ToByteArray();
            //act
            var version = (ushort)(bytes[7] >> 4);

            //Assert
            Assert.Equal(5, version);
        }
    }
}
