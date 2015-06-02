using System;

namespace reexjungle.xmisc.infrastructure.contracts
{
    /// <summary>
    /// Specifies a contract for generating content
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public interface IGenerator<out TValue>
    {
        /// <summary>
        /// Produces the next content
        /// </summary>
        /// <returns></returns>
        TValue GetNext();

        /// <summary>
        /// Resets the content generator
        /// </summary>
        void Reset();
    }

    /// <summary>
    /// Specifies a contract for generating unique identifiers
    /// </summary>
    /// <typeparam name="TKey">The given type of key</typeparam>
    public interface IKeyGenerator<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Produces the next key
        /// </summary>
        /// <returns>The next available key</returns>
        TKey GetNext();

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