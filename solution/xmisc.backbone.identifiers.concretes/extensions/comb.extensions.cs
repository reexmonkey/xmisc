using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using System;
using System.Data.SqlTypes;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.extensions
{
    /// <summary>
    /// Extends the features of the <see cref="SequentialGuid"/> structure.
    /// </summary>
    public static class SequentialGuidExtensions
    {
        /// <summary>
        /// Converts a specified <see cref="SequentialGuid"/> to its equivalent <see cref="SqlGuid"/> structure.
        /// </summary>
        /// <param name="guid">The RFC 4122 <see cref="SequentialGuid"/> to convert.</param>
        /// <returns>The equivalent <see cref="SqlGuid"/> structure.</returns>
        public static SqlGuid AsSqlGuid(this SequentialGuid guid)
        {
            var source = guid.ToByteArray();
            var destination = new byte[16];
            Buffer.BlockCopy(source, 0, destination, 0, source.Length);

            //Little Endian Swaps
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

            return new SqlGuid(destination);
        }

        /// <summary>
        /// Converts the specified <see cref="SqlGuid"/> to its equivalent <see cref="SequentialGuid"/> structure.
        /// </summary>
        /// <param name="guid">The <see cref="SqlGuid"/> to convert.</param>
        /// <returns>The equivalent <see cref="SequentialGuid"/> structure from the conversion.</returns>
        public static SequentialGuid AsSequentialGuid(this SqlGuid guid)
        {
            var source = guid.ToByteArray();
            var destination = new byte[16];
            Buffer.BlockCopy(source, 0, destination, 0, source.Length);

            //Big Endian Swaps
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
