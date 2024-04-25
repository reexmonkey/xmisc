namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies synchronous operations that populate one or more data models with references or details from a data store.
    /// </summary>
    /// <typeparam name="TKey">The type of unique key to identify the data model.</typeparam>
    /// <typeparam name="TModel">The type of data model to hydrate.</typeparam>
    public interface IPopulateRepository<TKey, TModel>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Populates the specified data model with references or details.
        /// </summary>
        /// <param name="model">The data model to hydrate.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        void Populate(TModel model, CancellationToken cancellation = default);

        /// <summary>
        /// Populates all of the specified data model with references or details.
        /// </summary>
        /// <param name="models">The data models to hydrate.</param>
        /// <param name="cancellation">Propagates the notification that the operation should be cancelled.</param>
        void PopulateAll(IEnumerable<TModel> models, CancellationToken cancellation = default);
    }
}