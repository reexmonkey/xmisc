using System;
using reexmonkey.xmisc.backbone.identifiers.contracts.infrastructure;

namespace reexmonkey.xmisc.backbone.identifiers.contracts.extensions
{
    public static class GuidExtensions
    {
        public static SequentialGuid ForSqlServer(this SequentialGuid guid)
        {
            var bytes = guid.ToByteArray();
            Array.Reverse(bytes, 0, 4);
            Array.Reverse(bytes, 4, 2);
            Array.Reverse(bytes, 6, 2);
            return new SequentialGuid(bytes);
        }
    }
}
