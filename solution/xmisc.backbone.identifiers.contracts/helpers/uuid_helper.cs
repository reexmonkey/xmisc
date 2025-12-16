using System;

namespace reexmonkey.xmisc.backbone.identifiers.contracts.helpers
{
    internal static class UuidHelper
    {
        internal static void Swap(ref byte first, ref byte second)
        {
            (second, first) = (first, second);
        }
        internal static byte[] SwapByteOrder(this byte[] bytes)
        {
            var buffer = new byte[bytes.Length];
            Array.Copy(bytes, buffer, bytes.Length);
            Swap(ref buffer[0], ref buffer[3]);
            Swap(ref buffer[1], ref buffer[2]);
            Swap(ref buffer[4], ref buffer[5]);
            Swap(ref buffer[6], ref buffer[7]);
            return buffer;
        }
    }
}
