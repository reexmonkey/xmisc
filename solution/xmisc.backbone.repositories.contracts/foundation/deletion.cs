using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using xmisc.backbone.repositories.contracts.infrastucture;

namespace xmisc.backbone.repositories.contracts.foundation
{
    /// <summary>
    /// Specifies a repository that marks data models for deletion by excluding access to them. 
    /// </summary>
    /// <typeparam name="TKey">The type of key that identifies the model to delete.</typeparam>
    /// <typeparam name="TModel">The type of model to delete.</typeparam>
    public interface IModelDeletionRepository<in TKey, in TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        where TModel : IForgettable
    {
        /// <summary>
        /// Excludes access to a data model that is specified by the provided <paramref name="key"/> from the data store.
        /// </summary>
        /// <param name="key">The identifier that specifies the data model to exclude.</param>
        void DeleteByKey(TKey key);

        /// <summary>
        /// Excludes access to data models that are specified by the provided <paramref name="keys"/> from the data store.
        /// </summary>
        /// <param name="keys">The identifiers that specify the data models to exclude.</param>
        void DeleteAllByKeys(IEnumerable<TKey> keys);

        /// <summary>
        /// Excludes access to the specified data model from the data store.
        /// </summary>
        /// <param name="model">The data model in the data store to exclude.</param>
        void Delete(TModel model);

        /// <summary>
        /// Excludes access to the specified data models from the data store.
        /// </summary>
        /// <param name="models">The data models in the data store to exclude.</param>
        void DeleteAll(IEnumerable<TModel> models);

        /// <summary>
        /// Asynchronously forgets a data model that is specified by the provided <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The identifier that specifies the data model to exclude.</param>
        /// <returns>A task that represents an asynchronous operation.</returns>
        Task DeleteByKeyAsync(TKey key);

        /// <summary>
        /// Asynchronously forgets data models that are specified by the provided <paramref name="keys"/>.
        /// </summary>
        /// <param name="keys">The identifiers that specify the data models to exclude.</param>
        /// <returns>A task that represents an asynchronous operation</returns>
        Task DeleteAllByKeysAsync(IEnumerable<TKey> keys);

        /// <summary>
        /// Asynchronously forgets the specified data model from the datastore.
        /// </summary>
        /// <param name="model">The data model in the data store to exclude.</param>
        /// <returns>A task that represents an asynchronous operation.</returns>
        Task DeleteAsync(TModel model);

        /// <summary>
        /// Asynchronously forgets the specified data models from the data store.
        /// </summary>
        /// <param name="models">The data models to exclude from the data store.</param>
        /// <returns>A task that represents an asynchronous operation.</returns>
        Task DeleteAllAsync(IEnumerable<TModel> models);
    }
}
