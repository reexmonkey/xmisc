using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using reexmonkey.xmisc.core.io.serializers;
using System;
using System.Text;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.models
{
    /// <summary>
    /// Provides a generator of MD5-based fingerprints for specified objects.
    /// </summary>
    public class Md5FingerprintGenerator : IFingerprintGenerator<Md5Guid>
    {
        private readonly byte[] namespaceId;
        private readonly BinarySerializerBase serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Md5FingerprintGenerator"/>.
        /// </summary>
        /// <param name="namespaceId">Any of the default namespace IDs for namespace-based Uuids (version 5) as defined in RFC 4122.</param>
        /// <param name="encoding">The character encoding to encode <paramref name="namespaceId"/> into bytes.</param>
        /// <param name="serializer">The binary serializer that serializes objects, whose fingerprints shall be generated.</param>
        public Md5FingerprintGenerator(Guid namespaceId, Encoding encoding, BinarySerializerBase serializer)
        {
            if (encoding == null) throw new ArgumentNullException(nameof(encoding));
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            this.namespaceId = namespaceId.ToByteArray();
        }

        /// <summary>
        /// Gets the default fingerprint for all types of objects.
        /// </summary>
        /// <returns>The default fingerprint for all types of objects.</returns>
        public Md5Guid GetNullFingerprint() => Md5Guid.Empty;

        /// <summary>
        /// Produces a fingerprint that uniquely or pesudo-uniquely identifies the specified object.
        /// </summary>
        /// <typeparam name="TModel">The type of obhect, whose fingerprint is produced.</typeparam>
        /// <param name="model">The object, whose fingerprint shall be produced.</param>
        /// <returns>The fingerprint that uniquely identifies or pseudo-identifies the specified <paramref name="model"/>.</returns>
        public Md5Guid GetFingerprint<TModel>(TModel model) => Md5Guid.NewGuid(namespaceId, serializer.Serialize(model));
    }
}