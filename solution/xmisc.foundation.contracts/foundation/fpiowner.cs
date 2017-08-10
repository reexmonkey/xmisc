namespace reexmonkey.xmisc.core.contracts.foundation
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
}