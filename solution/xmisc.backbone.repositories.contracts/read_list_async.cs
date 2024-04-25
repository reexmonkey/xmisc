using System.Linq.Expressions;

namespace reexmonkey.xmisc.backbone.repositories.contracts
{

    /// <summary>
    /// Specifies an interface that asynchronously queries a data store for many data models and returns a list of results when they are all available.
    /// </summary>
    /// <typeparam name="TKey">The type of the key that uniquely identifies a model.</typeparam>
    /// <typeparam name="TModel">The type of data model.</typeparam>
    public interface IListReadRepositoryAsync<TKey, TModel>
        where TKey : IEquatable<TKey>
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
        /// <returns>A promise that returns found data models; otherwise the default values of the data models.</returns>
        Task<List<TModel>> FindAllByKeysAsync(IEnumerable<TKey> keys, bool? references = null, int? offset = null, int? limit = null, CancellationToken cancellation = default);

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
        Task<List<TModel>> FindAllAsync(Expression<Func<TModel, bool>> predicate, bool? references = null, int? offset = null, int? limit = null, CancellationToken cancellation = default);

        /// <summary>
        /// Gets all the data models asynchronously from the data store and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="limit"/>.
        /// </summary>
        /// <param name="offset">Specifies the number of data models to bypass.</param>
        /// <param name="references">Decides whether to load the related references or details of the data models as well.</param>
        /// <param name="limit">Specifies the number of data models to return.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>A promise that returns retrieved data models; otherwise the default values of the data models.</returns>
        Task<List<TModel>> GetAsync(bool? references = null, int? offset = null, int? limit = null, CancellationToken cancellation = default);

        /// <summary>
        /// Retrieves asynchronously the keys of data models from the data store.
        /// <para /> Optionally the
        /// </summary>
        /// <param name="offset">Ignores the specified number of keys.</param>
        /// <param name="limit">Returns the the specified number of keys.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>The number of key of data models from the serach that may optionally have been filtered.</returns>
        Task<List<TKey>> GetKeysAsync(int? offset = null, int? limit = null, CancellationToken cancellation = default);
    }
}