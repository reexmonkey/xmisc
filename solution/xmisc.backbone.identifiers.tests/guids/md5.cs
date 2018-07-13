using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using reexmonkey.xmisc.backbone.identifiers.tests.fixtures;
using System.Text;
using Xunit;

namespace xmisc.backbone.identity.tests.guids
{
    public class Md5GuidTests
    {
        [Theory]
        [InlineData("test", "test")]
        [InlineData("unique", "unique")]
        [InlineData("guid", "guid")]
        [InlineData("md5", "md5")]
        public void TestUniqueness(string x, string y)
        {
            //act
            var first = Md5Guid.NewGuid(Fixture.Encoding.GetBytes(Fixture.DNsNamespace), Fixture.Encoding.GetBytes(x));
            var second = Md5Guid.NewGuid(Fixture.Encoding.GetBytes(Fixture.DNsNamespace), Encoding.Unicode.GetBytes(y));

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
            var guid = Md5Guid.NewGuid(Fixture.DNsNamespace, name, Fixture.Encoding);
            var bytes = guid.ToByteArray();
            //act
            var version = (ushort)(bytes[7] >> 4);

            //Assert
            Assert.Equal(3, version);
        }
    }
}
