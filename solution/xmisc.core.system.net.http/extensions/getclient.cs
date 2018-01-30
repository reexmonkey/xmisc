using reexmonkey.xmisc.core.io.serializers;
using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.core.system.net.http.extensions
{
    public static class HttpGetClientExtensions
    {
        private static async Task<T> DeserializeResponseAsync<T>(this TextSerializerBase serializer, HttpResponseMessage response)
        {
            var textual = await response.Content.ReadAsStringAsync();
            return await serializer.DeserializeAsync<T>(textual);
        }

        private static async Task<T> DeserializeResponseAsync<T>(this BinarySerializerBase serializer, HttpResponseMessage response)
        {
            return await serializer.DeserializeAsync<T>(await response.Content.ReadAsByteArrayAsync());
        }

        private static async Task<T> DeserializeResponseAsync<T>(this StreamSerializerBase serializer, HttpResponseMessage response)
        {
            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                return await serializer.DeserializeAsync<T>(stream);
            }
        }

        //Get<T> Methods (text serialization)

        public static T Get<T>(this HttpClient client, Uri requestUri, TextSerializerBase serializer)
        {
            return client.GetAsync<T>(requestUri, serializer).Result;
        }

        public static T Get<T>(this HttpClient client, string requestUri, TextSerializerBase serializer)
        {
            return client.GetAsync<T>(requestUri, serializer).Result;
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, Uri requestUri, TextSerializerBase serializer)
        {
            var response = await client.GetAsync(requestUri, HttpCompletionOption.ResponseContentRead);
            return await serializer.DeserializeResponseAsync<T>(response);
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, Uri requestUri, TextSerializerBase serializer, CancellationToken token)
        {
            return await serializer.DeserializeResponseAsync<T>(await client.GetAsync(requestUri, HttpCompletionOption.ResponseContentRead, token));
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, string requestUri, TextSerializerBase serializer)
        {
            return await serializer.DeserializeResponseAsync<T>(await client.GetAsync(requestUri, HttpCompletionOption.ResponseContentRead));
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, string requestUri, TextSerializerBase serializer, CancellationToken token)
        {
            return await serializer.DeserializeResponseAsync<T>(await client.GetAsync(requestUri, HttpCompletionOption.ResponseContentRead, token));
        }

        //Get<T> Methods (binary serialization)

        public static T Get<T>(this HttpClient client, Uri requestUri, BinarySerializerBase serializer)
        {
            return client.GetAsync<T>(requestUri, serializer).Result;
        }

        public static T Get<T>(this HttpClient client, string requestUri, BinarySerializerBase serializer)
        {
            return client.GetAsync<T>(requestUri, serializer).Result;
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, Uri requestUri, BinarySerializerBase serializer)
        {
            return await serializer.DeserializeResponseAsync<T>(await client.GetAsync(requestUri, HttpCompletionOption.ResponseContentRead));
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, Uri requestUri, BinarySerializerBase serializer, CancellationToken token)
        {
            var response = await client.GetAsync(requestUri, HttpCompletionOption.ResponseContentRead, token);
            return await serializer.DeserializeResponseAsync<T>(response);
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, string requestUri, BinarySerializerBase serializer)
        {
            var response = await client.GetAsync(requestUri, HttpCompletionOption.ResponseContentRead);
            return await serializer.DeserializeResponseAsync<T>(response);
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, string requestUri, BinarySerializerBase serializer, CancellationToken token)
        {
            var response = await client.GetAsync(requestUri, HttpCompletionOption.ResponseContentRead, token);
            return await serializer.DeserializeResponseAsync<T>(response);
        }

        //Get<T> Methods (stream serialization)

        public static T Get<T>(this HttpClient client, Uri requestUri, StreamSerializerBase serializer)
        {
            return client.GetAsync<T>(requestUri, serializer).Result;
        }

        public static T Get<T>(this HttpClient client, string requestUri, StreamSerializerBase serializer)
        {
            return client.GetAsync<T>(requestUri, serializer).Result;
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, Uri requestUri, StreamSerializerBase serializer)
        {
            return await serializer.DeserializeResponseAsync<T>(await client.GetAsync(requestUri, HttpCompletionOption.ResponseContentRead));
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, Uri requestUri, StreamSerializerBase serializer, CancellationToken token)
        {
            return await serializer.DeserializeResponseAsync<T>(await client.GetAsync(requestUri, HttpCompletionOption.ResponseContentRead, token));
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, string requestUri, StreamSerializerBase serializer)
        {
            return await serializer.DeserializeResponseAsync<T>(await client.GetAsync(requestUri, HttpCompletionOption.ResponseContentRead));
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, string requestUri, StreamSerializerBase serializer, CancellationToken token)
        {
            return await serializer.DeserializeResponseAsync<T>(await client.GetAsync(requestUri, HttpCompletionOption.ResponseContentRead, token));
        }

        //GetString Methods

        public static string GetString(this HttpClient client, Uri requestUri)
        {
            return client.GetStringAsync(requestUri).Result;
        }

        public static string GetString(this HttpClient client, string requestUri)
        {
            return client.GetStringAsync(requestUri).Result;
        }

        public static string GetString(this HttpClient client, Uri requestUri, TextSerializerBase serializer)
        {
            return client.GetAsync<string>(requestUri, serializer).Result;
        }

        public static string GetString(this HttpClient client, string requestUri, TextSerializerBase serializer)
        {
            return client.GetAsync<string>(requestUri, serializer).Result;
        }

        public static async Task<string> GetStringAsync(this HttpClient client, Uri requestUri, TextSerializerBase serializer)
        {
            return await client.GetAsync<string>(requestUri, serializer);
        }

        public static async Task<string> GetStringAsync(this HttpClient client, Uri requestUri, TextSerializerBase serializer, CancellationToken token)
        {
            return await client.GetAsync<string>(requestUri, serializer, token);
        }

        public static async Task<string> GetStringAsync(this HttpClient client, string requestUri, TextSerializerBase serializer)
        {
            return await client.GetAsync<string>(requestUri, serializer);
        }

        public static async Task<string> GetStringAsync(this HttpClient client, string requestUri, TextSerializerBase serializer, CancellationToken token)
        {
            return await client.GetAsync<string>(requestUri, serializer, token);
        }

        //GetByteArray Methods

        public static byte[] GetByteArray(this HttpClient client, Uri requestUri)
        {
            return client.GetByteArrayAsync(requestUri).Result;
        }

        public static byte[] GetByteArray(this HttpClient client, string requestUri)
        {
            return client.GetByteArrayAsync(requestUri).Result;
        }

        public static byte[] GetByteArray(this HttpClient client, Uri requestUri, BinarySerializerBase serializer)
        {
            return client.GetByteArrayAsync(requestUri, serializer).Result;
        }

        public static byte[] GetByteArray(this HttpClient client, string requestUri, BinarySerializerBase serializer)
        {
            return client.GetByteArrayAsync(requestUri, serializer).Result;
        }

        public static async Task<byte[]> GetByteArrayAsync(this HttpClient client, string requestUri, BinarySerializerBase serializer)
        {
            return await client.GetAsync<byte[]>(requestUri, serializer);
        }

        public static async Task<byte[]> GetByteArrayAsync(this HttpClient client, Uri requestUri, BinarySerializerBase serializer)
        {
            return await client.GetAsync<byte[]>(requestUri, serializer);
        }

        public static async Task<byte[]> GetByteArrayAsync(this HttpClient client, Uri requestUri, BinarySerializerBase serializer, CancellationToken token)
        {
            return await client.GetAsync<byte[]>(requestUri, serializer, token);
        }

        public static async Task<byte[]> GetByteArray(this HttpClient client, string requestUri, BinarySerializerBase serializer, CancellationToken token)
        {
            return await client.GetAsync<byte[]>(requestUri, serializer, token);
        }

        //GetStream Methods

        public static Stream GetStream(this HttpClient client, Uri requestUri)
        {
            return client.GetStreamAsync(requestUri).Result;
        }

        public static Stream GetStream(this HttpClient client, string requestUri)
        {
            return client.GetStreamAsync(requestUri).Result;
        }

        public static Stream GetStream(this HttpClient client, Uri requestUri, StreamSerializerBase serializer)
        {
            return client.GetStreamAsync(requestUri, serializer).Result;
        }

        public static Stream GetStream(this HttpClient client, string requestUri, StreamSerializerBase serializer)
        {
            return client.GetStreamAsync(requestUri, serializer).Result;
        }

        public static async Task<Stream> GetStreamAsync(this HttpClient client, string requestUri, StreamSerializerBase serializer)
        {
            return await client.GetAsync<Stream>(requestUri, serializer);
        }

        public static async Task<Stream> GetStreamAsync(this HttpClient client, Uri requestUri, StreamSerializerBase serializer)
        {
            return await client.GetAsync<Stream>(requestUri, serializer);
        }

        public static async Task<Stream> GetStreamAsync(this HttpClient client, Uri requestUri, StreamSerializerBase serializer, CancellationToken token)
        {
            return await client.GetAsync<Stream>(requestUri, serializer, token);
        }

        public static async Task<Stream> GetStream(this HttpClient client, string requestUri, StreamSerializerBase serializer, CancellationToken token)
        {
            return await client.GetAsync<Stream>(requestUri, serializer, token);
        }
    }
}