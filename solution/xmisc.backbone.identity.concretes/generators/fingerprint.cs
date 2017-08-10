using reexmonkey.xmisc.core.io.infrastructure;
using reexmonkey.xmisc.core.security;
using System;
using System.Security.Cryptography;
using xmisc.backbone.identity.contracts.generators;
using xmisc.backbone.identity.contracts.infrastructure;

namespace xmisc.backbone.identity.concretes.generators
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