using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using xmisc.backbone.repositories.contracts.infrastucture;

namespace xmisc.backbone.repositories.contracts.foundation
{
    /// <summary>
    /// Specifies a repository that queries information about a data model from the data store.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public interface IModelQueryRepository<in TKey, TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        where TModel : IForgettable
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
        TModel Find(Func<TModel, bool> predicate);

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
        IEnumerable<TModel> FindAll(Func<TModel, bool> predicate, int? offset = null, int? count = null);

        /// <summary>
        /// Gets all the data models in the data store and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="count"/>.
        /// </summary>
        /// <param name="offset">Specifies the number of data models to skip.</param>
        /// <param name="count">Specifies the numbers of data models to return per page.</param>
        /// <returns>All data models in data store.</returns>
        IEnumerable<TModel> Get(int? offset = null, int? count = null);

        /// <summary>
        /// Asynchronously finds a data model in the data store that is specified by the given key.
        /// </summary>
        /// <param name="key">The unique identifier of the data model.</param>
        /// <returns>The found data model; otherwise the default value of the data model.</returns>
        Task<TModel> FindByKeyAsync(TKey key);

        /// <summary>
        /// Asynchronously finds a data model that satisfies the given predicate.
        /// </summary>
        /// <param name="predicate">The condition that when evaluated to true, returns the found data model.</param>
        /// <returns>The found data model.</returns>
        Task<TModel> FindAsync(Func<TModel, bool> predicate);

        /// <summary>
        /// Asynchronously finds data models that are specified by the given keys and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="count"/>.
        /// </summary>
        /// <param name="keys">The keys of the data models to search for.</param>
        /// <param name="offset">Specifies the number of data models to skip.</param>
        /// <param name="count">Specifies the numbers of data models to return per page.</param>
        /// <returns>The found data models.</returns>
        Task<IEnumerable<TModel>> FindAllByKeysAsync(IEnumerable<TKey> keys, int? offset = null, int? count = null);

        /// <summary>
        /// Asynchronously finds all data models that satisfy the given predicate and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="count"/>.
        /// </summary>
        /// <param name="predicate">The condition that when evaluated to true, returns the found data model.</param>
        /// <param name="offset">Specifies the number of data models to skip.</param>
        /// <param name="count">Specifies the numbers of data models to return per page.</param>
        /// <returns>The found data models.</returns>
        IEnumerable<Task<TModel>> FindAllAsync(Func<TModel, bool> predicate, int? offset = null, int? count = null);

        /// <summary>
        /// Asynchronously gets all the data models in the data store and optionally paginates the results.
        /// <para/> For pagination to work, a non-null values must be given for <paramref name="offset"/> and <paramref name="count"/>.
        /// </summary>
        /// <param name="offset">Specifies the number of data models to skip.</param>
        /// <param name="count">Specifies the numbers of data models to return per page.</param>
        /// <returns>All data models in data store.</returns>
        Task<IEnumerable<TModel>> GetAsync(int? offset = null, int? count = null);

    }
}
