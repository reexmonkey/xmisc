using reexmonkey.xmisc.core.io.serializers;
using System.Net.Http;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.core.system.net.http.extensions
{
    internal static class HttpHelpers
    {
        internal static async Task<T> DeserializeResponseAsync<T>(this TextSerializerBase serializer, HttpResponseMessage response)
        {
            var textual = await response.Content.ReadAsStringAsync();
            return await serializer.DeserializeAsync<T>(textual);
        }

        internal static async Task<T> DeserializeResponseAsync<T>(this BinarySerializerBase serializer, HttpResponseMessage response)
        {
            return await serializer.DeserializeAsync<T>(await response.Content.ReadAsByteArrayAsync());
        }

        internal static async Task<T> DeserializeResponseAsync<T>(this StreamSerializerBase serializer, HttpResponseMessage response)
        {
            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                return await serializer.DeserializeAsync<T>(stream);
            }
        }

        internal static async Task<StringContent> AsContentAsync<T>(this TextSerializerBase serializer, T instance)
        {
            return new StringContent(await serializer.SerializeAsync(instance));
        }

        internal static async Task<ByteArrayContent> AsContentAsync<T>(this BinarySerializerBase serializer, T content)
        {
            return new ByteArrayContent(await serializer.SerializeAsync(content));
        }

        internal static async Task<StreamContent> AsContentAsync<T>(this StreamSerializerBase serializer, T content)
        {
            return new StreamContent(await serializer.SerializeAsync(content));
        }
    }
}