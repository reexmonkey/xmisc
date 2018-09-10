using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies a repository that returns the samllest or largest unique identifier of data models in a data store.
    /// </summary>
    /// <typeparam name="TKey">The type of the key that uniquely identifies data models in the data store.</typeparam>
    /// <typeparam name="TModel">The type of data model in the data store.</typeparam>
    public interface IMinMaxKeyRepository<TKey, TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// Determines the smallest unique identifier (key) for a set of data models that satisfy the specified predicate.
        /// </summary>
        /// <returns>The smallest unique identifier (key) of data models.</returns>
        TKey MinKey();

        /// <summary>
        /// Determines the smallest unique identifier (key) for a set of data models that satisfy the specified predicate.
        /// </summary>
        /// <param name="predicate">Filters the set of data models for which the smallest unique identifier (key) shall be identified.</param>
        /// <returns>The smallest unique identifier (key) of specified data models.</returns>
        TKey MinKey(Expression<Func<TModel, bool>> predicate);

        /// <summary>
        /// Determines the largest unique identifier (key) for a set of data models that satisfy the specified predicate.
        /// </summary>
        /// <returns>The largest unique identifier (key) of data models.</returns>
        TKey MaxKey();

        /// <summary>
        /// Determines the largest unique identifier (key) for a set of data models that satisfy the specified predicate.
        /// </summary>
        /// <param name="predicate">Filters the set of data models for which the largest unique identifier (key) shall be identified.</param>
        /// <returns>The largest unique identifier (key) of specified data models.</returns>
        TKey MaxKey(Expression<Func<TModel, bool>> predicate);

        /// <summary>
        /// Determines the smallest unique identifier (key) asynchronously for a set of data models that satisfy the specified predicate.
        /// </summary>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to return the smallest unique identifier (key) of data models.</returns>
        Task MinAsync(CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Determines the smallest unique identifier (key) asynchronously for a set of data models that satisfy the specified predicate.
        /// </summary>
        /// <param name="predicate">Filters the set of data models for which the smallest unique identifier (key) shall be identified.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>The smallest unique identifier (key) of specified data models.</returns>
        Task MinAsync(Expression<Func<TModel, bool>> predicate, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Determines the largest unique identifier (key) asynchronously for a set of data models that satisfy the specified predicate.
        /// </summary>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to return the largest unique identifier (key) of data models.</returns>
        Task MaxAsync(CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Determines the largest unique identifier (key) asynchronously for a set of data models that satisfy the specified predicate.
        /// </summary>
        /// <param name="predicate">Filters the set of data models for which the largest unique identifier (key) shall be identified.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>The largest unique identifier (key) of specified data models.</returns>
        Task MaxAsync(Expression<Func<TModel, bool>> predicate, CancellationToken token = default(CancellationToken));
    }
}
