using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using reexmonkey.xmisc.core.io.serializers;

namespace reexmonkey.xmisc.backbone.io.formatter.serializers
{
    public class StreamFormatSerializer : StreamSerializerBase
    {
        private readonly int bufferSize;

        public StreamFormatSerializer(int bufferSize)
        {
            if (bufferSize <= 0) throw new ArgumentOutOfRangeException(nameof(bufferSize));
            this.bufferSize = bufferSize;
        }

        public override Stream Serialize<TSource>(TSource source)
        {
            bool success = false;
            var stream = new MemoryStream();
            try
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, source);
                success = true;
                return stream;
            }
            finally
            {
                if (!success) stream.Dispose();
            }
        }

        public override TSource Deserialize<TSource>(Stream data)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                data.CopyTo(stream, bufferSize);
                return (TSource)formatter.Deserialize(stream);
            }
        }

        public override async Task<Stream> SerializeAsync<TSource>(TSource source) => await Task.FromResult(Serialize(source));

        public override async Task<TSource> DeserializeAsync<TSource>(Stream data)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                await data.CopyToAsync(stream, bufferSize);
                return await Task.FromResult((TSource)formatter.Deserialize(stream));
            }
        }
    }
}
