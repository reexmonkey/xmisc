using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies asynchronous operations that populate one or more data model with references or details from a data store.
    /// </summary>
    /// <typeparam name="TKey">The type of unique key to identify the data model.</typeparam>
    /// <typeparam name="TModel">The type of data model to hydrate.</typeparam>
    public interface IPopulateRepositoryAsync<TKey, TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {

        /// <summary>
        /// Populates the specified data model with references or details in an asynchronous operation.
        /// </summary>
        /// <param name="model">The data model to hydrate.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>A promise to populate the specified data model with references or details.</returns>
        Task PopulateAsync(TModel model, CancellationToken cancellation = default);

        /// <summary>
        /// Populates all of the specified data model with references or details in an asynchronous operation.
        /// </summary>
        /// <param name="models">The data models to hydrate.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>A promise to populate the specified data models with references or details..</returns>
        Task PopulateAllAsync(IEnumerable<TModel> models, CancellationToken cancellation = default);
    }
}