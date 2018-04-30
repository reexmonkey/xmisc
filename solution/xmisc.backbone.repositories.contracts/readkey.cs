using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies a repository that queries a data store for keys that uniquely identify data models in the store.
    /// </summary>
    /// <typeparam name="TKey">The type of the key that uniquely identifies a model.</typeparam>
    /// <typeparam name="TModel">The type of data model.</typeparam>
    public interface IReadKeyRepository<TKey, TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {

        /// <summary>
        /// Retrieves the keys of data models from the data store.
        /// <para /> Optionally the
        /// </summary>
        /// <param name="offset">Ignores the specified number of keys.</param>
        /// <param name="count">Returns the the specified number of keys.</param>
        /// <returns>The number of key of data models from the serach that may optionally have been filtered.</returns>
        IEnumerable<TKey> GetKeys(int? offset = null, int? count = null);

        /// <summary>
        /// Retrieves asynchronously the keys of data models from the data store.
        /// <para /> Optionally the
        /// </summary>
        /// <param name="offset">Ignores the specified number of keys.</param>
        /// <param name="count">Returns the the specified number of keys.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>The number of key of data models from the serach that may optionally have been filtered.</returns>
        Task<IEnumerable<TKey>> GetKeysAsync(int? offset = null, int? count = null, CancellationToken token = default(CancellationToken));
    }
}
