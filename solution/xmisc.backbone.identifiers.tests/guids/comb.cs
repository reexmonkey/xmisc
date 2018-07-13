﻿using reexmonkey.xmisc.backbone.identifiers.contracts.infrastructure;
using reexmonkey.xmisc.backbone.identifiers.contracts.models;
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
            Assert.True(first < second && second < third);
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
            Assert.Equal(1, version);
        }
    }
}
