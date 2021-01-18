using System;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace reexmonkey.xmisc.core.system.xmltools.extensions
{
    public static class XmlWriterExtensions
    {
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

        public static void SafeWriteElementString(this XmlWriter writer, string localName, string value, params (string localName, string value)[] attributes)
        {
            if (!string.IsNullOrEmpty(value))
            {
                writer.WriteStartElement(localName);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.localName, attribute.value);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, string ns, string value, params (string localName, string value)[] attributes)
        {
            if (!string.IsNullOrEmpty(value))
            {
                writer.WriteStartElement(localName, ns);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.localName, attribute.value);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString(this XmlWriter writer, string prefix, string localName, string ns, string value, params (string localName, string value)[] attributes)
        {
            if (!string.IsNullOrEmpty(value))
            {
                writer.WriteStartElement(prefix, localName, ns);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.localName, attribute.value);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, string value)
        {
            if (!string.IsNullOrEmpty(value)) writer.WriteAttributeString(localName, value);
        }

        public static void SafeWriteAttributeString(this XmlWriter writer, string localName, string ns, string value)
        {
            if (!string.IsNullOrEmpty(value)) writer.WriteAttributeString(localName, ns, value);
        }

        public static void SafeWriteAttributeString(this XmlWriter writer, string prefix, string localName, string ns, string value)
        {
            if (!string.IsNullOrEmpty(value)) writer.WriteAttributeString(prefix, localName, ns, value);
        }

        private static bool TryWritePrimitiveValue<T>(this XmlWriter writer, T value)
        {
            if (value is bool)
            {
                writer.WriteValue(Convert.ToBoolean(value));
                return true;
            }
            if (value is short)
            {
                writer.WriteValue(Convert.ToUInt16(value));
                return true;
            }
            if (value is int)
            {
                writer.WriteValue(Convert.ToInt32(value));
                return true;
            }
            if (value is long)
            {
                writer.WriteValue(Convert.ToInt64(value));
                return true;
            }
            if (value is float)
            {
                writer.WriteValue(Convert.ToSingle(value));
                return true;
            }

            if (value is decimal)
            {
                writer.WriteValue(Convert.ToDecimal(value));
                return true;
            }

            if (value is double)
            {
                writer.WriteValue(Convert.ToDouble(value));
                return true;
            }

            if (value is DateTime)
            {
                writer.WriteValue(Convert.ToDateTime(value));
                return true;
            }

            if (value is Enum)
            {
                var @enum = value as Enum;
                writer.WriteValue(@enum.ToString("F"));
                return true;
            }

            if (value is ushort || value is uint || value is ulong || value is DateTimeOffset)
            {
                writer.WriteValue(value);
                return true;
            }

            return false;
        }

        private static bool TryWriteStringValue<T>(this XmlWriter writer, T value)
        {
            if (value is string)
            {
                var str = value as string;
                if (str != null && string.IsNullOrWhiteSpace(str)) writer.WriteWhitespace(str); //str is whitespace
                if (!string.IsNullOrWhiteSpace(str)) writer.WriteValue(str);
                return true;
            }

            return false;
        }

        private static bool TryWriteXmlSerializableValue<T>(this XmlWriter writer, T value)
        {
            if (value is IXmlSerializable serializable)
            {
                serializable.WriteXml(writer);
                return true;
            }
            return false;
        }

        public static void WriteValue<T>(this XmlWriter writer, T value)
        {
            if (writer.TryWriteXmlSerializableValue(value)) return;
            if (writer.TryWriteStringValue(value)) return;
            if (writer.TryWritePrimitiveValue(value)) return;
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string localName, T value)
        {
            if (value is string) writer.SafeWriteElementString(localName, value.ToString());
            else
            {
                writer.WriteStartElement(localName);
                writer.WriteValue(value);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string localName, string ns, T value)
        {
            if (value is string) writer.SafeWriteElementString(localName, ns, value.ToString());
            else
            {
                writer.WriteStartElement(localName, ns);
                writer.WriteValue(value);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string prefix, string localName, string ns, T value)
        {
            if (value is string) writer.SafeWriteElementString(prefix, localName, ns, value.ToString());
            else
            {
                writer.WriteStartElement(prefix, localName, ns);
                writer.WriteValue(value);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string localName, T value, params (string localName, string value)[] attributes)
        {
            if (value is string) writer.SafeWriteElementString(localName, value.ToString(), attributes);
            else
            {
                writer.WriteStartElement(localName);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.localName, attribute.value);
                writer.WriteValue(value);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string localName, string ns, T value, params (string localName, string value)[] attributes)
        {
            if (value is string) writer.SafeWriteElementString(localName, ns, value.ToString(), attributes);
            else
            {
                writer.WriteStartElement(localName, ns);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.localName, attribute.value);
                writer.WriteValue(value);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string prefix, string localName, string ns, T value, params (string localName, string value)[] attributes)
        {
            if (value is string) writer.SafeWriteElementString(prefix, localName, ns, value.ToString(), attributes);
            else
            {
                writer.WriteStartElement(prefix, localName, ns);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.localName, attribute.value);
                writer.WriteValue(value);
                writer.WriteEndElement();
            }
        }

        public static async Task SafeWriteElementStringAsync(this XmlWriter writer, string localName, string value)
        {
            if (!string.IsNullOrEmpty(value))
                await writer.WriteElementStringAsync(null, localName, null, value);
        }

        public static async Task SafeWriteElementStringAsync(this XmlWriter writer, string localName, string ns, string value)
        {
            if (!string.IsNullOrEmpty(value))
                await writer.WriteElementStringAsync(null, localName, ns, value);
        }

        public static async Task SafeWriteElementStringAsync(this XmlWriter writer, string prefix, string localName, string ns, string value)
        {
            if (!string.IsNullOrEmpty(value))
                await writer.WriteElementStringAsync(prefix, localName, ns, value);
        }

        public static async Task SafeWriteElementStringAsync(this XmlWriter writer, string localName, string value, params (string localName, string value)[] attributes)
        {
            if (!string.IsNullOrEmpty(value))
            {
                await writer.WriteStartElementAsync(null, localName, null);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.localName, attribute.value);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync(this XmlWriter writer, string localName, string ns, string value, params (string localName, string value)[] attributes)
        {
            if (!string.IsNullOrEmpty(value))
            {
                await writer.WriteStartElementAsync(null, localName, ns);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.localName, attribute.value);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync(this XmlWriter writer, string prefix, string localName, string ns, string value, params (string localName, string value)[] attributes)
        {
            if (!string.IsNullOrEmpty(value))
            {
                await writer.WriteStartElementAsync(prefix, localName, ns);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.localName, attribute.value);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteAttributeStringAsync(this XmlWriter writer, string localName, string value)
        {
            if (!string.IsNullOrEmpty(value)) await writer.WriteAttributeStringAsync(null, localName, null, value);
        }

        public static async Task SafeWriteAttributeStringAsync(this XmlWriter writer, string localName, string ns, string value)
        {
            if (!string.IsNullOrEmpty(value)) await writer.WriteAttributeStringAsync(null, localName, ns, value);
        }

        public static async Task SafeWriteAttributeStringAsync(this XmlWriter writer, string prefix, string localName, string ns, string value)
        {
            if (!string.IsNullOrEmpty(value)) await writer.WriteAttributeStringAsync(prefix, localName, ns, value);
        }

        private static async Task<bool> TryWriteStringValueAsync<T>(this XmlWriter writer, T value)
        {
            if (!(value is string)) return false;
            var str = value.ToString();
            if (str != null && string.IsNullOrWhiteSpace(str)) await writer.WriteWhitespaceAsync(str);
            if (!string.IsNullOrWhiteSpace(str)) writer.WriteValue(str);
            return true;
        }

        public static async Task WriteValueAsync<T>(this XmlWriter writer, T value)
        {
            if (writer.TryWritePrimitiveValue(value)) return;
            if (await writer.TryWriteStringValueAsync(value)) return;
            if (writer.TryWriteXmlSerializableValue(value)) return;
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string localName, T value)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(localName, value.ToString());
            else
            {
                await writer.WriteStartElementAsync(null, localName, null);
                await writer.WriteValueAsync(value);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string localName, string ns, T value)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(localName, ns, value.ToString());
            else
            {
                await writer.WriteStartElementAsync(null, localName, ns);
                await writer.WriteValueAsync(value);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string prefix, string localName, string ns, T value)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(prefix, localName, ns, value.ToString());
            else
            {
                await writer.WriteStartElementAsync(prefix, localName, ns);
                await writer.WriteValueAsync(value);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string localName, T value, params (string localName, string value)[] attributes)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(localName, value.ToString(), attributes);
            else
            {
                await writer.WriteStartElementAsync(null, localName, null);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.localName, attribute.value);
                await writer.WriteValueAsync(value);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string localName, string ns, T value, params (string localName, string value)[] attributes)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(localName, ns, value.ToString(), attributes);
            else
            {
                await writer.WriteStartElementAsync(null, localName, ns);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.localName, attribute.value);
                await writer.WriteValueAsync(value);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string prefix, string localName, string ns, T value, params (string localName, string value)[] attributes)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(prefix, localName, ns, value.ToString(), attributes);
            else
            {
                await writer.WriteStartElementAsync(prefix, localName, ns);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.localName, attribute.value);

                await writer.WriteValueAsync(value);
                await writer.WriteEndElementAsync();
            }
        }
    }
}