using MsgPack.Serialization;
using reexmonkey.xmisc.core.io.infrastructure;
using System.IO;

namespace reexmonkey.xmisc.backbone.io.messagepack.infrastructure
{
    public class MessagePackSerializer : BinarySerializerBase
    {
        public override byte[] Serialize<TSource>(TSource source)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = SerializationContext.Default.GetSerializer<TSource>();
                serializer.Pack(stream, source);
                return stream.ToArray();
            }
        }

        public override TSource Deserialize<TSource>(byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                var serializer = SerializationContext.Default.GetSerializer<TSource>();
                return serializer.Unpack(stream);
            }
        }
    }
}
