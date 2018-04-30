using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies a repository that queries a data store for data models of the specified type <typeparamref name="TModel"/>.
    /// </summary>
    /// <typeparam name="TKey">The type of the key that uniquely identifies a model.</typeparam>
    /// <typeparam name="TModel">The type of data model.</typeparam>
    public interface IReadRepository<TKey, TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// Finds a data model in the data store that is specified by a unique identifier or an evaluated condition.
        /// </summary>
        /// <param name="key">The key that uniquely identifies the data model.</param>
        /// <param name="references">Decides whether to load the related references of the data model as well.</param>
        /// <returns>The found data model; otherwise the default value of the data model.</returns>
        TModel FindByKey(TKey key, bool references = true);

        /// <summary>
        /// Finds a data model that satisfies the given predicate.
        /// </summary>
        /// <param name="predicate">The condition that when evaluated to true, returns the found data model.</param>
        /// <param name="references">Decides whether to load the related references of the data model as well.</param>
        /// <returns>The found data model.</returns>
        TModel Find(Expression<Func<TModel, bool>> predicate, bool references = true);

        /// <summary>
        /// Finds data models that are specified by the given keys and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="count"/>.
        /// </summary>
        /// <param name="keys">The keys of the data models to search for.</param>
        /// <param name="references">Decides whether to load the related references of the data models as well.</param>
        /// <param name="offset">Specifies the number of data models to bypass.</param>
        /// <param name="count">Specifies the number of data models to return.</param>
        /// <returns>The found data models.</returns>
        IEnumerable<TModel> FindAllByKeys(IEnumerable<TKey> keys, bool references = true, int? offset = null, int? count = null);

        /// <summary>
        /// Finds all data models that satisfy the given predicate and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="count"/>.
        /// </summary>
        /// <param name="predicate">The condition that when evaluated to true, returns the found data models.</param>
        /// <param name="references">Decides whether to load the related references of the data models as well.</param>
        /// <param name="offset">Specifies the number of data models to bypass.</param>
        /// <param name="count">Specifies the number of data models to return.</param>
        /// <returns>The found data models; otherwise an empty sequence.</returns>
        IEnumerable<TModel> FindAll(Expression<Func<TModel, bool>> predicate, bool references = true, int ? offset = null, int? count = null);

        /// <summary>
        /// Gets all the data models from the data store and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="count"/>.
        /// </summary>
        /// <param name="references">Decides whether to load the related references of the data model as well.</param>
        /// <param name="offset">Specifies the number of data models to bypass.</param>
        /// <param name="count">Specifies the number of data models to return.</param>
        /// <returns>All data models in data store.</returns>
        IEnumerable<TModel> Get(bool references = true, int ? offset = null, int? count = null);

        /// <summary>
        /// Finds a data model asynchronously in the data store that is specified by the given key.
        /// </summary>
        /// <param name="key">The unique identifier of the data model.</param>
        /// <param name="references">Decides whether to load the related references of the data model as well.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise that returns the found data model; otherwise a default value of the data model.</returns>
        Task<TModel> FindByKeyAsync(TKey key, bool references = true, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Finds a data model that asynchronously satisfies the given predicate.
        /// </summary>
        /// <param name="predicate">The condition that when evaluated to true, returns the found data model.</param>
        /// <param name="references">Decides whether to load the related references of the data model as well.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise that returns the found data model; otherwise a default value of the data model.</returns>
        Task<TModel> FindAsync(Expression<Func<TModel, bool>> predicate, bool references = true, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Finds data models asynchronously that are specified by the given keys and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="count"/>.
        /// </summary>
        /// <param name="keys">The keys of the data models to search for.</param>
        /// <param name="references">Decides whether to load the related references of the data models as well.</param>
        /// <param name="offset">Specifies the number of data models to bypass.</param>
        /// <param name="count">Specifies the number of data models to return.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise that returns found data models; otherwise the default values of the data models.</returns>
        Task<IEnumerable<TModel>> FindAllByKeysAsync(IEnumerable<TKey> keys, bool references = true, int? offset = null, int? count = null, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Find all data models asynchronously that satisfy the given predicate and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="count"/>.
        /// </summary>
        /// <param name="predicate">The condition that when evaluated to true, returns the found data model.</param>
        /// <param name="references">Decides whether to load the related references of the data models as well.</param>
        /// <param name="offset">Specifies the number of data models to bypass.</param>
        /// <param name="count">Specifies the number of data models to return.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise that returns found data models; otherwise the default values of the data models.</returns>
        Task<IEnumerable<TModel>> FindAllAsync(Expression<Func<TModel, bool>> predicate, bool references = true, int? offset = null, int? count = null, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets all the data models asynchronously from the data store and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="count"/>.
        /// </summary>
        /// <param name="offset">Specifies the number of data models to bypass.</param>
        /// <param name="references">Decides whether to load the related references of the data models as well.</param>
        /// <param name="count">Specifies the number of data models to return.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise that returns retrieved data models; otherwise the default values of the data models.</returns>
        Task<IEnumerable<TModel>> GetAsync(int? offset = null, bool references = true, int? count = null, CancellationToken token = default(CancellationToken));    }
}
