using reexmonkey.xmisc.backbone.identifiers.contracts.extensions;
using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using Xunit;
using Xunit.Abstractions;

namespace xmisc.backbone.identity.tests.guids
{
    public class SequentialGuidTests
    {
        private readonly ITestOutputHelper console;

        public SequentialGuidTests(ITestOutputHelper console)
        {
            this.console = console;
        }

        [Fact]
        public void TestUniqueness()
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
        public void TestLexicalOrder()
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
        public void TestVersionNumber()
        {
            //arrange
            var comb = SequentialGuid.NewGuid();

            //act
            var version = comb.AsGuid().GetVersion();

            //Assert
            Assert.Equal(1, version);
        }
    }
}