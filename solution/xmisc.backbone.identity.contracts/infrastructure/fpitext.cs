namespace xmisc.backbone.identity.contracts.infrastructure
{
    /// <summary>
    /// Specifies a contract for the Text identifier of a Formal Public Identifiers (FPI)
    /// </summary>
    public interface IFpiText
    {
        /// <summary>
        /// Gets or sets the product class of the FPI
        /// </summary>
        string Product { get; }

        /// <summary>
        /// Gets or sets the description of the FPI
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets or sets the langauage of the FPI according to ISO 639
        /// </summary>
        string Language { get; }
    }
}