using reexmonkey.xmisc.backbone.identifiers.contracts.generators;
using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using System;
using System.Text;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.models
{
    /// <summary>
    /// Specifies a UUID provider that produces SHA1-based (version 5) universal unique identifiers
    /// from a specified namespace and name as defined in RFC 4122.
    /// </summary>
    public abstract class Sha1GuidKeyGeneratorBase : INameKeyGenerator<Sha1Guid>
    {
        private readonly Guid namespaceId;
        private readonly Encoding encoding;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sha1GuidKeyGeneratorBase"/> class.
        /// </summary>
        /// <param name="namespaceId">One of the default namespaces as defined in RFC 4122. </param>
        /// <param name="encoding">The character encoding to encode the name to its equivalent byte representation.</param>
        public Sha1GuidKeyGeneratorBase(Guid namespaceId, Encoding encoding)
        {
            this.namespaceId = namespaceId;
            this.encoding = encoding;
        }

        /// <summary>
        /// Gets the default namespace and name-based SHA1 (version 5) universal unique identifier (UUID).
        /// </summary>
        /// <returns>The default unique identifier.</returns>
        public Sha1Guid GetDefaultKey() => Sha1Guid.Empty;

        /// <summary>
        /// Generates a SHA1 (version 5) universal unique identifier (UUID) for the specified name value.
        /// </summary>
        /// <param name="name">The name-based value, from which the SHA1 (version 5) universal unique identifier is generated.</param>
        /// <returns>The generated universal unique identifier.</returns>
        public Sha1Guid GetKey(string name) => Sha1Guid.NewGuid(namespaceId, name, encoding);
    }

    /// <summary>
    /// Represents a UUID provider that produces SHA1-based (version 5) universal unique identifiers for DNS as defined in RFC 4122.
    /// </summary>
    public sealed class DnsSha1GuidKeyGenerator : Sha1GuidKeyGeneratorBase
    {
        /// <summary>
        /// Initializes an instance of the <see cref="DnsSha1GuidKeyGenerator"/> class.
        /// </summary>
        /// <param name="encoding">The type of character encoding to encode the DNS value.</param>
        public DnsSha1GuidKeyGenerator(Encoding encoding) : base(Sha1Guid.DnsNamespaceId, encoding)
        {
        }
    }

    /// <summary>
    /// Represents a UUID provider that produces SHA1-based (version 5) universal unique identifiers for URLs as defined in RFC 4122.
    /// </summary>
    public sealed class UrlSha1GuidKeyGenerator : Sha1GuidKeyGeneratorBase
    {
        /// <summary>
        /// Initializes an instance of the <see cref="UrlSha1GuidKeyGenerator"/> class.
        /// </summary>
        /// <param name="encoding">The type of character encoding to encode the URL value</param>
        ///
        public UrlSha1GuidKeyGenerator(Encoding encoding) : base(Sha1Guid.UrlNamespaceId, encoding)
        {
        }
    }

    /// <summary>
    /// Represents a UUID provider that produces SHA1-based (version 5) universal unique identifiers  for ISO OIDs as defined in RFC 4122.
    /// </summary>
    public sealed class IsoOidSha1GuidKeyGenerator : Sha1GuidKeyGeneratorBase
    {
        /// <summary>
        /// Initializes an instance of the <see cref="UrlSha1GuidKeyGenerator"/> class.
        /// </summary>
        /// <param name="encoding">The type of character encoding to encode the ISO OID value.</param>
        ///
        public IsoOidSha1GuidKeyGenerator(Encoding encoding) : base(Sha1Guid.IsoOidNamespaceId, encoding)
        {
        }
    }
}