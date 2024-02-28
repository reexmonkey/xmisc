using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies asynchronous operations that register one or more models by assigning each model a unique identifier.
    /// </summary>
    /// <typeparam name="TKey">The type of unique identifier to assign to each model.</typeparam>
    /// <typeparam name="TModel">The type of model to register or deregister. </typeparam>
    public interface IRegistryRepository<in TKey, in TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {

        /// <summary>
        /// Registers the model asynchronously by assigning a unique identifier to it.
        /// </summary>
        /// <param name="model">The model to register.</param>
        /// <param name="references">Decides to deregister related references or details of the model as well.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>The promise to register the model.</returns>
        Task RegisterAsync(TModel model, bool? references = null, CancellationToken cancellation = default);

        /// <summary>
        /// Registers the given models asynchronously by assigning each a unique identifier.
        /// </summary>
        /// <param name="models">The models to register.</param>
        /// <param name="references">Decides to register related references or details of the model as well.</param>
        /// <param name="offset">How many models to skip.</param>
        /// <param name="limit">How many models to return.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>The promise to register the models.</returns>
        Task RegisterAllAsync(IEnumerable<TModel> models, bool? references = null, int? offset = null, int? limit = null, CancellationToken cancellation = default);
    }
}