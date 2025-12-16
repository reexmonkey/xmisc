using reexmonkey.xmisc.backbone.identifiers.contracts.generators;
using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using System;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.generators
{
    /// <summary>
    /// Provides a generator of SHA1-based fingerprints for specified objects.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="Sha1FingerprintGenerator"/>.
    /// </remarks>
    /// <param name="namespaceId">Any of the default namespace IDs for namespace-based Uuids (version 5) as defined in RFC 9562.</param>
    public abstract class Sha1FingerprintGenerator(Guid namespaceId) : IFingerprintGenerator<Sha1Guid>
    {
        private readonly byte[] namespaceId = namespaceId.ToByteArray();

        /// <summary>
        /// Gets the default fingerprint for all types of objects.
        /// </summary>
        /// <returns>The default fingerprint for all types of objects.</returns>
        public Sha1Guid GetDefaultFingerprint() => Sha1Guid.Empty;

        /// <summary>
        /// Produces a fingerprint that uniquely or pseudo-uniquely identifies the specified object.
        /// </summary>
        /// <typeparam name="TModel">The type of the object whose fingerprint shall be generated.</typeparam>
        /// <param name="model">The object whose fingerprint shall be generated.</param>
        /// <param name="serializeFunc">The binary serializer that serializes objects, whose fingerprints shall be generated.</param>
        /// <returns>The fingerprint for the specified object.</returns>
        public Sha1Guid GetFingerprint<TModel>(TModel model, Func<TModel, byte[]> serializeFunc)
        {
            var data = serializeFunc(model);
            return Sha1Guid.NewGuid(namespaceId, data);
        }
    }

    /// <summary>
    /// Provides a generator of SHA1-based fingerprints for specified objects in the DNS namespace.
    /// </summary>
    public class DnsSha1FingerprintGenerator : Sha1FingerprintGenerator
    {
        /// <summary>
        /// Initializes an instance of the <see cref="DnsSha1FingerprintGenerator"/> class.
        /// </summary>
        public DnsSha1FingerprintGenerator()
            : base(Sha1Guid.DnsNamespaceId)
        {
        }
    }

    /// <summary>
    /// Provides a generator of SHA1-based fingerprints for specified objects in the URL namespace.
    /// </summary>
    public class UrlSha1FingerprintGenerator : Sha1FingerprintGenerator
    {
        /// <summary>
        /// Initializes an instance of the <see cref="UrlSha1FingerprintGenerator"/> class.
        /// </summary>
        public UrlSha1FingerprintGenerator()
            : base(Sha1Guid.UrlNamespaceId)
        {
        }
    }

    /// <summary>
    /// Provides a generator of SHA1-based fingerprints for specified objects in the ISO OID namespace.
    /// </summary>
    public class OidSha1FingerprintGenerator : Sha1FingerprintGenerator
    {
        /// <summary>
        /// Initializes a new instance of the OidSha1FingerprintGenerator class using the OID namespace identifier for
        /// SHA-1 fingerprint generation.
        /// </summary>
        public OidSha1FingerprintGenerator()
            : base(Sha1Guid.OidNamespaceId)
        {
        }
    }
}