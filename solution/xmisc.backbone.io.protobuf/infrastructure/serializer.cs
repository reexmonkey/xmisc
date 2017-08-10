using ProtoBuf;
using reexmonkey.xmisc.core.io.infrastructure;
using System.IO;

namespace reexmonkey.xmisc.backbone.io.protobuf.infrastructure
{
    public class ProtoBufSerializer : BinarySerializerBase
    {
        public override byte[] Serialize<TSource>(TSource source)
        {
            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, source);
                return stream.ToArray();
            }
        }

        public override TSource Deserialize<TSource>(byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                return Serializer.Deserialize<TSource>(stream);
            }
        }
    }
}
