namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies asynchronous operations that insert or update models into/in a data store.
    /// </summary>
    /// <typeparam name="TKey">The type of key, which identifies the model to insert or update.</typeparam>
    /// <typeparam name="TModel">The type of model to persist.</typeparam>
    public interface IWriteRepositoryAsync<in TKey, in TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>

    {
        /// <summary>
        /// Inserts or updates the specified model asynchronously to a data store.
        /// </summary>
        /// <param name="model">The model to persist to the data store.</param>
        /// <param name="references">Should the references or details of the model also be saved?</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>A promise to return true if the model was inserted in the data store; otherwise false if it was updated.</returns>
        Task<bool> SaveAsync(TModel model, bool? references = true, CancellationToken cancellation = default);

        /// <summary>
        /// Inserts or updates the specified models asynchronously to a data store.
        /// </summary>
        /// <param name="models">The models to persist to the data store.</param>
        /// <param name="references">Should the references or details of the model also be saved?</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>A promise to return the number of models persisted to the data store.</returns>
        Task<long> SaveAllAsync(IEnumerable<TModel> models, bool? references = true, CancellationToken cancellation = default);
    }
}
