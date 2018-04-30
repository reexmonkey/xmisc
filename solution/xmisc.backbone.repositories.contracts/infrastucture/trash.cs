using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts.infrastucture
{
    /// <summary>
    /// Specifies a repository that trashes models by marking them for deletion.
    /// <para/> The marked models are excluded from access until the repository restores them by unmarking them.
    /// </summary>
    /// <typeparam name="TKey">The type of key that identifies the model to trash or restore.</typeparam>
    /// <typeparam name="TModel">The type of model to trash or restore.</typeparam>
    public interface ITrashRepository<in TKey, in TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// Soft-deletes the data model that is specified by the provided <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The unique identifier that specifies the data model to soft-delete.</param>
        /// <param name="token">Propagates the notification that the operation should be cancelled.</param>
        void TrashByKey(TKey key, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Soft-deletes the data models that are specified by the provided <paramref name="keys"/>.
        /// </summary>
        /// <param name="keys">The identifiers that specify the data models to soft-delete.</param>
        /// <param name="offset">The number of unique identifiers to bypass.</param>
        /// <param name="count">The numbers of unique identifiers to return.</param>
        /// <param name="token">Propagates the notification that the operation should be cancelled.</param>
        void TrashAllByKeys(IEnumerable<TKey> keys, int? offset = null, int? count = null, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Soft-deletes the given data model.
        /// </summary>
        /// <param name="model">The data model to soft-delete.</param>
        void Trash(TModel model);

        /// <summary>
        /// Soft-deletes the given data models.
        /// </summary>
        /// <param name="models">The data models to soft-delete.</param>
        /// <param name="token">Propagates the notification that the operation should be cancelled.</param>
        void TrashAll(IEnumerable<TModel> models, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Restores a data model that is specified by the provided unique identifier.
        /// </summary>
        /// <param name="key">The unique identifier that specifies the data model to restore.</param>
        /// <param name="token">Propagates the notification that the operation should be cancelled.</param>
        void RestoreByKey(TKey key, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Restores data models that are specified by the provided unique identifiers.
        /// </summary>
        /// <param name="keys">The unique identifiers that specify the data models to restore.</param>
        /// <param name="offset">The number of unique identifiers to bypass.</param>
        /// <param name="count">The numbers of unique identifiers to return.</param>
        /// <param name="token">Propagates the notification that the operation should be cancelled.</param>
        void RestoreAllByKeys(IEnumerable<TKey> keys, int? offset = null, int? count = null, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Restores the specified data model.
        /// </summary>
        /// <param name="model">The data model to restore.</param>
        void Restore(TModel model);

        /// <summary>
        /// Restores the specified data models.
        /// </summary>
        /// <param name="models">The data models to restore.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        void RestoreAll(IEnumerable<TModel> models, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Soft-deletes asynchronously a data model that is specified by the provided unique identifier.
        /// </summary>
        /// <param name="key">The unique identifier that specifies the data model to soft-delete.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to soft-delete a data model that is specified by a provided <paramref name="key"/>.</returns>
        Task TrashByKeyAsync(TKey key, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Soft-deletes asynchronously data models that are specified by the provided unique identifiers.
        /// </summary>
        /// <param name="keys">The unique identifiers that specify the data models to soft-delete.</param>
        /// <param name="offset">The number of data models to bypass.</param>
        /// <param name="count">The numbers of data models to return.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to soft-delete a data model that is specified by a provided unique identifier.</returns>
        Task TrashAllByKeysAsync(IEnumerable<TKey> keys, int? offset = null, int? count = null, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Soft-deletes the specified data model asynchronously.
        /// </summary>
        /// <param name="model">The data model to soft-delete.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to soft-delete the given data model.</returns>
        Task TrashAsync(TModel model);

        /// <summary>
        /// Soft-deletes the specified data models asynchronously.
        /// </summary>
        /// <param name="models">The data models to soft-delete.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to soft-delete the given data models.</returns>
        Task TrashAllAsync(IEnumerable<TModel> models, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Restores a data model that is specified by the provided <paramref name="key"/> asynchronously.
        /// </summary>
        /// <param name="key">The identifier that specifies the data model to exclude.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to soft-delete the given data model.</returns>
        Task RestoreByKeyAsync(TKey key, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Restores data models that are specified by the provided unique identifiers asynchronously.
        /// </summary>
        /// <param name="keys">The identifiers that specify the data models to restore.</param>
        /// <param name="offset">The number of data models to bypass.</param>
        /// <param name="count">The numbers of data models to return.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to restore data models specified by the provided unique identifiers, for deletion.</returns>
        Task RestoreAllByKeysAsync(IEnumerable<TKey> keys, int? offset = null, int? count = null, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Restores the specified data model asynchronously.
        /// </summary>
        /// <param name="model">The data model to restore.</param>
        /// <returns>A promise to restore the given data model.</returns>
        Task RestoreAsync(TModel model);

        /// <summary>
        /// Restores the specified data models asynchronously.
        /// </summary>
        /// <param name="models">The data models to restore.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to restore the given data models.</returns>
        Task RestoreAllAsync(IEnumerable<TModel> models, CancellationToken token = default(CancellationToken));
    }
}
