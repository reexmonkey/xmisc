using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts.infrastucture
{
    /// <summary>
    /// Specifies a repository that expands ("hydrates") a data model with information from a data store.
    /// </summary>
    /// <typeparam name="TModel">The type of the model to expand with information.</typeparam>
    public interface IHydrateRepository<TModel>
    {
        /// <summary>
        /// Fills the specified model with data from the data store.
        /// </summary>
        /// <param name="model">The model to hydrate.</param>
        /// <returns>The hydrated data model.</returns>
        TModel Hydrate(TModel model);

        /// <summary>
        /// Fills the specified models with data from the data store.
        /// </summary>
        /// <param name="models">The models to hydrate.</param>
        /// <returns>Hydrated data models.</returns>
        IEnumerable<TModel> HydrateAll(IEnumerable<TModel> models);


        /// <summary>
        /// Asynchronously fills the specified model with data from the data store.
        /// </summary>
        /// <param name="model">The model to hydrate.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param> 
        /// <returns>A promise of the hydrated data model.</returns>
        Task<TModel> HydrateAsync(TModel model, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Asynchronously fills the specified models with data from the data store.
        /// </summary>
        /// <param name="models">The models to hydrate.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param> 
        /// <returns>A promise of hydrated data models.</returns>
        Task<IEnumerable<TModel>> HydrateAllAsync(IEnumerable<TModel> models, CancellationToken token = default(CancellationToken));

    }
}
