using System;

namespace reexmonkey.xmisc.backbone.identifiers.contracts.generators
{
    /// <summary>
    /// Specifies a generator that produces unique identifiers.
    /// </summary>
    /// <typeparam name="TKey">The type of unique identifier.</typeparam>
    public interface IKeyGenerator<out TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Gets the default unique identifier
        /// </summary>
        /// <returns>The default unique identifier.</returns>
        TKey GetDefaultKey();

        /// <summary>
        /// Generates the next unique identifier.
        /// </summary>
        /// <returns>The generated unique identifier.</returns>
        TKey GetNext();
    }

    /// <summary>
    /// Specifies a generator that produces universal unique identifiers from a name-based value.
    /// </summary>
    /// <typeparam name="TKey">The type of unique identifier.</typeparam>
    public interface INameKeyGenerator<out TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Gets the default unique identifier
        /// </summary>
        /// <returns>The default unique identifier.</returns>
        TKey GetDefaultKey();

        /// <summary>
        /// Generates the unique identifier for the given name-based value.
        /// </summary>
        /// <param name="name">The name-based value, for which the unique identifier is generated.</param>
        /// <returns>The generated unique identifier.</returns>
        TKey GetKey(string name);
    }
}