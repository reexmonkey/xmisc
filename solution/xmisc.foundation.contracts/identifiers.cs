namespace reexjungle.xmisc.foundation.contracts
{
    /// <summary>
    /// Specifies a contract for the Owner identifier of a Formal Public Identifiers (FPI)
    /// </summary>
    public interface IFpiOwner
    {
        /// <summary>
        /// Gets or sets the approval status of the FPI
        /// </summary>
        ApprovalStatus Status { get; set; }

        /// <summary>
        ///  Gets or sets the owner of the FPI, if it is approved by a standard authority e.g. ISO
        /// </summary>
        string Author { get; set; }
    }

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