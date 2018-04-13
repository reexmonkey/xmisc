using System;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts.infrastucture
{
    /// <summary>
    /// Specifies an interface for an entity that contains a data item (key), which uniquely identifies the entity.
    /// </summary>
    /// <typeparam name="TKey">The type of key that uniquely identifies the entity.</typeparam>
    public interface IHasKey<TKey> where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// Gets the data item that uniquely identifies the entity.
        /// </summary>
        /// <returns>The data item that uniquely identifies the entity.</returns>
        TKey GetKey();

        /// <summary>
        /// Sets the data item that uniquely identifies the entity.
        /// </summary>
        /// <param name="key"></param>
        void SetKey(TKey key);

        /// <summary>
        /// Asynchronously gets the data item that uniquely identifies the entity.
        /// </summary>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param> 
        /// <returns>The asynchronous operation that returns a data item that uniquely identifies the entity.</returns>
        Task<TKey> GetKeyAsync(CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Asynchronously sets the data item that uniquely identifies the entity.
        /// </summary>
        /// <param name="key">The data item that uniquely identifies the entity</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param> 
        /// <returns>The asynchronous operation that sets the the data item, which uniquely identifies the entity.</returns>
        Task SetKeyAsync(TKey key, CancellationToken token = default(CancellationToken));
    }

    /// <summary>
    /// Specifies an interface for an enity that contains multiple data items (key attributes)
    ///  that together uniquely identifies the entity.
    /// </summary>
    /// <typeparam name="TKey">The type of composite key that is assembled from provided key attributes.</typeparam>
    public interface IHasCompositeKey<TKey> : IHasKey<TKey>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// Adds a key attribute to the pool of key attributes.
        /// </summary>
        /// <typeparam name="TKeyAttribute"></typeparam>
        /// <param name="attribute">The key attribute to add to the pool of key attributes.</param>
        void AddKeyAttribute<TKeyAttribute>(TKeyAttribute attribute);

        /// <summary>
        /// Removes a key attribute from the pool of key attributes.
        /// </summary>
        /// <typeparam name="TKeyAttribute">The type of key attribute to remove.</typeparam>
        /// <param name="attribute">The key attribute to remove from the pool of key attributes.</param>
        void RemoveKeyAttribute<TKeyAttribute>(TKeyAttribute attribute);

        /// <summary>
        /// Asynchronously adds a key attribute to the pool of key attributes.
        /// </summary>
        /// <typeparam name="TKeyAttribute"></typeparam>
        /// <param name="attribute">The key attribute to add to the pool of key attributes.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param> 
        /// <returns>A promise that the key attribute shall be added to the pool of key attributes.</returns>
        Task AddKeyAttributeAsync<TKeyAttribute>(TKeyAttribute attribute, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Asynchronously removes a key attribute from the pool of key attributes.
        /// </summary>
        /// <typeparam name="TKeyAttribute">The type of key attribute to remove.</typeparam>
        /// <param name="attribute">The key attribute to remove from the pool of key attributes.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param> 
        /// <returns>A promise that the key attribute shall be remnoved from the pool of key attributes.</returns>
        Task RemoveKeyAttributeAsync<TKeyAttribute>(TKeyAttribute attribute, CancellationToken token = default(CancellationToken));
    }
}
