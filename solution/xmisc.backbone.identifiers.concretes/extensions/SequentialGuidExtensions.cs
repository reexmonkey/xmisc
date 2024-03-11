using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using System;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.extensions
{
    /// <summary>
    /// Extends the features of the <see cref="SequentialGuid"/> structure.
    /// </summary>
    public static class SequentialGuidExtensions
    {
        /// <summary>
        /// Reverses the byte array order of the specified sequential globally unique identifier (GUID).
        /// <para/> A Little-Endian GUID shall be converted into a Big-Endian GUID and vice-versa.
        /// <para /> Please note a Big-Endian <see cref="SequentialGuid"/> is compliant to RFC 4122
        /// meanwhile a Little-Endian  <see cref="SequentialGuid"/> is not compatible.
        /// </summary>
        /// <param name="source">The GUID, whose byte order is to be reversed.</param>
        /// <returns>The reversed byte order of <paramref name="source"/>.</returns>
        public static SequentialGuid Reverse(this SequentialGuid source)
        {
            var bytes = source.ToByteArray();
            Array.Reverse(bytes);
            return new SequentialGuid(bytes);
        }

        /// <summary>
        /// Converts the specified RFC4122-compliant sequential GUID into SQL Server-compatible GUID.
        /// </summary>
        /// <param name="guid">.</param>
        /// <returns>The SQL Server-compatible GUID from the conversion.</returns>
        public static SequentialGuid Shuffle(this SequentialGuid guid)
        {
            var source = guid.ToByteArray();
            var destination = new byte[16];
            Buffer.BlockCopy(source, 0, destination, 0, source.Length);

            //Big Endian Swaps
            //swap 4 bytes (uint low)
            destination[3] = source[0];
            destination[2] = source[1];
            destination[1] = source[2];
            destination[0] = source[3];

            //swap next 2 bytes (ushort mid)
            destination[5] = source[4];
            destination[4] = source[5];

            //swap next 2 bytes (ushort hi)
            destination[7] = source[6];
            destination[6] = source[7];

            return new SequentialGuid(destination);
        }

        /// <summary>
        /// Converts the specified SQL Server's byte-shuffled GUID to a <see cref="SequentialGuid"/> structure.
        /// <para/> Note the resultant <see cref="SequentialGuid"/> structure shall not be RFC compliant
        /// if the input GUID was not a shuffled <see cref="SequentialGuid"/>.
        /// </summary>
        /// <param name="guid">The SQL Server GUID to convert.</param>
        /// <returns>The sequential GUID from the conversion.</returns>
        public static SequentialGuid Unshuffle(this SequentialGuid guid)
        {
            var source = guid.ToByteArray();
            var destination = new byte[16];
            Buffer.BlockCopy(source, 0, destination, 0, source.Length);

            //Little Endian Swaps
            //swap 4 bytes (uint low)
            destination[0] = source[3];
            destination[1] = source[2];
            destination[2] = source[1];

            destination[3] = source[0];

            //swap next 2 bytes (ushort mid)
            destination[4] = source[5];
            destination[5] = source[4];

            //swap next 2 bytes (ushort hi)
            destination[6] = source[7];
            destination[7] = source[6];

            return new SequentialGuid(destination);
        }
    }
}
