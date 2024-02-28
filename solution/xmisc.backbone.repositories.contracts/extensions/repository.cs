using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace reexmonkey.xmisc.backbone.repositories.contracts.extensions
{
    /// <summary>
    /// Extends the features of repositories.
    /// </summary>
    public static class RepositoryExtensions
    {
        /// <summary>
        /// Finds all data models that satisfy the given predicate and groups them according to the specified key selector.
        /// </summary>
        /// <typeparam name="TKey">The type of key to uniquely identify each data model.</typeparam>
        /// <typeparam name="TModel">The type of data model to search.</typeparam>
        /// <param name="repository">The repository that contains the data model.</param>
        /// <param name="predicate">The condition that when evaluated to true, returns the found data models.</param>
        /// <param name="keySelector">A function to extract the specified key for each data model group.</param>
        /// <param name="references">Decides whether to load the related references or details of the data models as well.</param>
        /// <param name="offset">The number of data models to bypass.</param>
        /// <param name="limit">The number of data models to return.</param>
        /// <returns>A dictionary that contains grouped data models satisfying the <paramref name="predicate"/>.</returns>
        public static IDictionary<TKey, List<TModel>> FindAll<TKey, TModel>(
            this IReadRepository<TKey, TModel> repository,
            Expression<Func<TModel, bool>> predicate,
            Func<TModel, TKey> keySelector,
            bool? references = true,
            int? offset = null,
            int? limit = null)
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            return repository
                .FindAll(predicate, references, offset, limit)
                .GroupBy(keySelector).ToDictionary(g => g.Key, g => g.ToList());
        }

        /// <summary>
        /// Finds all data models asynchronously that satisfy the given predicate and groups them according to the specified key selector.
        /// </summary>
        /// <typeparam name="TKey">The type of key to uniquely identify each data model.</typeparam>
        /// <typeparam name="TModel">The type of data model to search.</typeparam>
        /// <param name="repository">The repository that contains the data model.</param>
        /// <param name="predicate">The condition that when evaluated to true, returns the found data models.</param>
        /// <param name="keySelector">A function to extract the specified key for each data model group.</param>
        /// <param name="references">Decides whether to load the related references or details of the data models as well.</param>
        /// <param name="offset">The number of data models to bypass.</param>
        /// <param name="limit">The number of data models to return.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>A dictionary that contains grouped data models satisfying the <paramref name="predicate"/>.</returns>
        public static async Task<IDictionary<TKey, List<TModel>>> FindAllAsync<TKey, TModel>(
            this IReadRepositoryAsync<TKey, TModel> repository,
            Expression<Func<TModel, bool>> predicate,
            Func<TModel, TKey> keySelector,
            bool? references = true,
            int? offset = null,
            int? limit = null,
            CancellationToken cancellation = default)
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            var matches = repository.FindAllAsync(predicate, references, offset, limit, cancellation);
            var list = new List<TModel>();
            await foreach(var match in matches)
            {
                list.Add(match);
            };
            return list.GroupBy(keySelector).ToDictionary(g => g.Key, g => g.ToList());
        }

        /// <summary>
        /// Creates a transaction scope from the given option details.
        /// </summary>
        /// <param name="scopeOption">Additional options for creating a transaction scope.</param>
        /// <param name="isolation">The isolation level of a transaction.</param>
        /// <param name="timeout">The timeout period for the transaction.</param>
        /// <returns>A transaction scope that makes a block code transactional.</returns>
        public static TransactionScope AsTransactionScope(this TransactionScopeOption scopeOption, IsolationLevel isolation = IsolationLevel.Serializable, TimeSpan? timeout = null)
        {
            var options = new TransactionOptions
            {
                IsolationLevel = isolation,
                Timeout = timeout ?? TransactionManager.DefaultTimeout
            };
            return new TransactionScope(scopeOption, options);
        }

        /// <summary>
        /// Creates a transaction scope flow from the given option details.
        /// <para /> Recommended for transaction code blocks involved in asynchronous operations.
        /// </summary>
        /// <param name="scopeOption">Additional options for creating a transaction scope.</param>
        /// <param name="isolation">The isolation level of a transaction.</param>
        /// <param name="timeout">The timeout period for the transaction.</param>
        /// <returns>A transaction scope that makes a block code transactional.</returns>
        public static TransactionScope AsTransactionScopeFlow(this TransactionScopeOption scopeOption, IsolationLevel isolation = IsolationLevel.Serializable, TimeSpan? timeout = null)
        {
            var options = new TransactionOptions
            {
                IsolationLevel = isolation,
                Timeout = timeout ?? TransactionManager.DefaultTimeout
            };
            return new TransactionScope(scopeOption, options, TransactionScopeAsyncFlowOption.Enabled);
        }

    }
}