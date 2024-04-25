namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies synchronous operations that restore one or more temporarily deleted data models.
    /// </summary>
    /// <typeparam name="TKey">The type of key that identifies the model to mark for deletion.</typeparam>
    /// <typeparam name="TModel">The type of model to mark for deletion.</typeparam>
    public interface IRestoreRepository<in TKey, TModel>
        where TKey : IEquatable<TKey>
        where TModel : ISupportTrashing<TModel>
    {
        /// <summary>
        /// Restores a data model that is specified by a unique identifier.
        /// </summary>
        /// <param name="key">The unique identifier that specifies the data model to restore.</param>
        /// <param name="references">Should the references or details of the restored data model also be restored?</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>The data model that is specified by the provided <paramref name="key"/>.</returns>
        TModel RestoreByKey(TKey key, bool? references = null, CancellationToken cancellation = default);

        /// <summary>
        /// Restores data models that are specified by the provided unique identifiers.
        /// </summary>
        /// <param name="keys">The unique identifiers that specify the data models to restore.</param>
        /// <param name="references">Should the references or details of restored data models also be restored?</param>
        /// <param name="offset">The number of unique identifiers to bypass.</param>
        /// <param name="limit">The numbers of unique identifiers to return.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        /// <returns>The data models that are specified by the provided <paramref name="keys"/>. </returns>
        List<TModel> RestoreAllByKeys(IEnumerable<TKey> keys, bool? references = null, int? offset = null, int? limit = null, CancellationToken cancellation = default);

        /// <summary>
        /// Restores the specified data model.
        /// </summary>
        /// <param name="model">The data model to restore.</param>
        /// <param name="references">Should the references or details of the restored data model also be restored?</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        TModel Restore(TModel model, bool? references = null, CancellationToken cancellation = default);

        /// <summary>
        /// Restores the specified data models.
        /// </summary>
        /// <param name="models">The data models to restore.</param>
        /// <param name="references">Should the references or details of restored data models also be restored?</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        List<TModel> RestoreAll(IEnumerable<TModel> models, bool? references = null, CancellationToken cancellation = default);
    }
}