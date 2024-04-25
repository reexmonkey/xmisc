using System.Linq.Expressions;

namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies an interface that queries a data store for one or more data in a sychronous operation.
    /// </summary>
    /// <typeparam name="TKey">The type of the key that uniquely identifies a model.</typeparam>
    /// <typeparam name="TModel">The type of data model.</typeparam>
    public interface IReadRepository<TKey, TModel>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Finds a data model in the data store that is specified by a unique identifier or an evaluated condition.
        /// </summary>
        /// <param name="key">The key that uniquely identifies the data model.</param>
        /// <param name="references">Decides whether to load the related references or details of the data model as well.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>The found data model; otherwise the default value of the data model.</returns>
        TModel? FindByKey(TKey key, bool? references = null, CancellationToken cancellation = default);

        /// <summary>
        /// Finds data models that are specified by the given keys and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="limit"/>.
        /// </summary>
        /// <param name="keys">The keys of the data models to search for.</param>
        /// <param name="references">Decides whether to load the related references or details of the data models as well.</param>
        /// <param name="offset">Specifies the number of data models to bypass.</param>
        /// <param name="limit">Specifies the number of data models to return.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>The found data models.</returns>
        List<TModel> FindAllByKeys(IEnumerable<TKey> keys, bool? references = null, int? offset = null, int? limit = null, CancellationToken cancellation = default);

        /// <summary>
        /// Finds all data models that satisfy the given predicate and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="limit"/>.
        /// </summary>
        /// <param name="predicate">The condition that when evaluated to true, returns the found data models.</param>
        /// <param name="references">Decides whether to load the related references or details of the data models as well.</param>
        /// <param name="offset">Specifies the number of data models to bypass.</param>
        /// <param name="limit">Specifies the number of data models to return.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>The found data models; otherwise an empty sequence.</returns>
        List<TModel> FindAll(Expression<Func<TModel, bool>> predicate, bool? references = null, int? offset = null, int? limit = null, CancellationToken cancellation = default);

        /// <summary>
        /// Gets all the data models from the data store and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="limit"/>.
        /// </summary>
        /// <param name="references">Decides whether to load the related references or details of the data model as well.</param>
        /// <param name="offset">Specifies the number of data models to bypass.</param>
        /// <param name="limit">Specifies the number of data models to return.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>All data models in data store.</returns>
        List<TModel> Get(bool? references = null, int? offset = null, int? limit = null, CancellationToken cancellation = default);

        /// <summary>
        /// Retrieves the keys of data models from the data store.
        /// <para /> Optionally the
        /// </summary>
        /// <param name="offset">Ignores the specified number of keys.</param>
        /// <param name="limit">Returns the the specified number of keys.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>The number of key of data models from the serach that may optionally have been filtered.</returns>
        List<TKey> GetKeys(int? offset = null, int? limit = null, CancellationToken cancellation = default);

        /// <summary>
        /// Determines whether the data store contains the specified key.
        /// </summary>
        /// <param name="key">The key to search for.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>True if the data store contains the key; otherwise false.</returns>
        bool ContainsKey(TKey key, CancellationToken cancellation = default);

        /// <summary>
        /// Determines whether the data store contains the specified keys.
        /// </summary>
        /// <param name="keys">The keys to search for.</param>
        /// <param name="strict">Specifies whether the search is successful if and only if all of the keys have been found.
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// True if all the keys must be found; otherwise false if only some of the keys have been found.</param>
        /// <returns>
        ///   <c>true</c> if the specified keys contains keys; otherwise, <c>false</c>.
        /// </returns>
        bool ContainsKeys(IEnumerable<TKey> keys, bool strict = true, CancellationToken cancellation = default);
    }
}