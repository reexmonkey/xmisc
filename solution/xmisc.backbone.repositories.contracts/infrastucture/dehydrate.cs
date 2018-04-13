using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts.infrastucture
{
    /// <summary>
    /// Specifies a repository that reduces a data model to its minimal form.
    /// </summary>
    /// <typeparam name="TModel">The type of the data model that is reduced to its minmal form.</typeparam>
    public interface IDehydrateRepository<TModel>
    {
        /// <summary>
        /// Removes information from the specified data model.
        /// </summary>
        /// <param name="model">The model to dehydrate.</param>
        /// <returns>The dehydrated data model.</returns>
        TModel Dehydrate(TModel model);

        /// <summary>
        /// Removes information from the specified data models.
        /// </summary>
        /// <param name="models">The models to dehydrate.</param>
        /// <returns>The dehydrated data models.</returns>
        IEnumerable<TModel> DehydrateAll(IEnumerable<TModel> models);

        /// <summary>
        /// Asynchronously removes information from the specified data model.
        /// </summary>
        /// <param name="model">The model to dehydrate.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise that returns the dehydrated data model.</returns>
        Task<TModel> DehydrateAsync(TModel model, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Asynchronously removes information from the specified data models.
        /// </summary>
        /// <param name="models">The models to dehydrate.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise that returns the dehydrated data models.</returns>
        Task<IEnumerable<TModel>> DehydrateAllAsync(IEnumerable<TModel> models, CancellationToken token = default(CancellationToken));
    }
}
