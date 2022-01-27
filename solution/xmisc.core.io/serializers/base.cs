using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.core.io.serializers
{
    /// <summary>
    /// Specifies an abstract serializer that serializes objects to the <typeparamref name="TData"/> format.
    /// </summary>
    /// <typeparam name="TData">
    /// The type of the data that the serializer produces from the serialization process.
    /// </typeparam>
    public abstract class SerializerBase<TData>
    {
        /// <summary>
        /// Serializes a specified object into the data of type <typeparamref name="TData"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of source object to serialize.</typeparam>
        /// <param name="source">The source object to serialize.</param>
        /// <returns>The result of the serialization.</returns>
        public abstract TData Serialize<TSource>(TSource source);

        /// <summary>
        /// Deserializes the specified data into an object of type <typeparamref name="TSource"/>.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of object that the serializer produces from the deserialization.
        /// </typeparam>
        /// <param name="data">The data to deserialize.</param>
        /// <returns>The result of the deserialization</returns>
        public abstract TSource Deserialize<TSource>(TData data);

        /// <summary>
        /// Attempts to serialize a specified object into the data of type <typeparamref name="TData"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of source object to convert.</typeparam>
        /// <param name="source">The source object to convert.</param>
        /// <param name="data">The result of the serialization.</param>
        /// <returns>True if the serialization succeeds; otherwise false.</returns>
        public bool TrySerialize<TSource>(TSource source, out TData data)
        {
            try
            {
                data = Serialize(source);
                return true;
            }
            catch (ArgumentNullException)
            {
                data = default;
                return false;
            }
            catch (SerializationException)
            {
                data = default;
                return false;
            }
            catch (NotSupportedException)
            {
                data = default;
                return false;
            }
            catch (Exception)
            {
                data = default;
                return false;
            }
        }

        /// <summary>
        /// Attempts to deserialize the specified data into an object of type <typeparamref name="TSource"/>.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of object that the serializer produces from the deserialization.
        /// </typeparam>
        /// <param name="data">The result of the deserialization.</param>
        /// <param name="source">The source.</param>
        /// <returns>True if the deserialization succeeds; otherwise false.</returns>
        public bool TryDeserialize<TSource>(TData data, out TSource source)
        {
            try
            {
                source = Deserialize<TSource>(data);
                return true;
            }
            catch (ArgumentNullException)
            {
                source = default;
                return false;
            }
            catch (SerializationException)
            {
                source = default;
                return false;
            }
            catch (NotSupportedException)
            {
                source = default;
                return false;
            }
            catch (Exception)
            {
                source = default;
                return false;
            }
        }

        /// <summary>
        /// Asynchronously serializes a specified object into the data of type <typeparamref name="TData"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of source object to serialize.</typeparam>
        /// <param name="source">The source object to serialize.</param>
        /// <returns>The result of the serialization.</returns>
        public abstract Task<TData> SerializeAsync<TSource>(TSource source);

        /// <summary>
        /// Asynchronously deserializes the specified data into an object of type <typeparamref name="TSource"/>.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of object that the serializer produces from the deserialization.
        /// </typeparam>
        /// <param name="data">The data to deserialize.</param>
        /// <returns>The result of the deserialization</returns>
        public abstract Task<TSource> DeserializeAsync<TSource>(TData data);
    }
}
