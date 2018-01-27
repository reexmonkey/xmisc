using System;
using System.IO;
using System.Threading.Tasks;
using ProtoBuf;
using reexmonkey.xmisc.core.io.serializers;

namespace reexmonkey.xmisc.backbone.io.protobuf.serializers
{
    public class ProtoBufStreamSerializer : StreamSerializerBase
    {
        private readonly int bufferSize;

        public ProtoBufStreamSerializer(int bufferSize)
        {
            if (bufferSize <= 0) throw new ArgumentOutOfRangeException(nameof(bufferSize));
            this.bufferSize = bufferSize;
        }

        public override Stream Serialize<TSource>(TSource source)
        {
            var stream = new MemoryStream();
            Serializer.Serialize(stream, source);
            return stream;
        }

        public override TSource Deserialize<TSource>(Stream data)
        {
            var stream = new MemoryStream();
            data.CopyTo(stream, bufferSize);
            return Serializer.Deserialize<TSource>(stream);
        }

        public override async Task<Stream> SerializeAsync<TSource>(TSource source) =>
            await Task.FromResult(Serialize(source));

        public override async Task<TSource> DeserializeAsync<TSource>(Stream data)
        {
            var stream = new MemoryStream();
            await data.CopyToAsync(stream, bufferSize);
            return await Task.FromResult(Serializer.Deserialize<TSource>(stream));
        }
    }
}
