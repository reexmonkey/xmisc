namespace reexmonkey.xmisc.core.contracts.foundation
{
    /// <summary>
    /// Specifies a contract for the Text identifier of a Formal Public Identifiers (FPI)
    /// </summary>
    public interface IFpiText
    {
        /// <summary>
        /// Gets or sets the product class of the FPI
        /// </summary>
        string Product { get; set; }

        /// <summary>
        /// Gets or sets the description of the FPI
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the langauage of the FPI according to ISO 639
        /// </summary>
        string Language { get; set; }
    }
}