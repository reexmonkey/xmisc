using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts
{

    /// <summary>
    /// Specifies an interface that queries a data store for a single data model or primary key in an asynchronous operation.
    /// </summary>
    /// <typeparam name="TKey">The type of the key that uniquely identifies a data model.</typeparam>
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