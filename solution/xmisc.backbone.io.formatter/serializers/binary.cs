using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using reexmonkey.xmisc.core.io.serializers;

namespace reexmonkey.xmisc.backbone.io.formatter.serializers
{
    public class BinaryFormatSerializer : BinarySerializerBase
    {
        public override byte[] Serialize<TSource>(TSource source)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, source);
                return stream.ToArray();
            }
        }

        public override TSource Deserialize<TSource>(byte[] format)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                return (TSource)formatter.Deserialize(stream);
            }
        }

        public override async Task<byte[]> SerializeAsync<TSource>(TSource source) => await Task.FromResult(Serialize(source));

        public override async Task<TSource> DeserializeAsync<TSource>(byte[] data) => await Task.FromResult(Deserialize<TSource>(data));
    }
}
