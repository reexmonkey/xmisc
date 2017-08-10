using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace xmisc.backbone.repositories.contracts.foundation
{
    /// <summary>
    /// Specifies a repository that provides means to search for unique values of data models from the data store.
    /// </summary>
    /// <typeparam name="TKey">The type of the key to query.</typeparam>
    public interface IKeyQueryRepository<TKey>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>

    {
        /// <summary>
        /// Retrieves the keys of data models from the data store.
        /// <para /> Optionally the 
        /// </summary>
        /// <param name="offset">Ignores the specified number of keys.</param>
        /// <param name="count">Returns the the specified numbers of keys.</param>
        /// <returns>The number of key of data models from the serach that may optionally have been filtered.</returns>
        IEnumerable<TKey> GetKeys(int? offset = null, int? count = null);

        /// <summary>
        /// Determines whether the data store contains the specified key.
        /// </summary>
        /// <param name="key">The key to search for.</param>
        /// <returns>
        ///   <c>true</c> if the specified key contains key; otherwise, <c>false</c>.
        /// </returns>
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
        /// Asynchronously determines whether the data store contains the specified key.
        /// </summary>
        /// <param name="key">TThe key to search for.</param>
        /// <returns>
        ///   <c>true</c> if the specified key contains key; otherwise, <c>false</c>.
        /// </returns>
        Task<bool> ContainsKeyAsync(TKey key);

        /// <summary>
        /// Asynchropnously determines whether the data store contains the specified keys.
        /// </summary>
        /// <param name="keys">The keys to search for.</param>
        /// <param name="strict">Specifies whether the search is successful if and only if all of the keys have been found.
        /// True if all the keys must be found; otherwise false if only some of the keys have been found.</param>
        /// <returns>
        ///   <c>true</c> if the specified keys contains keys; otherwise, <c>false</c>.
        /// </returns>
        Task<bool> ContainsKeysAsync(IEnumerable<TKey> keys, bool strict = true);

        /// <summary>
        /// Asynchronously retrieves the keys of data models from the data store.
        /// <para /> Optionally the 
        /// </summary>
        /// <param name="offset">Ignores the specified number of keys.</param>
        /// <param name="count">Returns the the specified numbers of keys.</param>
        /// <returns>The number of key of data models from the serach that may optionally have been filtered.</returns>

        Task<IEnumerable<TKey>> GetKeysAsync(int? offset = null, int? count = null);


    }
}
