using System;
using System.Security.Cryptography;
using reexmonkey.xmisc.backbone.identifiers.contracts.generators;
using reexmonkey.xmisc.backbone.identifiers.contracts.infrastructure;
using reexmonkey.xmisc.core.cryptography.extensions;
using reexmonkey.xmisc.core.io.infrastructure;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.generators
{
    public class Md5FingerprintGenerator : IFingerprintGenerator<Md5Guid>
    {
        private readonly BinarySerializerBase serializer;
        private readonly HashAlgorithm cipher;

        public Md5FingerprintGenerator(BinarySerializerBase serializer, HashAlgorithm cipher)
        {
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            this.cipher = cipher ?? throw new ArgumentNullException(nameof(cipher));
        }

        public Md5Guid GetNullFingerprint() => Md5Guid.Empty;


        public Md5Guid GetFingerprint<TModel>(TModel model) => Md5Guid.NewGuid(model.GetBinaryHash(serializer, cipher));
    }

    public class Sha1FingerprintGenerator : IFingerprintGenerator<Sha1Guid>
    {
        private readonly BinarySerializerBase serializer;
        private readonly HashAlgorithm cipher;

        public Sha1FingerprintGenerator(BinarySerializerBase serializer, HashAlgorithm cipher)
        {
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            this.cipher = cipher ?? throw new ArgumentNullException(nameof(cipher));
        }

        public Sha1Guid GetNullFingerprint() => Sha1Guid.Empty;

        public Sha1Guid GetFingerprint<TModel>(TModel model) => Sha1Guid.NewGuid(model.GetBinaryHash(serializer, cipher));
    }
}