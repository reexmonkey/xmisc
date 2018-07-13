using reexmonkey.xmisc.backbone.identifiers.contracts.extensions;
using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using System;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.models
{
    /// <summary>
    /// Represents a provider that produces randomly or pseudo-randomly generated globall unique identifiers (version 4) as defined in RFC 4122.
    /// </summary>
    public class RandomGuidKeyGenerator : IKeyGenerator<Guid>
    {
        /// <summary>
        /// Gets the default globally unique identifier.
        /// </summary>
        /// <returns>The default unique identifier.</returns>
        public Guid GetNullKey() => Guid.Empty;

        /// <summary>
        /// Generates the next randomly or pseudo-randomly generated global unique identifier (version 4).
        /// </summary>
        /// <returns>The generated global unique identifier.</returns>
        public Guid GetNext() => Guid.NewGuid();
    }

}
