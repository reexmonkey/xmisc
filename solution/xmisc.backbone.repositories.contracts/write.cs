using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies a repository that inserts or updates models to a data store.
    /// </summary>
    /// <typeparam name="TKey">The type of key, which identifies the model to insert or update.</typeparam>
    /// <typeparam name="TModel">The type of model to persist.</typeparam>
    public interface IWriteRepository<in TKey, in TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>

    {
        /// <summary>
        /// Inserts or updates the specified model to a data store.
        /// </summary>
        /// <param name="model">The model to persist to the data store.</param>
        /// <param name="references">Should the references of the model also be saved?</param>
        /// <returns>True if the model was inserted in the data store; otherwise false if it was updated.</returns>
        bool Save(TModel model, bool references = true);

        /// <summary>
        /// Inserts or updates the specified models to a data store.
        /// </summary>
        /// <param name="models">The models to persist to the data store.</param>
        /// <param name="references">Should the references of the models also be saved?</param>
        /// <returns>The number of models added to the data store.</returns>
        int SaveAll(IEnumerable<TModel> models, bool references = true);

        /// <summary>
        /// Inserts or updates the specified model asynchronously to a data store.
        /// </summary>
        /// <param name="model">The model to persist to the data store.</param>
        /// <param name="references">Should the references of the model also be saved?</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise that indicates whether the model was inserted in the data store or updated.
        /// <para/> True if the model was inserted in the data store; otherwise false if it was updated.
        /// </returns>
        Task<bool> SaveAsync(TModel model, bool references = true, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Inserts or updates the specified models asynchronously to a data store.
        /// </summary>
        /// <param name="models">The models to persist to the data store.</param>
        /// <param name="references">Should the references of the model also be saved?</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise that indiactes number of models added to the data store.</returns>
        Task<int> SaveAllAsync(IEnumerable<TModel> models, bool references = true, CancellationToken token = default(CancellationToken));
    }
}
