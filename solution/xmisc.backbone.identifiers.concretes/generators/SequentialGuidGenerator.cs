using reexmonkey.xmisc.backbone.identifiers.contracts.generators;
using reexmonkey.xmisc.backbone.identifiers.contracts.models;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.models
{
    /// <summary>
    /// Represents a provider that produces time-based (version 1) universal unique identifiers as defined in RFC 4122.
    /// </summary>
    public class SequentialGuidKeyGenerator : IKeyGenerator<SequentialGuid>
    {
        /// <summary>
        /// Gets the default time-based (version 1) universal unique identifier.
        /// </summary>
        /// <returns>The default unique identifier.</returns>
        public SequentialGuid GetDefaultKey() => SequentialGuid.Empty;

        /// <summary>
        /// Generates the next time-based (version 1) universal unique identifier.
        /// </summary>
        /// <returns>The generated universal unique identifier.</returns>
        public SequentialGuid GetNext() => SequentialGuid.NewGuid();
    }
}