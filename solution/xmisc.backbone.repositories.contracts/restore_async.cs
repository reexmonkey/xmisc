namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies asynchronous operations that restore one or more temporarily deleted data models.
    /// </summary>
    /// <typeparam name="TKey">The type of key that identifies the model to restore.</typeparam>
    /// <typeparam name="TModel">The type of model to restore.</typeparam>
    public interface IRestoreRepositoryAsync<in TKey, TModel>
        where TKey : IEquatable<TKey>
        where TModel : ISupportTrashing<TModel>
    {
        /// <summary>
        /// Restores a data model asynchronously that is specified by the provided <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The identifier that specifies the data model to restore.</param>
        /// <param name="references">Should the references or details of the restored data model also be restored?</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>A promise to restore and return the trashed data model that is specified by <paramref name="key"/>.</returns>
        Task<TModel> RestoreByKeyAsync(TKey key, bool? references = null, CancellationToken cancellation = default);

        /// <summary>
        /// Restores data models asynchronously that are specified by unique identifiers .
        /// </summary>
        /// <param name="keys">The identifiers that specify the data models to restore.</param>
        /// <param name="references">Should the references or details of the restored data model also be restored?</param>
        /// <param name="offset">The number of data models to bypass.</param>
        /// <param name="limit">The numbers of data models to return.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>A promise to restore and return the trashed data models that are specified by <paramref name="keys"/>.</returns>
        IAsyncEnumerable<TModel> RestoreAllByKeysAsync(IEnumerable<TKey> keys, bool? references = null, int? offset = null, int? limit = null, CancellationToken cancellation = default);

        /// <summary>
        /// Restores the specified data model asynchronously.
        /// </summary>
        /// <param name="model">The data model to restore.</param>
        /// <param name="references">Should the references or details of the data model also be restored?</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>A promise to return the restored data model.</returns>
        TModel RestoreAsync(TModel model, bool? references = null, CancellationToken cancellation = default);

        /// <summary>
        /// Restores the specified data models asynchronously.
        /// </summary>
        /// <param name="models">The data models to restore.</param>
        /// <param name="references">Should the references or details of the data models also be restored?</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>A promise to return the restored data models.</returns>
        IAsyncEnumerable<TModel> RestoreAllAsync(IEnumerable<TModel> models, bool? references = null, CancellationToken cancellation = default);
    }
}