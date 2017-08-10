﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using reexmonkey.xmisc.core.io.infrastructure;

namespace reexmonkey.xmisc.backbone.io.formatter.infrastructure
{
    public class BinaryFormatSerializer : BinarySerializerBase
    {
        public override byte[] Serialize<TSource>(TSource source)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter(); ;
                formatter.Serialize(stream, source);
                return stream.ToArray();
            }
        }

        public override TSource Deserialize<TSource>(byte[] format)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter(); ;
                return (TSource)formatter.Deserialize(stream);
            }
        }

    }
}
