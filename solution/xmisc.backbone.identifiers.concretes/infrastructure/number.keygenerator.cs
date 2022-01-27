using System;
using reexmonkey.xmisc.backbone.identifiers.contracts.models;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.infrastructure
{
    /// <summary>
    /// Represents a provider that produces unique numeric identifiers.
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    public abstract class NumberKeyGeneratorBase<TNumber> : IKeyGenerator<TNumber>
        where TNumber : struct, IEquatable<TNumber>, IComparable<TNumber>, IComparable, IFormattable, IConvertible
    {
        /// <summary>
        /// Gets the default unique numeric identifier.
        /// </summary>
        /// <returns>The default unique identifier.</returns>
        public TNumber GetNullKey() => default;

        /// <summary>
        /// Generates the next unique numeric identifier.
        /// </summary>
        /// <returns>The generated unique numeric identifier.</returns>
        public abstract TNumber GetNext();

    }

}