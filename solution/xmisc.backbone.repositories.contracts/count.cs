using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies a repository that counts the data models of a data store.
    /// </summary>
    /// <typeparam name="TModel">The type of data model to count.</typeparam>
    public interface ICountRepository<TModel>
    {
        /// <summary>
        /// Counts all data models in the data store.
        /// </summary>
        /// <returns>The total number of data models in the data store.</returns>
        long Count();

        /// <summary>
        /// Counts the specified data models in the data store that satisfy the given predicate.
        /// </summary>
        /// <param name="predicate">The condition for a data model that when evaluated to true, includes the data model in the count.</param>
        /// <returns>The total number of data models that satisfy the given predicate.</returns>
        long Count(Expression<Func<TModel, bool>> predicate);

        /// <summary>
        /// Asynchronously counts all data models in the data store.
        /// </summary>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>The total number of data models in the data store.</returns>
        Task<long> CountAsync(CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Asynchronously counts the specified data models in the data store that satisfy the given predicate.
        /// </summary>
        /// <param name="predicate">The condition for a data model that when evaluated to true, includes the data model in the count.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>The total number of data models that satisfy the given predicate.</returns>
        Task<long> CountAsync(Expression<Func<TModel, bool>> predicate, CancellationToken token = default(CancellationToken));
    }
}
