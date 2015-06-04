using System;

namespace reexjungle.xmisc.infrastructure.contracts
{
    /// <summary>
    /// Specifies a contract for generating values
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public interface IGenerator<out TValue>
    {
        /// <summary>
        /// Generates a value.
        /// </summary>
        /// <returns>The generated value.</returns>
        TValue GetNext();

        /// <summary>
        /// Re-initializes the generator.
        /// </summary>
        void Reset();
    }

    /// <summary>
    /// Specifies a contract for generating unique identifiers.
    /// </summary>
    /// <typeparam name="TKey">The type of key to generate.</typeparam>
    public interface IKeyGenerator<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Generates a key.
        /// </summary>
        /// <returns>The generated key.</returns>
        TKey GetNext();

        /// <summary>
        /// Recycles a used key for reuse purposes.
        /// </summary>
        /// <param name="key">The key to be recycled.</param>
        void Recapture(TKey key);

        /// <summary>
        /// Re-initializes the key generator.
        /// </summary>
        void Reset();
    }
}