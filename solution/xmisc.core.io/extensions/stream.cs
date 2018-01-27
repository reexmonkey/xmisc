using reexmonkey.xmisc.core.io.infrastructure;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.core.io.extensions
{
    /// <summary>
    /// Extends features of the <see cref="System.IO.Stream"/> class.
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Reads bytes from stream.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <param name="buffersize">The size of the buffer during the read process.</param>
        /// <returns>Bytes read from the stream.</returns>
        public static byte[] ReadBytes(this Stream stream, int buffersize = 4096)
        {
            if (stream is MemoryStream memorystream) return memorystream.ToArray();

            var buffer = new byte[buffersize];
            using (var mstream = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0) mstream.Write(buffer, 0, read);
                return mstream.ToArray();
            }
        }

        /// <summary>
        /// Gets the checksum of the <paramref name="stream"/> object using the provided <paramref name="algorithm"/>.
        /// </summary>
        /// <param name="stream">The stream whose integerity is checked.</param>
        /// <param name="algorithm">
        /// The algorithm to compute the hash of the <paramref name="stream"/> object..
        /// </param>
        /// <returns>The checksum of the stream object.</returns>
        public static string GetChecksum(this Stream stream, HashAlgorithm algorithm)
        {
            var hash = algorithm.ComputeHash(stream);
            return hash.Any() ? BitConverter.ToString(hash) : string.Empty;
        }

        /// <summary>
        /// Serializes the given instance into a memory stream.
        /// </summary>
        /// <typeparam name="TSource">The type of instance to serialize and stream.</typeparam>
        /// <param name="source">The instance to stream.</param>
        /// <param name="serializer">
        /// The serializer that serialiizes <paramref name="source"/> into an array of bytes.
        /// </param>
        /// <returns>A memory stream, to which the serialized bytes are wriiten.</returns>
        public static Stream Stream<TSource>(this TSource source, BinarySerializerBase serializer)
        {
            var stream = new MemoryStream();
            var bytes = serializer.Serialize(source);
            stream.Write(bytes, 0, bytes.Length);
            return stream;
        }

        /// <summary>
        /// Serializes the given instance into a memory stream in an asynchronous operation.
        /// </summary>
        /// <typeparam name="TSource">The type of instance to serialize and stream.</typeparam>
        /// <param name="source">The instance to stream.</param>
        /// <param name="serializer">
        /// The serializer that serialiizes <paramref name="source"/> into an array of bytes.
        /// </param>
        /// <returns>
        /// The asynchronous operation that returns a memory stream, to which the serialized bytes
        /// are wriiten.
        /// </returns>
        public static async Task<Stream> StreamAsync<TSource>(this TSource source, BinarySerializerBase serializer)
        {
            var stream = new MemoryStream();
            var bytes = serializer.Serialize(source);
            await stream.WriteAsync(bytes, 0, bytes.Length);
            return await Task.FromResult(stream);
        }

        /// <summary>
        /// Serializes the given instance into a memory stream.
        /// </summary>
        /// <typeparam name="TSource">The type of instance to serialize and stream.</typeparam>
        /// <param name="source">The instance to stream.</param>
        /// <param name="bufferSize">The size, in bytes, for buffer used in the streaming.</param>
        /// <param name="serializer">
        /// The serializer that serialiizes <paramref name="source"/> into an array of bytes.
        /// </param>
        /// <returns>A memory stream, to which the serialized bytes are wriiten.</returns>
        public static Stream Stream<TSource>(this TSource source, int bufferSize, TextSerializerBase serializer)
        {
            var stream = new MemoryStream();
            using (var writer = new StreamWriter(stream, new UTF8Encoding(false), bufferSize, true) { AutoFlush = true })
            {
                writer.Write(serializer.Serialize(source));
            }
            return stream;
        }

        /// <summary>
        /// Serializes the given instance into a memory stream in an asynchronous operation.
        /// </summary>
        /// <typeparam name="TSource">The type of instance to serialize and stream.</typeparam>
        /// <param name="source">The instance to stream.</param>
        /// <param name="bufferSize">The size, in bytes, for buffer used in the streaming.</param>
        /// <param name="serializer">
        /// The serializer that serialiizes <paramref name="source"/> into an array of bytes.
        /// </param>
        /// <returns>
        /// An asynchronous operation that returns a memory stream, to which the serialized bytes are wriiten.
        /// </returns>
        public static async Task<Stream> StreamAsync<TSource>(this TSource source, int bufferSize, TextSerializerBase serializer)
        {
            var stream = new MemoryStream();
            using (var writer = new StreamWriter(stream, new UTF8Encoding(false), bufferSize, true) { AutoFlush = true })
            {
                await writer.WriteAsync(await serializer.SerializeAsync(source));
            }
            return stream;
        }
    }
}