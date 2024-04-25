namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies synchronous operations that erase one or more data models from a data store.
    /// </summary>
    /// <typeparam name="TKey">The type of key, which identifies the model to erase.</typeparam>
    /// <typeparam name="TModel">The type of model to erase.</typeparam>
    public interface IEraseRepository<in TKey, in TModel>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Erases a data mode that is specified by the provided <paramref name="key"/> from the data store.
        /// </summary>
        /// <param name="key">The identifier that specifies the data model to erase.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>True if the data model was erased; otherwise false.</returns>
        bool EraseByKey(TKey key, CancellationToken cancellation = default);

        /// <summary>
        /// Erases data models that are specified by the provided <paramref name="keys"/> from the data store.
        /// </summary>
        /// <param name="keys">The identifiers that specify the data models to erase.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>The number of data models erased.</returns>
        long EraseAllByKeys(IEnumerable<TKey> keys, CancellationToken cancellation = default);

        /// <summary>
        /// Erases the specified data model from the data store.
        /// </summary>
        /// <param name="model">The data model in the data store to erase.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>True if the data model was erased; otherwise false.</returns>
        bool Erase(TModel model, CancellationToken cancellation = default);

        /// <summary>
        /// Erases the specified data models from the data store.
        /// </summary>
        /// <param name="models">The data models in the data store to erase.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>The number of data models erased.</returns>
        long EraseAll(IEnumerable<TModel> models, CancellationToken cancellation = default);
    }
}