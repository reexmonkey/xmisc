using reexmonkey.xmisc.backbone.identifiers.contracts.extensions;
using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using System;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.models
{

    /// <summary>
    /// Represents a provider that produces time-based global unique identifiers (version 1) as defined in RFC 4122.
    /// </summary>
    public class SequentialGuidKeyGenerator : IKeyGenerator<Guid>
    {
        /// <summary>
        /// Gets the default time-based globally unique identifier.
        /// </summary>
        /// <returns>The default unique identifier.</returns>
        public Guid GetNullKey() => SequentialGuid.Empty;

        /// <summary>
        /// Generates the next unique randomly or pseudo-randomly generated (version 1) global unique identifier.
        /// </summary>
        /// <returns>The generated global unique identifier.</returns>
        public Guid GetNext() => SequentialGuid.NewGuid();
    }

}