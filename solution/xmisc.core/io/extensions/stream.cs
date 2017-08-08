using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace reexmonkey.xmisc.core.io.extensions
{
    /// <summary>
    /// Extends features of the <see cref="Stream"/> class.
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
        /// Gets the checksum of the <paramref name="stream"/> object using the provided <paramref name="algorithm"/>.
        /// </summary>
        /// <param name="stream">The stream whose integerity is checked.</param>
        /// <param name="algorithm">The algorithm to compute the hash of the <paramref name="stream"/> object..</param>
        /// <returns>The checksum of the stream object.</returns>
        public static string GetChecksum(this Stream stream, HashAlgorithm algorithm)
        {
            var hash = algorithm.ComputeHash(stream);
            return hash.Any() ? BitConverter.ToString(hash) : string.Empty;
        }

    }
}