namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies synchronous operations that trash one or more data models temporarily by marking them for deletion.
    /// <para/> The "soft-deleted" models are excluded from access until the repository restores them.
    /// </summary>
    /// <typeparam name="TKey">The type of key that identifies the model to mark for deletion.</typeparam>
    /// <typeparam name="TModel">The type of model to mark for deletion.</typeparam>
    public interface ITrashRepository<in TKey, TModel>
        where TKey : IEquatable<TKey>
        where TModel : ISupportTrashing<TModel>
    {
        /// <summary>
        /// Marks the given data model, which is specified by a unique identifier, for deletion.
        /// </summary>
        /// <param name="key">The unique identifier that specifies the data model to mark for deletion.</param>
        /// <param name="references">Should the references or details of the data model also be marked for deletion?</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>The data model that is specified by the provided <paramref name="key"/>.</returns>
        TModel TrashByKey(TKey key, bool? references = null, CancellationToken cancellation = default);

        /// <summary>
        /// Marks data models, which are specified by unique identifiers, for deletion.
        /// </summary>
        /// <param name="keys">The unique identifiers that specify the data models to mark for deletion.</param>
        /// <param name="references">Should the references or details of located data models also be marked for deletion?</param>
        /// <param name="offset">The number of models (identified by unique identifiers) to bypass.</param>
        /// <param name="limit">The numbers of models (identified unique identifiers) to mark for deletion.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>The data models specified by the provided <paramref name="keys"/>. </returns>
        List<TModel> TrashAllByKeys(IEnumerable<TKey> keys, bool? references = null, int? offset = null, int? limit = null, CancellationToken cancellation = default);

        /// <summary>
        /// Marks the given data model for deletion.
        /// </summary>
        /// <param name="model">The data model to mark for deletion.</param>
        /// <param name="references">Should the references or details of the data model also be marked for deletion?</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>The data model marked for deletion.</returns>
        TModel Trash(TModel model, bool? references = null, CancellationToken cancellation = default);

        /// <summary>
        /// Marks the given data models for deletion.
        /// </summary>
        /// <param name="models">The data models to mark for deletion.</param>
        /// <param name="references">Should the references or details of the data model also be marked for deletion?</param>
        /// <param name="offset">The number of data models to bypass.</param>
        /// <param name="limit">The numbers of data models to mark for deletion.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        List<TModel> TrashAll(IEnumerable<TModel> models, bool? references = null, int? offset = null, int? limit = null, CancellationToken cancellation = default);

    }
}