using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies an interface that asynchronously queries a data store for many data models and returns the results
    /// as they are available.
    /// </summary>
    /// <typeparam name="TKey">The type of the key that uniquely identifies a model.</typeparam>
    /// <typeparam name="TModel">The type of data model.</typeparam>
    public interface IAsyncEnumerableReadRepositoryAsync<TKey, TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// Finds data models asynchronously that are specified by the given keys and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="limit"/>.
        /// </summary>
        /// <param name="keys">The keys of the data models to search for.</param>
        /// <param name="references">Decides whether to load the related references or details of the data models as well.</param>
        /// <param name="offset">Specifies the number of data models to bypass.</param>
        /// <param name="limit">Specifies the number of data models to return.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>An enumerator that provides asynchronous iteration over the found models; otherwise an empty enumerator.</returns>
        IAsyncEnumerable<TModel> FindAllByKeysAsync(IEnumerable<TKey> keys, bool? references = null, int? offset = null, int? limit = null, CancellationToken cancellation = default);

        /// <summary>
        /// Find all data models asynchronously that satisfy the given predicate and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="limit"/>.
        /// </summary>
        /// <param name="predicate">The condition that when evaluated to true, returns the found data model.</param>
        /// <param name="references">Decides whether to load the related references or details of the data models as well.</param>
        /// <param name="offset">Specifies the number of data models to bypass.</param>
        /// <param name="limit">Specifies the number of data models to return.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>An enumerator that provides asynchronous iteration over the found models; otherwise an empty enumerator.</returns>
        IAsyncEnumerable<TModel> FindAllAsync(Expression<Func<TModel, bool>> predicate, bool? references = null, int? offset = null, int? limit = null, CancellationToken cancellation = default);

        /// <summary>
        /// Gets all the data models asynchronously from the data store and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="limit"/>.
        /// </summary>
        /// <param name="offset">Specifies the number of data models to bypass.</param>
        /// <param name="references">Decides whether to load the related references or details of the data models as well.</param>
        /// <param name="limit">Specifies the number of data models to return.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>An enumerator that provides asynchronous iteration over the found models; otherwise an empty enumerator.</returns>
        IAsyncEnumerable<TModel> GetAsync(bool? references = null, int? offset = null, int? limit = null, CancellationToken cancellation = default);

        /// <summary>
        /// Retrieves asynchronously the keys of data models from the data store.
        /// <para /> Optionally the
        /// </summary>
        /// <param name="offset">Ignores the specified number of keys.</param>
        /// <param name="limit">Returns the the specified number of keys.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>An enumerator that provides asynchronous iteration over the found keys; otherwise an empty enumerator.</returns>
        IAsyncEnumerable<TKey> GetKeysAsync(int? offset = null, int? limit = null, CancellationToken cancellation = default);
    }
}