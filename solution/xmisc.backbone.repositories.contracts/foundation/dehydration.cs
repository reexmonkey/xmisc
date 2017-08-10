using System.Collections.Generic;
using System.Threading.Tasks;

namespace xmisc.backbone.repositories.contracts.foundation
{
    /// <summary>
    /// Specifies a repository that removes relational data store information from a data model.
    /// </summary>
    /// <typeparam name="TModel">The type of the data model whose relational details are to be removed.</typeparam>
    public interface IDehydration<TModel>
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
        /// <returns>The dehydrated data model.</returns>
        Task<TModel> DehydrateAsync(TModel model);

        /// <summary>
        /// Asynchronously removes information from the specified data models.
        /// </summary>
        /// <param name="models">The models to dehydrate.</param>
        /// <returns>The dehydrated data models.</returns>
        Task<IEnumerable<TModel>> DehydrateAllAsync(IEnumerable<TModel> models);
    }
}
