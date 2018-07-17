namespace reexmonkey.xmisc.backbone.identifiers.contracts.models
{
    /// <summary>
    /// Specifies a provider that produces a fingerprint that uniquely or pesudo-uniquely identifies an object.
    /// </summary>
    /// <typeparam name="TFingerprint">The type of fingerprint that is produced.</typeparam>
    public interface IFingerprintGenerator<out TFingerprint>
    {
        /// <summary>
        /// Gets the default fingerprint for all types of objects. 
        /// </summary>
        /// <returns>The default fingerprint for all types of objects.</returns>
        TFingerprint GetNullFingerprint();

        /// <summary>
        /// Produces a fingerprint that uniquely or pesudo-uniquely identifies the specified object.
        /// </summary>
        /// <typeparam name="TModel">The type of obhect, whose fingerprint is produced.</typeparam>
        /// <param name="model">The object, whose fingerprint shall be produced.</param>
        /// <returns>The fingerprint that uniquely identifies or pseudo-identifies the specified <paramref name="model"/>.</returns>
        TFingerprint GetFingerprint<TModel>(TModel model);
    }
}
