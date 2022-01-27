using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using System;
using System.Text;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.models
{
    /// <summary>
    /// Represents an abstract provider that produces SHA1-based global unique identifiers (version 5) as defined in RFC 4122.
    /// </summary>
    public abstract class Md5GuidKeyGeneratorBase : IKeyGenerator<Md5Guid>
    {
        private readonly Guid namespaceId;
        private readonly string name;
        private readonly Encoding encoding;

        /// <summary>
        /// Initializes a new instance of the <see cref="Md5GuidKeyGeneratorBase"/> class.
        /// </summary>
        /// <param name="namespaceId">One of the default namespaces as defined in RFC 4122. </param>
        /// <param name="name">The unique identifying string, which together with the default namespaces form a unique namespace for the UUID generation.</param>
        /// <param name="encoding">The character encoding to encode <paramref name="name"/> to its byte representation.</param>
        public Md5GuidKeyGeneratorBase(Guid namespaceId, string name, Encoding encoding)
        {
            this.namespaceId = namespaceId;

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));

            this.name = name;
            this.encoding = encoding;
        }

        /// <summary>
        /// Gets the default namespace-based globally unique identifier.
        /// </summary>
        /// <returns>The default unique identifier.</returns>
        public Md5Guid GetNullKey() => Md5Guid.Empty;

        /// <summary>
        /// Generates the next unique randomly or pseudo-randomly generated (version 1) global unique identifier.
        /// </summary>
        /// <returns>The generated global unique identifier.</returns>
        public Md5Guid GetNext() => Md5Guid.NewGuid(namespaceId, name, encoding);
    }


    /// <summary>
    /// Represents an SHA1 UUID provider that produces SHA1-based global unique identifiers (version 5) for DNS as defined in RFC 4122.
    /// </summary>
    public sealed class DnsMd5GuidKeyGenerator : Md5GuidKeyGeneratorBase
    {
        /// <summary>
        /// Initializes an instance of the <see cref="DnsMd5GuidKeyGenerator"/> class.
        /// </summary>
        /// <param name="dns">The DNS that forms part of the namespace of the generated UUID.</param>
        /// <param name="encoding">The type of character encoding to encode <paramref name="dns"/></param>
        public DnsMd5GuidKeyGenerator(string dns, Encoding encoding) : base(Md5Guid.DnsNamespaceId, dns, encoding)
        {
        }
    }

    /// <summary>
    /// Represents an SHA1 UUID provider that produces SHA1-based global unique identifiers (version 5) for URLs as defined in RFC 4122.
    /// </summary>
    public sealed class UrlMd5GuidKeyGenerator : Md5GuidKeyGeneratorBase
    {
        /// <summary>
        /// Initializes an instance of the <see cref="UrlMd5GuidKeyGenerator"/> class.
        /// </summary>
        /// <param name="url">The URL that forms part of the namespace of the generated UUID.</param>
        /// <param name="encoding">The type of character encoding to encode <paramref name="url"/></param>
        public UrlMd5GuidKeyGenerator(string url, Encoding encoding) : base(Md5Guid.UrlNamespaceId, url, encoding)
        {
        }
    }

    /// <summary>
    /// Represents an SHA1 UUID provider that produces SHA1-based global unique identifiers (version 5) for ISO OIDs as defined in RFC 4122.
    /// </summary>
    public sealed class IsoOidMd5GuidKeyGenerator : Md5GuidKeyGeneratorBase
    {
        /// <summary>
        /// Initializes an instance of the <see cref="UrlMd5GuidKeyGenerator"/> class.
        /// </summary>
        /// <param name="oid">The OID that forms part of the namespace of the generated UUID.</param>
        /// <param name="encoding">The type of character encoding to encode <paramref name="oid"/></param>
        public IsoOidMd5GuidKeyGenerator(string oid, Encoding encoding) : base(Md5Guid.IsoOidNamespaceId, oid, encoding)
        {
        }
    }
}