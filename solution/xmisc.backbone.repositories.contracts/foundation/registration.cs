using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using xmisc.backbone.repositories.contracts.infrastucture;

namespace xmisc.backbone.repositories.contracts.foundation
{
    /// <summary>
    /// Specifies a repository that registers data models by assigning identities to them.
    /// </summary>
    /// <typeparam name="TKey">The type of identifier that the data model uses.</typeparam>
    /// <typeparam name="TModel">The type of data model to create.</typeparam>
    public interface IModelRegistrationRepository<TKey, TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        where TModel : IHasKey<TKey>
    {
        /// <summary>
        /// Registers a new data model.
        /// </summary>
        /// <returns>A new data model.</returns>
        TModel Register(TModel model);

        /// <summary>
        /// Registers the <paramref name="models"/> by assigning each data model an identity.
        /// </summary>
        /// <param name="models">The models to register.</param>
        /// <returns>Data models with assigned identities.</returns>
        IEnumerable<TModel> RegisterAll(IEnumerable<TModel> models);

        /// <summary>
        /// Asynchronously registers the <paramref name="model"/> by assigning an identity to it.
        /// </summary>
        /// <param name="model">The model to register.</param>
        /// <returns>The model with an assigned identity.</returns>
        Task<TModel> RegisterAsync(TModel model);

        /// <summary>
        /// Asynchronously registers the <paramref name="models"/> by assigning each data model an identity.
        /// </summary>
        /// <param name="models">The models to register.</param>
        /// <returns>Data models with assigned identities.</returns>
        Task<IEnumerable<TModel>> RegisterAllAsync(IEnumerable<TModel> models);
    }
}