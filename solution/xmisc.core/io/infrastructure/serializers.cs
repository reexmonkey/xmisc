using System;
using System.Text;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.core.io.infrastructure
{
    public abstract class SerializerBase<TData>: IDisposable
    {
        public abstract TData Serialize<TSource>(TSource source);

        public abstract TSource Deserialize<TSource>(TData format);

        public abstract bool TrySerialize<TSource>(TSource source, out TData format);

        public abstract bool TryDeserialize<TSource>(TData format, out TSource source);

        public abstract Task<TData> SerializeAsync<TSource>(TSource source);

        public abstract Task<TSource> DeserializeAsync<TSource>(TData format);

        protected abstract void Dispose(bool disposing);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public abstract class BinarySerializerBase: SerializerBase<byte[]>
    {
        
    }

    public abstract class TextSerializerBase: SerializerBase<string>
    {



    }


}
