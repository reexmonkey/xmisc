using System;

namespace reexmonkey.xmisc.backbone.identifiers.contracts.generators
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
        TFingerprint GetDefaultFingerprint();

        /// <summary>
        /// Produces a fingerprint that uniquely or pesudo-uniquely identifies the specified object.
        /// </summary>
        /// <typeparam name="TModel">The type of object, whose fingerprint shall be produced.</typeparam>
        /// <param name="model">The object, whose fingerprint shall be produced.</param>
        /// <param name="serializeFunc">The function to serialize the object into a byte array.</param>
        /// <returns>The fingerprint that uniquely identifies or pseudo-identifies the specified <paramref name="model"/>.</returns>
        TFingerprint GetFingerprint<TModel>(TModel model, Func<TModel, byte[]> serializeFunc);
    }
}