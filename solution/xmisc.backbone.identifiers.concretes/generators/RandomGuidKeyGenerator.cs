using reexmonkey.xmisc.backbone.identifiers.contracts.generators;
using System;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.models
{
    /// <summary>
    /// Represents a provider that produces pseudo-random (version 4) universal unique identifiers as defined in RFC 9562.
    /// </summary>
    public class RandomGuidKeyGenerator : IKeyGenerator<Guid>
    {
        /// <summary>
        /// Gets the default pseudo-random (version 4) universal unique identifier.
        /// </summary>
        /// <returns>The default unique identifier.</returns>
        public Guid GetDefaultKey() => Guid.Empty;

        /// <summary>
        /// Generates the next pseudo-random (version 4) universal unique identifier (UUID).
        /// </summary>
        /// <returns>The generated universal unique identifier (UUID).</returns>
        public Guid GetNext() => Guid.NewGuid();
    }
}