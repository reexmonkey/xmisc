using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts.infrastucture
{
    /// <summary>
    /// Specifies a repository that erases entities from a data store.
    /// </summary>
    /// <typeparam name="TKey">The type of key, which identifies the model to erase.</typeparam>
    /// <typeparam name="TModel">The type of model to erase.</typeparam>
    public interface IEraseRepository<in TKey, in TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// Erases an entity that is specified by the provided <paramref name="key"/> from the data store.
        /// </summary>
        /// <param name="key">The identifier that specifies the entity to erase.</param>
        /// <returns>True if the entity was erased; otherwise false.</returns>
        bool EraseByKey(TKey key);

        /// <summary>
        /// Erases entities that are specified by the provided <paramref name="keys"/> from the data store.
        /// </summary>
        /// <param name="keys">The identifiers that specify the entities to erase.</param>
        /// <returns>The number of entities erased.</returns>
        int EraseAllByKeys(IEnumerable<TKey> keys);

        /// <summary>
        /// Erases the specified entity from the data store.
        /// </summary>
        /// <param name="model">The entity in the data store to erase.</param>
        /// <returns>True if the entity was erased; otherwise false.</returns>
        bool Erase(TModel model);

        /// <summary>
        /// Erases the specified entities from the data store.
        /// </summary>
        /// <param name="models">The entities in the data store to erase.</param>
        /// <returns>The number of entities erased.</returns>
        int EraseAll(IEnumerable<TModel> models);

        /// <summary>
        /// Asynchronously erases an entity that is specified by the provided <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The identifier that specifies the entity to erase.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param> 
        /// <returns>A promise that returns true if the entity was erased; otherwise false.</returns>
        Task<bool> EraseByKeyAsync(TKey key, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Asynchronously erases entities that are specified by the provided <paramref name="keys"/>.
        /// </summary>
        /// <param name="keys">The identifiers that specify the entities to erase.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param> 
        /// <returns>A promise that returns the number of entities erased.</returns>
        Task<int> EraseAllByKeysAsync(IEnumerable<TKey> keys, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Asynchronously erases the specified entity from the datastore.
        /// </summary>
        /// <param name="model">The entity in the data store to erase.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param> 
        /// <returns>A promise that returns true if the entity was erased; otherwise false.</returns>
        Task<bool> EraseAsync(TModel model, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Asynchronously erases the specified entities from the data store.
        /// </summary>
        /// <param name="models">The entities to erase from the data store.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param> 
        /// <returns>A promise that returns the number of entities erased.</returns>
        Task<int> EraseAllAsync(IEnumerable<TModel> models, CancellationToken token = default(CancellationToken));
    }
}
