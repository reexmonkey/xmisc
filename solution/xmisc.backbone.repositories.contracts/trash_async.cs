namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies asynchronous operations that trash one or models temporarily by marking them for deletion.
    /// <para/> The "soft-deleted" models are excluded from access until the repository restores them.
    /// </summary>
    /// <typeparam name="TKey">The type of key that identifies the model to mark for deletion.</typeparam>
    /// <typeparam name="TModel">The type of model to mark for deletion.</typeparam>
    public interface ITrashRepositoryAsync<in TKey, TModel>
        where TKey : IEquatable<TKey>
        where TModel : ISupportTrashing<TModel>
    {

        /// <summary>
        /// Trashes a data model for deletion that is specified by a provided unique identifier.
        /// </summary>
        /// <param name="key">The unique identifier that specifies the data model to mark for deletion.</param>
        /// <param name="references">Should the references or details of the data model also be marked for deletion?</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>A promise to mark for deletion a data model that is specified by <paramref name="key"/>.</returns>
        Task<TModel> TrashByKeyAsync(TKey key, bool? references = null, CancellationToken cancellation = default);

        /// <summary>
        /// Trashes data models for deletion that are specified by the provided unique identifiers.
        /// </summary>
        /// <param name="keys">The unique identifiers that specify the data models to mark for deletion.</param>
        /// <param name="references">Should the references or details of located data models also be marked for deletion?</param>
        /// <param name="offset">The number of data models to bypass.</param>
        /// <param name="limit">The numbers of data models to return.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>A promise to mark for deletion and return data models that are specified by <paramref name="keys"/>.</returns>
        IAsyncEnumerable<TModel> TrashAllByKeysAsync(IEnumerable<TKey> keys, bool? references = null, int? offset = null, int? limit = null, CancellationToken cancellation = default);

        /// <summary>
        /// Marks the specified data model for deletion.
        /// </summary>
        /// <param name="model">The data model to mark for deletion.</param>
        /// <param name="references">Should the references or details of the data model also be marked for deletion?</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>A promise to mark for deletion the given data model.</returns>
        Task<TModel> TrashAsync(TModel model, bool? references = null, CancellationToken cancellation = default);

        /// <summary>
        /// Marks the specified data models for deletion.
        /// </summary>
        /// <param name="models">The data models to mark for deletion.</param>
        /// <param name="references">Should the references or details of the data models also be marked for deletion?</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>A promise to mark for deletion the given data models.</returns>
        IAsyncEnumerable<TModel> TrashAllAsync(IEnumerable<TModel> models, bool? references = null, CancellationToken cancellation = default);

    }
}