﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies a repository that trashes models temporarily by marking them for deletion.
    /// <para/> The trashed models are excluded from access until the repository restores them.
    /// </summary>
    /// <typeparam name="TKey">The type of key that identifies the model to trash or restore.</typeparam>
    /// <typeparam name="TModel">The type of model to trash or restore.</typeparam>
    public interface ITrashRepository<in TKey, TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// Trashes the data model that is specified by a unique identifier.
        /// </summary>
        /// <param name="key">The unique identifier that specifies the data model to trash.</param>
        /// <param name="references">Should the references of the data model also be trashed?</param>
        /// <returns>The data model that is specified by the provided <paramref name="key"/>.</returns>
        TModel TrashByKey(TKey key, bool? references = null);

        /// <summary>
        /// Trashes the data models that are specified by unique identifiers.
        /// </summary>
        /// <param name="keys">The unique identifiers that specify the data models to trash.</param>
        /// <param name="references">Should the references of located data models also be trashed?</param>
        /// <param name="offset">The number of unique identifiers to bypass.</param>
        /// <param name="count">The numbers of unique identifiers to return.</param>
        /// <returns>The data models specified by the provided <paramref name="keys"/>. </returns>
        IEnumerable<TModel> TrashAllByKeys(IEnumerable<TKey> keys, bool? references = null, int? offset = null, int? count = null);

        /// <summary>
        /// Trashes the given data model.
        /// </summary>
        /// <param name="model">The data model to trash.</param>
        /// <param name="references">Should the references of the data model also be trashed?</param>
        void Trash(TModel model, bool? references = null);

        /// <summary>
        /// Trashes the given data models.
        /// </summary>
        /// <param name="models">The data models to trash.</param>
        /// <param name="references">Should the references of the data model also be trashed?</param>
        void TrashAll(IEnumerable<TModel> models, bool? references = null);

        /// <summary>
        /// Restores a data model that is specified by a unique identifier.
        /// </summary>
        /// <param name="key">The unique identifier that specifies the data model to restore.</param>
        /// <param name="references">Should the references of the restored data model also be restored?</param>
        /// <returns>The data model that is specified by the provided <paramref name="key"/>.</returns>
        TModel RestoreByKey(TKey key, bool? references = null);

        /// <summary>
        /// Restores data models that are specified by the provided unique identifiers.
        /// </summary>
        /// <param name="keys">The unique identifiers that specify the data models to restore.</param>
        /// <param name="references">Should the references of restored data models also be restored?</param>
        /// <param name="offset">The number of unique identifiers to bypass.</param>
        /// <param name="count">The numbers of unique identifiers to return.</param>
        /// <returns>The data models that are specified by the provided <paramref name="keys"/>. </returns>
        IEnumerable<TModel> RestoreAllByKeys(IEnumerable<TKey> keys, bool? references = null, int? offset = null, int? count = null);

        /// <summary>
        /// Restores the specified data model.
        /// </summary>
        /// <param name="model">The data model to restore.</param>
        /// <param name="references">Should the references of the restored data model also be restored?</param>
        void Restore(TModel model, bool? references = null);

        /// <summary>
        /// Restores the specified data models.
        /// </summary>
        /// <param name="models">The data models to restore.</param>
        /// <param name="references">Should the references of restored data models also be restored?</param>
        void RestoreAll(IEnumerable<TModel> models, bool? references = null);

        /// <summary>
        /// Trashes a data model asynchronously that is specified by a provided unique identifier.
        /// </summary>
        /// <param name="key">The unique identifier that specifies the data model to trash.</param>
        /// <param name="references">Should the references of the data model also be trashed?</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to trash a data model that is specified by <paramref name="key"/>.</returns>
        Task<TModel> TrashByKeyAsync(TKey key, bool? references = null, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Trashes data models asynchronously that are specified by the provided unique identifiers.
        /// </summary>
        /// <param name="keys">The unique identifiers that specify the data models to trash.</param>
        /// <param name="references">Should the references of located data models also be trashed?</param>
        /// <param name="offset">The number of data models to bypass.</param>
        /// <param name="count">The numbers of data models to return.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to trash and return data models that are specified by <paramref name="keys"/>.</returns>
        Task<IEnumerable<TModel>> TrashAllByKeysAsync(IEnumerable<TKey> keys, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Trashes the specified data model asynchronously.
        /// </summary>
        /// <param name="model">The data model to trash.</param>
        /// <param name="references">Should the references of the data model also be trashed?</param>
        /// <returns>A promise to trash the given data model.</returns>
        Task TrashAsync(TModel model, bool? references = null);

        /// <summary>
        /// Trashes the specified data models asynchronously.
        /// </summary>
        /// <param name="models">The data models to trash.</param>
        /// <param name="references">Should the references of the data models also be trashed?</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to trash the given data models.</returns>
        Task TrashAllAsync(IEnumerable<TModel> models, bool? references = null, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Restores a data model asynchronously that is specified by the provided <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The identifier that specifies the data model to restore.</param>
        /// <param name="references">Should the references of the restored data model also be restored?</param>
        /// <returns>A promise to restore and return the trashed data model that is specified by <paramref name="key"/>.</returns>
        Task<TModel> RestoreByKeyAsync(TKey key, bool? references = null);

        /// <summary>
        /// Restores data models asynchronously that are specified by unique identifiers .
        /// </summary>
        /// <param name="keys">The identifiers that specify the data models to restore.</param>
        /// <param name="references">Should the references of the restored data model also be restored?</param>
        /// <param name="offset">The number of data models to bypass.</param>
        /// <param name="count">The numbers of data models to return.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to restore and return the trashed data models that are specified by <paramref name="keys"/>.</returns>
        Task<IEnumerable<TModel>> RestoreAllByKeysAsync(IEnumerable<TKey> keys, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Restores the specified data model asynchronously.
        /// </summary>
        /// <param name="model">The data model to restore.</param>
        /// <param name="references">Should the references of the data model also be restored?</param>
        /// <returns>A promise to restore the given data model.</returns>
        Task RestoreAsync(TModel model, bool? references = null);

        /// <summary>
        /// Restores the specified data models asynchronously.
        /// </summary>
        /// <param name="models">The data models to restore.</param>
        /// <param name="references">Should the references of the data models also be restored?</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to restore the given data models.</returns>
        Task RestoreAllAsync(IEnumerable<TModel> models, bool? references = null, CancellationToken token = default(CancellationToken));
    }
}