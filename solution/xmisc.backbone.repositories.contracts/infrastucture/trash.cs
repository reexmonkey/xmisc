using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts.infrastucture
{
    /// <summary>
    /// Specifies a repository that trashes models by excluding access to them, as well as
    /// restores access to excluded models in the data store.
    /// </summary>
    /// <typeparam name="TKey">The type of key that identifies the model to trash or restore.</typeparam>
    /// <typeparam name="TModel">The type of model to trash or restore.</typeparam>
    public interface ITrashRepository<in TKey, in TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// Excludes access to a data model that is specified by the provided <paramref name="key"/> from the data store.
        /// </summary>
        /// <param name="key">The identifier that specifies the data model to exclude.</param>
        /// <returns>True if the data was trashed; otherwise false.</returns>
        bool TrashByKey(TKey key);

        /// <summary>
        /// Excludes access to data models that are specified by the provided <paramref name="keys"/> from the data store.
        /// </summary>
        /// <param name="keys">The identifiers that specify the data models to exclude.</param>
        /// <returns>The number of data models trashed.</returns>
        int TrashAllByKeys(IEnumerable<TKey> keys);

        /// <summary>
        /// Excludes access to the specified data model from the data store.
        /// </summary>
        /// <param name="model">The data model in the data store to exclude.</param>
        /// <returns>True if the data was trashed; otherwise false.</returns>
        bool Trash(TModel model);

        /// <summary>
        /// Excludes access to the specified data models from the data store.
        /// </summary>
        /// <param name="models">The data models in the data store to exclude.</param>
        /// <returns>The number of data models trashed.</returns>
        int TrashAll(IEnumerable<TModel> models);

        /// <summary>
        /// Restores a data model that is specified by the provided <paramref name="key"/> from the data store.
        /// </summary>
        /// <param name="key">The identifier that specifies the data model to exclude.</param>
        /// <returns>True if the data was trashed; otherwise false.</returns>
        bool RestoreByKey(TKey key);

        /// <summary>
        /// Restores data models that are specified by the provided <paramref name="keys"/> from the data store.
        /// </summary>
        /// <param name="keys">The identifiers that specify the data models to exclude.</param>
        /// <returns>The number of data models trashed.</returns>
        int RestoreAllByKeys(IEnumerable<TKey> keys);

        /// <summary>
        /// Restores the specified data model from the data store.
        /// </summary>
        /// <param name="model">The data model in the data store to exclude.</param>
        /// <returns>True if the data was trashed; otherwise false.</returns>
        bool Restore(TModel model);

        /// <summary>
        /// Restores the specified data models from the data store.
        /// </summary>
        /// <param name="models">The data models in the data store to exclude.</param>
        /// <returns>The number of data models trashed.</returns>
        int RestoreAll(IEnumerable<TModel> models);

        /// <summary>
        /// Excludes asynchronously a data model that is specified by the provided <paramref name="key"/> from the data store.
        /// </summary>
        /// <param name="key">The identifier that specifies the data model to exclude.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to return true if the data was trashed; otherwise false.</returns>
        Task<bool> TrashByKeyAsync(TKey key, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Excludes asynchronously data models that are specified by the provided <paramref name="keys"/> from the data store.
        /// </summary>
        /// <param name="keys">The identifiers that specify the data models to exclude.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to return the number of data models trashed.</returns>
        Task<int> TrashAllByKeysAsync(IEnumerable<TKey> keys, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Excludes asynchronously the specified data model from the datastore.
        /// </summary>
        /// <param name="model">The data model in the data store to exclude.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to return true if the data was trashed; otherwise false.</returns>
        Task<bool> TrashAsync(TModel model, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Excludes asynchronously the specified data models from the data store.
        /// </summary>
        /// <param name="models">The data models to exclude from the data store.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to return the number of data models trashed.</returns>
        Task<int> TrashAllAsync(IEnumerable<TModel> models, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Excludes asynchronously a data model that is specified by the provided <paramref name="key"/> from the data store.
        /// </summary>
        /// <param name="key">The identifier that specifies the data model to exclude.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to return true if the data was trashed; otherwise false.</returns>
        Task<bool> RestoreByKeyAsync(TKey key, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Excludes asynchronously data models that are specified by the provided <paramref name="keys"/> from the data store.
        /// </summary>
        /// <param name="keys">The identifiers that specify the data models to exclude.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to return the number of data models trashed.</returns>
        Task<int> RestoreAllByKeysAsync(IEnumerable<TKey> keys, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Excludes asynchronously the specified data model from the data store.
        /// </summary>
        /// <param name="model">The data model in the data store to exclude.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to return true if the data was trashed; otherwise false.</returns>
        Task<bool> RestoreAsync(TModel model, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Excludes asynchronously the specified data models from the data store.
        /// </summary>
        /// <param name="models">The data models to exclude from the data store.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to return the number of data models trashed.</returns>
        Task<int> RestoreAllAsync(IEnumerable<TModel> models, CancellationToken token = default(CancellationToken));
    }
}
