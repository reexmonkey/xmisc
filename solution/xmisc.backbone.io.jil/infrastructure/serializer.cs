using Jil;
using reexmonkey.xmisc.core.io.infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace xmisc.backbone.io.jil.infrastructure
{
    public class JilSerializer : TextSerializerBase
    {
        public override TSource Deserialize<TSource>(string data)
        {
            using (var reader = new StringReader(data))
            {
                return JSON.Deserialize<TSource>(data);
            }
        }

        public TSource Deserialize<TSource>(string data, Options options)
        {
            using (var reader = new StringReader(data))
            {
                return JSON.Deserialize<TSource>(data, options);
            }
        }

        public override string Serialize<TSource>(TSource source)
        {
            return JSON.Serialize(source);
        }

        public string Serialize<TSource>(TSource source, Options options)
        {
            return JSON.Serialize(source, options);
        }
    }
}
