using reexmonkey.xmisc.core.io.serializers;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.core.system.net.http.extensions
{
    public static class HttpPatchClientExtensions
    {
    
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
            return await client.PatchAsync(requestUri, await serializer.AsStringContentAsync(content));
        }

        public static async Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, Uri requestUri, T content, TextSerializerBase serializer, CancellationToken token)
        {
            return await client.PatchAsync(requestUri, await serializer.AsStringContentAsync(content), token);
        }

        public static HttpResponseMessage Patch<T>(this HttpClient client, string requestUri, T content, TextSerializerBase serializer)
        {
            return client.PatchAsync(requestUri, content, serializer).Result;
        }

        public static async Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, string requestUri, T content, TextSerializerBase serializer)
        {
            return await client.PatchAsync(requestUri, await serializer.AsStringContentAsync(content));
        }

        public static async Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, string requestUri, T content, TextSerializerBase serializer, CancellationToken token)
        {
            return await client.PatchAsync(requestUri, await serializer.AsStringContentAsync(content), token);
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
            return await client.PatchAsync(requestUri, await serializer.AsStringContentAsync(content));
        }

        public static async Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, Uri requestUri, T content, BinarySerializerBase serializer, CancellationToken token)
        {
            return await client.PatchAsync(requestUri, await serializer.AsStringContentAsync(content), token);
        }

        public static async Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, string requestUri, T content, BinarySerializerBase serializer)
        {
            return await client.PatchAsync(requestUri, await serializer.AsStringContentAsync(content));
        }

        public static async Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, string requestUri, T content, BinarySerializerBase serializer, CancellationToken token)
        {
            return await client.PatchAsync(requestUri, await serializer.AsStringContentAsync(content), token);
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
            using (var stream = await serializer.AsStringContentAsync(content))
            {
                return await client.PatchAsync(requestUri, stream);
            }
        }

        public static async Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, Uri requestUri, T content, StreamSerializerBase serializer, CancellationToken token)
        {
            using (var stream = await serializer.AsStringContentAsync(content))
            {
                return await client.PatchAsync(requestUri, stream, token);
            }
        }

        public static async Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, string requestUri, T content, StreamSerializerBase serializer)
        {
            using (var stream = await serializer.AsStringContentAsync(content))
            {
                return await client.PatchAsync(requestUri, stream);
            }
        }

        public static async Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, string requestUri, T content, StreamSerializerBase serializer, CancellationToken token)
        {
            using (var stream = await serializer.AsStringContentAsync(content))
            {
                return await client.PatchAsync(requestUri, stream, token);
            }
        }

        #endregion PATCH Methods
    }
}