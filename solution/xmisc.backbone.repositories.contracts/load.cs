using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies a repository that loads related references of a data model from a data store.
    /// </summary>
    /// <typeparam name="TKey">The type of the key that uniquely identifies the model.</typeparam>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public interface ILoadRepository<TKey, TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// Loads the references of the specified data model.
        /// </summary>
        /// <param name="model">The model, whose references are loaded.</param>
        void Load(TModel model);

        /// <summary>
        /// Gets the owners of the specified data models.
        /// </summary>
        /// <param name="models">The models, whose references are loaded.</param>
        void LoadAll(IEnumerable<TModel> models);

        /// <summary>
        /// Loads the references of the specified data model in an asynchronous operation.
        /// </summary>
        /// <param name="model">The models, whose references are loaded.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        Task LoadAsync(TModel model, CancellationToken token = default);

        /// <summary>
        /// Loads all the references of the specified data models in an asynchronous operation.
        /// </summary>
        /// <param name="models">The models, whose owners are sought.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>Owners of the models in the data store; otherwise an empty collection.</returns>
        Task LoadAllAsync(IEnumerable<TModel> models, CancellationToken token = default);
    }
}