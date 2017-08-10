using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace xmisc.backbone.repositories.contracts.foundation
{
    /// <summary>
    /// Specifies a repository that restores access to excluded data models in the data store.
    /// </summary>
    /// <typeparam name="TKey">The type of key that identifies the model to restore.</typeparam>
    /// <typeparam name="TModel">The type of model to delete.</typeparam>
    public interface IModelRecoveryRepository<in TKey, in TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>

    {
        /// <summary>
        /// Recovers a data model that is specified by the provided <paramref name="key"/> from the data store.
        /// </summary>
        /// <param name="key">The identifier that specifies the data model to exclude.</param>
        void RecoverByKey(TKey key);

        /// <summary>
        /// Recovers data models that are specified by the provided <paramref name="keys"/> from the data store.
        /// </summary>
        /// <param name="keys">The identifiers that specify the data models to exclude.</param>
        void RecoverAllByKeys(IEnumerable<TKey> keys);

        /// <summary>
        /// Recovers the specified data model from the data store.
        /// </summary>
        /// <param name="model">The data model in the data store to exclude.</param>
        void Recover(TModel model);

        /// <summary>
        /// Recovers the specified data models from the data store.
        /// </summary>
        /// <param name="models">The data models in the data store to exclude.</param>
        void RecoverAll(IEnumerable<TModel> models);

        /// <summary>
        /// Asynchronously forgets a data model that is specified by the provided <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The identifier that specifies the data model to exclude.</param>
        /// <returns>A task that represents an asynchronous operation.</returns>
        Task RecoverByKeyAsync(TKey key);

        /// <summary>
        /// Asynchronously forgets data models that are specified by the provided <paramref name="keys"/>.
        /// </summary>
        /// <param name="keys">The identifiers that specify the data models to exclude.</param>
        /// <returns>A task that represents an asynchronous operation</returns>
        Task RecoverAllByKeysAsync(IEnumerable<TKey> keys);

        /// <summary>
        /// Asynchronously forgets the specified data model from the data store.
        /// </summary>
        /// <param name="model">The data model in the data store to exclude.</param>
        /// <returns>A task that represents an asynchronous operation.</returns>
        Task RecoverAsync(TModel model);

        /// <summary>
        /// Asynchronously forgets the specified data models from the data store.
        /// </summary>
        /// <param name="models">The data models to exclude from the data store.</param>
        /// <returns>A task that represents an asynchronous operation.</returns>
        Task RecoverAllAsync(IEnumerable<TModel> models);
    }

}
