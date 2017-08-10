using System;
using System.Collections.Generic;
using System.Linq;

namespace reexmonkey.xmisc.core.io.extensions
{
    /// <summary>
    /// Provides extensions to an arraay of <see cref="byte"/>s.
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// Splits the byte array into a sequence of byte arrays.
        /// </summary>
        /// <param name="bytes">The byte array to split.</param>
        /// <param name="blocksize">The size of of each byte array in the sequence.</param>
        /// <returns>A sequence of byte arrays.</returns>
        public static IEnumerable<byte[]> Split(this byte[] bytes, int blocksize = 64 * 1024)
        {
            var offset = 0;
            while (offset < bytes.Length)
            {
                var buffer = new byte[blocksize];
                Buffer.BlockCopy(bytes, offset, buffer, 0, blocksize);
                offset += blocksize;
                yield return buffer;
            }
        }

        /// <summary>
        /// Gets the total number of elements in a given byte matrix (array of byte arrays).
        /// </summary>
        /// <param name="arrays">The byte matrix, whose length is to be computed.</param>
        /// <returns>The total number of elements in the byte matrix.</returns>
        public static long GetLongLength(this byte[][] arrays) => arrays.Where(x => x != null).Sum(array => array.LongCount());

        /// <summary>
        /// Merges the given array of byte arrays to this byte array into a new byte array.
        /// </summary>
        /// <param name="bytes">The first byte array to merge.</param>
        /// <param name="others">The other byte arrays to merge</param>
        /// <returns>A byte array that is the result of the merge between <paramref name="bytes"/> and <paramref name="others"/>.</returns>
        public static byte[] Merge(this byte[] bytes, params byte[][] others)
        {
            var offset = 0;
            var length = others.GetLongLength();
            var result = new byte[length];
            for (var i = 0; i < others.Length; i++)
            {
                var other = others[i];
                Buffer.BlockCopy(other, 0, bytes, offset, other.Length);
                offset += bytes.Length;
            }
            return result;
        }
    }
}
