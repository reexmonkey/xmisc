using reexmonkey.xmisc.backbone.identifiers.contracts.generators;
using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using System;
using System.Text;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.models
{
    /// <summary>
    /// Represents a UUID provider that produces SHA1-based (version 5) universal unique identifiers
    /// from a specified namespace and name as defined in RFC 9562.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="Sha1GuidKeyGenerator"/> class.
    /// </remarks>
    /// <param name="namespaceId">One of the default namespaces as defined in RFC 9562. </param>
    public abstract class Sha1GuidKeyGenerator(Guid namespaceId) : INameKeyGenerator<Sha1Guid>
    {

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
        public Sha1Guid GetKey(string name) => Sha1Guid.NewGuid(namespaceId, name, Encoding.UTF8);
    }

    /// <summary>
    /// Represents a UUID provider that produces SHA1-based (version 5) universal unique identifiers for DNS as defined in RFC 9562.
    /// </summary>
    public sealed class DnsSha1GuidKeyGenerator : Sha1GuidKeyGenerator
    {
        /// <summary>
        /// Initializes an instance of the <see cref="DnsSha1GuidKeyGenerator"/> class.
        /// </summary>
        public DnsSha1GuidKeyGenerator() : base(Sha1Guid.DnsNamespaceId)
        {
        }
    }

    /// <summary>
    /// Represents a UUID provider that produces SHA1-based (version 5) universal unique identifiers for URLs as defined in RFC 9562.
    /// </summary>
    public sealed class UrlSha1GuidKeyGenerator : Sha1GuidKeyGenerator
    {
        /// <summary>
        /// Initializes an instance of the <see cref="UrlSha1GuidKeyGenerator"/> class.
        /// </summary>
        public UrlSha1GuidKeyGenerator() : base(Sha1Guid.UrlNamespaceId)
        {
        }
    }

    /// <summary>
    /// Represents a UUID provider that produces SHA1-based (version 5) universal unique identifiers  for ISO OIDs as defined in RFC 9562.
    /// </summary>
    public sealed class OidSha1GuidKeyGenerator : Sha1GuidKeyGenerator
    {
        /// <summary>
        /// Initializes an instance of the <see cref="UrlSha1GuidKeyGenerator"/> class.
        /// </summary>
        public OidSha1GuidKeyGenerator() : base(Sha1Guid.OidNamespaceId)
        {
        }
    }
}