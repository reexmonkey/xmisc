using reexmonkey.xmisc.backbone.identifiers.contracts.helpers;
using reexmonkey.xmisc.backbone.identifiers.contracts.models;

namespace reexmonkey.xmisc.backbone.identifiers.contracts.extensions
{
    /// <summary>
    /// Extends the features of the <see cref="SequentialGuid"/> structure.
    /// </summary>
    public static class SequentialGuidExtensions
    {
        /// <summary>
        /// Converts the byte order of the specified sequential GUID to Big Endian.
        /// </summary>
        /// <param name="guid">.</param>
        /// <returns>A sequential GUID in network byte order.</returns>
        public static SequentialGuid ToNetworkOrder(this SequentialGuid guid)
        {
            var data = guid.ToByteArray();
            var swapped = data.SwapByteOrder();
            return new SequentialGuid(swapped);
        }
    }
}
