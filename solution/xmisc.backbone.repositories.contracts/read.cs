using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    #nullable enable

    /// <summary>
    /// Specifies a repository that queries a data store for data models of the specified type <typeparamref name="TModel"/>.
    /// </summary>
    /// <typeparam name="TKey">The type of the key that uniquely identifies a model.</typeparam>
    /// <typeparam name="TModel">The type of data model.</typeparam>
    public interface IReadRepository<TKey, TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// Finds a data model in the data store that is specified by a unique identifier or an evaluated condition.
        /// </summary>
        /// <param name="key">The key that uniquely identifies the data model.</param>
        /// <param name="references">Decides whether to load the related references of the data model as well.</param>
        /// <returns>The found data model; otherwise the default value of the data model.</returns>
        TModel? FindByKey(TKey key, bool? references = null);

        /// <summary>
        /// Finds data models that are specified by the given keys and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="count"/>.
        /// </summary>
        /// <param name="keys">The keys of the data models to search for.</param>
        /// <param name="references">Decides whether to load the related references of the data models as well.</param>
        /// <param name="offset">Specifies the number of data models to bypass.</param>
        /// <param name="count">Specifies the number of data models to return.</param>
        /// <returns>The found data models.</returns>
        List<TModel> FindAllByKeys(IEnumerable<TKey> keys, bool? references = null, int? offset = null, int? count = null);

        /// <summary>
        /// Finds all data models that satisfy the given predicate and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="count"/>.
        /// </summary>
        /// <param name="predicate">The condition that when evaluated to true, returns the found data models.</param>
        /// <param name="references">Decides whether to load the related references of the data models as well.</param>
        /// <param name="offset">Specifies the number of data models to bypass.</param>
        /// <param name="count">Specifies the number of data models to return.</param>
        /// <returns>The found data models; otherwise an empty sequence.</returns>
        List<TModel> FindAll(Expression<Func<TModel, bool>> predicate, bool? references = null, int? offset = null, int? count = null);

        /// <summary>
        /// Gets all the data models from the data store and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="count"/>.
        /// </summary>
        /// <param name="references">Decides whether to load the related references of the data model as well.</param>
        /// <param name="offset">Specifies the number of data models to bypass.</param>
        /// <param name="count">Specifies the number of data models to return.</param>
        /// <returns>All data models in data store.</returns>
        List<TModel> Get(bool? references = null, int? offset = null, int? count = null);

        /// <summary>
        /// Finds a data model asynchronously in the data store that is specified by the given key.
        /// </summary>
        /// <param name="key">The unique identifier of the data model.</param>
        /// <param name="references">Decides whether to load the related references of the data model as well.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise that returns the found data model; otherwise a default value of the data model.</returns>
        Task<TModel?> FindByKeyAsync(TKey key, bool? references = null, CancellationToken token = default);

        /// <summary>
        /// Finds data models asynchronously that are specified by the given keys and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="count"/>.
        /// </summary>
        /// <param name="keys">The keys of the data models to search for.</param>
        /// <param name="references">Decides whether to load the related references of the data models as well.</param>
        /// <param name="offset">Specifies the number of data models to bypass.</param>
        /// <param name="count">Specifies the number of data models to return.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise that returns found data models; otherwise the default values of the data models.</returns>
        Task<List<TModel>> FindAllByKeysAsync(IEnumerable<TKey> keys, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default);

        /// <summary>
        /// Find all data models asynchronously that satisfy the given predicate and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="count"/>.
        /// </summary>
        /// <param name="predicate">The condition that when evaluated to true, returns the found data model.</param>
        /// <param name="references">Decides whether to load the related references of the data models as well.</param>
        /// <param name="offset">Specifies the number of data models to bypass.</param>
        /// <param name="count">Specifies the number of data models to return.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise that returns found data models; otherwise the default values of the data models.</returns>
        Task<List<TModel>> FindAllAsync(Expression<Func<TModel, bool>> predicate, bool? references = null, int? offset = null, int? count = null, CancellationToken token = default);

        /// <summary>
        /// Gets all the data models asynchronously from the data store and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="count"/>.
        /// </summary>
        /// <param name="offset">Specifies the number of data models to bypass.</param>
        /// <param name="references">Decides whether to load the related references of the data models as well.</param>
        /// <param name="count">Specifies the number of data models to return.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise that returns retrieved data models; otherwise the default values of the data models.</returns>
        Task<List<TModel>> GetAsync(bool? references = null, int? offset = null, int? count = null, CancellationToken token = default);

        /// <summary>
        /// Retrieves the keys of data models from the data store.
        /// <para /> Optionally the
        /// </summary>
        /// <param name="offset">Ignores the specified number of keys.</param>
        /// <param name="count">Returns the the specified number of keys.</param>
        /// <returns>The number of key of data models from the serach that may optionally have been filtered.</returns>
        List<TKey> GetKeys(int? offset = null, int? count = null);

        /// <summary>
        /// Retrieves asynchronously the keys of data models from the data store.
        /// <para /> Optionally the
        /// </summary>
        /// <param name="offset">Ignores the specified number of keys.</param>
        /// <param name="count">Returns the the specified number of keys.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>The number of key of data models from the serach that may optionally have been filtered.</returns>
        Task<List<TKey>> GetKeysAsync(int? offset = null, int? count = null, CancellationToken token = default);

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
        Task<bool> ContainsKeyAsync(TKey key, CancellationToken token = default);

        /// <summary>
        /// Determines asynchronously whether the data store contains the specified keys.
        /// </summary>
        /// <param name="keys">The keys to search for.</param>
        /// <param name="strict">Specifies whether the search is successful if and only if all of the keys have been found.
        /// True if all the keys must be found; otherwise false if only some of the keys have been found.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns></returns>
        Task<bool> ContainsKeysAsync(IEnumerable<TKey> keys, bool strict = true, CancellationToken token = default);
    }
}