using MsgPack.Serialization;
using System.IO;
using System.Threading.Tasks;
using reexmonkey.xmisc.core.io.serializers;

namespace reexmonkey.xmisc.backbone.io.messagepack.serializers
{
    /// <summary>
    /// Represents a binary serializer that uses MessagePack serialization.
    /// </summary>
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

        public override async Task<byte[]> SerializeAsync<TSource>(TSource source)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = SerializationContext.Default.GetSerializer<TSource>();
                await serializer.PackAsync(stream, source);
                return await Task.FromResult(stream.ToArray());
            }
        }

        public override async Task<TSource> DeserializeAsync<TSource>(byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                var serializer = SerializationContext.Default.GetSerializer<TSource>();
                return await serializer.UnpackAsync(stream);
            }
        }
    }
}
