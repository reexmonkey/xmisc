using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace reexmonkey.xmisc.core.system.xmltools.extensions
{
    public static class XmlWriterExtensions
    {
        public static void SafeWriteElementString(this XmlWriter writer, string localName, string value)
        {
            if (!string.IsNullOrEmpty(value)) writer.WriteElementString(localName, value);
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, string ns, string value)
        {
            if (!string.IsNullOrEmpty(value)) writer.WriteElementString(localName, ns, value);
        }

        public static void SafeWriteElementString(this XmlWriter writer, string prefix, string localName, string ns, string value)
        {
            if (!string.IsNullOrEmpty(value)) writer.WriteElementString(prefix, localName, ns, value);
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

        public static void SafeWriteElementString(this XmlWriter writer, string localName, string value, params (string localName, string ns, string value)[] attributes)
        {
            if (!string.IsNullOrEmpty(value))
            {
                writer.WriteStartElement(localName);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.localName, attribute.ns, attribute.value);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, string ns, string value, params (string localName, string ns, string value)[] attributes)
        {
            if (!string.IsNullOrEmpty(value))
            {
                writer.WriteStartElement(localName, ns);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.localName, attribute.ns, attribute.value);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString(this XmlWriter writer, string prefix, string localName, string ns, string value, params (string localName, string ns, string value)[] attributes)
        {
            if (!string.IsNullOrEmpty(value))
            {
                writer.WriteStartElement(prefix, localName, ns);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.localName, attribute.ns, attribute.value);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, string value, params (string prefix, string localName, string ns, string value)[] attributes)
        {
            if (!string.IsNullOrEmpty(value))
            {
                writer.WriteStartElement(localName);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.prefix, attribute.localName, attribute.ns, attribute.value);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString(this XmlWriter writer, string localName, string ns, string value, params (string prefix, string localName, string ns, string value)[] attributes)
        {
            if (!string.IsNullOrEmpty(value))
            {
                writer.WriteStartElement(localName, ns);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.prefix, attribute.localName, attribute.ns, attribute.value);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString(this XmlWriter writer, string prefix, string localName, string ns, string value, params (string prefix, string localName, string ns, string value)[] attributes)
        {
            if (!string.IsNullOrEmpty(value))
            {
                writer.WriteStartElement(prefix, localName, ns);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.prefix, attribute.localName, attribute.ns, attribute.value);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementStrings(this XmlWriter writer, string localName, IEnumerable<string> values)
        {
            foreach (var value in values) writer.WriteElementString(localName, value);
        }

        public static void SafeWriteElementStrings(this XmlWriter writer, string localName, string ns, IEnumerable<string> values)
        {
            foreach (var value in values) writer.WriteElementString(localName, ns, value);
        }

        public static void SafeWriteElementStrings(this XmlWriter writer, string prefix, string localName, string ns, IEnumerable<string> values)
        {
            foreach (var value in values) writer.WriteElementString(prefix, localName, ns, value);
        }

        public static void SafeWriteElementString(this XmlWriter writer, (string localName, string value) element)
        {
            writer.WriteElementString(element.localName, element.value);
        }

        public static void SafeWriteElementString(this XmlWriter writer, (string localName, string ns, string value) element)
        {
            writer.WriteElementString(element.localName, element.ns, element.value);
        }

        public static void SafeWriteElementString(this XmlWriter writer, (string prefix, string localName, string ns, string value) element)
        {
            writer.WriteElementString(element.prefix, element.localName, element.ns, element.value);
        }

        public static void SafeWriteElementStrings(this XmlWriter writer, IEnumerable<(string localName, string value)> elements)
        {
            foreach (var element in elements) writer.SafeWriteElementString(element);
        }

        public static void SafeWriteElementStrings(this XmlWriter writer, IEnumerable<(string localName, string ns, string value)> elements)
        {
            foreach (var element in elements) writer.SafeWriteElementString(element);
        }

        public static void SafeWriteElementStrings(this XmlWriter writer, IEnumerable<(string prefix, string localName, string ns, string value)> elements)
        {
            foreach (var element in elements) writer.SafeWriteElementString(element);
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
                var str = value.ToString();
                if (str != null && string.IsNullOrWhiteSpace(str)) writer.WriteWhitespace(str); //str is whitespace
                if (!string.IsNullOrWhiteSpace(str)) writer.WriteValue(str);
                return true;
            }

            return false;
        }

        private static bool TryWriteXElementValue<T>(this XmlWriter writer, T value, Encoding encoding)
        {
            if (value is XElement)
            {
                var element = value.AsXElement(encoding);
                element.WriteTo(writer);
                return true;
            }
            return false;
        }

        private static bool TryWriteXElementValue<T>(this XmlWriter writer, T value, Encoding encoding, string defaultNamespace)
        {
            if (value is XElement)
            {
                var element = value.AsXElement(encoding, defaultNamespace);
                element.WriteTo(writer);
                return true;
            }
            return false;
        }

        private static bool TryWriteXElementValue<T>(this XmlWriter writer, T value, Encoding encoding, XmlRootAttribute attribute)
        {
            if (value is XElement)
            {
                var element = value.AsXElement(encoding, attribute);
                element.WriteTo(writer);
                return true;
            }
            return false;
        }

        private static bool TryWriteXElementValue<T>(this XmlWriter writer, T value, Encoding encoding, Type[] extraTypes)
        {
            if (value is XElement)
            {
                var element = value.AsXElement(encoding, extraTypes);
                element.WriteTo(writer);
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

        private static void SerializeValue<T>(this XmlWriter writer, T value)
        {
            var serializer = new XmlSerializer(value.GetType());
            serializer.Serialize(writer, value);
        }

        private static void SerializeValue<T>(this XmlWriter writer, T value, string defaultNamespace)
        {
            var serializer = new XmlSerializer(value.GetType(), defaultNamespace);
            serializer.Serialize(writer, value);
        }

        private static void SerializeValue<T>(this XmlWriter writer, T value, XmlRootAttribute attribute)
        {
            var serializer = new XmlSerializer(value.GetType(), attribute);
            serializer.Serialize(writer, value);
        }

        private static void SerializeValue<T>(this XmlWriter writer, T value, Type[] extraTypes)
        {
            var serializer = new XmlSerializer(value.GetType(), extraTypes);
            serializer.Serialize(writer, value);
        }

        public static void WriteValue<T>(this XmlWriter writer, T value, Encoding encoding)
        {
            if (writer.TryWritePrimitiveValue(value)) return;
            if (writer.TryWriteStringValue(value)) return;
            if (writer.TryWriteXElementValue(value, encoding)) return;
            if (writer.TryWriteXmlSerializableValue(value)) return;
            writer.SerializeValue(value);
        }

        public static void WriteValue<T>(this XmlWriter writer, T value, Encoding encoding, string defaultNamespace)
        {
            if (writer.TryWritePrimitiveValue(value)) return;
            if (writer.TryWriteStringValue(value)) return;
            if (writer.TryWriteXElementValue(value, encoding, defaultNamespace)) return;
            if (writer.TryWriteXmlSerializableValue(value)) return;
            writer.SerializeValue(value, defaultNamespace);
        }

        public static void WriteValue<T>(this XmlWriter writer, T value, Encoding encoding, XmlRootAttribute attribute)
        {
            if (writer.TryWritePrimitiveValue(value)) return;
            if (writer.TryWriteStringValue(value)) return;
            if (writer.TryWriteXElementValue(value, encoding, attribute)) return;
            if (writer.TryWriteXmlSerializableValue(value)) return;
            writer.SerializeValue(value, attribute);
        }

        public static void WriteValue<T>(this XmlWriter writer, T value, Encoding encoding, Type[] extraTypes)
        {
            if (writer.TryWritePrimitiveValue(value)) return;
            if (writer.TryWriteStringValue(value)) return;
            if (writer.TryWriteXElementValue(value, encoding, extraTypes)) return;
            if (writer.TryWriteXmlSerializableValue(value)) return;
            writer.SerializeValue(value, extraTypes);
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string localName, T value, Encoding encoding)
        {
            if (value is string) writer.SafeWriteElementString(localName, value.ToString());
            else
            {
                writer.WriteStartElement(localName);
                writer.WriteValue(value, encoding);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string localName, string ns, T value, Encoding encoding)
        {
            if (value is string) writer.SafeWriteElementString(localName, ns, value.ToString());
            else
            {
                writer.WriteStartElement(localName, ns);
                writer.WriteValue(value, encoding, ns);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string prefix, string localName, string ns, T value, Encoding encoding)
        {
            if (value is string) writer.SafeWriteElementString(prefix, localName, ns, value.ToString());
            else
            {
                writer.WriteStartElement(prefix, localName, ns);
                writer.WriteValue(value, encoding, ns);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string localName, T value, Encoding encoding, XmlRootAttribute rootAttribue)
        {
            if (value is string) writer.SafeWriteElementString(localName, rootAttribue.Namespace, value.ToString());
            else
            {
                writer.WriteStartElement(localName);
                writer.WriteValue(value, encoding, rootAttribue);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string localName, T value, Encoding encoding, Type[] extraTypes)
        {
            if (value is string) writer.SafeWriteElementString(localName, value.ToString());
            else
            {
                writer.WriteStartElement(localName);
                writer.WriteValue(value, encoding, extraTypes);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string localName, string ns, T value, Encoding encoding, Type[] extraTypes)
        {
            if (value is string) writer.SafeWriteElementString(localName, ns, value.ToString());
            else
            {
                writer.WriteStartElement(localName, ns);
                writer.WriteValue(value, encoding, extraTypes);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string prefix, string localName, string ns, T value, Encoding encoding, Type[] extraTypes)
        {
            if (value is string) writer.SafeWriteElementString(prefix, localName, ns, value.ToString());
            else
            {
                writer.WriteStartElement(prefix, localName, ns);
                writer.WriteValue(value, encoding, extraTypes);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string localName, T value, Encoding encoding, params (string localName, string value)[] attributes)
        {
            if (value is string) writer.SafeWriteElementString(localName, value.ToString(), attributes);
            else
            {
                writer.WriteStartElement(localName);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.localName, attribute.value);
                writer.WriteValue(value, encoding);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string localName, T value, Encoding encoding, params (string localName, string ns, string value)[] attributes)
        {
            if (value is string) writer.SafeWriteElementString(localName, value.ToString(), attributes);
            else
            {
                writer.WriteStartElement(localName);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.localName, attribute.ns, attribute.value);
                writer.WriteValue(value, encoding);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string localName, T value, Encoding encoding, params (string prefix, string localName, string ns, string value)[] attributes)
        {
            if (value is string) writer.SafeWriteElementString(localName, value.ToString(), attributes);
            else
            {
                writer.WriteStartElement(localName);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.prefix, attribute.localName, attribute.ns, attribute.value);
                writer.WriteValue(value, encoding);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string localName, string ns, T value, Encoding encoding, params (string localName, string value)[] attributes)
        {
            if (value is string) writer.SafeWriteElementString(localName, ns, value.ToString(), attributes);
            else
            {
                writer.WriteStartElement(localName, ns);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.localName, attribute.value);
                writer.WriteValue(value, encoding, ns);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string localName, string ns, T value, Encoding encoding, params (string localName, string ns, string value)[] attributes)
        {
            if (value is string) writer.SafeWriteElementString(localName, ns, value.ToString(), attributes);
            else
            {
                writer.WriteStartElement(localName, ns);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.localName, attribute.ns, attribute.value);
                writer.WriteValue(value, encoding, ns);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string localName, string ns, T value, Encoding encoding, params (string prefix, string localName, string ns, string value)[] attributes)
        {
            if (value is string) writer.SafeWriteElementString(localName, ns, value.ToString(), attributes);
            else
            {
                writer.WriteStartElement(localName, ns);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.prefix, attribute.localName, attribute.ns, attribute.value);
                writer.WriteValue(value, encoding, ns);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string prefix, string localName, string ns, T value, Encoding encoding, params (string localName, string value)[] attributes)
        {
            if (value is string) writer.SafeWriteElementString(prefix, localName, ns, value.ToString(), attributes);
            else
            {
                writer.WriteStartElement(prefix, localName, ns);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.localName, attribute.value);
                writer.WriteValue(value, encoding, ns);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string prefix, string localName, string ns, T value, Encoding encoding, params (string localName, string ns, string value)[] attributes)
        {
            if (value is string) writer.SafeWriteElementString(prefix, localName, ns, value.ToString(), attributes);
            else
            {
                writer.WriteStartElement(prefix, localName, ns);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.localName, attribute.ns, attribute.value);
                writer.WriteValue(value, encoding, ns);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string prefix, string localName, string ns, T value, Encoding encoding, params (string prefix, string localName, string ns, string value)[] attributes)
        {
            if (value is string) writer.SafeWriteElementString(prefix, localName, ns, value.ToString(), attributes);
            else
            {
                writer.WriteStartElement(prefix, localName, ns);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.prefix, attribute.localName, attribute.ns, attribute.value);
                writer.WriteValue(value, encoding, ns);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string localName, T value, Encoding encoding, XmlRootAttribute rootAttribue, params (string localName, string value)[] attributes)
        {
            if (value is string) writer.SafeWriteElementString(localName, rootAttribue.Namespace, value.ToString(), attributes);
            else
            {
                writer.WriteStartElement(localName);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.localName, attribute.value);
                writer.WriteValue(value, encoding, rootAttribue);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string localName, T value, Encoding encoding, XmlRootAttribute rootAttribute, params (string localName, string ns, string value)[] attributes)
        {
            if (value is string) writer.SafeWriteElementString(localName, rootAttribute.Namespace, value.ToString(), attributes);
            else
            {
                writer.WriteStartElement(localName);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.localName, attribute.ns, attribute.value);
                writer.WriteValue(value, encoding, rootAttribute);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string localName, T value, Encoding encoding, XmlRootAttribute rootAttribute, params (string prefix, string localName, string ns, string value)[] attributes)
        {
            if (value is string) writer.SafeWriteElementString(localName, rootAttribute.Namespace, value.ToString(), attributes);
            else
            {
                writer.WriteStartElement(localName);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.prefix, attribute.localName, attribute.ns, attribute.value);
                writer.WriteValue(value, encoding, rootAttribute);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string localName, T value, Encoding encoding, Type[] extraTypes, params (string localName, string value)[] attributes)
        {
            if (value is string) writer.SafeWriteElementString(localName, value.ToString(), attributes);
            else
            {
                writer.WriteStartElement(localName);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.localName, attribute.value);
                writer.WriteValue(value, encoding, extraTypes);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string localName, T value, Encoding encoding, Type[] extraTypes, params (string localName, string ns, string value)[] attributes)
        {
            if (value is string) writer.SafeWriteElementString(localName, value.ToString(), attributes);
            else
            {
                writer.WriteStartElement(localName);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.localName, attribute.ns, attribute.value);
                writer.WriteValue(value, encoding, extraTypes);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string localName, T value, Encoding encoding, Type[] extraTypes, params (string prefix, string localName, string ns, string value)[] attributes)
        {
            if (value is string) writer.SafeWriteElementString(localName, value.ToString(), attributes);
            else
            {
                writer.WriteStartElement(localName);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.prefix, attribute.localName, attribute.ns, attribute.value);
                writer.WriteValue(value, encoding, extraTypes);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string localName, string ns, T value, Encoding encoding, Type[] extraTypes, params (string localName, string value)[] attributes)
        {
            if (value is string) writer.SafeWriteElementString(localName, ns, value.ToString(), attributes);
            else
            {
                writer.WriteStartElement(localName, ns);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.localName, attribute.value);
                writer.WriteValue(value, encoding, extraTypes);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string localName, string ns, T value, Encoding encoding, Type[] extraTypes, params (string localName, string ns, string value)[] attributes)
        {
            if (value is string) writer.SafeWriteElementString(localName, ns, value.ToString(), attributes);
            else
            {
                writer.WriteStartElement(localName, ns);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.localName, attribute.ns, attribute.value);
                writer.WriteValue(value, encoding, extraTypes);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string localName, string ns, T value, Encoding encoding, Type[] extraTypes, params (string prefix, string localName, string ns, string value)[] attributes)
        {
            if (value is string) writer.SafeWriteElementString(localName, ns, value.ToString(), attributes);
            else
            {
                writer.WriteStartElement(localName, ns);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.prefix, attribute.localName, attribute.ns, attribute.value);
                writer.WriteValue(value, encoding, extraTypes);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string prefix, string localName, string ns, T value, Encoding encoding, Type[] extraTypes, params (string localName, string value)[] attributes)
        {
            if (value is string) writer.SafeWriteElementString(prefix, localName, ns, value.ToString(), attributes);
            else
            {
                writer.WriteStartElement(prefix, localName, ns);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.localName, attribute.value); writer.WriteValue(value, encoding, extraTypes);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string prefix, string localName, string ns, T value, Encoding encoding, Type[] extraTypes, params (string localName, string ns, string value)[] attributes)
        {
            if (value is string) writer.SafeWriteElementString(prefix, localName, ns, value.ToString(), attributes);
            else
            {
                writer.WriteStartElement(prefix, localName, ns);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.localName, attribute.ns, attribute.value);
                writer.WriteValue(value, encoding, extraTypes);
                writer.WriteEndElement();
            }
        }

        public static void SafeWriteElementString<T>(this XmlWriter writer, string prefix, string localName, string ns, T value, Encoding encoding, Type[] extraTypes, params (string prefix, string localName, string ns, string value)[] attributes)
        {
            if (value is string) writer.SafeWriteElementString(prefix, localName, ns, value.ToString(), attributes);
            else
            {
                writer.WriteStartElement(prefix, localName, ns);
                foreach (var attribute in attributes)
                    writer.SafeWriteAttributeString(attribute.prefix, attribute.localName, attribute.ns, attribute.value);
                writer.WriteValue(value, encoding, extraTypes);
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

        public static async Task SafeWriteElementStringAsync(this XmlWriter writer, string localName, string value, params (string localName, string ns, string value)[] attributes)
        {
            if (!string.IsNullOrEmpty(value))
            {
                await writer.WriteStartElementAsync(null, localName, null);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.localName, attribute.ns, attribute.value);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync(this XmlWriter writer, string localName, string ns, string value, params (string localName, string ns, string value)[] attributes)
        {
            if (!string.IsNullOrEmpty(value))
            {
                await writer.WriteStartElementAsync(null, localName, ns);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.localName, attribute.ns, attribute.value);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync(this XmlWriter writer, string prefix, string localName, string ns, string value, params (string localName, string ns, string value)[] attributes)
        {
            if (!string.IsNullOrEmpty(value))
            {
                await writer.WriteStartElementAsync(prefix, localName, ns);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.localName, attribute.ns, attribute.value);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync(this XmlWriter writer, string localName, string value, params (string prefix, string localName, string ns, string value)[] attributes)
        {
            if (!string.IsNullOrEmpty(value))
            {
                await writer.WriteStartElementAsync(null, localName, null);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.prefix, attribute.localName, attribute.ns, attribute.value);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync(this XmlWriter writer, string localName, string ns, string value, params (string prefix, string localName, string ns, string value)[] attributes)
        {
            if (!string.IsNullOrEmpty(value))
            {
                await writer.WriteStartElementAsync(null, localName, ns);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.prefix, attribute.localName, attribute.ns, attribute.value);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync(this XmlWriter writer, string prefix, string localName, string ns, string value, params (string prefix, string localName, string ns, string value)[] attributes)
        {
            if (!string.IsNullOrEmpty(value))
            {
                await writer.WriteStartElementAsync(prefix, localName, ns);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.prefix, attribute.localName, attribute.ns, attribute.value);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringsAsync(this XmlWriter writer, string localName, IEnumerable<string> values)
        {
            foreach (var value in values)
                await writer.SafeWriteElementStringAsync(localName, value);
        }

        public static async Task SafeWriteElementStringsAsync(this XmlWriter writer, string localName, string ns, IEnumerable<string> values)
        {
            foreach (var value in values)
                await writer.SafeWriteElementStringAsync(localName, ns, value);
        }

        public static async Task SafeWriteElementStringsAsync(this XmlWriter writer, string prefix, string localName, string ns, IEnumerable<string> values)
        {
            foreach (var value in values)
                await writer.SafeWriteElementStringAsync(prefix, localName, ns, value);
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

        public static async Task SafeWriteElementStringAsync(this XmlWriter writer, (string localName, string value) element)
        {
            await writer.SafeWriteElementStringAsync(element.localName, element.value);
        }

        public static async Task SafeWriteElementStringAsync(this XmlWriter writer, (string localName, string ns, string value) element)
        {
            await writer.SafeWriteElementStringAsync(element.localName, element.ns, element.value);
        }

        public static async Task SafeWriteElementStringAsync(this XmlWriter writer, (string prefix, string localName, string ns, string value) element)
        {
            await writer.SafeWriteElementStringAsync(element.prefix, element.localName, element.ns, element.value);
        }

        public static async Task SafeWriteElementStringsAsync(this XmlWriter writer, IEnumerable<(string localName, string value)> elements)
        {
            foreach (var element in elements) await writer.SafeWriteElementStringAsync(element);
        }

        public static async Task SafeWriteElementStringsAsync(this XmlWriter writer, IEnumerable<(string localName, string ns, string value)> elements)
        {
            foreach (var element in elements) await writer.SafeWriteElementStringAsync(element);
        }

        public static async Task SafeWriteElementStringsAsync(this XmlWriter writer, IEnumerable<(string prefix, string localName, string ns, string value)> elements)
        {
            foreach (var element in elements) await writer.SafeWriteElementStringAsync(element);
        }

        private static Task<bool> TryWritePrimitiveValueAsync<T>(this XmlWriter writer, T value)
        {
            return Task.FromResult(writer.TryWritePrimitiveValue(value));
        }

        private static async Task<bool> TryWriteStringValueAsync<T>(this XmlWriter writer, T value)
        {
            if (value is string)
            {
                var str = value.ToString();
                if (str != null && string.IsNullOrWhiteSpace(str)) await writer.WriteWhitespaceAsync(str); //str is whitespace
                if (!string.IsNullOrWhiteSpace(str)) writer.WriteValue(str);
                return true;
            }
            return false;
        }

        private static async Task<bool> TryWriteXElementValueAsync<T>(this XmlWriter writer, T value, Encoding encoding)
        {
            if (value is XElement)
            {
                var element = await value.AsXElementAsync(encoding);
                element.WriteTo(writer);
                return true;
            }

            return false;
        }

        private static async Task<bool> TryWriteXElementValueAsync<T>(this XmlWriter writer, T value, Encoding encoding, string defaultNamespace)
        {
            if (value is XElement)
            {
                var element = await value.AsXElementAsync(encoding, defaultNamespace);
                element.WriteTo(writer);
                return true;
            }

            return false;
        }

        private static async Task<bool> TryWriteXElementValueAsync<T>(this XmlWriter writer, T value, Encoding encoding, XmlRootAttribute attribute)
        {
            if (value is XElement)
            {
                var element = await value.AsXElementAsync(encoding, attribute);
                element.WriteTo(writer);
                return true;
            }

            return false;
        }

        private static async Task<bool> TryWriteXElementValueAsync<T>(this XmlWriter writer, T value, Encoding encoding, Type[] extraTypes)
        {
            if (value is XElement)
            {
                var element = await value.AsXElementAsync(encoding, extraTypes);
                element.WriteTo(writer);
                return true;
            }

            return false;
        }

        private static Task<bool> TryWriteXmlSerializableValueAsync<T>(this XmlWriter writer, T value)
        {
            return Task.FromResult(writer.TryWriteXmlSerializableValue(value));
        }

        public static async Task WriteValueAsync<T>(this XmlWriter writer, T value, Encoding encoding)
        {
            if (await writer.TryWritePrimitiveValueAsync(value)) return;
            if (await writer.TryWriteStringValueAsync(value)) return;
            if (await writer.TryWriteXElementValueAsync(value, encoding)) return;
            if (await writer.TryWriteXmlSerializableValueAsync(value)) return;
            writer.SerializeValue(value);
        }

        public static async Task WriteValueAsync<T>(this XmlWriter writer, T value, Encoding encoding, string defaultNamespace)
        {
            if (await writer.TryWritePrimitiveValueAsync(value)) return;
            if (await writer.TryWriteStringValueAsync(value)) return;
            if (await writer.TryWriteXElementValueAsync(value, encoding, defaultNamespace)) return;
            if (await writer.TryWriteXmlSerializableValueAsync(value)) return;
            writer.SerializeValue(value, defaultNamespace);
        }

        public static async Task WriteValueAsync<T>(this XmlWriter writer, T value, Encoding encoding, XmlRootAttribute attribute)
        {
            if (await writer.TryWritePrimitiveValueAsync(value)) return;
            if (await writer.TryWriteStringValueAsync(value)) return;
            if (await writer.TryWriteXElementValueAsync(value, encoding, attribute)) return;
            if (await writer.TryWriteXmlSerializableValueAsync(value)) return;
            writer.SerializeValue(value, attribute);
        }

        public static async Task WriteValueAsync<T>(this XmlWriter writer, T value, Encoding encoding, Type[] extraTypes)
        {
            if (await writer.TryWritePrimitiveValueAsync(value)) return;
            if (await writer.TryWriteStringValueAsync(value)) return;
            if (await writer.TryWriteXElementValueAsync(value, encoding, extraTypes)) return;
            if (await writer.TryWriteXmlSerializableValueAsync(value)) return;
            writer.SerializeValue(value, extraTypes);
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string localName, T value, Encoding encoding)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(localName, value.ToString());
            else
            {
                await writer.WriteStartElementAsync(null, localName, null);
                await writer.WriteValueAsync(value, encoding);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string localName, string ns, T value, Encoding encoding)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(localName, ns, value.ToString());
            else
            {
                await writer.WriteStartElementAsync(null, localName, ns);
                await writer.WriteValueAsync(value, encoding, ns);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string prefix, string localName, string ns, T value, Encoding encoding)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(prefix, localName, ns, value.ToString());
            else
            {
                await writer.WriteStartElementAsync(prefix, localName, ns);
                await writer.WriteValueAsync(value, encoding, ns);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string localName, T value, Encoding encoding, XmlRootAttribute rootAttribue)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(localName, rootAttribue.Namespace, value.ToString());
            else
            {
                await writer.WriteStartElementAsync(null, localName, null);
                await writer.WriteValueAsync(value, encoding, rootAttribue);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string localName, T value, Encoding encoding, Type[] extraTypes)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(localName, value.ToString());
            else
            {
                await writer.WriteStartElementAsync(null, localName, null);
                await writer.WriteValueAsync(value, encoding, extraTypes);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string localName, string ns, T value, Encoding encoding, Type[] extraTypes)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(localName, ns, value.ToString());
            else
            {
                await writer.WriteStartElementAsync(null, localName, ns);
                await writer.WriteValueAsync(value, encoding, extraTypes);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string prefix, string localName, string ns, T value, Encoding encoding, Type[] extraTypes)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(prefix, localName, ns, value.ToString());
            else
            {
                await writer.WriteStartElementAsync(prefix, localName, ns);
                await writer.WriteValueAsync(value, encoding, extraTypes);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string localName, T value, Encoding encoding, params (string localName, string value)[] attributes)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(localName, value.ToString(), attributes);
            else
            {
                await writer.WriteStartElementAsync(null, localName, null);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.localName, attribute.value);
                await writer.WriteValueAsync(value, encoding);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string localName, T value, Encoding encoding, params (string localName, string ns, string value)[] attributes)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(localName, value.ToString(), attributes);
            else
            {
                await writer.WriteStartElementAsync(null, localName, null);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.localName, attribute.ns, attribute.value);
                await writer.WriteValueAsync(value, encoding);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string localName, T value, Encoding encoding, params (string prefix, string localName, string ns, string value)[] attributes)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(localName, value.ToString(), attributes);
            else
            {
                await writer.WriteStartElementAsync(null, localName, null);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.prefix, attribute.localName, attribute.ns, attribute.value);
                await writer.WriteValueAsync(value, encoding);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string localName, string ns, T value, Encoding encoding, params (string localName, string value)[] attributes)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(localName, ns, value.ToString(), attributes);
            else
            {
                await writer.WriteStartElementAsync(null, localName, ns);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.localName, attribute.value);
                await writer.WriteValueAsync(value, encoding, ns);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string localName, string ns, T value, Encoding encoding, params (string localName, string ns, string value)[] attributes)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(localName, ns, value.ToString(), attributes);
            else
            {
                await writer.WriteStartElementAsync(null, localName, ns);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.localName, attribute.ns, attribute.value);
                await writer.WriteValueAsync(value, encoding, ns);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string localName, string ns, T value, Encoding encoding, params (string prefix, string localName, string ns, string value)[] attributes)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(localName, ns, value.ToString(), attributes);
            else
            {
                await writer.WriteStartElementAsync(null, localName, ns);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.prefix, attribute.localName, attribute.ns, attribute.value);
                await writer.WriteValueAsync(value, encoding, ns);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string prefix, string localName, string ns, T value, Encoding encoding, params (string localName, string value)[] attributes)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(prefix, localName, ns, value.ToString(), attributes);
            else
            {
                await writer.WriteStartElementAsync(prefix, localName, ns);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.localName, attribute.value);

                await writer.WriteValueAsync(value, encoding, ns);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string prefix, string localName, string ns, T value, Encoding encoding, params (string localName, string ns, string value)[] attributes)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(prefix, localName, ns, value.ToString(), attributes);
            else
            {
                await writer.WriteStartElementAsync(prefix, localName, ns);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.localName, attribute.ns, attribute.value);
                await writer.WriteValueAsync(value, encoding, ns);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string prefix, string localName, string ns, T value, Encoding encoding, params (string prefix, string localName, string ns, string value)[] attributes)
        {
            if (value is string) writer.SafeWriteElementStringAsync(prefix, localName, ns, value.ToString(), attributes);
            else
            {
                await writer.WriteStartElementAsync(prefix, localName, ns);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.prefix, attribute.localName, attribute.ns, attribute.value);
                await writer.WriteValueAsync(value, encoding, ns);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string localName, T value, Encoding encoding, XmlRootAttribute rootAttribue, params (string localName, string value)[] attributes)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(localName, rootAttribue.Namespace, value.ToString(), attributes);
            else
            {
                await writer.WriteStartElementAsync(null, localName, null);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.localName, attribute.value);
                await writer.WriteValueAsync(value, encoding, rootAttribue);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string localName, T value, Encoding encoding, XmlRootAttribute rootAttribute, params (string localName, string ns, string value)[] attributes)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(localName, rootAttribute.Namespace, value.ToString(), attributes);
            else
            {
                await writer.WriteStartElementAsync(null, localName, null);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.localName, attribute.ns, attribute.value);
                await writer.WriteValueAsync(value, encoding, rootAttribute);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string localName, T value, Encoding encoding, XmlRootAttribute rootAttribute, params (string prefix, string localName, string ns, string value)[] attributes)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(localName, rootAttribute.Namespace, value.ToString(), attributes);
            else
            {
                await writer.WriteStartElementAsync(null, localName, null);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.prefix, attribute.localName, attribute.ns, attribute.value);
                await writer.WriteValueAsync(value, encoding, rootAttribute);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string localName, T value, Encoding encoding, Type[] extraTypes, params (string localName, string value)[] attributes)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(localName, value.ToString(), attributes);
            else
            {
                await writer.WriteStartElementAsync(null, localName, null);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.localName, attribute.value);
                await writer.WriteValueAsync(value, encoding, extraTypes);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string localName, T value, Encoding encoding, Type[] extraTypes, params (string localName, string ns, string value)[] attributes)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(localName, value.ToString(), attributes);
            else
            {
                await writer.WriteStartElementAsync(null, localName, null);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.localName, attribute.ns, attribute.value);
                await writer.WriteValueAsync(value, encoding, extraTypes);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string localName, T value, Encoding encoding, Type[] extraTypes, params (string prefix, string localName, string ns, string value)[] attributes)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(localName, value.ToString(), attributes);
            else
            {
                await writer.WriteStartElementAsync(null, localName, null);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.prefix, attribute.localName, attribute.ns, attribute.value);
                await writer.WriteValueAsync(value, encoding, extraTypes);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string localName, string ns, T value, Encoding encoding, Type[] extraTypes, params (string localName, string value)[] attributes)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(localName, ns, value.ToString(), attributes);
            else
            {
                await writer.WriteStartElementAsync(null, localName, ns);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.localName, attribute.value);
                await writer.WriteValueAsync(value, encoding, extraTypes);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string localName, string ns, T value, Encoding encoding, Type[] extraTypes, params (string localName, string ns, string value)[] attributes)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(localName, ns, value.ToString(), attributes);
            else
            {
                await writer.WriteStartElementAsync(null, localName, ns);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.localName, attribute.ns, attribute.value);
                await writer.WriteValueAsync(value, encoding, extraTypes);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string localName, string ns, T value, Encoding encoding, Type[] extraTypes, params (string prefix, string localName, string ns, string value)[] attributes)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(localName, ns, value.ToString(), attributes);
            else
            {
                await writer.WriteStartElementAsync(null, localName, ns);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.prefix, attribute.localName, attribute.ns, attribute.value);
                await writer.WriteValueAsync(value, encoding, extraTypes);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string prefix, string localName, string ns, T value, Encoding encoding, Type[] extraTypes, params (string localName, string value)[] attributes)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(prefix, localName, ns, value.ToString(), attributes);
            else
            {
                await writer.WriteStartElementAsync(prefix, localName, ns);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.localName, attribute.value); await writer.WriteValueAsync(value, encoding, extraTypes);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string prefix, string localName, string ns, T value, Encoding encoding, Type[] extraTypes, params (string localName, string ns, string value)[] attributes)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(prefix, localName, ns, value.ToString(), attributes);
            else
            {
                await writer.WriteStartElementAsync(prefix, localName, ns);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.localName, attribute.ns, attribute.value);
                await writer.WriteValueAsync(value, encoding, extraTypes);
                await writer.WriteEndElementAsync();
            }
        }

        public static async Task SafeWriteElementStringAsync<T>(this XmlWriter writer, string prefix, string localName, string ns, T value, Encoding encoding, Type[] extraTypes, params (string prefix, string localName, string ns, string value)[] attributes)
        {
            if (value is string) await writer.SafeWriteElementStringAsync(prefix, localName, ns, value.ToString(), attributes);
            else
            {
                await writer.WriteStartElementAsync(prefix, localName, ns);
                foreach (var attribute in attributes)
                    await writer.SafeWriteAttributeStringAsync(attribute.prefix, attribute.localName, attribute.ns, attribute.value);
                await writer.WriteValueAsync(value, encoding, extraTypes);
                await writer.WriteEndElementAsync();
            }
        }
    }
}
