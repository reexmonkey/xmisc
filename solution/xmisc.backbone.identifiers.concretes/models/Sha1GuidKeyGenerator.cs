using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using System;
using System.Text;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.models
{
    /// <summary>
    /// Represents an abstract provider that produces SHA1-based global unique identifiers (version 5) as defined in RFC 4122.
    /// </summary>
    public abstract class Sha1GuidKeyGeneratorBase : IKeyGenerator<Sha1Guid>
    {
        private readonly Guid namespaceId;
        private readonly string name;
        private readonly Encoding encoding;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sha1GuidKeyGeneratorBase"/> class.
        /// </summary>
        /// <param name="namespaceId">One of the default namespaces as defined in RFC 4122. </param>
        /// <param name="name">The unique identifying string, which together with the default namespaces form a unique namespace for the UUID generation.</param>
        /// <param name="encoding">The character encoding to encode <paramref name="name"/> to its byte representation.</param>
        /// <exception cref="ArgumentException"></exception>
        public Sha1GuidKeyGeneratorBase(Guid namespaceId, string name, Encoding encoding)
        {
            this.namespaceId = namespaceId;

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));

            this.name = name;
            this.encoding = encoding;
        }

        /// <summary>
        /// Gets the default time-based globally unique identifier.
        /// </summary>
        /// <returns>The default unique identifier.</returns>
        public Sha1Guid GetNullKey() => Sha1Guid.Empty;

        /// <summary>
        /// Generates the next unique randomly or pseudo-randomly generated (version 1) global unique identifier.
        /// </summary>
        /// <returns>The generated global unique identifier.</returns>
        public Sha1Guid GetNext() => Sha1Guid.NewGuid(namespaceId, name, encoding);
    }


    public sealed class DnsSha1GuidKeyGenerator : Sha1GuidKeyGeneratorBase
    {
        public DnsSha1GuidKeyGenerator(string dns, Encoding encoding) : base(Sha1Guid.DnsNamespaceId, dns, encoding)
        {
        }
    }

    public sealed class UrlSha1GuidKeyGenerator : Sha1GuidKeyGeneratorBase
    {
        public UrlSha1GuidKeyGenerator(string url, Encoding encoding) : base(Sha1Guid.UrlNamespaceId, url, encoding)
        {
        }
    }

    public sealed class IsoOidSha1GuidKeyGenerator : Sha1GuidKeyGeneratorBase
    {
        public IsoOidSha1GuidKeyGenerator(string oid, Encoding encoding) : base(Sha1Guid.IsoOidNamespaceId, oid, encoding)
        {
        }
    }
}