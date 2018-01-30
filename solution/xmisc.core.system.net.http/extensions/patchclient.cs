using reexmonkey.xmisc.core.io.serializers;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.core.system.net.http.extensions
{
    public static class HttpPatchClientExtensions
    {
        #region Conversion Methods

        private static async Task<StringContent> AsContentAsync<T>(this TextSerializerBase serializer, T instance)
        {
            return new StringContent(await serializer.SerializeAsync(instance));
        }

        private static async Task<ByteArrayContent> AsContentAsync<T>(this BinarySerializerBase serializer, T content)
        {
            return new ByteArrayContent(await serializer.SerializeAsync(content));
        }

        private static async Task<StreamContent> AsContentAsync<T>(this StreamSerializerBase serializer, T content)
        {
            return new StreamContent(await serializer.SerializeAsync(content));
        }

        #endregion Conversion Methods

        #region PATCH Methods

        public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, Uri requestUri, HttpContent content)
        {
            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content
            };
            return await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
        }

        public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, Uri requestUri, HttpContent content, CancellationToken token)
        {
            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content,
            };
            return await client.SendAsync(request, HttpCompletionOption.ResponseContentRead, token);
        }

        public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent content)
        {
            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content
            };
            return await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
        }

        public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent content, CancellationToken token)
        {
            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content,
            };
            return await client.SendAsync(request, HttpCompletionOption.ResponseContentRead, token);
        }

        //Put <T> Methods (text serialization)

        public static HttpResponseMessage Patch<T>(this HttpClient client, Uri requestUri, T content, TextSerializerBase serializer)
        {
            return client.PatchAsync(requestUri, content, serializer).Result;
        }

        public static async Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, Uri requestUri, T content, TextSerializerBase serializer)
        {
            return await client.PatchAsync(requestUri, await serializer.AsContentAsync(content));
        }

        public static async Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, Uri requestUri, T content, TextSerializerBase serializer, CancellationToken token)
        {
            return await client.PatchAsync(requestUri, await serializer.AsContentAsync(content), token);
        }

        public static HttpResponseMessage Patch<T>(this HttpClient client, string requestUri, T content, TextSerializerBase serializer)
        {
            return client.PatchAsync(requestUri, content, serializer).Result;
        }

        public static async Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, string requestUri, T content, TextSerializerBase serializer)
        {
            return await client.PatchAsync(requestUri, await serializer.AsContentAsync(content));
        }

        public static async Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, string requestUri, T content, TextSerializerBase serializer, CancellationToken token)
        {
            return await client.PatchAsync(requestUri, await serializer.AsContentAsync(content), token);
        }

        //Patch <T> Methods (binary serialization)

        public static HttpResponseMessage Patch<T>(this HttpClient client, Uri requestUri, T content, BinarySerializerBase serializer)
        {
            return client.PatchAsync(requestUri, content, serializer).Result;
        }

        public static HttpResponseMessage Patch<T>(this HttpClient client, string requestUri, T content, BinarySerializerBase serializer)
        {
            return client.PatchAsync(requestUri, content, serializer).Result;
        }

        public static async Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, Uri requestUri, T content, BinarySerializerBase serializer)
        {
            return await client.PatchAsync(requestUri, await serializer.AsContentAsync(content));
        }

        public static async Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, Uri requestUri, T content, BinarySerializerBase serializer, CancellationToken token)
        {
            return await client.PatchAsync(requestUri, await serializer.AsContentAsync(content), token);
        }

        public static async Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, string requestUri, T content, BinarySerializerBase serializer)
        {
            return await client.PatchAsync(requestUri, await serializer.AsContentAsync(content));
        }

        public static async Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, string requestUri, T content, BinarySerializerBase serializer, CancellationToken token)
        {
            return await client.PatchAsync(requestUri, await serializer.AsContentAsync(content), token);
        }

        //Patch Methods (stream serialization)

        public static HttpResponseMessage Patch<T>(this HttpClient client, Uri requestUri, T content, StreamSerializerBase serializer)
        {
            return client.PatchAsync(requestUri, content, serializer).Result;
        }

        public static HttpResponseMessage Patch<T>(this HttpClient client, string requestUri, T content, StreamSerializerBase serializer)
        {
            return client.PatchAsync(requestUri, content, serializer).Result;
        }

        public static async Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, Uri requestUri, T content, StreamSerializerBase serializer)
        {
            using (var stream = await serializer.AsContentAsync(content))
            {
                return await client.PatchAsync(requestUri, stream);
            }
        }

        public static async Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, Uri requestUri, T content, StreamSerializerBase serializer, CancellationToken token)
        {
            using (var stream = await serializer.AsContentAsync(content))
            {
                return await client.PatchAsync(requestUri, stream, token);
            }
        }

        public static async Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, string requestUri, T content, StreamSerializerBase serializer)
        {
            using (var stream = await serializer.AsContentAsync(content))
            {
                return await client.PatchAsync(requestUri, stream);
            }
        }

        public static async Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, string requestUri, T content, StreamSerializerBase serializer, CancellationToken token)
        {
            using (var stream = await serializer.AsContentAsync(content))
            {
                return await client.PatchAsync(requestUri, stream, token);
            }
        }

        #endregion PATCH Methods
    }
}