using xmisc.backbone.identity.contracts.infrastructure;
using Xunit;

namespace xmisc.backbone.identity.tests.guids
{
    public class SequentialGuidTests
    {
        [Fact]
        public void TestUniqueness()
        {
            //Act
            var first = SequentialGuid.NewGuid();
            var second = SequentialGuid.NewGuid();

            //Assert
            Assert.NotEqual(first, second);
        }

        [Fact]
        public void TestLexicalOrder()
        {
            //Act
            var first = SequentialGuid.NewGuid();
            var second = SequentialGuid.NewGuid();
            var third = SequentialGuid.NewGuid();

            //Assert
            Assert.Equal(first < second && second < third, true);
        }


        [Fact]
        public void TestVersionNumber()
        {
            //arrange
            var guid = SequentialGuid.NewGuid();
            var bytes = guid.ToByteArray();

            //act
            var version = (ushort)(bytes[7] >> 4);

            //Assert
            Assert.Equal(version, 1);
        }
    }
}
