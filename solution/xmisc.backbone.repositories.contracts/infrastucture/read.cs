﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts.infrastucture
{
    /// <summary>
    /// Specifies a repository that queries information about a data model from the data store.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public interface IReadRepository<TKey, TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// Finds a data model in the data store that is specified by the given key.
        /// </summary>
        /// <param name="key">The unique identifier of the data model.</param>
        /// <returns>The found data model; otherwise the default value of the data model.</returns>
        TModel FindByKey(TKey key);

        /// <summary>
        /// Finds a data model that satisfies the given predicate.
        /// </summary>
        /// <param name="predicate">The condition that when evaluated to true, returns the found data model.</param>
        /// <returns>The found data model.</returns>
        TModel Find(Expression<Func<TModel, bool>> predicate);

        /// <summary>
        /// Finds data models that are specified by the given keys and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="count"/>.
        /// </summary>
        /// <param name="keys">The keys of the data models to search for.</param>
        /// <param name="offset">Specifies the number of data models to skip.</param>
        /// <param name="count">Specifies the numbers of data models to return per page.</param>
        /// <returns>The found data models.</returns>
        IEnumerable<TModel> FindByKeysAll(IEnumerable<TKey> keys, int? offset = null, int? count = null);

        /// <summary>
        /// Finds all data models that satisfy the given predicate and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="count"/>.
        /// </summary>
        /// <param name="predicate">The condition that when evaluated to true, returns the found data model.</param>
        /// <param name="offset">Specifies the number of data models to skip.</param>
        /// <param name="count">Specifies the numbers of data models to return per page.</param>
        /// <returns>The found data models.</returns>
        IEnumerable<TModel> FindAll(Expression<Func<TModel, bool>> predicate, int? offset = null, int? count = null);

        /// <summary>
        /// Gets all the data models in the data store and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="count"/>.
        /// </summary>
        /// <param name="offset">Specifies the number of data models to skip.</param>
        /// <param name="count">Specifies the numbers of data models to return per page.</param>
        /// <returns>All data models in data store.</returns>
        IEnumerable<TModel> Get(int? offset = null, int? count = null);

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
        /// Finds asynchronously a data model in the data store that is specified by the given key.
        /// </summary>
        /// <param name="key">The unique identifier of the data model.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise that returns the found data model; otherwise a default value of the data model.</returns>
        Task<TModel> FindByKeyAsync(TKey key, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Finds asynchronously a data model that satisfies the given predicate.
        /// </summary>
        /// <param name="predicate">The condition that when evaluated to true, returns the found data model.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise that returns the found data model; otherwise a default value of the data model.</returns>
        Task<TModel> FindAsync(Expression<Func<TModel, bool>> predicate, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Finds asynchronously data models that are specified by the given keys and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="count"/>.
        /// </summary>
        /// <param name="keys">The keys of the data models to search for.</param>
        /// <param name="offset">Specifies the number of data models to skip.</param>
        /// <param name="count">Specifies the numbers of data models to return per page.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise that returns found data models; otherwise the default values of the data models.</returns>
        Task<IEnumerable<TModel>> FindAllByKeysAsync(IEnumerable<TKey> keys, int? offset = null, int? count = null, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Finds asynchronously all data models that satisfy the given predicate and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="count"/>.
        /// </summary>
        /// <param name="predicate">The condition that when evaluated to true, returns the found data model.</param>
        /// <param name="offset">Specifies the number of data models to skip.</param>
        /// <param name="count">Specifies the numbers of data models to return per page.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise that returns found data models; otherwise the default values of the data models.</returns>
        Task<IEnumerable<TModel>> FindAllAsync(Expression<Func<TModel, bool>> predicate, int? offset = null, int? count = null, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets asynchronously all the data models in the data store and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="count"/>.
        /// </summary>
        /// <param name="offset">Specifies the number of data models to skip.</param>
        /// <param name="count">Specifies the numbers of data models to return per page.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise that returns retrieved data models; otherwise the default values of the data models.</returns>
        Task<IEnumerable<TModel>> GetAsync(int? offset = null, int? count = null, CancellationToken token = default(CancellationToken));

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

        /// <summary>
        /// Retrieves asynchronously the keys of data models from the data store.
        /// <para /> Optionally the
        /// </summary>
        /// <param name="offset">Ignores the specified number of keys.</param>
        /// <param name="count">Returns the the specified numbers of keys.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>The number of key of data models from the serach that may optionally have been filtered.</returns>
        Task<IEnumerable<TKey>> GetKeysAsync(int? offset = null, int? count = null, CancellationToken token = default(CancellationToken));
    }
}
