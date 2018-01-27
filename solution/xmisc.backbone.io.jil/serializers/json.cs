using System.Threading.Tasks;
using Jil;
using reexmonkey.xmisc.core.io.serializers;

namespace reexmonkey.xmisc.backbone.io.jil.serializers
{
    public class JilSerializer : TextSerializerBase
    {
        public override TSource Deserialize<TSource>(string data) => JSON.Deserialize<TSource>(data, Options.IncludeInherited);

        public TSource Deserialize<TSource>(string data, Options options) => JSON.Deserialize<TSource>(data, options);

        public override async Task<TSource> DeserializeAsync<TSource>(string data) => await Task.FromResult(Deserialize<TSource>(data));

        public TSource DeserializeDynamic<TSource>(string data) => JSON.DeserializeDynamic(data);

        public TSource DeserializeDynamic<TSource>(string data, Options options) => JSON.Deserialize<TSource>(data, options);

        public override string Serialize<TSource>(TSource source) => JSON.Serialize(source, Options.IncludeInherited);

        public string Serialize<TSource>(TSource source, Options options) => JSON.Serialize(source, options);

        public override async Task<string> SerializeAsync<TSource>(TSource source) => await Task.FromResult(Serialize(source));

        public string SerializeDynamic<TSource>(TSource source) => JSON.SerializeDynamic(source, Options.IncludeInherited);

        public string SerializeDynamic<TSource>(TSource source, Options options) => JSON.SerializeDynamic(source, options);
    }
}
