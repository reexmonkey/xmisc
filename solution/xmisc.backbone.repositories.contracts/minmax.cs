using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies a repository that returns the smallest or largest scalar unique identifier (key) of data models in a data store.
    /// </summary>
    /// <typeparam name="TKey">The type of the key that uniquely identifies data models in the data store.</typeparam>
    /// <typeparam name="TModel">The type of data model in the data store.</typeparam>
    public interface IMinMaxKeyRepository<TKey, TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// Determines the smallest scalar unique identifier (key) for a set of data models that satisfy the specified predicate.
        /// </summary>
        /// <returns>The smallest scalar unique identifier (key) of data models.</returns>
        TKey MinKey();

        /// <summary>
        /// Determines the smallest scalar unique identifier (key) for a set of data models that satisfy the specified predicate.
        /// </summary>
        /// <param name="predicate">Filters the set of data models for which the smallest unique identifier (key) shall be identified.</param>
        /// <returns>The smallest scalar unique identifier (key) of specified data models.</returns>
        TKey MinKey(Expression<Func<TModel, bool>> predicate);

        /// <summary>
        /// Determines the largest scalar unique identifier (key) for a set of data models that satisfy the specified predicate.
        /// </summary>
        /// <returns>The largest scalar unique identifier (key) of data models.</returns>
        TKey MaxKey();

        /// <summary>
        /// Determines the largest scalar unique identifier (key) for a set of data models that satisfy the specified predicate.
        /// </summary>
        /// <param name="predicate">Filters the set of data models for which the largest unique identifier (key) shall be identified.</param>
        /// <returns>The largest scalar unique identifier (key) of specified data models.</returns>
        TKey MaxKey(Expression<Func<TModel, bool>> predicate);

        /// <summary>
        /// Determines the smallest scalar unique identifier (key) asynchronously for a set of data models that satisfy the specified predicate.
        /// </summary>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to return the smallest scalar unique identifier (key) of data models.</returns>
        Task<TKey> MinKeyAsync(CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Determines the smallest scalar unique identifier (key) asynchronously for a set of data models that satisfy the specified predicate.
        /// </summary>
        /// <param name="predicate">Filters the set of data models for which the smallest unique identifier (key) shall be identified.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>The smallest unique identifier (key) of specified data models.</returns>
        Task<TKey> MinKeyAsync(Expression<Func<TModel, bool>> predicate, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Determines the largest scalar unique identifier (key) asynchronously for a set of data models that satisfy the specified predicate.
        /// </summary>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to return the largest scalar unique identifier (key) of data models.</returns>
        Task<TKey> MaxKeyAsync(CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Determines the largest scalar unique identifier (key) asynchronously for a set of data models that satisfy the specified predicate.
        /// </summary>
        /// <param name="predicate">Filters the set of data models for which the largest unique identifier (key) shall be identified.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>The largest scalar unique identifier (key) of specified data models.</returns>
        Task<TKey> MaxKeyAsync(Expression<Func<TModel, bool>> predicate, CancellationToken token = default(CancellationToken));
    }
}
