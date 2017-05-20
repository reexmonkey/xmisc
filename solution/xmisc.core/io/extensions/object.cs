using System;
using System.Runtime.Serialization;
using reexmonkey.xmisc.core.io.infrastructure;

namespace reexmonkey.xmisc.core.io.extensions
{
    public static class ObjectExtensions
    {
        public static TSource Clone<TSource, TSerializer, TData>(this TSource source, TSerializer serializer)
            where TSerializer: SerializerBase<TData>
        {
            TSource clone;
            using (serializer)
            {
                var data = serializer.Serialize(source);
                clone = serializer.Deserialize<TSource>(data);
            }
            return clone;
        }
    }
}
