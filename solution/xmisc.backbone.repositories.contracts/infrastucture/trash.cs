using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts.infrastucture
{
    /// <summary>
    /// Specifies a repository that trashes models by excluding access to them, as well as
    /// restores access to excluded models in the data store..
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
        void TrashByKey(TKey key);

        /// <summary>
        /// Excludes access to data models that are specified by the provided <paramref name="keys"/> from the data store.
        /// </summary>
        /// <param name="keys">The identifiers that specify the data models to exclude.</param>
        void TrashAllByKeys(IEnumerable<TKey> keys);

        /// <summary>
        /// Excludes access to the specified data model from the data store.
        /// </summary>
        /// <param name="model">The data model in the data store to exclude.</param>
        void Trash(TModel model);

        /// <summary>
        /// Excludes access to the specified data models from the data store.
        /// </summary>
        /// <param name="models">The data models in the data store to exclude.</param>
        void TrashAll(IEnumerable<TModel> models);

        /// <summary>
        /// Restores a data model that is specified by the provided <paramref name="key"/> from the data store.
        /// </summary>
        /// <param name="key">The identifier that specifies the data model to exclude.</param>
        void RestoreByKey(TKey key);

        /// <summary>
        /// Restores data models that are specified by the provided <paramref name="keys"/> from the data store.
        /// </summary>
        /// <param name="keys">The identifiers that specify the data models to exclude.</param>
        void RestoreAllByKeys(IEnumerable<TKey> keys);

        /// <summary>
        /// Restores the specified data model from the data store.
        /// </summary>
        /// <param name="model">The data model in the data store to exclude.</param>
        void Restore(TModel model);

        /// <summary>
        /// Restores the specified data models from the data store.
        /// </summary>
        /// <param name="models">The data models in the data store to exclude.</param>
        void RestoreAll(IEnumerable<TModel> models);

        /// <summary>
        /// Asynchronously forgets a data model that is specified by the provided <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The identifier that specifies the data model to exclude.</param>
        /// <returns>A task that represents an asynchronous operation.</returns>
        Task TrashByKeyAsync(TKey key);

        /// <summary>
        /// Asynchronously forgets data models that are specified by the provided <paramref name="keys"/>.
        /// </summary>
        /// <param name="keys">The identifiers that specify the data models to exclude.</param>
        /// <returns>A task that represents an asynchronous operation</returns>
        Task TrashAllByKeysAsync(IEnumerable<TKey> keys);

        /// <summary>
        /// Asynchronously forgets the specified data model from the datastore.
        /// </summary>
        /// <param name="model">The data model in the data store to exclude.</param>
        /// <returns>A task that represents an asynchronous operation.</returns>
        Task TrashAsync(TModel model);

        /// <summary>
        /// Asynchronously forgets the specified data models from the data store.
        /// </summary>
        /// <param name="models">The data models to exclude from the data store.</param>
        /// <returns>A task that represents an asynchronous operation.</returns>
        Task TrashAllAsync(IEnumerable<TModel> models);

        /// <summary>
        /// Asynchronously forgets a data model that is specified by the provided <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The identifier that specifies the data model to exclude.</param>
        /// <returns>A task that represents an asynchronous operation.</returns>
        Task RestoreByKeyAsync(TKey key);

        /// <summary>
        /// Asynchronously forgets data models that are specified by the provided <paramref name="keys"/>.
        /// </summary>
        /// <param name="keys">The identifiers that specify the data models to exclude.</param>
        /// <returns>A task that represents an asynchronous operation</returns>
        Task RestoreAllByKeysAsync(IEnumerable<TKey> keys);

        /// <summary>
        /// Asynchronously forgets the specified data model from the data store.
        /// </summary>
        /// <param name="model">The data model in the data store to exclude.</param>
        /// <returns>A task that represents an asynchronous operation.</returns>
        Task RestoreAsync(TModel model);

        /// <summary>
        /// Asynchronously forgets the specified data models from the data store.
        /// </summary>
        /// <param name="models">The data models to exclude from the data store.</param>
        /// <returns>A task that represents an asynchronous operation.</returns>
        Task RestoreAllAsync(IEnumerable<TModel> models);
    }
}
