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
        /// Reads bytes from stream.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <param name="buffersize">The size of the buffer during the read process.</param>
        /// <returns>Bytes read from the stream.</returns>
        public static byte[] ReadBytes(this Stream stream, int buffersize = 4096)
        {
            if (stream is MemoryStream memorystream) return memorystream.ToArray();

            var buffer = new byte[buffersize];
            using (var mstream = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0) mstream.Write(buffer, 0, read);
                return mstream.ToArray();
            }
        }

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
            return hash.Any() ? BitConverter.ToString(hash) : string.Empty;
        }

        /// <summary>
        /// Reads bytes from stream in an asynchronous operation.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <param name="buffersize">The size of the buffer during the read process.</param>
        /// <returns>The promise of bytes read from the stream.</returns>
        public static async Task<byte[]> ReadBytesAsync(this Stream stream, int buffersize = 4096)
        {
            if (stream is MemoryStream memorystream) return await Task.FromResult(memorystream.ToArray());

            var buffer = new byte[buffersize];
            using (var mstream = new MemoryStream())
            {
                int read;
                while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await mstream.WriteAsync(buffer, 0, read);
                }
                return await Task.FromResult(mstream.ToArray());
            }
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