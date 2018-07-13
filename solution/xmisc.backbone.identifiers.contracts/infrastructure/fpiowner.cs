namespace reexmonkey.xmisc.backbone.identifiers.contracts.infrastructure
{
    /// <summary>
    /// Specifies a contract for the Owner identifier of a Formal Public Identifiers (FPI)
    /// </summary>
    public interface IFpiOwner
    {
        /// <summary>
        /// Gets the approval status of the FPI
        /// </summary>
        ApprovalStatus Status { get; }

        /// <summary>
        ///  Gets the owner of the FPI
        /// </summary>
        string Author { get; }

        /// <summary>
        /// Gets or sets the reference to the standard authority that approved the FPI
        /// </summary>
        string Reference { get; }
    }
}