using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace reexmonkey.xmisc.core.system.xmltools.extensions
{
    public static class XElementExtensions
    {
        public static T Deserialize<T>(this XmlSerializer serializer, XElement element)
        {
            var reader = element.CreateReader();
            return serializer.CanDeserialize(reader)
                ? (T)serializer.Deserialize(reader)
                : default;
        }

        public static XElement Serialize<T>(this XmlSerializer serializer, T value, Encoding encoding)
        {
            using (var stream = new MemoryStream())
            {
                serializer.Serialize(stream, value);
                return XElement.Parse(encoding.GetString(stream.ToArray()));
            }
        }

        public static IEnumerable<XElement> Serialize<T>(this XmlSerializer serializer, IEnumerable<T> values, Encoding encoding)
        {
            List<XElement> elements = new List<XElement>();
            foreach (var value in values)
            {
                var element = serializer.Serialize(value, encoding);
                if (element != null) elements.Add(element);
            }
            return elements;
        }

        public static bool IsValid(this XElement element) => !string.IsNullOrEmpty((string)element);
    }
}