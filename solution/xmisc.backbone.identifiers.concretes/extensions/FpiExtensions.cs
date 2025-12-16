using reexmonkey.xmisc.backbone.identifiers.concretes.models;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.extensions
{
    /// <summary>
    /// Provides extensions to the <see cref="Fpi"/> class.
    /// </summary>
    public static class FpiExtensions
    {
        /// <summary>
        /// Converts a Formal Public Identifier (Fpi) to an equivalent Universal Resource Name (URN) expression
        /// </summary>
        /// <param name="urn">The equivalent URN expression from the conversion.</param>
        /// <returns></returns>
        public static Fpi AsFpi(this string urn) => new($"urn:{urn[4..].Replace(":", "//")}");

        /// <summary>
        /// Creates a Formal Public Identifier (Fpi) from an an equivalent Universal Resource Name (URN) expression
        /// </summary>
        /// <param name="fpi"></param>
        /// <returns>The equivalent Formal Public Identifier (Fpi) from the conversion.</returns>
        public static string AsUrn(this Fpi fpi) => $"urn:{fpi.ToString().Replace("//", ":")}";
    }
}