using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies a repository that can register one or more models by assigning each model a unique identifier.
    /// <para/>Also, the repository can unregister one or more models by assigning each model a default identifier.
    /// </summary>
    /// <typeparam name="TKey">The type of unique identifier to assign to each model.</typeparam>
    /// <typeparam name="TModel">The type of model to register or unregister. </typeparam>
    public interface IRegistryRepository<in TKey, in TModel>
        where TKey: IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// Registers the model by assigning a unique identifier to it.
        /// </summary>
        /// <param name="model">The model to register.</param>
        /// <param name="references">Decides to register related references of the model as well.</param>
        void Register(TModel model, bool references = false);

        /// <summary>
        /// Registers the given models by assigning each a unique identifier.
        /// </summary>
        /// <param name="models">The models to register.</param>
        /// <param name="references">Decides to register related references of the models as well.</param>
        void RegisterAll(IEnumerable<TModel> models, bool references = false);

        /// <summary>
        /// Unregisters the model by assigning a default identifier to it.
        /// </summary>
        /// <param name="model">The model to unregister.</param>
        /// <param name="references">Decides to unregister related references of the model as well.</param>
        void Unregister(TModel model, bool references = false);

        /// <summary>
        /// Unregisters the models by assigning each a unique identifier.
        /// </summary>
        /// <param name="models">The models to unregister.</param>
        /// <param name="references">Decides to unregister related references of the models as well.</param>
        void UnregisterAll(IEnumerable<TModel> models, bool references = false);

        /// <summary>
        /// Registers the model asynchronously by assigning a unique identifier to it.
        /// </summary>
        /// <param name="model">The model to register.</param>
        /// <param name="references">Decides to unregister related references of the model as well.</param>
        /// <returns>The promise to register the model.</returns>
        Task RegisterAsync(TModel model, bool references = false);

        /// <summary>
        /// Registers the given models asynchronously by assigning each a unique identifier.
        /// </summary>
        /// <param name="models">The models to register.</param>
        /// <param name="references">Decides to register related references of the model as well.</param>
        /// <param name="token">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>The promise to register the models.</returns>
        Task RegisterAllAsync(IEnumerable<TModel> models, bool references = false, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Unregisters the model asynchronously by assigning a default identifier to it.
        /// </summary>
        /// <param name="model">The model to unregister.</param>
        /// <param name="references">Decides to unregister related references of the model as well.</param>
        /// <returns>The promise to unregister the models.</returns>
        Task UnregisterAsync(TModel model, bool references = false);

        /// <summary>
        /// Unregisters the given models asynchronously by assigning each a default identifier.
        /// </summary>
        /// <param name="models">The models to register.</param>
        /// <param name="references">Decides to unregister related references of the models as well.</param>
        /// <param name="token">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>The promise to unregister the models.</returns>
        Task UnregisterAllAsync(IEnumerable<TModel> models, bool references = false, CancellationToken token = default(CancellationToken));
    }
}
