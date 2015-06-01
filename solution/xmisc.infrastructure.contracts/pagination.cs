namespace reexjungle.xmisc.infrastructure.contracts
{
    /// <summary>
    /// Speicifies a contract to paginate results
    /// </summary>
    /// <typeparam name="T">Numerical integral type used for pagination</typeparam>
    public interface IPaginated<T>
        where T : struct
    {
        /// <summary>
        /// Page number &gt;=1. Can be optionally null, when pagination is not required.
        /// </summary>
        T? Page { get; set; }

        /// <summary>
        /// Number of results per page &gt;=1. Must be set when <see cref="Page"/> is not null.
        /// Can be optionally null, when pagination is not required.
        /// </summary>
        T? Size { get; set; }
    }
}