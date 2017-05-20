using System;
using xmisc.backbone.identity.concretes.extensions;
using Xunit;

namespace xmisc.backbone.identity.tests.extensions
{
    public class GuidExtensionTests
    {
        [Fact]
        public void TestConvertToVersion1Guid()
        {
            //Act
            var first = Guid.NewGuid().AsVersion1Guid();
            var second = Guid.NewGuid().AsVersion1Guid();
            var third = Guid.NewGuid().AsVersion1Guid();

            //Assert
            Assert.Equal(first.CompareTo(second) < 0 && second.CompareTo(third) < 0, true);
        }
    }
}
