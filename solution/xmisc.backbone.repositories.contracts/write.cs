namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies synchronous operations that insert or update one or more data models into/in a data store.
    /// </summary>
    /// <typeparam name="TKey">The type of key, which identifies the model to insert or update.</typeparam>
    /// <typeparam name="TModel">The type of model to persist.</typeparam>
    public interface IWriteRepository<in TKey, in TModel>
        where TKey : IEquatable<TKey>

    {
        /// <summary>
        /// Inserts or updates the specified model to a data store.
        /// </summary>
        /// <param name="model">The model to persist to the data store.</param>
        /// <param name="references">Should the references or details of the model also be saved?</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>True if the model was inserted in the data store; otherwise false if it was updated.</returns>
        bool Save(TModel model, bool? references = true, CancellationToken cancellation = default);

        /// <summary>
        /// Inserts or updates the specified models to a data store.
        /// </summary>
        /// <param name="models">The models to persist to the data store.</param>
        /// <param name="references">Should the references or details of the models also be saved?</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>The number of models added to the data store.</returns>
        long SaveAll(IEnumerable<TModel> models, bool? references = true, CancellationToken cancellation = default);
    }
}
