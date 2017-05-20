using System;

namespace xmisc.backbone.identity.contracts.infrastructure
{
    /// <summary>
    /// Specifies a contract for identifying a component
    /// </summary>
    /// <typeparam name="TKey">The type of identifier</typeparam>
    public interface IContainsKey<out TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Gets the identifier-key of the component
        /// </summary>
        TKey Id { get; }
    }
}