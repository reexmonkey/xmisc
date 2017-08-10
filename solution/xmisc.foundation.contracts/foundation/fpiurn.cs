namespace reexmonkey.xmisc.core.contracts.foundation
{
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