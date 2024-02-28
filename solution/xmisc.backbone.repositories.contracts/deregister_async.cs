using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies asynchronous operations to deregister one or more models by assigning each model a default identifier.
    /// </summary>
    /// <typeparam name="TKey">The type of default identifier to assign to each model.</typeparam>
    /// <typeparam name="TModel">The type of model to register or deregister. </typeparam>
    public interface IDeregisterRepositoryAsync<in TKey, in TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {

        /// <summary>
        /// Deregisters the model asynchronously by assigning a default identifier to it.
        /// </summary>
        /// <param name="model">The model to deregister.</param>
        /// <param name="references">Decides to deregister related references or details of the model as well.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>The promise to deregister the models.</returns>
        Task DeregisterAsync(TModel model, bool? references = null, CancellationToken cancellation = default);

        /// <summary>
        /// Deregisters the given models asynchronously by assigning each a default identifier.
        /// </summary>
        /// <param name="models">The models to register.</param>
        /// <param name="references">Decides to deregister related references or details of the models as well.</param>
        /// <param name="offset">How many models to skip.</param>
        /// <param name="limit">How many models to return.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>The promise to deregister the models.</returns>
        Task DeregisterAllAsync(IEnumerable<TModel> models, bool? references = null, int? offset = null, int? limit = null, CancellationToken cancellation = default);
    }
}