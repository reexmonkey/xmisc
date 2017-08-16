using System.Security.Cryptography;
using reexmonkey.xmisc.backbone.identifiers.concretes.generators;
using reexmonkey.xmisc.backbone.identifiers.contracts.infrastructure;
using reexmonkey.xmisc.backbone.io.messagepack.infrastructure;
using Xunit;

namespace xmisc.backbone.identity.tests.generators
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

            //assrt
            Assert.NotEqual(fingerprint, Md5Guid.Empty);

        }

        [Theory]
        [InlineData("The quick brown fox jumped over the lazy dogs")]
        [InlineData("Fingerpint generaration is easy but not 100 % accurate")]
        public void TestSimpleFingerprintUniqueness(string model)
        {
            //arrange
            var generator = new Md5FingerprintGenerator(new MessagePackSerializer(), new MD5CryptoServiceProvider());

            //act
            var fingerprint = generator.GetFingerprint(model);
            var other = generator.GetFingerprint(model);


            //assrt
            Assert.Equal(fingerprint, other);

        }
    }
}
