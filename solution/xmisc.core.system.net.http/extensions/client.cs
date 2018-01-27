using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using reexmonkey.xmisc.core.io.serializers;

namespace reexmonkey.xmisc.core.system.net.http.extensions
{
    public static class HttpClientExtensions
    {
        #region Conversion Methods

        private static async Task<T> DeserializeResponseAsync<T>(this TextSerializerBase serializer, HttpResponseMessage response)
        {
            return await serializer.DeserializeAsync<T>(await response.Content.ReadAsStringAsync());
        }

        private static async Task<T> DeserializeResponseAsync<T>(this BinarySerializerBase serializer, HttpResponseMessage response)
        {
            return await serializer.DeserializeAsync<T>(await response.Content.ReadAsByteArrayAsync());
        }

        private static async Task<T> DeserializeResponseAsync<T>(this StreamSerializerBase serializer, HttpResponseMessage response)
        {
            return await serializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync());
        }

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

        #region GET Methods

        //Get<T> Methods (string-based serialization)

        public static T Get<T>(this HttpClient client, Uri requestUri, HttpCompletionOption option, TextSerializerBase serializer)
        {
            return client.GetAsync<T>(requestUri, option, serializer).Result;
        }

        public static T Get<T>(this HttpClient client, string requestUri, HttpCompletionOption option, TextSerializerBase serializer)
        {
            return client.GetAsync<T>(requestUri, option, serializer).Result;
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, Uri requestUri, HttpCompletionOption option, TextSerializerBase serializer)
        {
            return await serializer.DeserializeResponseAsync<T>(await client.GetAsync(requestUri, option));
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, Uri requestUri, HttpCompletionOption option, TextSerializerBase serializer, CancellationToken token)
        {
            return await serializer.DeserializeResponseAsync<T>(await client.GetAsync(requestUri, option, token));
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, string requestUri, HttpCompletionOption option, TextSerializerBase serializer)
        {
            return await serializer.DeserializeResponseAsync<T>(await client.GetAsync(requestUri, option));
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, string requestUri, HttpCompletionOption option, TextSerializerBase serializer, CancellationToken token)
        {
            return await serializer.DeserializeResponseAsync<T>(await client.GetAsync(requestUri, option, token));
        }

        //Get<T> Methods (binary serialization)

        public static T Get<T>(this HttpClient client, Uri requestUri, HttpCompletionOption option, BinarySerializerBase serializer)
        {
            return client.GetAsync<T>(requestUri, option, serializer).Result;
        }

        public static T Get<T>(this HttpClient client, string requestUri, HttpCompletionOption option, BinarySerializerBase serializer)
        {
            return client.GetAsync<T>(requestUri, option, serializer).Result;
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, Uri requestUri, HttpCompletionOption option, BinarySerializerBase serializer)
        {
            return await serializer.DeserializeResponseAsync<T>(await client.GetAsync(requestUri, option));
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, Uri requestUri, HttpCompletionOption option, BinarySerializerBase serializer, CancellationToken token)
        {
            var response = await client.GetAsync(requestUri, option, token);
            return await serializer.DeserializeResponseAsync<T>(response);
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, string requestUri, HttpCompletionOption option, BinarySerializerBase serializer)
        {
            var response = await client.GetAsync(requestUri, option);
            return await serializer.DeserializeResponseAsync<T>(response);
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, string requestUri, HttpCompletionOption option, BinarySerializerBase serializer, CancellationToken token)
        {
            var response = await client.GetAsync(requestUri, option, token);
            return await serializer.DeserializeResponseAsync<T>(response);
        }

        //Get<T> Methods (stream serialization)

        public static T Get<T>(this HttpClient client, Uri requestUri, HttpCompletionOption option, StreamSerializerBase serializer)
        {
            return client.GetAsync<T>(requestUri, option, serializer).Result;
        }

        public static T Get<T>(this HttpClient client, string requestUri, HttpCompletionOption option, StreamSerializerBase serializer)
        {
            return client.GetAsync<T>(requestUri, option, serializer).Result;
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, Uri requestUri, HttpCompletionOption option, StreamSerializerBase serializer)
        {
            return await serializer.DeserializeResponseAsync<T>(await client.GetAsync(requestUri, option));
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, Uri requestUri, HttpCompletionOption option, StreamSerializerBase serializer, CancellationToken token)
        {
            var response = await client.GetAsync(requestUri, option, token);
            return await serializer.DeserializeResponseAsync<T>(response);
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, string requestUri, HttpCompletionOption option, StreamSerializerBase serializer)
        {
            var response = await client.GetAsync(requestUri, option);
            return await serializer.DeserializeResponseAsync<T>(response);
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, string requestUri, HttpCompletionOption option, StreamSerializerBase serializer, CancellationToken token)
        {
            var response = await client.GetAsync(requestUri, option, token);
            return await serializer.DeserializeResponseAsync<T>(response);
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
            return client.GetAsync<string>(requestUri, HttpCompletionOption.ResponseContentRead, serializer).Result;
        }

        public static string GetString(this HttpClient client, string requestUri, TextSerializerBase serializer)
        {
            return client.GetAsync<string>(requestUri, HttpCompletionOption.ResponseContentRead, serializer).Result;
        }

        public static async Task<string> GetStringAsync(this HttpClient client, Uri requestUri, TextSerializerBase serializer)
        {
            return await client.GetAsync<string>(requestUri, HttpCompletionOption.ResponseContentRead, serializer);
        }

        public static async Task<string> GetStringAsync(this HttpClient client, Uri requestUri, TextSerializerBase serializer, CancellationToken token)
        {
            return await client.GetAsync<string>(requestUri, HttpCompletionOption.ResponseContentRead, serializer, token);
        }

        public static async Task<string> GetStringAsync(this HttpClient client, string requestUri, TextSerializerBase serializer)
        {
            return await client.GetAsync<string>(requestUri, HttpCompletionOption.ResponseContentRead, serializer);
        }

        public static async Task<string> GetStringAsync(this HttpClient client, string requestUri, TextSerializerBase serializer, CancellationToken token)
        {
            return await client.GetAsync<string>(requestUri, HttpCompletionOption.ResponseContentRead, serializer, token);
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
            return await client.GetAsync<byte[]>(requestUri, HttpCompletionOption.ResponseContentRead, serializer);
        }

        public static async Task<byte[]> GetByteArrayAsync(this HttpClient client, Uri requestUri, BinarySerializerBase serializer)
        {
            return await client.GetAsync<byte[]>(requestUri, HttpCompletionOption.ResponseContentRead, serializer);
        }

        public static async Task<byte[]> GetByteArrayAsync(this HttpClient client, Uri requestUri, BinarySerializerBase serializer, CancellationToken token)
        {
            return await client.GetAsync<byte[]>(requestUri, HttpCompletionOption.ResponseContentRead, serializer, token);
        }

        public static async Task<byte[]> GetByteArray(this HttpClient client, string requestUri, BinarySerializerBase serializer, CancellationToken token)
        {
            return await client.GetAsync<byte[]>(requestUri, HttpCompletionOption.ResponseContentRead, serializer, token);
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
            return await client.GetAsync<Stream>(requestUri, HttpCompletionOption.ResponseContentRead, serializer);
        }

        public static async Task<Stream> GetStreamAsync(this HttpClient client, Uri requestUri, StreamSerializerBase serializer)
        {
            return await client.GetAsync<Stream>(requestUri, HttpCompletionOption.ResponseContentRead, serializer);
        }

        public static async Task<Stream> GetStreamAsync(this HttpClient client, Uri requestUri, StreamSerializerBase serializer, CancellationToken token)
        {
            return await client.GetAsync<Stream>(requestUri, HttpCompletionOption.ResponseContentRead, serializer, token);
        }

        public static async Task<Stream> GetStream(this HttpClient client, string requestUri, StreamSerializerBase serializer, CancellationToken token)
        {
            return await client.GetAsync<Stream>(requestUri, HttpCompletionOption.ResponseContentRead, serializer, token);
        }

        #endregion GET Methods

        #region POST Methods

        //Post <T> Methods (string-based serialization)

        public static HttpResponseMessage Post<T>(this HttpClient client, Uri requestUri, T content, TextSerializerBase serializer)
        {
            return client.PostAsync(requestUri, content, serializer).Result;
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, Uri requestUri, T content, TextSerializerBase serializer)
        {
            return await client.PostAsync(requestUri, await serializer.AsContentAsync(content));
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, Uri requestUri, T content, TextSerializerBase serializer, CancellationToken token)
        {
            return await client.PostAsync(requestUri, await serializer.AsContentAsync(content), token);
        }

        public static HttpResponseMessage Post<T>(this HttpClient client, string requestUri, T content, TextSerializerBase serializer)
        {
            return client.PostAsync(requestUri, content, serializer).Result;
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string requestUri, T content, TextSerializerBase serializer)
        {
            return await client.PostAsync(requestUri, await serializer.AsContentAsync(content));
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string requestUri, T content, TextSerializerBase serializer, CancellationToken token)
        {
            return await client.PostAsync(requestUri, await serializer.AsContentAsync(content), token);
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
            return await client.PostAsync(requestUri, await serializer.AsContentAsync(content));
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, Uri requestUri, T content, BinarySerializerBase serializer, CancellationToken token)
        {
            return await client.PostAsync(requestUri, await serializer.AsContentAsync(content), token);
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string requestUri, T content, BinarySerializerBase serializer)
        {
            return await client.PostAsync(requestUri, await serializer.AsContentAsync(content));
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string requestUri, T content, BinarySerializerBase serializer, CancellationToken token)
        {
            return await client.PostAsync(requestUri, await serializer.AsContentAsync(content), token);
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
            return await client.PostAsync(requestUri, await serializer.AsContentAsync(content));
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, Uri requestUri, T content, StreamSerializerBase serializer, CancellationToken token)
        {
            return await client.PostAsync(requestUri, await serializer.AsContentAsync(content), token);
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string requestUri, T content, StreamSerializerBase serializer)
        {
            return await client.PostAsync(requestUri, await serializer.AsContentAsync(content));
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string requestUri, T content, StreamSerializerBase serializer, CancellationToken token)
        {
            return await client.PostAsync(requestUri, await serializer.AsContentAsync(content), token);
        }

        #endregion POST Methods

        #region PUT Methods

        //Put <T> Methods (string-based serialization)

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
            return await client.PutAsync(requestUri, await serializer.AsContentAsync(content));
        }

        public static async Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, Uri requestUri, T content, StreamSerializerBase serializer, CancellationToken token)
        {
            return await client.PutAsync(requestUri, await serializer.AsContentAsync(content), token);
        }

        public static async Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, string requestUri, T content, StreamSerializerBase serializer)
        {
            return await client.PutAsync(requestUri, await serializer.AsContentAsync(content));
        }

        public static async Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, string requestUri, T content, StreamSerializerBase serializer, CancellationToken token)
        {
            return await client.PutAsync(requestUri, await serializer.AsContentAsync(content), token);
        }

        #endregion PUT Methods

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

        //Put <T> Methods (string-based serialization)

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
            return await client.PatchAsync(requestUri, await serializer.AsContentAsync(content));
        }

        public static async Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, Uri requestUri, T content, StreamSerializerBase serializer, CancellationToken token)
        {
            return await client.PatchAsync(requestUri, await serializer.AsContentAsync(content), token);
        }

        public static async Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, string requestUri, T content, StreamSerializerBase serializer)
        {
            return await client.PatchAsync(requestUri, await serializer.AsContentAsync(content));
        }

        public static async Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, string requestUri, T content, StreamSerializerBase serializer, CancellationToken token)
        {
            return await client.PatchAsync(requestUri, await serializer.AsContentAsync(content), token);
        }

        #endregion PATCH Methods
    }
}
