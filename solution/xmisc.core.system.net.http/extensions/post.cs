﻿using reexmonkey.xmisc.core.io.serializers;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.core.system.net.http.extensions
{
    public static class HttpPostClientExtensions
    {
        //Post <T> Methods (text serialization)

        public static HttpResponseMessage Post<T>(this HttpClient client, Uri requestUri, T content, TextSerializerBase serializer)
        {
            return client.PostAsync(requestUri, content, serializer).Result;
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, Uri requestUri, T content, TextSerializerBase serializer)
        {
            return await client.PostAsync(requestUri, await serializer.AsStringContentAsync(content));
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, Uri requestUri, T content, TextSerializerBase serializer, CancellationToken token)
        {
            return await client.PostAsync(requestUri, await serializer.AsStringContentAsync(content), token);
        }

        public static HttpResponseMessage Post<T>(this HttpClient client, string requestUri, T content, TextSerializerBase serializer)
        {
            return client.PostAsync(requestUri, content, serializer).Result;
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string requestUri, T content, TextSerializerBase serializer)
        {
            return await client.PostAsync(requestUri, await serializer.AsStringContentAsync(content));
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string requestUri, T content, TextSerializerBase serializer, CancellationToken token)
        {
            return await client.PostAsync(requestUri, await serializer.AsStringContentAsync(content), token);
        }

        //Post <T> Methods (binary serialization)

        public static HttpResponseMessage Post<T>(this HttpClient client, Uri requestUri, T content, BinarySerializerBase serializer)
        {
            return client.PostAsync(requestUri, content, serializer).Result;
        }

        public static HttpResponseMessage Post<T>(this HttpClient client, string requestUri, T content, BinarySerializerBase serializer)
        {
            return client.PostAsync(requestUri, content, serializer).Result;
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, Uri requestUri, T content, BinarySerializerBase serializer)
        {
            return await client.PostAsync(requestUri, await serializer.AsStringContentAsync(content));
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, Uri requestUri, T content, BinarySerializerBase serializer, CancellationToken token)
        {
            return await client.PostAsync(requestUri, await serializer.AsStringContentAsync(content), token);
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string requestUri, T content, BinarySerializerBase serializer)
        {
            return await client.PostAsync(requestUri, await serializer.AsStringContentAsync(content));
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string requestUri, T content, BinarySerializerBase serializer, CancellationToken token)
        {
            return await client.PostAsync(requestUri, await serializer.AsStringContentAsync(content), token);
        }

        //Post Methods (stream serialization)

        public static HttpResponseMessage Post<T>(this HttpClient client, Uri requestUri, T content, StreamSerializerBase serializer)
        {
            return client.PostAsync(requestUri, content, serializer).Result;
        }

        public static HttpResponseMessage Post<T>(this HttpClient client, string requestUri, T content, StreamSerializerBase serializer)
        {
            return client.PostAsync(requestUri, content, serializer).Result;
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, Uri requestUri, T content, StreamSerializerBase serializer)
        {
            using (var stream = await serializer.AsStringContentAsync(content))
            {
                return await client.PostAsync(requestUri, stream);
            }
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, Uri requestUri, T content, StreamSerializerBase serializer, CancellationToken token)
        {
            using (var stream = await serializer.AsStringContentAsync(content))
            {
                return await client.PostAsync(requestUri, stream, token);
            }
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string requestUri, T content, StreamSerializerBase serializer)
        {
            using (var stream = await serializer.AsStringContentAsync(content))
            {
                return await client.PostAsync(requestUri, stream);
            }
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string requestUri, T content, StreamSerializerBase serializer, CancellationToken token)
        {
            using (var stream = await serializer.AsStringContentAsync(content))
            {
                return await client.PostAsync(requestUri, stream, token);
            }
        }
    }
}