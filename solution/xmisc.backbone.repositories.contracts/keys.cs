using System;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts
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
}