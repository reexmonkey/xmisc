using reexmonkey.xmisc.backbone.identifiers.concretes.generators;
using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using reexmonkey.xmisc.backbone.io.messagepack.serializers;
using reexmonkey.xmisc.backbone.io.protobuf.serializers;
using Xunit.Abstractions;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.tests.generators
{
    public class FingerprintTests(ITestOutputHelper console)
    {
        [Theory]
        [InlineData("The quick brown fox jumped over the lazy dogs")]
        public void ShouldGenerateMd5Fingerprint(string model)
        {
            //arrange
            var serializer = new MessagePackSerializer();
            var generator = new DnsMd5FingerprintGenerator();

            //act
            var fingerprint = generator.GetFingerprint(model, serializer.Serialize);

            //assert
            Assert.NotEqual(fingerprint, Md5Guid.Empty);

            console.WriteLine($"Fingerprint: {fingerprint}");
        }

        [Theory]
        [InlineData(123456)]
        public void TestMd5FingerprintUniqueness(int model)
        {
            //arrange
            var serializer = new ProtoBufSerializer();
            var generator = new DnsMd5FingerprintGenerator();

            //act
            var fingerprint = generator.GetFingerprint(model, serializer.Serialize);
            var other = generator.GetFingerprint(model, serializer.Serialize);

            //assert
            Assert.Equal(fingerprint, other);

            console.WriteLine($"Fingerprint: {fingerprint}");
        }

        [Theory]
        [InlineData("The quick brown fox jumped over the lazy dogs")]
        public void ShouldGenerateSha1MFingerprint(string model)
        {
            //arrange
            var serializer = new ProtoBufSerializer();
            var generator = new DnsSha1FingerprintGenerator();

            //act
            var fingerprint = generator.GetFingerprint(model, serializer.Serialize);

            //assert
            Assert.NotEqual(fingerprint, Sha1Guid.Empty);

            console.WriteLine($"Fingerprint: {fingerprint}");
        }


        [Theory]
        [InlineData(123456)]
        public void TestSha1FingerprintUniqueness(int model)
        {
            //arrange
            var serializer = new MessagePackSerializer();
            var generator = new DnsSha1FingerprintGenerator();

            //act
            var fingerprint = generator.GetFingerprint(model, serializer.Serialize);
            var other = generator.GetFingerprint(model, serializer.Serialize);

            //assert
            Assert.Equal(fingerprint, other);

            console.WriteLine($"Fingerprint: {fingerprint}");
        }
    }
}
