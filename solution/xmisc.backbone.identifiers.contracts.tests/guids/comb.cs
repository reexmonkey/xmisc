using reexmonkey.xmisc.backbone.identifiers.contracts.extensions;
using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using Xunit;
using Xunit.Abstractions;

namespace reexmonkey.xmisc.backbone.identifiers.tests.guids
{
    public class SequentialGuidTests(ITestOutputHelper console)
    {
        private readonly ITestOutputHelper console = console;

        [Fact]
        public void ShouldBeUnique()
        {
            //Act
            var first = SequentialGuid.NewGuid();
            var second = SequentialGuid.NewGuid();

            //Assert
            Assert.NotEqual(first, second);

            console.WriteLine("first: {0}", first);
            console.WriteLine("second: {0}", second);
        }

        [Fact]
        public void ShouldBeOrdered()
        {
            //Act
            var first = SequentialGuid.NewGuid();
            var second = SequentialGuid.NewGuid();
            var third = SequentialGuid.NewGuid();

            //Assert
            Assert.True(first < second && second < third);

            console.WriteLine("first: {0}", first);
            console.WriteLine("second: {0}", second);
            console.WriteLine("third: {0}", third);
        }

        [Fact]
        public void VersionNumberShouldMatch1()
        {
            //arrange
            var comb = SequentialGuid.NewGuid();

            //act
            var version = comb.AsGuid().GetVersion();

            //Assert
            Assert.Equal(1, version);
        }

        [Fact]
        public void BigEndianSequentialGuidShouldBeUnique()
        {
            //Act
            var first = SequentialGuid.NewGuid().ToNetworkOrder();
            var second = SequentialGuid.NewGuid().ToNetworkOrder();

            //Assert
            Assert.NotEqual(first, second);
        }

        [Fact]
        public void BigEndianSequentialShouldBeOrdered()
        {
            //Act
            var first = SequentialGuid.NewGuid().ToNetworkOrder();
            var second = SequentialGuid.NewGuid().ToNetworkOrder();
            var third = SequentialGuid.NewGuid().ToNetworkOrder();

            //Assert
            Assert.True(first > second || second > third);

            console.WriteLine("first: {0}", first);
            console.WriteLine("second: {0}", second);
            console.WriteLine("third: {0}", third);
        }
    }
}