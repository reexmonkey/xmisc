namespace reexjungle.xmisc.infrastructure.contracts
{
    public enum FlushMode
    {
        soft,
        hard
    };

    /// <summary>
    ///
    /// </summary>
    public enum ExpectationMode
    {
        /// <summary>
        ///
        /// </summary>
        Optimistic,

        /// <summary>
        ///
        /// </summary>
        Pessimistic,

        /// <summary>
        ///
        /// </summary>
        Unknown
    }
}