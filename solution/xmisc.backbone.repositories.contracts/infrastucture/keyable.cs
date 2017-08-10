using System;

namespace xmisc.backbone.repositories.contracts.infrastucture
{
    /// <summary>
    /// Specifies an interface for an entity that contains a unique key identifier.
    /// </summary>
    /// <typeparam name="TKey">The type of unique key identifier.</typeparam>
    public interface IHasKey<TKey> where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// Gets the unique key identifier of the entity.
        /// </summary>
        /// <returns></returns>
        TKey GetKey();

        /// <summary>
        /// Sets the unique key identifier of the entity.
        /// </summary>
        /// <param name="key">The unique key identifier of the entity to set.</param>
        void SetKey(TKey key);
    }

    /// <summary>
    /// Specifies an interface for an entity that contains a unqiue composite key identifier constructed from a collection of keys.</summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public interface IHasCompositeKey<out TKey> where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// Gets the unique key identifier of the entity.
        /// </summary>
        /// <returns>The unique composite key of the entity</returns>
        TKey GetCompositeKey();

        /// <summary>
        /// Adds the given key to the composite pool of keys.
        /// </summary>
        /// <param name="key">The key identifier to add to the pool of composite keys.</param>
        void AddKey(object key);


        /// <summary>
        /// Removes the given key from the composite pool of keys.
        /// </summary>
        /// <param name="key">The key identifier to removed from the pool of composite keys.</param>
        void RemoveKey(object key);


    }
}
