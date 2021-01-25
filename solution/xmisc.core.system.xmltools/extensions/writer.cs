using System;
using System.Xml;
using System.Xml.Serialization;

namespace reexmonkey.xmisc.core.system.xmltools.extensions
{
    public static class XmlWriterExtensions
    {
        #region Values

        public static void WriteInt16Value(this XmlWriter writer, short value) => writer.WriteValue(XmlConvert.ToString(value));

        public static void WriteUInt16Value(this XmlWriter writer, ushort value) => writer.WriteValue(XmlConvert.ToString(value));

        public static void WriteUInt32Value(this XmlWriter writer, uint value) => writer.WriteValue(XmlConvert.ToString(value));

        public static void WriteUInt64Value(this XmlWriter writer, ulong value) => writer.WriteValue(XmlConvert.ToString(value));

        public static void WriteGuidValue(this XmlWriter writer, Guid value) => writer.WriteValue(XmlConvert.ToString(value));

        public static void WriteValue(this XmlWriter writer, DateTime value, XmlDateTimeSerializationMode mode)
            => writer.WriteValue(XmlConvert.ToString(value, mode));

        public static void WriteDateTimeOffsetValue(this XmlWriter writer, DateTimeOffset value) => writer.WriteValue(XmlConvert.ToString(value));

        public static void WriteValue(this XmlWriter writer, DateTimeOffset value, string format) => writer.WriteValue(XmlConvert.ToString(value, format));

        public static void WriteTimeSpanValue(this XmlWriter writer, TimeSpan value) => writer.WriteValue(XmlConvert.ToString(value));

        public static void WriteByteValue(this XmlWriter writer, byte value) => writer.WriteValue(XmlConvert.ToString(value));

        public static void WriteSByteValue(this XmlWriter writer, sbyte value) => writer.WriteValue(XmlConvert.ToString(value));

        public static void WriteCharValue(this XmlWriter writer, char value) => writer.WriteValue(XmlConvert.ToString(value));

        public static void WriteBytes(this XmlWriter writer, byte[] value) => writer.WriteValue(Convert.ToBase64String(value));

        public static void WriteEnumValue<T>(this XmlWriter writer, T value, string format) where T : struct, Enum
            => writer.WriteValue(value.ToString(format));

        public static void WriteValue<T>(this XmlWriter writer, T value) where T : IXmlSerializable
        {
            value.WriteXml(writer);
        }

        #endregion Values

        #region Attributes

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, string value)
            => writer.WriteAttributeString(localName, value);

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, string ns, string value)
            => writer.WriteAttributeString(localName, ns, value);

        public static void SafeWriteAttributeString(this XmlWriter writer, string prefix, string localName, string ns, string value)
            => writer.WriteAttributeString(prefix, localName, ns, value);

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, bool value)
    => writer.WriteAttributeString(localName, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, string ns, bool value)
            => writer.WriteAttributeString(localName, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string prefix, string localName, string ns, bool value)
            => writer.WriteAttributeString(localName, prefix, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, short value)
            => writer.WriteAttributeString(localName, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, string ns, short value)
            => writer.WriteAttributeString(localName, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string prefix, string localName, string ns, short value)
            => writer.WriteAttributeString(localName, prefix, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, int value)
            => writer.WriteAttributeString(localName, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, string ns, int value)
            => writer.WriteAttributeString(localName, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string prefix, string localName, string ns, int value)
            => writer.WriteAttributeString(localName, prefix, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, long value)
            => writer.WriteAttributeString(localName, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, string ns, long value)
            => writer.WriteAttributeString(localName, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string prefix, string localName, string ns, long value)
            => writer.WriteAttributeString(localName, prefix, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, float value)
            => writer.WriteAttributeString(localName, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, string ns, float value)
            => writer.WriteAttributeString(localName, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string prefix, string localName, string ns, float value)
            => writer.WriteAttributeString(localName, prefix, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, double value)
            => writer.WriteAttributeString(localName, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, string ns, double value)
            => writer.WriteAttributeString(localName, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string prefix, string localName, string ns, double value)
            => writer.WriteAttributeString(localName, prefix, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, decimal value)
            => writer.WriteAttributeString(localName, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, string ns, decimal value)
            => writer.WriteAttributeString(localName, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string prefix, string localName, string ns, decimal value)
            => writer.WriteAttributeString(localName, prefix, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, DateTime value, XmlDateTimeSerializationMode? mode = null)
        {
            if (mode.HasValue) writer.WriteAttributeString(localName, XmlConvert.ToString(value, mode.Value));
            else writer.WriteAttributeString(localName, XmlConvert.ToString(value, mode.Value));
        }

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, string ns, DateTime value, XmlDateTimeSerializationMode? mode = null)
        {
            if (mode.HasValue) writer.WriteAttributeString(localName, ns, XmlConvert.ToString(value));
            else writer.WriteAttributeString(localName, ns, XmlConvert.ToString(value, mode.Value));
        }

        public static void SafeWriteAttributeString(this XmlWriter writer, string prefix, string localName, string ns, DateTime value, XmlDateTimeSerializationMode? mode = null)
        {
            if (mode.HasValue) writer.WriteAttributeString(prefix, localName, ns, XmlConvert.ToString(value));
            else writer.WriteAttributeString(prefix, localName, ns, XmlConvert.ToString(value));
        }

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, DateTimeOffset value, string format = null)
        {
            if (string.IsNullOrEmpty(format)) writer.WriteAttributeString(localName, XmlConvert.ToString(value, format));
            else writer.WriteAttributeString(localName, XmlConvert.ToString(value));
        }

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, string ns, DateTimeOffset value, string format = null)
        {
            if (string.IsNullOrEmpty(format)) writer.WriteAttributeString(localName, ns, XmlConvert.ToString(value, format));
            else writer.WriteAttributeString(localName, ns, XmlConvert.ToString(value));
        }

        public static void SafeWriteAttributeString(this XmlWriter writer, string prefix, string localName, string ns, DateTimeOffset value, string format = null)
        {
            if (string.IsNullOrEmpty(format)) writer.WriteAttributeString(prefix, localName, ns, XmlConvert.ToString(value, format));
            else writer.WriteAttributeString(prefix, localName, ns, XmlConvert.ToString(value));
        }

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, TimeSpan value)
            => writer.WriteAttributeString(localName, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, string ns, TimeSpan value)
            => writer.WriteAttributeString(localName, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string prefix, string localName, string ns, TimeSpan value)
            => writer.WriteAttributeString(prefix, localName, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, Guid value)
            => writer.WriteAttributeString(localName, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, string ns, Guid value)
            => writer.WriteAttributeString(localName, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string prefix, string localName, string ns, Guid value)
            => writer.WriteAttributeString(prefix, localName, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, byte value)
            => writer.WriteAttributeString(localName, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, string ns, byte value)
            => writer.WriteAttributeString(localName, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string prefix, string localName, string ns, byte value)
            => writer.WriteAttributeString(prefix, localName, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, sbyte value)
            => writer.WriteAttributeString(localName, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, string ns, sbyte value)
            => writer.WriteAttributeString(localName, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string prefix, string localName, string ns, sbyte value)
            => writer.WriteAttributeString(prefix, localName, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, char value)
            => writer.WriteAttributeString(localName, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, string ns, char value)
            => writer.WriteAttributeString(localName, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string prefix, string localName, string ns, char value)
            => writer.WriteAttributeString(prefix, localName, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, ushort value)
            => writer.WriteAttributeString(localName, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, string ns, ushort value)
            => writer.WriteAttributeString(localName, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string prefix, string localName, string ns, ushort value)
            => writer.WriteAttributeString(prefix, localName, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, uint value)
            => writer.WriteAttributeString(localName, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, string ns, uint value)
            => writer.WriteAttributeString(localName, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string prefix, string localName, string ns, uint value)
            => writer.WriteAttributeString(prefix, localName, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, ulong value)
            => writer.WriteAttributeString(localName, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, string ns, ulong value)
            => writer.WriteAttributeString(localName, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string prefix, string localName, string ns, ulong value)
            => writer.WriteAttributeString(prefix, localName, ns, XmlConvert.ToString(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, byte[] value)
            => writer.WriteAttributeString(localName, Convert.ToBase64String(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, string ns, byte[] value)
            => writer.WriteAttributeString(localName, ns, Convert.ToBase64String(value));

        public static void SafeWriteAttributeString(this XmlWriter writer, string prefix, string localName, string ns, byte[] value)
            => writer.WriteAttributeString(prefix, localName, ns, Convert.ToBase64String(value));

        public static void SafeWriteAttributeString<T>(this XmlWriter writer, string localName, T value, string format) where T : struct, Enum
            => writer.WriteAttributeString(localName, value.ToString(format));

        public static void SafeWriteAttributeString<T>(this XmlWriter writer, string localName, string ns, T value, string format) where T : struct, Enum
            => writer.WriteAttributeString(localName, ns, value.ToString(format));

        public static void SafeWriteAttributeString<T>(this XmlWriter writer, string prefix, string localName, string ns, T value, string format)
            where T : struct, Enum
            => writer.WriteAttributeString(prefix, localName, ns, value.ToString(format));

        #endregion Attributes

        #region Elements

        public static void SafeWriteElementString<T>(this XmlWriter writer, string localName, T value) where T : IXmlSerializable
        {
            writer.WriteStartElement(localName);
            writer.WriteValue<T>(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string localName, string ns, T value) where T : IXmlSerializable
        {
            writer.WriteStartElement(localName, ns);
            writer.WriteValue<T>(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string prefix, string localName, string ns, T value) where T : IXmlSerializable
        {
            writer.WriteStartElement(prefix, localName, ns);
            writer.WriteValue<T>(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, string value)
        {
            if (!string.IsNullOrEmpty(value))
                writer.WriteElementString(localName, value);
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, string ns, string value)
        {
            if (!string.IsNullOrEmpty(value))
                writer.WriteElementString(localName, ns, value);
        }

        public static void SafeWriteElementString(this XmlWriter writer, string prefix, string localName, string ns, string value)
        {
            if (!string.IsNullOrEmpty(value))
                writer.WriteElementString(prefix, localName, ns, value);
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, bool value)
        {
            writer.WriteStartElement(localName);
            writer.WriteValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, string ns, bool value)
        {
            writer.WriteStartElement(localName, ns);
            writer.WriteValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string prefix, string localName, string ns, bool value)
        {
            writer.WriteStartElement(prefix, localName, ns);
            writer.WriteValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, short value)
        {
            writer.WriteStartElement(localName);
            writer.WriteInt16Value(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, string ns, short value)
        {
            writer.WriteStartElement(localName, ns);
            writer.WriteInt16Value(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string prefix, string localName, string ns, short value)
        {
            writer.WriteStartElement(prefix, localName, ns);
            writer.WriteInt16Value(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, int value)
        {
            writer.WriteStartElement(localName);
            writer.WriteValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, string ns, int value)
        {
            writer.WriteStartElement(localName, ns);
            writer.WriteValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string prefix, string localName, string ns, int value)
        {
            writer.WriteStartElement(prefix, localName, ns);
            writer.WriteValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, long value)
        {
            writer.WriteStartElement(localName);
            writer.WriteValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, string ns, long value)
        {
            writer.WriteStartElement(localName, ns);
            writer.WriteValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string prefix, string localName, string ns, long value)
        {
            writer.WriteStartElement(prefix, localName, ns);
            writer.WriteValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, float value)
        {
            writer.WriteStartElement(localName);
            writer.WriteValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, string ns, float value)
        {
            writer.WriteStartElement(localName, ns);
            writer.WriteValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string prefix, string localName, string ns, float value)
        {
            writer.WriteStartElement(prefix, localName, ns);
            writer.WriteValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, double value)
        {
            writer.WriteStartElement(localName);
            writer.WriteValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, string ns, double value)
        {
            writer.WriteStartElement(localName, ns);
            writer.WriteValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string prefix, string localName, string ns, double value)
        {
            writer.WriteStartElement(prefix, localName, ns);
            writer.WriteValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, decimal value)
        {
            writer.WriteStartElement(localName);
            writer.WriteValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, string ns, decimal value)
        {
            writer.WriteStartElement(localName, ns);
            writer.WriteValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string prefix, string localName, string ns, decimal value)
        {
            writer.WriteStartElement(prefix, localName, ns);
            writer.WriteValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, DateTime value, XmlDateTimeSerializationMode? mode = null)
        {
            writer.WriteStartElement(localName);
            if (mode.HasValue) writer.WriteValue(value, mode.Value);
            else writer.WriteValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, string ns, DateTime value, XmlDateTimeSerializationMode? mode = null)
        {
            writer.WriteStartElement(localName, ns);
            if (mode.HasValue) writer.WriteValue(value, mode.Value);
            else writer.WriteValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string prefix, string localName, string ns, DateTime value, XmlDateTimeSerializationMode? mode = null)
        {
            writer.WriteStartElement(prefix, localName, ns);
            if (mode.HasValue) writer.WriteValue(value, mode.Value);
            else writer.WriteValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, DateTimeOffset value, string format = null)
        {
            writer.WriteStartElement(localName);
            if (string.IsNullOrEmpty(format)) writer.WriteValue(value);
            else writer.WriteValue(value, format);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, string ns, DateTimeOffset value, string format = null)
        {
            writer.WriteStartElement(localName, ns);
            if (string.IsNullOrEmpty(format)) writer.WriteValue(value);
            else writer.WriteValue(value, format);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string prefix, string localName, string ns, DateTimeOffset value, string format = null)
        {
            writer.WriteStartElement(prefix, localName, ns);
            if (string.IsNullOrEmpty(format)) writer.WriteValue(value);
            else writer.WriteValue(value, format);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, TimeSpan value)
        {
            writer.WriteStartElement(localName);
            writer.WriteTimeSpanValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, string ns, TimeSpan value)
        {
            writer.WriteStartElement(localName, ns);
            writer.WriteTimeSpanValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string prefix, string localName, string ns, TimeSpan value)
        {
            writer.WriteStartElement(prefix, localName, ns);
            writer.WriteTimeSpanValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, Guid value)
        {
            writer.WriteStartElement(localName);
            writer.WriteGuidValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, string ns, Guid value)
        {
            writer.WriteStartElement(localName, ns);
            writer.WriteGuidValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string prefix, string localName, string ns, Guid value)
        {
            writer.WriteStartElement(prefix, localName, ns);
            writer.WriteGuidValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, byte value)
        {
            writer.WriteStartElement(localName);
            writer.WriteByteValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, string ns, byte value)
        {
            writer.WriteStartElement(localName, ns);
            writer.WriteByteValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string prefix, string localName, string ns, byte value)
        {
            writer.WriteStartElement(prefix, localName, ns);
            writer.WriteByteValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, sbyte value)
        {
            writer.WriteStartElement(localName);
            writer.WriteSByteValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, string ns, sbyte value)
        {
            writer.WriteStartElement(localName, ns);
            writer.WriteSByteValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string prefix, string localName, string ns, sbyte value)
        {
            writer.WriteStartElement(prefix, localName, ns);
            writer.WriteSByteValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, char value)
        {
            writer.WriteStartElement(localName);
            writer.WriteCharValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, string ns, char value)
        {
            writer.WriteStartElement(localName, ns);
            writer.WriteCharValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string prefix, string localName, string ns, char value)
        {
            writer.WriteStartElement(prefix, localName, ns);
            writer.WriteCharValue(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, ushort value)
        {
            writer.WriteStartElement(localName);
            writer.WriteUInt16Value(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, string ns, ushort value)
        {
            writer.WriteStartElement(localName, ns);
            writer.WriteUInt16Value(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string prefix, string localName, string ns, ushort value)
        {
            writer.WriteStartElement(prefix, localName, ns);
            writer.WriteUInt16Value(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, uint value)
        {
            writer.WriteStartElement(localName);
            writer.WriteUInt32Value(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, string ns, uint value)
        {
            writer.WriteStartElement(localName, ns);
            writer.WriteUInt32Value(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string prefix, string localName, string ns, uint value)
        {
            writer.WriteStartElement(prefix, localName, ns);
            writer.WriteUInt32Value(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, ulong value)
        {
            writer.WriteStartElement(localName);
            writer.WriteUInt64Value(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, string ns, ulong value)
        {
            writer.WriteStartElement(localName, ns);
            writer.WriteUInt64Value(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string prefix, string localName, string ns, ulong value)
        {
            writer.WriteStartElement(prefix, localName, ns);
            writer.WriteUInt64Value(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, byte[] value)
        {
            writer.WriteStartElement(localName);
            writer.WriteBytes(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, string ns, byte[] value)
        {
            writer.WriteStartElement(localName, ns);
            writer.WriteBytes(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString(this XmlWriter writer, string prefix, string localName, string ns, byte[] value)
        {
            writer.WriteStartElement(prefix, localName, ns);
            writer.WriteBytes(value);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string localName, T value, string format) where T : struct, Enum
        {
            writer.WriteStartElement(localName);
            writer.WriteEnumValue(value, format);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string localName, string ns, T value, string format) where T : struct, Enum
        {
            writer.WriteStartElement(localName, ns);
            writer.WriteEnumValue(value, format);
            writer.WriteEndElement();
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string prefix, string localName, string ns, T value, string format) where T : struct, Enum
        {
            writer.WriteStartElement(prefix, localName, ns);
            writer.WriteEnumValue(value, format);
            writer.WriteEndElement();
        }

        #endregion Elements
    }
}