using reexmonkey.xmisc.backbone.identifiers.contracts.generators;
using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using System;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.generators
{
    /// <summary>
    /// Provides a generator of MD5-based fingerprints for specified objects.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="Md5FingerprintGenerator"/>.
    /// </remarks>
    /// <param name="namespaceId">Any of the default namespace IDs for namespace-based Uuids (version 5) as defined in RFC 9562.</param>
    public abstract class Md5FingerprintGenerator(Guid namespaceId) : IFingerprintGenerator<Md5Guid>
    {
        private readonly byte[] namespaceId = namespaceId.ToByteArray();

        /// <summary>
        /// Gets the default fingerprint for all types of objects.
        /// </summary>
        /// <returns>The default fingerprint for all types of objects.</returns>
        public Md5Guid GetDefaultFingerprint() => Md5Guid.Empty;

        /// <summary>
        /// Produces a fingerprint that uniquely or pseudo-uniquely identifies the specified object.
        /// </summary>
        /// <typeparam name="TModel">The type of the object whose fingerprint shall be generated.</typeparam>
        /// <param name="model">The object whose fingerprint shall be generated.</param>
        /// <param name="serializeFunc">The binary serializer that serializes objects, whose fingerprints shall be generated.</param>
        /// <returns>The fingerprint for the specified object.</returns>
        public Md5Guid GetFingerprint<TModel>(TModel model, Func<TModel, byte[]> serializeFunc)
        {
            var data = serializeFunc(model);
            return Md5Guid.NewGuid(namespaceId, data);
        }
    }

    /// <summary>
    /// Provides an MD5-based fingerprint generator that uses the DNS namespace as defined by RFC 9562.
    /// </summary>
    /// <remarks>Use this class to generate MD5 fingerprints or UUIDs that are namespaced to DNS, ensuring
    /// compatibility with systems that require DNS-based UUID generation. Inherits from <see
    /// cref="Md5FingerprintGenerator"/> and automatically applies the DNS namespace identifier.</remarks>
    public class DnsMd5FingerprintGenerator : Md5FingerprintGenerator
    {
        /// <summary>
        /// Initializes an instance of the <see cref="DnsMd5FingerprintGenerator"/> class.
        /// </summary>
        public DnsMd5FingerprintGenerator()
            : base(Md5Guid.DnsNamespaceId)
        {
        }
    }

    /// <summary>
    /// Provides an MD5-based fingerprint generator that uses the URL namespace for generating unique identifiers.
    /// </summary>
    public class UrlMd5FingerprintGenerator : Md5FingerprintGenerator
    {
        /// <summary>
        /// Initializes an instance of the <see cref="UrlMd5FingerprintGenerator"/> class.
        /// </summary>
        public UrlMd5FingerprintGenerator()
            : base(Md5Guid.UrlNamespaceId)
        {
        }
    }

    /// <summary>
    /// Provides an MD5-based fingerprint generator that uses the OID namespace for generating unique identifiers.
    /// </summary>
    public class OidMd5FingerprintGenerator : Md5FingerprintGenerator
    {
        /// <summary>
        /// Initializes an instance of the <see cref="OidMd5FingerprintGenerator"/> class.
        /// </summary>
        public OidMd5FingerprintGenerator()
            : base(Md5Guid.OidNamespaceId)
        {
        }
    }

}