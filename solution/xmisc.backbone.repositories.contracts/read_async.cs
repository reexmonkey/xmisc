using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts
{
#nullable enable

    /// <summary>
    /// Specifies synchronous operations that query a data store for one or more data models.
    /// </summary>
    /// <typeparam name="TKey">The type of the key that uniquely identifies a model.</typeparam>
    /// <typeparam name="TModel">The type of data model.</typeparam>
    public interface IReadRepositoryAsync<TKey, TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// Finds a data model asynchronously in the data store that is specified by the given key.
        /// </summary>
        /// <param name="key">The unique identifier of the data model.</param>
        /// <param name="references">Decides whether to load the related references or details of the data model as well.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>A promise that returns the found data model; otherwise a default value of the data model.</returns>
        Task<TModel?> FindByKeyAsync(TKey key, bool? references = null, CancellationToken cancellation = default);

        /// <summary>
        /// Finds data models asynchronously that are specified by the given keys and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="limit"/>.
        /// </summary>
        /// <param name="keys">The keys of the data models to search for.</param>
        /// <param name="references">Decides whether to load the related references or details of the data models as well.</param>
        /// <param name="offset">Specifies the number of data models to bypass.</param>
        /// <param name="limit">Specifies the number of data models to return.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>A promise that returns found data models; otherwise the default values of the data models.</returns>
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
        /// <returns>A promise that returns found data models; otherwise the default values of the data models.</returns>
        IAsyncEnumerable<TModel> FindAllAsync(Expression<Func<TModel, bool>> predicate, bool? references = null, int? offset = null, int? limit = null, CancellationToken cancellation = default);

        /// <summary>
        /// Gets all the data models asynchronously from the data store and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="limit"/>.
        /// </summary>
        /// <param name="offset">Specifies the number of data models to bypass.</param>
        /// <param name="references">Decides whether to load the related references or details of the data models as well.</param>
        /// <param name="limit">Specifies the number of data models to return.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>A promise that returns retrieved data models; otherwise the default values of the data models.</returns>
        IAsyncEnumerable<TModel> GetAsync(bool? references = null, int? offset = null, int? limit = null, CancellationToken cancellation = default);

        /// <summary>
        /// Retrieves asynchronously the keys of data models from the data store.
        /// <para /> Optionally the
        /// </summary>
        /// <param name="offset">Ignores the specified number of keys.</param>
        /// <param name="limit">Returns the the specified number of keys.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>The number of key of data models from the serach that may optionally have been filtered.</returns>
        IAsyncEnumerable<TKey> GetKeysAsync(int? offset = null, int? limit = null, CancellationToken cancellation = default);

        /// <summary>
        /// Determines asynchronously whether the data store contains the specified key.
        /// </summary>
        /// <param name="key">The key to search for.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>A promise that returns true if the data store contains the key; otherwise false.</returns>
        Task<bool> ContainsKeyAsync(TKey key, CancellationToken cancellation = default);

        /// <summary>
        /// Determines asynchronously whether the data store contains the specified keys.
        /// </summary>
        /// <param name="keys">The keys to search for.</param>
        /// <param name="strict">Specifies whether the search is successful if and only if all of the keys have been found.
        /// True if all the keys must be found; otherwise false if only some of the keys have been found.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns></returns>
        Task<bool> ContainsKeysAsync(IEnumerable<TKey> keys, bool strict = true, CancellationToken cancellation = default);
    }
}