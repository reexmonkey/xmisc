using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies a repository that loads related references of a data model from a data store.
    /// </summary>
    /// <typeparam name="TKey">The type of the key that uniquely identifies the model.</typeparam>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public interface ILoadReferencesRepository<TKey, TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// Loads the related references of a data mode from the data store by using the specified key.
        /// </summary>
        /// <param name="key">The key that uniquely identifies the model in the dat store.</param>
        /// <returns>The found data model; otherwise the default value of the data model</returns>
        TModel LoadReferences(TKey key);

        /// <summary>
        /// Loads related references of data models from the data store by using the specified keys.
        /// </summary>
        /// <param name="keys">The keys that uniquely identify the models in the dat store.</param>
        /// <param name="offset">The number of data models to bypass.</param>
        /// <param name="count">The number of data models to return.</param>
        /// <returns>The found data models; otherwise an empty sequence.</returns>
        IEnumerable<TModel> LoadReferences(IEnumerable<TKey> keys, int? offset = null, int? count = null);

        /// <summary>
        /// Loads related references of data models from the data store by using the specified condition.
        /// </summary>
        /// <param name="predicate">The condition that when evaluated to true, returns the found data model.</param>
        /// <param name="offset">The number of data models to bypass.</param>
        /// <param name="count">The number of data models to return.</param>
        /// <returns>The found data models; otherwise an empty sequence.</returns>
        IEnumerable<TModel> LoadReferences(Expression<Func<TModel, bool>> predicate, int? offset = null, int? count = null);

        /// <summary>
        /// Loads related references of data models asynchronously from the data store by using the specified key.
        /// </summary>
        /// <param name="key">The key that uniquely identifies the model in the dat store.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>The promise to return a found data model; otherwise the promise to return the default value of the data model</returns>
        Task<TModel> LoadReferencesAsync(TKey key, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Loads related references of data models asynchronously from the data store by using the specified keys.
        /// </summary>
        /// <param name="keys">The keys that uniquely identify the models in the dat store.</param>
        /// <param name="offset">The number of data models to bypass.</param>
        /// <param name="count">The number of data models to return.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>The promise to return found data models; otherwise the promise to return an empty sequence.</returns>
        Task<IEnumerable<TModel>> LoadReferencesAsync(IEnumerable<TKey> keys, int? offset = null, int? count = null, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Loads related references of data models asynchronously from the data store by using the specified condition.
        /// </summary>
        /// <param name="predicate">The condition that when evaluated to true, returns the found data model.</param>
        /// <param name="offset">The number of data models to bypass.</param>
        /// <param name="count">The number of data models to return.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>The promise to return found data models; otherwise the promise to return an empty sequence.</returns>
        Task<IEnumerable<TModel>> LoadReferencesAsync(Expression<Func<TModel, bool>> predicate, int? offset = null, int? count = null, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Loads related references of the specified data model asynchronously from the data store.
        /// </summary>
        /// <param name="model">The model whose related references are loaded.</param>
        /// <returns>The data model with its related references.</returns>
        TModel LoadReferences(TModel model);

        /// <summary>
        /// Loads related references of the specified data models asynchronously from the data store.
        /// </summary>
        /// <param name="models">The data models whose related references are loaded.</param>
        /// <returns>The data model with its related references.</returns>
        IEnumerable<TModel> LoadReferences(IEnumerable<TModel> models);

        /// <summary>
        /// Loads related references of the specified data model asynchronously from the data store.
        /// </summary>
        /// <param name="model">The model to hydrate.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to return the data model with its related references.</returns>
        Task<TModel> LoadReferencesAsync(TModel model, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Loads related references of the specified data models asynchronously from the data store.
        /// </summary>
        /// <param name="models">The models to hydrate.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to return the data models with their related references.</returns>
        Task<IEnumerable<TModel>> LoadReferencesAsync(IEnumerable<TModel> models, CancellationToken token = default(CancellationToken));
    }
}
