using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using System;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace reexmonkey.xmisc.backbone.identifiers.tests.guids
{
    public class Md5GuidTests(ITestOutputHelper console)
    {

        [Theory]
        [InlineData("www.example.com", "5df41881-3aed-3515-88a7-2f4a814cf09e")]
        public void ShouldReturnExpectedUuid(string name, Guid uuid)
        {
            //act
            var result = Md5Guid.NewGuid(Md5Guid.DnsNamespaceId, name, Encoding.UTF8);

            //assert
            Assert.Equal(uuid, (Guid)result);

            console.WriteLine("result: {0}", result);
        }

        [Theory]
        [InlineData("www.example.com")]
        public void TestUniqueness(string name)
        {
            //act
            var first = Md5Guid.NewGuid(Md5Guid.DnsNamespaceId, name, Encoding.UTF8);
            var second = Md5Guid.NewGuid(Md5Guid.DnsNamespaceId, name, Encoding.UTF8);

            //assert
            Assert.Equal(first, second);

            console.WriteLine("first: {0}", first);
            console.WriteLine("second: {0}", second);
        }

        [Fact]
        public void VersionNumberShouldMatch3()
        {
            //arrange
            var guid = Md5Guid.NewGuid(Md5Guid.DnsNamespaceId, "www.example.com", Encoding.UTF8);
            var bytes = guid.ToByteArray();

            //act
            var version = (ushort)(bytes[7] >> 4);

            //Assert
            Assert.Equal(3, version);
        }
    }
}
