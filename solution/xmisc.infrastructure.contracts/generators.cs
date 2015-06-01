using System;

namespace reexjungle.xmisc.infrastructure.contracts
{
    /// <summary>
    /// Specifies a contract for generating keys
    /// </summary>
    /// <typeparam name="TKey">The given type of key</typeparam>
    public interface IKeyGenerator<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Produces the next key
        /// </summary>
        /// <returns>The next available key</returns>
        TKey GetNextKey();

        /// <summary>
        /// Recaptures a key for re-use purposes
        /// </summary>
        /// <param name="key">The key that shall later be reused</param>
        void Recapture(TKey key);

        /// <summary>
        /// Reinitializes the key generator.
        /// </summary>
        void Reset();
    }
}