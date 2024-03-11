using reexmonkey.xmisc.backbone.identifiers.contracts.generators;
using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using System;
using System.Text;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.models
{
    /// <summary>
    /// Specfiies a UUID provider that produces MD5-based (version 3) universal unique identifiers as defined in RFC 4122.
    /// </summary>
    public abstract class Md5GuidKeyGeneratorBase : INameKeyGenerator<Md5Guid>
    {
        private readonly Guid namespaceId;
        private readonly Encoding encoding;

        /// <summary>
        /// Initializes a new instance of the <see cref="Md5GuidKeyGeneratorBase"/> class.
        /// </summary>
        /// <param name="namespaceId">One of the default namespaces as defined in RFC 4122. </param>
        /// <param name="encoding">The character encoding to encode the values to its byte representation.</param>
        protected Md5GuidKeyGeneratorBase(Guid namespaceId, Encoding encoding)
        {
            this.namespaceId = namespaceId;
            this.encoding = encoding;
        }

        /// <summary>
        /// Gets the default MD5-based (version 3) universal unique identifier (UUID).
        /// </summary>
        /// <returns>The default unique identifier.</returns>
        public Md5Guid GetDefaultKey() => Md5Guid.Empty;

        /// <summary>
        /// Generates an MD5 (version 3) universal unique identifier (UUID) from the specifed name-based value.
        /// </summary>
        /// <param name="name">The name-based value, from which the version 3 universal unique identifier (UUID)is generated.</param>
        /// <returns>The generated universal unique identifier.</returns>
        public Md5Guid GetKey(string name) => Md5Guid.NewGuid(namespaceId, name, encoding);
    }

    /// <summary>
    /// Represents a UUID provider that produces MD5-based (version 3) universal unique identifiers for DNS as defined in RFC 4122.
    /// </summary>
    public sealed class DnsMd5GuidKeyGenerator : Md5GuidKeyGeneratorBase
    {
        /// <summary>
        /// Initializes an instance of the <see cref="DnsMd5GuidKeyGenerator"/> class.
        /// </summary>
        /// <param name="encoding">The type of character encoding to encode the DNS.</param>
        public DnsMd5GuidKeyGenerator(Encoding encoding) : base(Md5Guid.DnsNamespaceId, encoding)
        {
        }
    }

    /// <summary>
    /// Represents a UUID provider that produces MD5-based (version 3) universal unique identifiers for URLs as defined in RFC 4122.
    /// </summary>
    public sealed class UrlMd5GuidKeyGenerator : Md5GuidKeyGeneratorBase
    {
        /// <summary>
        /// Initializes an instance of the <see cref="UrlMd5GuidKeyGenerator"/> class.
        /// </summary>
        /// <param name="encoding">The type of character encoding to encode the URL value.</param>
        public UrlMd5GuidKeyGenerator(Encoding encoding) : base(Md5Guid.UrlNamespaceId, encoding)
        {
        }
    }

    /// <summary>
    /// Represents a UUID provider that produces MD5-based (version 3) universal unique identifiers for ISO OIDs as defined in RFC 4122.
    /// </summary>
    public sealed class IsoOidMd5GuidKeyGenerator : Md5GuidKeyGeneratorBase
    {
        /// <summary>
        /// Initializes an instance of the <see cref="UrlMd5GuidKeyGenerator"/> class.
        /// </summary>
        /// <param name="encoding">The type of character encoding to encode the ISO OID value.</param>
        public IsoOidMd5GuidKeyGenerator(Encoding encoding) : base(Md5Guid.IsoOidNamespaceId, encoding)
        {
        }
    }
}