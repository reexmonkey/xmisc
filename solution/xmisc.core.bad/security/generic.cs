using reexmonkey.xmisc.core.io.infrastructure;
using System.Security.Cryptography;
using System.Text;

namespace reexmonkey.xmisc.core.security
{
    /// <summary>
    /// Provides cryptographic extensions to generic types.
    /// </summary>
    public static class GenericTypeCryptographicExtensions
    {
        /// <summary>
        /// Computes the binary hash value of an instance of the specifed type <typeparamref name="TValue"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of instance, whose hash value shall be computed.</typeparam>
        /// <typeparam name="TSerializer">The type of the serializer that serializes the instance of <typeparamref name="TValue"/> into bytes.</typeparam>
        /// <param name="value">The value whose hash code shall be computed.</param>
        /// <param name="serializer">The serializer that serializes the specified <paramref name="value"/> into bytes.</param>
        /// <param name="cipher">The cryptographic hash algorithm used to compute the hash value.</param>
        /// <returns>The cryptographic hash of the instance of type <typeparamref name="TValue"/></returns>
        public static byte[] GetBinaryHash<TValue, TSerializer>(this TValue value, TSerializer serializer, HashAlgorithm cipher)
            where TSerializer : BinarySerializerBase => serializer.Serialize(value).GetHash(cipher);

        /// <summary>
        /// Computes the textual hash value of an instance of the specifed type <typeparamref name="TValue"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of instance, whose hash value shall be computed.</typeparam>
        /// <typeparam name="TSerializer">The type of the serializer that serializes the instance of <typeparamref name="TValue"/> into a <see cref="string"/>.</typeparam>
        /// <param name="value">The value whose hash code shall be computed.</param>
        /// <param name="serializer">The serializer that serializes the specified <paramref name="value"/> into a string.</param>
        /// <param name="encoding">The encoding used in .</param>
        /// <param name="cipher">The cipher.</param>
        /// <returns></returns>
        public static string GetTextualHash<TValue, TSerializer>(this TValue value, TSerializer serializer, Encoding encoding, HashAlgorithm cipher)
            where TSerializer : TextSerializerBase => serializer.Serialize(value).GetHash(encoding, cipher);

        public static TValue GetSaltedBinaryHash<TValue, TSerializer>(this TValue value, TSerializer serializer, HashAlgorithm cipher, RandomNumberGenerator sprinkler, int saltLength, decimal separator)
            where TSerializer : BinarySerializerBase
            => serializer.Deserialize<TValue>(serializer.Serialize(value).GetSaltedHash(sprinkler, saltLength, cipher));

        public static TValue GetSaltedTextualHash<TValue, TSerializer>(this TValue value, TSerializer serializer, Encoding encoding, HashAlgorithm cipher, RandomNumberGenerator sprinkler, int saltLength, decimal separator)
            where TSerializer : TextSerializerBase
            => serializer.Deserialize<TValue>(serializer.Serialize(value).GetSaltedHash(encoding, sprinkler, saltLength, cipher));


    }
}
