using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using System;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace reexmonkey.xmisc.backbone.identifiers.tests.guids
{
    public class Sha1GuidTests(ITestOutputHelper console)
    {
        [Theory]
        [InlineData("www.example.com", "2ed6657d-e927-568b-95e1-2665a8aea6a2")]
        public void ShouldReturnExpectedUuid(string name, Guid uuid)
        {
            //act
            var result = Sha1Guid.NewGuid(Sha1Guid.DnsNamespaceId, name, Encoding.UTF8);

            //assert
            Assert.Equal(uuid, (Guid)result);

            console.WriteLine("result: {0}", result);
        }

        [Theory]
        [InlineData("www.example.com")]
        public void ShouldBeUnique(string name)
        {
            //act
            var first = Sha1Guid.NewGuid(Sha1Guid.DnsNamespaceId, name, Encoding.UTF8);
            var second = Sha1Guid.NewGuid(Sha1Guid.DnsNamespaceId, name, Encoding.UTF8);

            //assert
            Assert.Equal(first, second);

            console.WriteLine("first: {0}", first);
            console.WriteLine("second: {0}", second);
        }

        [Fact]
        public void VersionNumberShouldMatch5()
        {
            //arrange
            var guid = Sha1Guid.NewGuid(Sha1Guid.DnsNamespaceId, "www.example.com", Encoding.UTF8);
            var bytes = guid.ToByteArray();
            
            //act
            var version = (ushort)(bytes[7] >> 4);

            //Assert
            Assert.Equal(5, version);
        }
    }
}
