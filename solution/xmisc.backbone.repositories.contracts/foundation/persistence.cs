using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace xmisc.backbone.repositories.contracts.foundation
{
    /// <summary>
    /// Specifies a repository that persists models to a data store.
    /// </summary>
    /// <typeparam name="TKey">The type of key, which identifies the model to persist.</typeparam>
    /// <typeparam name="TModel">The type of model to persist.</typeparam>
    public interface IModelPersistenceRepository<in TKey, in TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        where TModel : new()

    {
        /// <summary>
        /// Saves the specified model to a data store.
        /// </summary>
        /// <param name="model">The model to save.</param>
        void Persist(TModel model);

        /// <summary>
        /// Saves all the specified models to a data store.
        /// </summary>
        /// <param name="models">The models to save.</param>
        void PersistAll(IEnumerable<TModel> models);

        /// <summary>
        /// Saves asynchronously the specified model to a data store.
        /// </summary>
        /// <param name="model">The model to save.</param>
        /// <returns>A task that represents an asynchronous operation.</returns>
        Task PersistAsync(TModel model);

        /// <summary>
        /// Saves asynchronously all the specified models to a data store.
        /// </summary>
        /// <param name="models">The models to save.</param>
        /// <returns>A task that represents an asynchronous operation.</returns>
        Task PersistAllAsync(IEnumerable<TModel> models);

    }
}
