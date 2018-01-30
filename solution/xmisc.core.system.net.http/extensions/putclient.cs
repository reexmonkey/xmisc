﻿using reexmonkey.xmisc.core.io.serializers;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.core.system.net.http.extensions
{
    public static class HttpPutClientExtensions
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

        #region PUT Methods

        //Put <T> Methods (text serialization)

        public static HttpResponseMessage Put<T>(this HttpClient client, Uri requestUri, T content, TextSerializerBase serializer)
        {
            return client.PutAsync(requestUri, content, serializer).Result;
        }

        public static async Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, Uri requestUri, T content, TextSerializerBase serializer)
        {
            return await client.PutAsync(requestUri, await serializer.AsContentAsync(content));
        }

        public static async Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, Uri requestUri, T content, TextSerializerBase serializer, CancellationToken token)
        {
            return await client.PutAsync(requestUri, await serializer.AsContentAsync(content), token);
        }

        public static HttpResponseMessage Put<T>(this HttpClient client, string requestUri, T content, TextSerializerBase serializer)
        {
            return client.PutAsync(requestUri, content, serializer).Result;
        }

        public static async Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, string requestUri, T content, TextSerializerBase serializer)
        {
            return await client.PutAsync(requestUri, await serializer.AsContentAsync(content));
        }

        public static async Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, string requestUri, T content, TextSerializerBase serializer, CancellationToken token)
        {
            return await client.PutAsync(requestUri, await serializer.AsContentAsync(content), token);
        }

        //Put <T> Methods (binary serialization)

        public static HttpResponseMessage Put<T>(this HttpClient client, Uri requestUri, T content, BinarySerializerBase serializer)
        {
            return client.PutAsync(requestUri, content, serializer).Result;
        }

        public static HttpResponseMessage Put<T>(this HttpClient client, string requestUri, T content, BinarySerializerBase serializer)
        {
            return client.PutAsync(requestUri, content, serializer).Result;
        }

        public static async Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, Uri requestUri, T content, BinarySerializerBase serializer)
        {
            return await client.PutAsync(requestUri, await serializer.AsContentAsync(content));
        }

        public static async Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, Uri requestUri, T content, BinarySerializerBase serializer, CancellationToken token)
        {
            return await client.PutAsync(requestUri, await serializer.AsContentAsync(content), token);
        }

        public static async Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, string requestUri, T content, BinarySerializerBase serializer)
        {
            return await client.PutAsync(requestUri, await serializer.AsContentAsync(content));
        }

        public static async Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, string requestUri, T content, BinarySerializerBase serializer, CancellationToken token)
        {
            return await client.PutAsync(requestUri, await serializer.AsContentAsync(content), token);
        }

        //Put Methods (stream serialization)

        public static HttpResponseMessage Put<T>(this HttpClient client, Uri requestUri, T content, StreamSerializerBase serializer)
        {
            return client.PutAsync(requestUri, content, serializer).Result;
        }

        public static HttpResponseMessage Put<T>(this HttpClient client, string requestUri, T content, StreamSerializerBase serializer)
        {
            return client.PutAsync(requestUri, content, serializer).Result;
        }

        public static async Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, Uri requestUri, T content, StreamSerializerBase serializer)
        {
            using (var stream = await serializer.AsContentAsync(content))
            {
                return await client.PutAsync(requestUri, stream);
            }
        }

        public static async Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, Uri requestUri, T content, StreamSerializerBase serializer, CancellationToken token)
        {
            using (var stream = await serializer.AsContentAsync(content))
            {
                return await client.PutAsync(requestUri, stream, token);
            }
        }

        public static async Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, string requestUri, T content, StreamSerializerBase serializer)
        {
            using (var stream = await serializer.AsContentAsync(content))
            {
                return await client.PutAsync(requestUri, stream);
            }
        }

        public static async Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, string requestUri, T content, StreamSerializerBase serializer, CancellationToken token)
        {
            using (var stream = await serializer.AsContentAsync(content))
            {
                return await client.PutAsync(requestUri, stream, token);
            }
        }

        #endregion PUT Methods
    }
}