using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using System;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.models
{
    /// <summary>
    /// Represents Represents a provider that produces the equivalent string-represenatation of the time-based global unique identifiers (version 1) as defined in RFC 4122.
    /// </summary>
    public class SequentialGuidTextKeyGenerator : IKeyGenerator<string>
    {
        private readonly IKeyGenerator<SequentialGuid> inner;

        /// <summary>
        /// Initializes a new instance of the <see cref="SequentialGuidTextKeyGenerator"/> class with a sequential uuid generator.
        /// </summary>
        /// <param name="inner">The sequential uuid generator initialize this instance with.</param>
        public SequentialGuidTextKeyGenerator(IKeyGenerator<SequentialGuid> inner)
        {
            this.inner = inner ?? throw new ArgumentNullException(nameof(inner));
        }

        /// <summary>
        /// Generates the equivalent string representation of the next unique randomly or pseudo-randomly generated (version 1) global unique identifier.
        /// </summary>
        /// <returns>The equivalent string representation of the generated global unique identifier.</returns>
        public string GetNext() => inner.GetNext().ToString("D");

        /// <summary>
        /// Gets the equivalent string representation of the default time-based globally unique identifier.
        /// </summary>
        /// <returns>The equivalent string representation of the default time-based globally unique identifier.</returns>
        public string GetNullKey() => inner.GetNullKey().ToString("D");
    }
}