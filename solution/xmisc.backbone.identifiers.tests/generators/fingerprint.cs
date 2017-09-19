using System.Security.Cryptography;
using reexmonkey.xmisc.backbone.identifiers.concretes.generators;
using reexmonkey.xmisc.backbone.identifiers.contracts.infrastructure;
using reexmonkey.xmisc.backbone.io.formatter.infrastructure;
using reexmonkey.xmisc.backbone.io.messagepack.infrastructure;
using reexmonkey.xmisc.backbone.io.protobuf.infrastructure;
using Xunit;

namespace reexmonkey.xmisc.backbone.identifiers.tests.generators
{
    public class FingerprintTests
    {

        [Theory]
        [InlineData("The quick brown fox jumped over the lazy dogs")]
        public void TestSimpleFingerprintGeneration(string model)
        {
            //arrange
            var generator = new Md5FingerprintGenerator(new MessagePackSerializer(), new MD5CryptoServiceProvider());

            //act
            var fingerprint = generator.GetFingerprint(model);

            //assert
            Assert.NotEqual(fingerprint, Md5Guid.Empty);

        }

        [Theory]
        [InlineData("The quick brown fox jumped over the lazy dogs")]
        [InlineData("Fingerprint generation is easy but not 100 % accurate")]
        public void TestMd5FingerprintUniqueness(string model)
        {
            //arrange
            var generator = new Md5FingerprintGenerator(new ProtoBufSerializer(), new MD5CryptoServiceProvider());

            //act
            var fingerprint = generator.GetFingerprint(model);
            var other = generator.GetFingerprint(model);


            //assert
            Assert.Equal(fingerprint, other);

        }

        [Fact]
        public void TestSha1FingerprintUniqueness()
        {
            //arrange
            var generator = new Sha1FingerprintGenerator(new BinaryFormatSerializer(), new SHA1CryptoServiceProvider());

            //act
            var fingerprint = generator.GetFingerprint(123456);
            var other = generator.GetFingerprint(123456);


            //assert
            Assert.Equal(fingerprint, other);

        }
    }
}
