using System.Threading.Tasks;
using reexmonkey.xmisc.core.io.serializers;

namespace reexmonkey.xmisc.core.io.extensions
{
    /// <summary>
    /// Provides common I/O extensions to generic values.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Clones the given <paramref name="source"/> by using the provided <paramref name="serializer"/>.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the input data consumed by the <paramref name="serializer"/>.
        /// </typeparam>
        /// <typeparam name="TData">The type of output data produced by the <paramref name="serializer"/>.</typeparam>
        /// <param name="source">The source object to deep clone.</param>
        /// <param name="serializer">The serializer that clones the <paramref name="source"/> deeply.</param>
        /// <returns>A deep clone of the <paramref name="source"/>.</returns>
        public static TSource DeepClone<TSource, TData>(this TSource source, SerializerBase<TData> serializer)
        {
            var data = serializer.Serialize(source);
            return serializer.Deserialize<TSource>(data);
        }

        /// <summary>
        /// Asynchronously clones the given <paramref name="source"/> by using the provided <paramref name="serializer"/>.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the input data consumed by the <paramref name="serializer"/>.
        /// </typeparam>
        /// <typeparam name="TData">The type of output data produced by the <paramref name="serializer"/>.</typeparam>
        /// <param name="source">The source object to deep clone.</param>
        /// <param name="serializer">The serializer that clones the <paramref name="source"/> deeply.</param>
        /// <returns>A deep clone of the <paramref name="source"/>.</returns>
        public static async Task<TSource> DeepCloneAsync<TSource, TData>(this TSource source, SerializerBase<TData> serializer)
        {
            var data = await serializer.SerializeAsync(source);
            return await serializer.DeserializeAsync<TSource>(data);
        }
    }
}
