using reexmonkey.xmisc.backbone.identifiers.concretes.extensions;
using reexmonkey.xmisc.backbone.identifiers.contracts.extensions;
using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using reexmonkey.xmisc.backbone.identifiers.tests.fixtures;
using Xunit;
using Xunit.Abstractions;

namespace xmisc.backbone.identity.tests.guids
{
    public class ShuffledSequentialGuidTests
    {
        private readonly ITestOutputHelper console;

        public ShuffledSequentialGuidTests(ITestOutputHelper console)
        {
            this.console = console;
        }

        [Fact]
        public void TestUniqueness()
        {
            //Act
            var first = SequentialGuid.NewGuid().Shuffle();
            var second = SequentialGuid.NewGuid().Shuffle();

            //Assert
            Assert.NotEqual(first, second);
        }

        [Fact]
        public void TestLexicalOrder()
        {
            //Act
            var first = SequentialGuid.NewGuid().Reverse();
            var second = SequentialGuid.NewGuid().Reverse();
            var third = SequentialGuid.NewGuid().Reverse();

            //Assert
            Assert.True(Fixture.SequentialGuidComparer.Compare(first, second) < 0 && Fixture.SequentialGuidComparer.Compare(second, third) < 0);

            console.WriteLine("first: {0}", first);
            console.WriteLine("second: {0}", second);
            console.WriteLine("third: {0}", third);
        }

        [Fact]
        public void TestVersionNumber()
        {
            //arrange
            var sqlguid = SequentialGuid.NewGuid().Shuffle();

            //act
            var version = sqlguid.AsGuid().GetVersion();

            //Assert
            Assert.NotEqual(1, version);
        }
    }
}
