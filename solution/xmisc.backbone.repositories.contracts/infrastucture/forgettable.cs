namespace xmisc.backbone.repositories.contracts.infrastucture
{
    /// <summary>
    /// Specifies an interface for an entity that can be "forgotten" (marked for deletion).
    /// </summary>
    public interface IForgettable
    {
        /// <summary>
        /// Gets or sets whether the entity should be marked for deletion.
        /// </summary>
        bool? Forgotten { get; set; }
    }
}