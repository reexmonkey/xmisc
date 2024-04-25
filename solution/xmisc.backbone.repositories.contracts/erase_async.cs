namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies asynchronous operations that erase one or more data models from a data store.
    /// </summary>
    /// <typeparam name="TKey">The type of key, which identifies the model to erase.</typeparam>
    /// <typeparam name="TModel">The type of model to erase.</typeparam>
    public interface IEraseRepositoryAsync<in TKey, in TModel>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Asynchronously erases an data model that is specified by the provided <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The identifier that specifies the data model to erase.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>A promise that returns true if the data model was erased; otherwise false.</returns>
        Task<bool> EraseByKeyAsync(TKey key, CancellationToken cancellation = default);

        /// <summary>
        /// Asynchronously erases data models that are specified by the provided <paramref name="keys"/>.
        /// </summary>
        /// <param name="keys">The identifiers that specify the data models to erase.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>A promise that returns the number of data models erased.</returns>
        Task<long> EraseAllByKeysAsync(IEnumerable<TKey> keys, CancellationToken cancellation = default);

        /// <summary>
        /// Asynchronously erases the specified data model from the datastore.
        /// </summary>
        /// <param name="model">The data model in the data store to erase.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>A promise that returns true if the data model was erased; otherwise false.</returns>
        Task<bool> EraseAsync(TModel model, CancellationToken cancellation = default);

        /// <summary>
        /// Asynchronously erases the specified data models from the data store.
        /// </summary>
        /// <param name="models">The data models to erase from the data store.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>A promise that returns the number of data models erased.</returns>
        Task<long> EraseAllAsync(IEnumerable<TModel> models, CancellationToken cancellation = default);
    }
}