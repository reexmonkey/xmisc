using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace reexmonkey.xmisc.core.system.xml.extensions
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

        public static void SafeWriteElementStrings(this XmlWriter writer, string localName, IEnumerable<string> values)
        {
            foreach (var value in values)
                writer.WriteElementString(localName, value);
        }

        public static void SafeWriteElementStrings(this XmlWriter writer, string localName, string ns, IEnumerable<string> values)
        {
            foreach (var value in values)
                writer.WriteElementString(localName, ns, value);
        }

        public static void SafeWriteElementStrings(this XmlWriter writer, string prefix, string localName, string ns, IEnumerable<string> values)
        {
            foreach (var value in values)
                writer.WriteElementString(prefix, localName, ns, value);
        }

        public static async Task SafeWriteElementStringAsync(this XmlWriter writer, string localName, string value)
        {
            if (!string.IsNullOrEmpty(value))
                await writer.WriteElementStringAsync(string.Empty, localName, string.Empty, value);
        }

        public static async Task SafeWriteElementStringAsync(this XmlWriter writer, string localName, string ns, string value)
        {
            if (!string.IsNullOrEmpty(value))
                await writer.WriteElementStringAsync(string.Empty, localName, ns, value);
        }

        public static async Task SafeWriteElementStringAsync(this XmlWriter writer, string prefix, string localName, string ns, string value)
        {
            if (!string.IsNullOrEmpty(value))
                await writer.WriteElementStringAsync(prefix, localName, ns, value);
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
    }
}
