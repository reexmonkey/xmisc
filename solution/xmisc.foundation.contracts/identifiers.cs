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
        ///  Gets or sets the owner of the FPI
        /// </summary>
        string Author { get; set; }

        /// <summary>
        /// Gets or sets the reference to the standard authority that approved the FPI
        /// </summary>
        string Reference { get; set; }
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

    /// <summary>
    /// Performs conversions between a Uniform Resource Name (URN) and Formal Public Identifier (FPI)
    /// </summary>
    public interface IFpiUrnConverter
    {
        /// <summary>
        /// Converts a Formal Public Identifier (FPI) to an equivalent Universal Resource Name (URN) expression
        /// </summary>
        /// <returns>The equivalent URN expression</returns>
        string ToUrn();

        /// <summary>
        /// Creates a Formal Public Identifier (FPI) from an an equivalent Universal Resource Name (URN) expression
        /// </summary>
        /// <param name="urn">The equivalent URN expression</param>
        void FromUrn(string urn);
    }
}