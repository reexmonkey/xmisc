using System;
using System.IO;
using System.Threading.Tasks;
using reexmonkey.xmisc.core.io.extensions;
using reexmonkey.xmisc.core.io.serializers;

namespace reexmonkey.xmisc.backbone.io.jil.serializers
{
    public class JilStreamSerializer : StreamSerializerBase
    {
        private readonly JilSerializer textSerializer = new JilSerializer();
        private readonly BinarySerializerBase binarySerializer;
        private readonly int bufferSize;

        public JilStreamSerializer(int bufferSize, BinarySerializerBase binarySerializer)
        {
            if (bufferSize <= 0) throw new ArgumentOutOfRangeException(nameof(bufferSize));
            this.binarySerializer = binarySerializer ?? throw new ArgumentNullException(nameof(binarySerializer));
            this.bufferSize = bufferSize;
        }

        public override Stream Serialize<TSource>(TSource source)
        {
            return source.Stream(bufferSize, textSerializer);
        }

        public override TSource Deserialize<TSource>(Stream data)
        {
            var bytes = data.ReadBytes(bufferSize);
            return binarySerializer.Deserialize<TSource>(bytes);
        }

        public override async Task<Stream> SerializeAsync<TSource>(TSource source)
        {
            return await source.StreamAsync(bufferSize, textSerializer);
        }

        public override async Task<TSource> DeserializeAsync<TSource>(Stream data)
        {
            var bytes = data.ReadBytes(bufferSize);
            return await binarySerializer.DeserializeAsync<TSource>(bytes);
        }
    }
}
