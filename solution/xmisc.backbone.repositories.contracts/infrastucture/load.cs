using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts.infrastucture
{
    /// <summary>
    /// Specifies a repository that loads a data model and all its references from a data store.
    /// </summary>
    /// <typeparam name="TKey">The type of the key that uniquely identifies the model.</typeparam>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public interface ILoadRepository<TKey, TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// Loads the model from the data store by using the specified key.
        /// </summary>
        /// <param name="key">The key that uniquely identifies the model in the dat store.</param>
        /// <returns>The found data model; otherwise the default value of the data model</returns>
        TModel LoadByKey(TKey key);

        /// <summary>
        /// Loads models from the data store by using the specified keys.
        /// </summary>
        /// <param name="keys">The keys that uniquely identify the models in the dat store.</param>
        /// <param name="offset">The number of data models to bypass.</param>
        /// <param name="count">The number of data models to return.</param>
        /// <returns>The found data models; otherwise an empty sequence.</returns>
        IEnumerable<TModel> LoadAllByKeys(IEnumerable<TKey> keys, int? offset = null, int? count = null);

        /// <summary>
        /// Loads models from the data store by using the specified condition.
        /// </summary>
        /// <param name="predicate">The condition that when evaluated to true, returns the found data model.</param>
        /// <param name="offset">The number of data models to bypass.</param>
        /// <param name="count">The number of data models to return.</param>
        /// <returns>The found data models; otherwise an empty sequence.</returns>
        IEnumerable<TModel> LoadAll(Expression<Func<TModel, bool>> predicate, int? offset = null, int? count = null);

        /// <summary>
        /// Loads asynchronously the model from the data store by using the specified key.
        /// </summary>
        /// <param name="key">The key that uniquely identifies the model in the dat store.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>The promise to return a found data model; otherwise the promise to return the default value of the data model</returns>
        Task<TModel> LoadByKeyAsync(TKey key, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Loads asynchronously models from the data store by using the specified keys.
        /// </summary>
        /// <param name="keys">The keys that uniquely identify the models in the dat store.</param>
        /// <param name="offset">The number of data models to bypass.</param>
        /// <param name="count">The number of data models to return.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>The promise to return found data models; otherwise the promise to return an empty sequence.</returns>
        Task<IEnumerable<TModel>> LoadAllByKeysAsync(IEnumerable<TKey> keys, int? offset = null, int? count = null, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Loads asynchronously models from the data store by using the specified condition.
        /// </summary>
        /// <param name="predicate">The condition that when evaluated to true, returns the found data model.</param>
        /// <param name="offset">The number of data models to bypass.</param>
        /// <param name="count">The number of data models to return.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>The promise to return found data models; otherwise the promise to return an empty sequence.</returns>
        Task<IEnumerable<TModel>> LoadAllAsync(Expression<Func<TModel, bool>> predicate, int? offset = null, int? count = null, CancellationToken token = default(CancellationToken));
    }
}
