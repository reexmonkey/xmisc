using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection;

namespace reexmonkey.xmisc.core.system.xml.extensions
{
    public static class XmlReaderExtensions
    {
        public static string SafeReadElementContentAsString(this XmlReader reader)
            => !reader.IsEmptyElement ? reader.ReadElementContentAsString() : string.Empty;

        public static string SafeReadElementContentAsString(this XmlReader reader, string localName, string namespaceURI)
            => !reader.IsEmptyElement ? reader.ReadElementContentAsString(localName, namespaceURI) : string.Empty;

        public static int SafeReadElementContentAsInt(this XmlReader reader) => !reader.IsEmptyElement
            ? reader.ReadElementContentAsInt() : default(int);

        public static int SafeReadElementContentAsInt(this XmlReader reader, string localName, string namespaceURI) => !reader.IsEmptyElement
            ? reader.ReadElementContentAsInt(localName, namespaceURI) : default(int);

        public static long SafeReadElementContentAsLong(this XmlReader reader) => !reader.IsEmptyElement
            ? reader.ReadElementContentAsLong() : default(long);

        public static long SafeReadElementContentAsLong(this XmlReader reader, string localName, string namespaceURI) => !reader.IsEmptyElement
            ? reader.ReadElementContentAsLong(localName, namespaceURI) : default(long);

        public static double SafeReadElementContentAsDouble(this XmlReader reader) => !reader.IsEmptyElement
            ? reader.ReadElementContentAsDouble() : default(double);

        public static double SafeReadElementContentAsDouble(this XmlReader reader, string localName, string namespaceURI) => !reader.IsEmptyElement
            ? reader.ReadElementContentAsDouble(localName, namespaceURI) : default(double);

        public static float SafeReadElementContentAsFloat(this XmlReader reader) => !reader.IsEmptyElement
            ? reader.ReadElementContentAsFloat() : default(float);

        public static float SafeReadElementContentAsFloat(this XmlReader reader, string localName, string namespaceURI) => !reader.IsEmptyElement
            ? reader.ReadElementContentAsFloat(localName, namespaceURI) : default(float);

        public static decimal SafeReadElementContentAsDecimal(this XmlReader reader) => !reader.IsEmptyElement
            ? reader.ReadElementContentAsDecimal() : default(decimal);

        public static decimal SafeReadElementContentAsDecimal(this XmlReader reader, string localName, string namespaceURI) => !reader.IsEmptyElement
            ? reader.ReadElementContentAsDecimal(localName, namespaceURI) : default(decimal);

        public static bool SafeReadElementContentAsBoolean(this XmlReader reader) => !reader.IsEmptyElement
            ? reader.ReadElementContentAsBoolean() : default(bool);

        public static bool SafeReadElementContentAsBoolean(this XmlReader reader, string localName, string namespaceURI) => !reader.IsEmptyElement
            ? reader.ReadElementContentAsBoolean(localName, namespaceURI) : default(bool);

        public static int SafeReadElementContentAsBase64(this XmlReader reader, byte[] buffer, int index, int count)
            => !reader.IsEmptyElement ? reader.ReadElementContentAsBase64(buffer, index, count) : default(int);

        public static int SafeReadElementContentAsBinHex(this XmlReader reader, byte[] buffer, int index, int count)
            => !reader.IsEmptyElement ? reader.ReadElementContentAsBinHex(buffer, index, count) : default(int);

        private static T SafeReadAsEnum<T>(this string xml, bool ignoreCase) where T : struct => Enum.TryParse(xml, ignoreCase, out T result) ? result : default(T);

        public static T SafeReadElementContentAsEnum<T>(this XmlReader reader, bool ignoreCase) where T : struct
            => !reader.IsEmptyElement ? reader.SafeReadElementContentAsString().SafeReadAsEnum<T>(ignoreCase) : default(T);

        public static T SafeReadElementContentAsEnum<T>(this XmlReader reader, bool ignoreCase, string localName, string namespaceURI) where T : struct
            => !reader.IsEmptyElement ? reader.SafeReadElementContentAsString(localName, namespaceURI).SafeReadAsEnum<T>(ignoreCase) : default(T);

        public static T SafeReadElementContentAs<T>(this XmlReader reader, Func<string, T> transform)
            => !reader.IsEmptyElement ? transform(reader.ReadElementContentAsString()) : default(T);

        public static T SafeReadElementContentAs<T>(this XmlReader reader, Func<string, T> transform, string localName, string namespaceURI)
            => !reader.IsEmptyElement ? transform(reader.ReadElementContentAsString(localName, namespaceURI)) : default(T);

        public static T SafeReadElementContentAs<T>(this XmlReader reader, Func<object, T> transform)
            => !reader.IsEmptyElement ? transform(reader.ReadElementContentAsObject()) : default(T);

        public static T SafeReadElementContentAs<T>(this XmlReader reader, Func<object, T> transform, string localName, string namespaceURI)
            => !reader.IsEmptyElement ? transform(reader.ReadElementContentAsObject(localName, namespaceURI)) : default(T);

        public static T SafeReadElementContentAs<T>(this XmlReader reader, XmlSerializer serializer)
            => serializer.CanDeserialize(reader) ? (T)serializer.Deserialize(reader) : default(T);

        public static T SafeReadElementContentAs<T>(this XmlReader reader)
            => reader.SafeReadElementContentAs<T>(new XmlSerializer(typeof(T)));

        public static T SafeReadElementContentAs<T>(this XmlReader reader, string defaultNamespace)
            => reader.SafeReadElementContentAs<T>(new XmlSerializer(typeof(T), defaultNamespace));

        public static T SafeReadElementContentAs<T>(this XmlReader reader, XmlRootAttribute root)
            => reader.SafeReadElementContentAs<T>(new XmlSerializer(typeof(T), root));

        public static T SafeReadElementContentAs<T>(this XmlReader reader, XmlRootAttribute root, params Type[] extraTypes)
        {
            var match = extraTypes.FirstOrDefault(t =>
            {
                var ti = t.GetTypeInfo();
                return typeof(T).GetTypeInfo().IsAssignableFrom(ti) && !ti.IsAbstract;
            });
            return match != null ? reader.SafeReadElementContentAs<T>(new XmlSerializer(match, root)) : default(T);
        }

        public static async Task<string> SafeReadElementContentAsStringAsync(this XmlReader reader)
            => !reader.IsEmptyElement ? await reader.ReadElementContentAsStringAsync() : await Task.FromResult(string.Empty);

        public static async Task<string> SafeReadElementContentAsStringAsync(this XmlReader reader, string localName, string namespaceURI)
            => !reader.IsEmptyElement ? await Task.FromResult(reader.ReadElementContentAsString(localName, namespaceURI)) : await Task.FromResult(string.Empty);

        public static async Task<int> SafeReadElementContentAsBase64Async(this XmlReader reader, byte[] buffer, int index, int count)
            => !reader.IsEmptyElement ? await reader.ReadElementContentAsBase64Async(buffer, index, count) : await Task.FromResult(default(int));

        public static async Task<int> SafeReadElementContentAsBinHexAsync(this XmlReader reader, byte[] buffer, int index, int count)
            => !reader.IsEmptyElement ? await reader.ReadElementContentAsBinHexAsync(buffer, index, count) : await Task.FromResult(default(int));

        public static async Task<T> SafeReadElementContentAsAsync<T>(this XmlReader reader, Func<string, T> transform)
            => !reader.IsEmptyElement ? await Task.FromResult(transform(await reader.ReadElementContentAsStringAsync())) : await Task.FromResult(default(T));

        public static async Task<T> SafeReadElementContentAsAsync<T>(this XmlReader reader, Func<string, T> transform, string localName, string namespaceURI)
            => !reader.IsEmptyElement ? await Task.FromResult(transform(reader.ReadElementContentAsString(localName, namespaceURI))) : await Task.FromResult(default(T));

        public static async Task<T> SafeReadElementContentAsAsync<T>(this XmlReader reader, Func<object, T> transform)
            => !reader.IsEmptyElement ? await Task.FromResult(transform(await reader.ReadElementContentAsObjectAsync())) : await Task.FromResult(default(T));

        public static async Task<T> SafeReadElementContentAsAsync<T>(this XmlReader reader, Func<object, T> transform, string localName, string namespaceURI)
            => !reader.IsEmptyElement ? await Task.FromResult(transform(reader.ReadElementContentAsObject(localName, namespaceURI))) : await Task.FromResult(default(T));

        public static async Task<T> SafeReadElementContentAsAsync<T>(this XmlReader reader, XmlSerializer serializer)
            => await Task.FromResult(reader.SafeReadElementContentAs<T>(serializer));

        public static async Task<T> SafeReadElementContentAsAsync<T>(this XmlReader reader)
            => await Task.FromResult(reader.SafeReadElementContentAs<T>());

        public static async Task<T> SafeReadElementContentAsAsync<T>(this XmlReader reader, string defaultNamespace)
            => await Task.FromResult(reader.SafeReadElementContentAs<T>(defaultNamespace));

        public static async Task<T> SafeReadElementContentAsAsync<T>(this XmlReader reader, XmlRootAttribute root)
            => await Task.FromResult(reader.SafeReadElementContentAs<T>(root));

        public static async Task<T> SafeReadElementContentAsAsync<T>(this XmlReader reader, XmlRootAttribute root, params Type[] extraTypes)
        {
            var match = extraTypes.FirstOrDefault(t =>
            {
                var ti = t.GetTypeInfo();
                return typeof(T).GetTypeInfo().IsAssignableFrom(ti) && !ti.IsAbstract;
            });
            return match != null ? await reader.SafeReadElementContentAsAsync<T>(new XmlSerializer(match, root)) : await Task.FromResult(default(T));
        }
    }
}
