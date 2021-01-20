using System;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace reexmonkey.xmisc.core.system.xmltools.extensions
{
    public static class XmlReaderExtensions
    {
        public static string SafeReadElementContentAsString(this XmlReader reader)
            => !reader.IsEmptyElement ? reader.ReadElementContentAsString() : string.Empty;

        public static string SafeReadElementContentAsString(this XmlReader reader, string localName, string namespaceURI)
            => !reader.IsEmptyElement ? reader.ReadElementContentAsString(localName, namespaceURI) : string.Empty;

        public static int SafeReadElementContentAsInt(this XmlReader reader) => !reader.IsEmptyElement
            ? reader.ReadElementContentAsInt() : default;

        public static int SafeReadElementContentAsInt(this XmlReader reader, string localName, string namespaceURI) => !reader.IsEmptyElement
            ? reader.ReadElementContentAsInt(localName, namespaceURI) : default;

        public static long SafeReadElementContentAsLong(this XmlReader reader) => !reader.IsEmptyElement
            ? reader.ReadElementContentAsLong() : default;

        public static long SafeReadElementContentAsLong(this XmlReader reader, string localName, string namespaceURI) => !reader.IsEmptyElement
            ? reader.ReadElementContentAsLong(localName, namespaceURI) : default;

        public static double SafeReadElementContentAsDouble(this XmlReader reader) => !reader.IsEmptyElement
            ? reader.ReadElementContentAsDouble() : default;

        public static double SafeReadElementContentAsDouble(this XmlReader reader, string localName, string namespaceURI) => !reader.IsEmptyElement
            ? reader.ReadElementContentAsDouble(localName, namespaceURI) : default;

        public static float SafeReadElementContentAsFloat(this XmlReader reader) => !reader.IsEmptyElement
            ? reader.ReadElementContentAsFloat() : default;

        public static float SafeReadElementContentAsFloat(this XmlReader reader, string localName, string namespaceURI) => !reader.IsEmptyElement
            ? reader.ReadElementContentAsFloat(localName, namespaceURI) : default;

        public static decimal SafeReadElementContentAsDecimal(this XmlReader reader) => !reader.IsEmptyElement
            ? reader.ReadElementContentAsDecimal() : default;

        public static decimal SafeReadElementContentAsDecimal(this XmlReader reader, string localName, string namespaceURI) => !reader.IsEmptyElement
            ? reader.ReadElementContentAsDecimal(localName, namespaceURI) : default;

        public static bool SafeReadElementContentAsBoolean(this XmlReader reader) => !reader.IsEmptyElement
            ? reader.ReadElementContentAsBoolean() : default;

        public static bool SafeReadElementContentAsBoolean(this XmlReader reader, string localName, string namespaceURI) => !reader.IsEmptyElement
            ? reader.ReadElementContentAsBoolean(localName, namespaceURI) : default;

        public static int SafeReadElementContentAsBase64(this XmlReader reader, byte[] buffer, int index, int count)
            => !reader.IsEmptyElement ? reader.ReadElementContentAsBase64(buffer, index, count) : default;

        public static int SafeReadElementContentAsBinHex(this XmlReader reader, byte[] buffer, int index, int count)
            => !reader.IsEmptyElement ? reader.ReadElementContentAsBinHex(buffer, index, count) : default;

        private static T AsEnum<T>(this string value, bool ignoreCase) where T : struct, Enum
            => Enum.TryParse(value, ignoreCase, out T result) ? result : default;

        public static T SafeReadElementContentAsEnum<T>(this XmlReader reader, bool ignoreCase = true) where T : struct, Enum
        {
            var value = reader.SafeReadElementContentAsString();
            return !string.IsNullOrEmpty(value)
                ? value.AsEnum<T>(ignoreCase)
                : default;
        }

        public static T SafeReadElementContentAsEnum<T>(this XmlReader reader, string localName, string namespaceURI, bool ignoreCase = true) where T : struct, Enum
        {
            var value = reader.SafeReadElementContentAsString(localName, namespaceURI);
            return !string.IsNullOrEmpty(value)
                ? value.AsEnum<T>(ignoreCase)
                : default;
        }

        public static T SafeReadElementContentAs<T>(this XmlReader reader, Func<string, T> transform)
        {
            var value = reader.SafeReadElementContentAsString();
            return !string.IsNullOrEmpty(value)
                ? transform(value)
                : default;
        }

        public static T SafeReadElementContentAs<T>(this XmlReader reader, string localName, string namespaceURI, Func<string, T> transform)
        {
            var value = reader.SafeReadElementContentAsString(localName, namespaceURI);
            return !string.IsNullOrEmpty(value)
                ? transform(value)
                : default;
        }

        public static T SafeReadElementContentAs<T>(this XmlReader reader, Func<object, T> transform)
        {
            var value = reader.ReadElementContentAsObject();
            return value != null
                ? transform(value)
                : default;
        }

        public static T SafeReadElementContentAs<T>(this XmlReader reader, string localName, string namespaceURI, Func<object, T> transform)
        {
            var value = reader.ReadElementContentAsObject(localName, namespaceURI);
            return value != null
                ? transform(value)
                : default;
        }

        public static T SafeReadElementContentAs<T>(this XmlReader reader, XmlSerializer serializer)
        {
            return serializer.CanDeserialize(reader)
                ? (T)serializer.Deserialize(reader)
                : default;
        }

        public static T SafeReadElementContentAs<T>(this XmlReader reader, Func<T> ctor)
            where T : IXmlSerializable
        {
            if (reader.IsEmptyElement && !reader.HasAttributes ) return default;
            var value = ctor();
            value.ReadXml(reader);
            return value;
        }

        public static async Task<int> SafeReadElementContentAsBase64Async(this XmlReader reader, byte[] buffer, int index, int count)
        {
            return !reader.IsEmptyElement
                ? await reader.ReadElementContentAsBase64Async(buffer, index, count)
                : await Task.FromResult(default(int));
        }

        public static async Task<int> SafeReadElementContentAsBinHexAsync(this XmlReader reader, byte[] buffer, int index, int count)
        {
            return !reader.IsEmptyElement
                ? await reader.ReadElementContentAsBinHexAsync(buffer, index, count)
                : await Task.FromResult(default(int));
        }

        public static async Task<string> SafeReadElementContentAsStringAsync(this XmlReader reader)
        {
            if (reader.IsEmptyElement) return string.Empty;
            return await reader.ReadElementContentAsStringAsync();
        }

        public static async Task<T> SafeReadElementContentAsAsync<T>(this XmlReader reader, Func<string, T> transform)
        {
            var value = await reader.SafeReadElementContentAsStringAsync();
            return !string.IsNullOrEmpty(value)
                ? transform(value)
                : default;
        }

        public static async Task<T> SafeReadElementContentAsAsync<T>(this XmlReader reader, Func<object, T> transform)
        {
            var value = await reader.SafeReadElementContentAsStringAsync();
            return !string.IsNullOrEmpty(value)
                ? transform(value)
                : default;
        }
    }
}