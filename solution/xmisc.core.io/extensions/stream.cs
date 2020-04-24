using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.core.io.extensions
{
    /// <summary>
    /// Extends features of the <see cref="System.IO.Stream"/> class.
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Gets the checksum of a <paramref name="stream"/> object.
        /// </summary>
        /// <param name="stream">The stream whose integerity is checked.</param>
        /// <param name="algorithm">
        /// The algorithm to compute the hash of the <paramref name="stream"/>.
        /// </param>
        /// <returns>The checksum of the stream object.</returns>
        public static string GetChecksum(this Stream stream, HashAlgorithm algorithm)
        {
            var hash = algorithm.ComputeHash(stream);
            return hash.Any()
                ? BitConverter.ToString(hash)
                : string.Empty;
        }

        /// <summary>
        /// Gets the checksum value of a <paramref name="stream"/> in asynchronous operation.
        /// </summary>
        /// <param name="stream">The stream whose integerity is checked.</param>
        /// <param name="algorithm">
        /// The algorithm to compute the hash of the <paramref name="stream"/>.
        /// </param>
        /// <returns>The promise of a checksum value.</returns>
        public static async Task<string> GetChecksumAsync(this Stream stream, HashAlgorithm algorithm)
            => await Task.FromResult(GetChecksum(stream, algorithm));
    }
}