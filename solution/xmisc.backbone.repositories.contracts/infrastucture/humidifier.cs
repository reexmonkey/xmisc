using System.Collections.Generic;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts.infrastucture
{
    /// <summary>
    /// Specifies a repository that fills ("hydrates") a data model with relational information from the data store.
    /// </summary>
    /// <typeparam name="TModel">The type of the model to hydrate.</typeparam>
    public interface IHumidifier<TModel>
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
        /// <returns>The hydrated data model.</returns>
        IEnumerable<TModel> HydrateAll(IEnumerable<TModel> models);


        /// <summary>
        /// Asynchronously fills the specified model with data from the data store.
        /// </summary>
        /// <param name="model">The model to hydrate.</param>
        /// <returns>The hydrated data model.</returns>
        Task<TModel> HydrateAsync(TModel model);

        /// <summary>
        /// Asynchronously fills the specified models with data from the data store.
        /// </summary>
        /// <param name="models">The models to hydrate.</param>
        /// <returns>The hydrated data model.</returns>
        Task<IEnumerable<TModel>> HydrateAllAsync(IEnumerable<TModel> models);

    }
}
