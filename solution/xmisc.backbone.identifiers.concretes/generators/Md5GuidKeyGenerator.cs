using reexmonkey.xmisc.backbone.identifiers.contracts.generators;
using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using System;
using System.Text;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.models
{
    /// <summary>
    /// Represents a UUID provider that produces MD5-based (version 3) universal unique identifiers as defined in RFC 9562.
    /// </summary>
    public class Md5GuidKeyGenerator : INameKeyGenerator<Md5Guid>
    {
        private readonly Guid namespaceId;

        /// <summary>
        /// Initializes a new instance of the <see cref="Md5GuidKeyGenerator"/> class.
        /// </summary>
        /// <param name="namespaceId">One of the default namespaces as defined in RFC 9562. </param>
        protected Md5GuidKeyGenerator(Guid namespaceId)
        {
            this.namespaceId = namespaceId;
        }

        /// <summary>
        /// Gets the default MD5-based (version 3) universal unique identifier (UUID).
        /// </summary>
        /// <returns>The default unique identifier.</returns>
        public Md5Guid GetDefaultKey() => Md5Guid.Empty;

        /// <summary>
        /// Generates an MD5 (version 3) universal unique identifier (UUID) from the specifed name-based value.
        /// </summary>
        /// <param name="name">The name-based value, from which the version 3 universal unique identifier (UUID) is generated.</param>
        /// <returns>The generated universal unique identifier.</returns>
        public Md5Guid GetKey(string name) => Md5Guid.NewGuid(namespaceId, name, Encoding.UTF8);
    }

    /// <summary>
    /// Represents a UUID provider that produces MD5-based (version 3) universal unique identifiers for DNS as defined in RFC 9562.
    /// </summary>
    public sealed class DnsMd5GuidKeyGenerator : Md5GuidKeyGenerator
    {
        /// <summary>
        /// Initializes an instance of the <see cref="DnsMd5GuidKeyGenerator"/> class.
        /// </summary>
        public DnsMd5GuidKeyGenerator() : base(Md5Guid.DnsNamespaceId)
        {
        }
    }

    /// <summary>
    /// Represents a UUID provider that produces MD5-based (version 3) universal unique identifiers for URLs as defined in RFC 9562.
    /// </summary>
    public sealed class UrlMd5GuidKeyGenerator : Md5GuidKeyGenerator
    {
        /// <summary>
        /// Initializes an instance of the <see cref="UrlMd5GuidKeyGenerator"/> class.
        /// </summary>
        public UrlMd5GuidKeyGenerator() : base(Md5Guid.UrlNamespaceId)
        {
        }
    }

    /// <summary>
    /// Represents a UUID provider that produces MD5-based (version 3) universal unique identifiers for ISO OIDs as defined in RFC 9562.
    /// </summary>
    public sealed class OidMd5GuidKeyGenerator : Md5GuidKeyGenerator
    {
        /// <summary>
        /// Initializes an instance of the <see cref="UrlMd5GuidKeyGenerator"/> class.
        /// </summary>
        public OidMd5GuidKeyGenerator() : base(Md5Guid.OidNamespaceId)
        {
        }
    }
}