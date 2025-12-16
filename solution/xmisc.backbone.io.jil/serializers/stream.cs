using reexmonkey.xmisc.core.io.serializers;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.io.jil.serializers
{
    public class JilStreamSerializer : StreamSerializerBase
    {
        private readonly JilTextSerializer inner = new();
        private readonly Encoding encoding;
        private readonly int bufferSize;

        public JilStreamSerializer(Encoding encoding, int bufferSize)
        {
            if (bufferSize <= 0) throw new ArgumentOutOfRangeException(nameof(bufferSize));
            this.encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
            this.bufferSize = bufferSize;
        }

        public override Stream Serialize<TSource>(TSource source)
        {
            var success = false;
            var stream = new MemoryStream();
            try
            {
                using (var writer = new StreamWriter(stream, encoding, bufferSize, true))
                {
                    writer.AutoFlush = true;
                    writer.Write(inner.SerializeAsync(source));
                }
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
            using (var reader = new StreamReader(data, encoding, true, bufferSize))
            {
                return inner.Deserialize<TSource>(reader.ReadToEnd());
            }
        }

        public override async Task<Stream> SerializeAsync<TSource>(TSource source)
        {
            var success = false;
            var stream = new MemoryStream();
            try
            {
                using (var writer = new StreamWriter(stream, encoding, bufferSize, true))
                {
                    writer.AutoFlush = true;
                    await writer.WriteAsync(await inner.SerializeAsync(source));
                }
                success = await Task.FromResult(true);
                return await Task.FromResult(stream);
            }
            finally
            {
                if (!success) stream.Dispose();
            }
        }

        public override async Task<TSource> DeserializeAsync<TSource>(Stream data)
        {
            using (var reader = new StreamReader(data, encoding, true, bufferSize))
            {
                return await inner.DeserializeAsync<TSource>(await reader.ReadToEndAsync());
            }
        }
    }
}