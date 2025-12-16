using ProtoBuf;
using System.IO;
using System.Threading.Tasks;
using reexmonkey.xmisc.core.io.serializers;

namespace reexmonkey.xmisc.backbone.io.protobuf.serializers
{
    public class ProtoBufSerializer : BinarySerializerBase
    {
        public override byte[] Serialize<TSource>(TSource source)
        {
            using var stream = new MemoryStream();
            Serializer.Serialize(stream, source);
            return stream.ToArray();
        }

        public override TSource Deserialize<TSource>(byte[] data)
        {
            using var stream = new MemoryStream(data);
            return Serializer.Deserialize<TSource>(stream);
        }

        public async override Task<byte[]> SerializeAsync<TSource>(TSource source) => await Task.FromResult(Serialize(source));

        public async override Task<TSource> DeserializeAsync<TSource>(byte[] data) => await Task.FromResult(Deserialize<TSource>(data));
    }
}
