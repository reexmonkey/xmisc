using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies a repository that populates a data model with references from a data store.
    /// </summary>
    /// <typeparam name="TKey">The type of unique key to identify the data model.</typeparam>
    /// <typeparam name="TModel">The type of data model to hydrate.</typeparam>
    public interface IHydrateRepository<TKey, TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// Populates the specified data model with references.
        /// </summary>
        /// <param name="model">The data model to hydrate.</param>
        void Hydrate(TModel model);

        /// <summary>
        /// Populates all of the specified data model with references.
        /// </summary>
        /// <param name="models">The data models to hydrate.</param>
        void HydrateAll(IEnumerable<TModel> models);

        /// <summary>
        /// Populates the specified data model with references in an asynchronous operation.
        /// </summary>
        /// <param name="model">The data model to hydrate.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        Task HydrateAsync(TModel model, CancellationToken token = default);

        /// <summary>
        /// Populates all of the specified data model with references in an asynchronous operation.
        /// </summary>
        /// <param name="models">The data models to hydrate.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>Owners of the models in the data store; otherwise an empty collection.</returns>
        Task HydrateAllAsync(IEnumerable<TModel> models, CancellationToken token = default);
    }
}