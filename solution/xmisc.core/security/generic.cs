using System.Security.Cryptography;
using System.Text;
using reexmonkey.xmisc.core.io.infrastructure;

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
        /// <returns>The cryptographic hash of the instance of type <typeparamref name="TValue"/>.</returns>
        public static byte[] GetBinaryHash<TValue, TSerializer>(this TValue value, TSerializer serializer, HashAlgorithm cipher)
            where TSerializer : BinarySerializerBase => serializer.Serialize(value).GetHash(cipher);

        /// <summary>
        /// Computes the textual hash value of an instance of the specifed type <typeparamref name="TValue"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of instance, whose hash value shall be computed.</typeparam>
        /// <typeparam name="TSerializer">The type of the serializer that serializes the instance of <typeparamref name="TValue"/> into a <see cref="string"/>.</typeparam>
        /// <param name="value">The value whose hash code shall be computed.</param>
        /// <param name="serializer">The serializer that serializes the specified <paramref name="value"/> into a string.</param>
        /// <param name="encoding">The character encoding that the serialization requires.</param>
        /// <param name="cipher">The cryptographic hash algorithm used to compute the hash value.</param>
        /// <returns>The cryptographic hash of the instance of type <typeparamref name="TValue"/>.</returns>
        public static string GetTextualHash<TValue, TSerializer>(this TValue value, TSerializer serializer, Encoding encoding, HashAlgorithm cipher)
            where TSerializer : TextSerializerBase => serializer.Serialize(value).GetHash(encoding, cipher);


        /// <summary>
        /// Computes the cryptographic salt and hash values of the instance of the specifed type <typeparamref name="TValue"/>. 
        /// Returns the  cryptographic salt value of the hash.
        /// </summary>
        /// <typeparam name="TValue">The type of instance, whose hashed value shall be salted. </typeparam>
        /// <typeparam name="TSerializer">The type of the serializer that serializes the instance of <typeparamref name="TValue"/> into an array of <see cref="byte"/>s.</typeparam>
        /// <param name="value">The value, whose salt and hash shall be computed.</param>
        /// <param name="serializer">The serializer that serializes the specified <paramref name="value"/> into an array of <see cref="byte"/>s.</param>
        /// <param name="cipher">The cryptographic hash algorithm used to compute the hash of the <paramref name="value"/></param>
        /// <param name="sprinkler">The number generator that generates a strong random cryptographic salt value.</param>
        /// <param name="saltLength">Length of the salt array.</param>
        /// <returns>The cr</returns>
        public static TValue GetSaltedBinaryHash<TValue, TSerializer>(this TValue value, TSerializer serializer, HashAlgorithm cipher, RandomNumberGenerator sprinkler, int saltLength)
            where TSerializer : BinarySerializerBase
            => serializer.Deserialize<TValue>(serializer.Serialize(value).GetSaltedHash(sprinkler, saltLength, cipher));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <typeparam name="TSerializer"></typeparam>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        /// <param name="encoding"></param>
        /// <param name="cipher"></param>
        /// <param name="sprinkler"></param>
        /// <param name="saltLength"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static TValue GetSaltedTextualHash<TValue, TSerializer>(this TValue value, TSerializer serializer, Encoding encoding, HashAlgorithm cipher, RandomNumberGenerator sprinkler, int saltLength, decimal separator)
            where TSerializer : TextSerializerBase
            => serializer.Deserialize<TValue>(serializer.Serialize(value).GetSaltedHash(encoding, sprinkler, saltLength, cipher));


    }
}
