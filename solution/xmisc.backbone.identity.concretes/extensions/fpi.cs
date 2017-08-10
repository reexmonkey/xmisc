using xmisc.backbone.identity.concretes.infrastructure;

namespace xmisc.backbone.identity.concretes.extensions
{
    /// <summary>
    /// Provides extensions to the <see cref="Fpi"/> class.
    /// </summary>
    public static class FpiExtensions
    {
        /// <summary>
        /// Converts a Formal Public Identifier (FPI) to an equivalent Universal Resource Name (URN) expression
        /// </summary>
        /// <param name="urn">The equivalent URN expression from the conversion.</param>
        /// <returns></returns>
        public static Fpi AsFpi(this string urn) => new Fpi($"urn:{urn.Substring(4).Replace(":", "//")}");

        /// <summary>
        /// Creates a Formal Public Identifier (FPI) from an an equivalent Universal Resource Name (URN) expression
        /// </summary>
        /// <param name="fpi"></param>
        /// <returns>The equivalent Formal Public Identifier (FPI) from the conversion.</returns>
        public static string AsUrn(this Fpi fpi) => $"urn:{fpi.ToString().Replace("//", ":")}";

    }
}
