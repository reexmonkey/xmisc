using System.IO;

namespace reexmonkey.xmisc.core.io.serializers
{
    /// <summary>
    /// Specifies an abstract serializer that serializes objects to <see cref="Stream"/> s.
    /// </summary>
    public abstract class StreamSerializerBase : SerializerBase<Stream>
    {
    }
}