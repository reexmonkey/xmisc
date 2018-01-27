using System;
using System.IO;
using System.Threading.Tasks;
using MsgPack.Serialization;
using reexmonkey.xmisc.core.io.serializers;

namespace reexmonkey.xmisc.backbone.io.messagepack.serializers
{
    public class MessagePackStreamSerializer : StreamSerializerBase
    {
        private readonly int bufferSize;

        public MessagePackStreamSerializer(int bufferSize)
        {
            if (bufferSize <= 0) throw new ArgumentOutOfRangeException(nameof(bufferSize));
            this.bufferSize = bufferSize;
        }

        public override Stream Serialize<TSource>(TSource source)
        {
            var stream = new MemoryStream();
            var serializer = SerializationContext.Default.GetSerializer<TSource>();
            serializer.Pack(stream, source);
            return stream;
        }

        public override TSource Deserialize<TSource>(Stream data)
        {
            var stream = new MemoryStream();
            data.CopyTo(stream, bufferSize);
            var serializer = SerializationContext.Default.GetSerializer<TSource>();
            return serializer.Unpack(stream);
        }

        public override async Task<Stream> SerializeAsync<TSource>(TSource source)
        {
            var stream = new MemoryStream();
            var serializer = SerializationContext.Default.GetSerializer<TSource>();
            await serializer.PackAsync(stream, source);
            return await Task.FromResult(stream);
        }

        public override async Task<TSource> DeserializeAsync<TSource>(Stream data)
        {
            var stream = new MemoryStream();
            await data.CopyToAsync(stream, bufferSize);
            var serializer = SerializationContext.Default.GetSerializer<TSource>();
            return await serializer.UnpackAsync(stream);
        }
    }
}
