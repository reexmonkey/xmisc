using System;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace reexmonkey.xmisc.core.system.xmltools.extensions
{
    public static class XmlReaderExtensions
    {
        public static string SafeReadElementContentAsString(this XmlReader reader)
            => !reader.IsEmptyElement
            ? reader.ReadElementContentAsString()
            : default;

        public static string SafeReadElementContentAsString(this XmlReader reader, string localName, string ns)
            => !reader.IsEmptyElement
            ? reader.ReadElementContentAsString(localName, ns)
            : default;

        public static short SafeReadElementContentAsInt16(this XmlReader reader)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString();
            return !string.IsNullOrEmpty(value) ? XmlConvert.ToInt16(value) : default;
        }

        public static short SafeReadElementContentAsInt16(this XmlReader reader, string localName, string ns)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString(localName, ns);
            return !string.IsNullOrEmpty(value) ? XmlConvert.ToInt16(value) : default;
        }

        public static short? SafeReadElementContentAsNullableInt16(this XmlReader reader)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString();
            return !string.IsNullOrEmpty(value) ? value.ToNullableInt16() : default;
        }

        public static short? SafeReadElementContentAsNullableInt16(this XmlReader reader, string localName, string ns)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString(localName, ns);
            return !string.IsNullOrEmpty(value) ? value.ToNullableInt16() : default;
        }

        public static int SafeReadElementContentAsInt32(this XmlReader reader) => !reader.IsEmptyElement
            ? reader.ReadElementContentAsInt()
            : default;

        public static int SafeReadElementContentAsInt32(this XmlReader reader, string localName, string ns)
            => !reader.IsEmptyElement
            ? reader.ReadElementContentAsInt(localName, ns)
            : default;

        public static int? SafeReadElementContentAsNullableInt32(this XmlReader reader)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString();
            return !string.IsNullOrEmpty(value) ? value.ToNullableInt32() : default;
        }

        public static int? SafeReadElementContentAsNullableInt32(this XmlReader reader, string localName, string ns)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString(localName, ns);
            return !string.IsNullOrEmpty(value) ? value.ToNullableInt32() : default;
        }

        public static long SafeReadElementContentAsInt64(this XmlReader reader)
            => !reader.IsEmptyElement
            ? reader.ReadElementContentAsLong()
            : default;

        public static long SafeReadElementContentAsInt64(this XmlReader reader, string localName, string ns)
            => !reader.IsEmptyElement
            ? reader.ReadElementContentAsLong(localName, ns)
            : default;

        public static long? SafeReadElementContentAsNullableInt64(this XmlReader reader)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString();
            return !string.IsNullOrEmpty(value) ? value.ToNullableInt64() : default;
        }

        public static long? SafeReadElementContentAsNullableInt64(this XmlReader reader, string localName, string ns)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString(localName, ns);
            return !string.IsNullOrEmpty(value) ? value.ToNullableInt64() : default;
        }

        public static byte SafeReadElementContentAsByte(this XmlReader reader)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString();
            return !string.IsNullOrEmpty(value) ? XmlConvert.ToByte(value) : default;
        }

        public static byte SafeReadElementContentAsByte(this XmlReader reader, string localName, string ns)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString(localName, ns);
            return !string.IsNullOrEmpty(value) ? XmlConvert.ToByte(value) : default;
        }

        public static byte? SafeReadElementContentAsNullableByte(this XmlReader reader)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString();
            return !string.IsNullOrEmpty(value) ? value.ToNullableByte() : default;
        }

        public static byte? SafeReadElementContentAsNullableByte(this XmlReader reader, string localName, string ns)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString(localName, ns);
            return !string.IsNullOrEmpty(value) ? value.ToNullableByte() : default;
        }

        public static sbyte SafeReadElementContentAsSByte(this XmlReader reader)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString();
            return !string.IsNullOrEmpty(value) ? XmlConvert.ToSByte(value) : default;
        }

        public static sbyte SafeReadElementContentAsSByte(this XmlReader reader, string localName, string ns)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString(localName, ns);
            return !string.IsNullOrEmpty(value) ? XmlConvert.ToSByte(value) : default;
        }

        public static sbyte? SafeReadElementContentAsNullableSByte(this XmlReader reader)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString();
            return !string.IsNullOrEmpty(value) ? value.ToNullableSByte() : default;
        }

        public static sbyte? SafeReadElementContentAsNullableSByte(this XmlReader reader, string localName, string ns)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString(localName, ns);
            return !string.IsNullOrEmpty(value) ? value.ToNullableSByte() : default;
        }

        public static char SafeReadElementContentAsChar(this XmlReader reader)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString();
            return !string.IsNullOrEmpty(value) ? XmlConvert.ToChar(value) : default;
        }

        public static char SafeReadElementContentAsChar(this XmlReader reader, string localName, string ns)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString(localName, ns);
            return !string.IsNullOrEmpty(value) ? XmlConvert.ToChar(value) : default;
        }

        public static char? SafeReadElementContentAsNullableChar(this XmlReader reader)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString();
            return !string.IsNullOrEmpty(value) ? value.ToNullableChar() : default;
        }

        public static char? SafeReadElementContentAsNullableChar(this XmlReader reader, string localName, string ns)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString(localName, ns);
            return !string.IsNullOrEmpty(value) ? value.ToNullableChar() : default;
        }

        public static Guid SafeReadElementContentAsGuid(this XmlReader reader)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString();
            return !string.IsNullOrEmpty(value) ? XmlConvert.ToGuid(value) : default;
        }

        public static Guid SafeReadElementContentAsGuid(this XmlReader reader, string localName, string ns)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString(localName, ns);
            return !string.IsNullOrEmpty(value) ? XmlConvert.ToGuid(value) : default;
        }

        public static Guid? SafeReadElementContentAsNullableGuid(this XmlReader reader)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString();
            return !string.IsNullOrEmpty(value) ? value.ToNullableGuid() : default;
        }

        public static Guid? SafeReadElementContentAsNullableGuid(this XmlReader reader, string localName, string ns)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString(localName, ns);
            return !string.IsNullOrEmpty(value) ? value.ToNullableGuid() : default;
        }

        public static DateTime SafeReadElementContentAsDateTime(this XmlReader reader, XmlDateTimeSerializationMode option)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString();
            return !string.IsNullOrEmpty(value) ? XmlConvert.ToDateTime(value, option) : default;
        }

        public static DateTime SafeReadElementContentAsDateTime(this XmlReader reader, string localName, string ns, XmlDateTimeSerializationMode option)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString(localName, ns);
            return !string.IsNullOrEmpty(value) ? XmlConvert.ToDateTime(value, option) : default;
        }

        public static DateTime? SafeReadElementContentAsNullableDateTime(this XmlReader reader, XmlDateTimeSerializationMode option)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString();
            return !string.IsNullOrEmpty(value) ? value.ToNullableDateTime(option) : default;
        }

        public static DateTime? SafeReadElementContentAsNullableDateTime(this XmlReader reader, string localName, string ns, XmlDateTimeSerializationMode option)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString(localName, ns);
            return !string.IsNullOrEmpty(value) ? value.ToNullableDateTime(option) : default;
        }

        public static DateTimeOffset SafeReadElementContentAsDateTimeOffset(this XmlReader reader)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString();
            return !string.IsNullOrEmpty(value) ? XmlConvert.ToDateTimeOffset(value) : default;
        }

        public static DateTimeOffset SafeReadElementContentAsDateTimeOffset(this XmlReader reader, string localName, string ns)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString(localName, ns);
            return !string.IsNullOrEmpty(value) ? XmlConvert.ToDateTimeOffset(value) : default;
        }

        public static DateTimeOffset SafeReadElementContentAsDateTimeOffset(this XmlReader reader, string format)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString();
            return !string.IsNullOrEmpty(value) ? XmlConvert.ToDateTimeOffset(value, format) : default;
        }

        public static DateTimeOffset SafeReadElementContentAsDateTimeOffset(this XmlReader reader, string localName, string ns, string format)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString(localName, ns);
            return !string.IsNullOrEmpty(value) ? XmlConvert.ToDateTimeOffset(value, format) : default;
        }

        public static DateTimeOffset? SafeReadElementContentAsNullableDateTimeOffset(this XmlReader reader)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString();
            return !string.IsNullOrEmpty(value) ? value.ToNullableDateTimeOffset() : default;
        }

        public static DateTimeOffset? SafeReadElementContentAsNullableDateTimeOffset(this XmlReader reader, string localName, string ns)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString(localName, ns);
            return !string.IsNullOrEmpty(value) ? value.ToNullableDateTimeOffset() : default;
        }

        public static DateTimeOffset? SafeReadElementContentAsNullableDateTimeOffset(this XmlReader reader, string format)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString();
            return !string.IsNullOrEmpty(value) ? value.ToNullableDateTimeOffset(format) : default;
        }

        public static DateTimeOffset? SafeReadElementContentAsNullableDateTimeOffset(this XmlReader reader, string localName, string ns, string format)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString(localName, ns);
            return !string.IsNullOrEmpty(value) ? value.ToNullableDateTimeOffset(format) : default;
        }

        public static double SafeReadElementContentAsDouble(this XmlReader reader)
            => !reader.IsEmptyElement
            ? reader.ReadElementContentAsDouble()
            : default;

        public static double SafeReadElementContentAsDouble(this XmlReader reader, string localName, string ns)
            => !reader.IsEmptyElement
            ? reader.ReadElementContentAsDouble(localName, ns)
            : default;

        public static double? SafeReadElementContentAsNullableDouble(this XmlReader reader)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString();
            return !string.IsNullOrEmpty(value) ? value.ToNullableDouble() : default;
        }

        public static double? SafeReadElementContentAsNullableDouble(this XmlReader reader, string localName, string ns)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString(localName, ns);
            return !string.IsNullOrEmpty(value) ? value.ToNullableDouble() : default;
        }

        public static float SafeReadElementContentAsFloat(this XmlReader reader)
            => !reader.IsEmptyElement
            ? reader.ReadElementContentAsFloat() : default;

        public static float SafeReadElementContentAsFloat(this XmlReader reader, string localName, string ns)
            => !reader.IsEmptyElement
            ? reader.ReadElementContentAsFloat(localName, ns)
            : default;

        public static float? SafeReadElementContentAsNullableFloat(this XmlReader reader)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString();
            return !string.IsNullOrEmpty(value) ? value.ToNullableFloat() : default;
        }

        public static float? SafeReadElementContentAsNullableFloat(this XmlReader reader, string localName, string ns)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString(localName, ns);
            return !string.IsNullOrEmpty(value) ? value.ToNullableFloat() : default;
        }

        public static decimal SafeReadElementContentAsDecimal(this XmlReader reader)
            => !reader.IsEmptyElement
            ? reader.ReadElementContentAsDecimal()
            : default;

        public static decimal SafeReadElementContentAsDecimal(this XmlReader reader, string localName, string ns)
            => !reader.IsEmptyElement
            ? reader.ReadElementContentAsDecimal(localName, ns)
            : default;

        public static decimal? SafeReadElementContentAsNullableDecimal(this XmlReader reader)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString();
            return !string.IsNullOrEmpty(value) ? value.ToNullableDecimal() : default;
        }

        public static decimal? SafeReadElementContentAsNullableDecimal(this XmlReader reader, string localName, string ns)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString(localName, ns);
            return !string.IsNullOrEmpty(value) ? value.ToNullableDecimal() : default;
        }

        public static bool SafeReadElementContentAsBoolean(this XmlReader reader) => !reader.IsEmptyElement && reader.ReadElementContentAsBoolean();

        public static bool SafeReadElementContentAsBoolean(this XmlReader reader, string localName, string ns) => !reader.IsEmptyElement && reader.ReadElementContentAsBoolean(localName, ns);

        public static bool? SafeReadElementContentAsNullableBoolean(this XmlReader reader)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString();
            return !string.IsNullOrEmpty(value) ? value.ToNullableBoolean() : default;
        }

        public static bool? SafeReadElementContentAsNullableBoolean(this XmlReader reader, string localName, string ns)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString(localName, ns);
            return !string.IsNullOrEmpty(value) ? value.ToNullableBoolean() : default;
        }

        public static TimeSpan SafeReadElementContentAsTimeSpan(this XmlReader reader)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString();
            return !string.IsNullOrEmpty(value) ? XmlConvert.ToTimeSpan(value) : default;
        }

        public static TimeSpan SafeReadElementContentAsTimeSpan(this XmlReader reader, string localName, string ns)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString(localName, ns);
            return !string.IsNullOrEmpty(value) ? XmlConvert.ToTimeSpan(value) : default;
        }

        public static TimeSpan? SafeReadElementContentAsNullableTimeSpan(this XmlReader reader)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString();
            return !string.IsNullOrEmpty(value) ? value.ToNullableTimeSpan() : default;
        }

        public static TimeSpan? SafeReadElementContentAsNullableTimeSpan(this XmlReader reader, string localName, string ns)
        {
            if (reader.IsEmptyElement) return default;
            var value = reader.ReadElementContentAsString(localName, ns);
            return !string.IsNullOrEmpty(value) ? value.ToNullableTimeSpan() : default;
        }

        public static int SafeReadElementContentAsBase64(this XmlReader reader, byte[] buffer, int index, int count)
            => !reader.IsEmptyElement
            ? reader.ReadElementContentAsBase64(buffer, index, count)
            : default;

        public static int SafeReadElementContentAsBinHex(this XmlReader reader, byte[] buffer, int index, int count)
            => !reader.IsEmptyElement
            ? reader.ReadElementContentAsBinHex(buffer, index, count)
            : default;

        public static T SafeReadElementContentAsEnum<T>(this XmlReader reader, bool ignoreCase = true) where T : struct, Enum
        {
            var value = reader.SafeReadElementContentAsString();
            return !string.IsNullOrEmpty(value)
                ? value.ToEnum<T>(ignoreCase)
                : default;
        }

        public static T SafeReadElementContentAsEnum<T>(this XmlReader reader, string localName, string ns, bool ignoreCase = true) where T : struct, Enum
        {
            var value = reader.SafeReadElementContentAsString(localName, ns);
            return !string.IsNullOrEmpty(value)
                ? value.ToEnum<T>(ignoreCase)
                : default;
        }

        public static T? SafeReadElementContentAsNullableEnum<T>(this XmlReader reader, bool ignoreCase = true) where T : struct, Enum
        {
            var value = reader.SafeReadElementContentAsString();
            return !string.IsNullOrEmpty(value)
                ? value.ToNullableEnum<T>(ignoreCase)
                : default(T?);
        }

        public static T? SafeReadElementContentAsNullableEnum<T>(this XmlReader reader, string localName, string ns, bool ignoreCase = true) where T : struct, Enum
        {
            var value = reader.SafeReadElementContentAsString(localName, ns);
            return !string.IsNullOrEmpty(value)
                ? value.ToNullableEnum<T>(ignoreCase)
                : default(T?);
        }

        public static T SafeReadElementContentAs<T>(this XmlReader reader, Func<string, T> func)
        {
            var value = reader.SafeReadElementContentAsString();
            return !string.IsNullOrEmpty(value)
                ? func(value)
                : default;
        }

        public static T SafeReadElementContentAs<T>(this XmlReader reader, string localName, string ns, Func<string, T> func)
        {
            var value = reader.SafeReadElementContentAsString(localName, ns);
            return !string.IsNullOrEmpty(value)
                ? func(value)
                : default;
        }

        public static T SafeReadElementContentAs<T>(this XmlReader reader, Func<object, T> func)
        {
            var value = reader.ReadElementContentAsObject();
            return value != null
                ? func(value)
                : default;
        }

        public static T SafeReadElementContentAs<T>(this XmlReader reader, string localName, string ns, Func<object, T> func)
        {
            var value = reader.ReadElementContentAsObject(localName, ns);
            return value != null
                ? func(value)
                : default;
        }

        public static T SafeReadElementContentAs<T>(this XmlReader reader, XmlSerializer serializer)
        {
            return serializer.CanDeserialize(reader)
                ? (T)serializer.Deserialize(reader)
                : default;
        }

        public static T SafeReadElementContentAs<T>(this XmlReader reader, Func<T> func)
            where T : IXmlSerializable
        {
            var value = func();
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
                : default;
        }

        public static async Task<string> SafeReadElementContentAsStringAsync(this XmlReader reader)
        {
            if (reader.IsEmptyElement) return string.Empty;
            return await reader.ReadElementContentAsStringAsync();
        }

        public static async Task<T> SafeReadElementContentAsAsync<T>(this XmlReader reader, Func<string, T> func)
        {
            var value = await reader.SafeReadElementContentAsStringAsync();
            return !string.IsNullOrEmpty(value) ? func(value) : default;
        }

        public static async Task<T> SafeReadElementContentAsAsync<T>(this XmlReader reader, Func<object, T> func)
        {
            var value = await reader.SafeReadElementContentAsStringAsync();
            return !string.IsNullOrEmpty(value) ? func(value) : default;
        }
    }
}