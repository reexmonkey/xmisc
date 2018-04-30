using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies a repository that checks the data store for the existence of specified attributes 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TModel"></typeparam>
    public interface ICheckRepository<TKey, TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// Determines whether the data store contains the specified key.
        /// </summary>
        /// <param name="key">The key to search for.</param>
        /// <returns>True if the data store contains the key; otherwise false.</returns>
        bool ContainsKey(TKey key);

        /// <summary>
        /// Determines whether the data store contains the specified keys.
        /// </summary>
        /// <param name="keys">The keys to search for.</param>
        /// <param name="strict">Specifies whether the search is successful if and only if all of the keys have been found.
        /// True if all the keys must be found; otherwise false if only some of the keys have been found.</param>
        /// <returns>
        ///   <c>true</c> if the specified keys contains keys; otherwise, <c>false</c>.
        /// </returns>
        bool ContainsKeys(IEnumerable<TKey> keys, bool strict = true);

        /// <summary>
        /// Determines asynchronously whether the data store contains the specified key.
        /// </summary>
        /// <param name="key">The key to search for.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise that returns true if the data store contains the key; otherwise false.</returns>
        Task<bool> ContainsKeyAsync(TKey key, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Determines asynchronously whether the data store contains the specified keys.
        /// </summary>
        /// <param name="keys">The keys to search for.</param>
        /// <param name="strict">Specifies whether the search is successful if and only if all of the keys have been found.
        /// True if all the keys must be found; otherwise false if only some of the keys have been found.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns></returns>
        Task<bool> ContainsKeysAsync(IEnumerable<TKey> keys, bool strict = true, CancellationToken token = default(CancellationToken));
    }
}
