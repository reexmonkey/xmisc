namespace reexjungle.xmisc.foundation.contracts
{
    /// <summary>
    /// Represents the status of an approval
    /// </summary>
    public enum ApprovalStatus
    {
        /// <summary>
        /// Approved formally by an authority
        /// </summary>
        Standard = 0x1,

        /// <summary>
        /// Approved informally by an authority
        /// </summary>
        Informal = 0x2,

        /// <summary>
        /// Not approved by any authority
        /// </summary>
        None = 0xf
    }
}