using reexmonkey.xmisc.backbone.identifiers.concretes.models;
using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using reexmonkey.xmisc.backbone.identifiers.tests.fixtures;
using reexmonkey.xmisc.backbone.io.formatter.serializers;
using reexmonkey.xmisc.backbone.io.messagepack.serializers;
using reexmonkey.xmisc.backbone.io.protobuf.serializers;
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
            var generator = new Md5FingerprintGenerator(Fixture.NamespaceId, Fixture.Encoding, new MessagePackSerializer());

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
            var generator = new Md5FingerprintGenerator(Fixture.NamespaceId, Fixture.Encoding, new ProtoBufSerializer());

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
            var generator = new Sha1FingerprintGenerator(Fixture.NamespaceId, Fixture.Encoding, new BinaryFormatSerializer());

            //act
            var fingerprint = generator.GetFingerprint(123456);
            var other = generator.GetFingerprint(123456);

            //assert
            Assert.Equal(fingerprint, other);
        }
    }
}
