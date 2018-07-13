using System;

namespace reexmonkey.xmisc.backbone.identifiers.contracts.models
{
    /// <summary>
    /// Specifies a provider that produces unique identifiers.
    /// </summary>
    /// <typeparam name="TKey">The type of unique identifier.</typeparam>
    public interface IKeyGenerator<out TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Gets the default unique identifier
        /// </summary>
        /// <returns>The default unique identifier.</returns>
        TKey GetNullKey();

        /// <summary>
        /// Generates the next unique identifier.
        /// </summary>
        /// <returns>The generated unique identifier.</returns>
        TKey GetNext();
    }
}