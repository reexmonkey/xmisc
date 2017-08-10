using System;
using System.Linq;
using System.Security.Cryptography;

namespace reexmonkey.xmisc.core.cryptography.extensions
{
    /// <summary>
    /// Provides cryptographic extensions to the <see cref="byte"/> class.
    /// </summary>
    public static class BytesCryptographicExtensions
    {
        /// <summary>
        /// Computes the hash value of the specified byte array.
        /// </summary>
        /// <param name="bytes">The byte array to hash.</param>
        /// <param name="cipher">The cryptographic hash algorithm used to compute the hash value.</param>
        /// <returns>The computed hash code.</returns>
        public static byte[] GetHash(this byte[] bytes, HashAlgorithm cipher) => cipher.ComputeHash(bytes);

        /// <summary>
        /// Gets the hash value for the specified region of a byte array.
        /// </summary>
        /// <param name="bytes">The byte array to hash.</param>
        /// <param name="offset">The offset into the byte array from which to begin using data.</param>
        /// <param name="count">The number of bytes in the array to use as data.</param>
        /// <param name="cipher">The cryptographic hash algorithm used to compute the hash value.</param>
        /// <returns>The computed hash code.</returns>
        public static byte[] GetHash(this byte[] bytes, int offset, int count, HashAlgorithm cipher) => cipher.ComputeHash(bytes, offset, count);

        /// <summary>
        /// Creates a cryptographic salt value for a byte array and prepends the salt to the specified byte array.
        /// </summary>
        /// <param name="sprinkler">The number generator that generates a strong random cryptographic value.</param>
        /// <param name="bytes">The array of bytes to append to the generated salt value.</param>
        /// <param name="saltLength">Length of the salt array.</param>
        /// <returns>A salted value that consists of the gnerated cryptographic salt prepended to the byte array.</returns>
        private static byte[] AddSalt(this RandomNumberGenerator sprinkler, byte[] bytes, int saltLength)
        {
            var buffer = new byte[bytes.Length + saltLength];
            var salt = sprinkler.Generate(saltLength);
            Buffer.BlockCopy(salt, 0, buffer, 0, salt.Length);
            Buffer.BlockCopy(bytes, 0, buffer, salt.Length, bytes.Length);
            return buffer;
        }

        /// <summary>
        /// Creates a cryptographic salt value for a byte array and prepends the salt to the specified region of a byte array.
        /// </summary>
        /// <param name="sprinkler">The number generator that generates a strong random cryptographic value.</param>
        /// <param name="bytes">he array of bytes, whose specified region is appended to the generated salt value.</param>
        /// <param name="offset">The offset into the byte array from which to begin using data.</param>
        /// <param name="count">The number of bytes in the array to use as data.</param>
        /// <param name="saltLength">Length of the salt array</param>
        /// <returns>A salted value that consists of the gnerated cryptographic salt prepended to the specified region of the byte array.</returns>
        private static byte[] AddSalt(this RandomNumberGenerator sprinkler, byte[] bytes, int offset, int count, int saltLength)
        {
            var buffer = new byte[count + saltLength];
            var salt = sprinkler.Generate(saltLength);
            Buffer.BlockCopy(salt, 0, buffer, 0, salt.Length);
            Buffer.BlockCopy(bytes, offset, buffer, salt.Length, count);
            return buffer;
        }

        /// <summary>
        /// Creates a cryptographic salt value for a specified byte array and combines the salt with the hash of the byte array.
        /// </summary>
        /// <param name="bytes">The array of bytes, whose salt value and hash value shall be computed.</param>
        /// <param name="sprinkler">The number generator that generates a strong random cryptographic salt value.</param>
        /// <param name="saltLength">Length of the salt array.</param>
        /// <param name="cipher">The cryptographic hash algorithm used to compute the hash value.</param>
        /// <returns>The resultant array that consists of a generated cryptographic salt and the hash of the byte array.</returns>
        public static byte[] GetSaltedHash(this byte[] bytes, RandomNumberGenerator sprinkler, int saltLength, HashAlgorithm cipher)
            => sprinkler.AddSalt(bytes, saltLength).GetHash(cipher);

        /// <summary>
        /// Creates a cryptographic salt value and combines the salt with the hash of the specified region of a byte array.
        /// </summary>
        /// <param name="bytes">The array of bytes, whose salt value and hash value shall be computed.</param>
        /// <param name="offset">The offset into the byte array from which to begin using data.</param>
        /// <param name="count">The number of bytes in the array to use as data.</param>
        /// <param name="sprinkler">The number generator that generates a strong random cryptographic salt value.</param>
        /// <param name="saltLength">Length of the salt array.</param>
        /// <param name="cipher">The cryptographic hash algorithm used to compute the hash value.</param>
        /// <returns>The resultant array that consists of a generated cryptographic salt and the hash of the byte array.</returns>
        public static byte[] GetSaltedHash(this byte[] bytes, int offset, int count, RandomNumberGenerator sprinkler, int saltLength, HashAlgorithm cipher)
            => sprinkler.AddSalt(bytes, offset, count, saltLength).GetHash(offset, count, cipher);

        /// <summary>
        /// Converts the specified byte array into a <see cref="string"/>.
        /// </summary>
        /// <param name="bytes">The byte array to convert.</param>
        /// <returns>The equivalent <see cref="string"/> representation of the byte array.</returns>
        public static string ToString(this byte[] bytes) => bytes != null && bytes.Any() ? BitConverter.ToString(bytes) : string.Empty;

        /// <summary>
        /// Converts the specified region of the byte array into a <see cref="string"/>.
        /// </summary>
        /// <param name="bytes">The byte array, whose specified region shall be converted into a string.</param>
        /// <param name="offset">The offset into the byte array from which to begin using data..</param>
        /// <param name="count">The number of bytes in the array to use as data.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public static string ToString(this byte[] bytes, int offset, int count) => bytes != null && bytes.Any() ? BitConverter.ToString(bytes, offset, count) : string.Empty;
    }
}