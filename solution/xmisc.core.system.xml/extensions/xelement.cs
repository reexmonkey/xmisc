using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace reexmonkey.xmisc.core.system.xml.extensions
{
    public static class XElementExtensions
    {
        private static T Deserialize<T>(this XmlSerializer serializer, XElement element)
        {
            var reader = element.CreateReader();
            return serializer.CanDeserialize(reader)
                ? (T)serializer.Deserialize(reader)
                : default(T);
        }

        private static XElement Serialize<T>(this XmlSerializer serializer, T value, Encoding encoding)
        {
            using (var stream = new MemoryStream())
            {
                serializer.Serialize(stream, value);
                return XElement.Parse(encoding.GetString(stream.ToArray()));
            }
        }

        private static IEnumerable<XElement> Serialize<T>(this XmlSerializer serializer, IEnumerable<T> values, Encoding encoding)
        {
            List<XElement> elements = new List<XElement>();
            using (var stream = new MemoryStream())
            {
                foreach (var value in values)
                {
                    serializer.Serialize(stream, value);
                    var element = XElement.Parse(encoding.GetString(stream.ToArray()));
                    if (element != null) elements.Add(element);
                }
            }
            return elements;
        }

        public static T As<T>(this XElement element)
        {
            var serializer = new XmlSerializer(typeof(T), element.GetDefaultNamespace().NamespaceName);
            return serializer.Deserialize<T>(element);
        }

        public static T As<T>(this XElement element, XmlRootAttribute attribute)
        {
            var serializer = new XmlSerializer(typeof(T), attribute);
            return serializer.Deserialize<T>(element);
        }

        public static T As<T>(this XElement element, Type[] extraTypes)
        {
            var serializer = new XmlSerializer(typeof(T), extraTypes);
            return serializer.Deserialize<T>(element);
        }

        public static IEnumerable<T> As<T>(this IEnumerable<XElement> elements, string defaultNamespace)
        {
            var serializer = new XmlSerializer(typeof(T), defaultNamespace);
            return elements.Select(serializer.Deserialize<T>);
        }

        public static IEnumerable<T> As<T>(this IEnumerable<XElement> elements, XmlRootAttribute attribute)
        {
            var serializer = new XmlSerializer(typeof(T), attribute);
            return elements.Select(serializer.Deserialize<T>);
        }

        public static IEnumerable<T> As<T>(this IEnumerable<XElement> elements, Type[] extraTypes)
        {
            var serializer = new XmlSerializer(typeof(T), extraTypes);
            return elements.Select(serializer.Deserialize<T>);
        }

        public static XElement AsXElement<T>(this T value, Encoding encoding)
        {
            var serializer = new XmlSerializer(value.GetType());
            return serializer.Serialize(value, encoding);
        }

        public static XElement AsXElement<T>(this T value, Encoding encoding, string defaultNamespace)
        {
            var serializer = new XmlSerializer(value.GetType(), defaultNamespace);
            return serializer.Serialize(value, encoding);
        }

        public static XElement AsXElement<T>(this T value, Encoding encoding, XmlRootAttribute attribute)
        {
            var serializer = new XmlSerializer(value.GetType(), attribute);
            return serializer.Serialize(value, encoding);
        }

        public static XElement AsXElement<T>(this T value, Encoding encoding, Type[] extraTypes)
        {
            var serializer = new XmlSerializer(value.GetType(), extraTypes);
            return serializer.Serialize(value, encoding);
        }

        public static IEnumerable<XElement> AsXElements<T>(this IEnumerable<T> values, Encoding encoding)
        {
            var elements = new List<XElement>();
            foreach (var value in values)
            {
                var serializer = new XmlSerializer(value.GetType());
                var element = serializer.Serialize(value, encoding);
                elements.Add(element);
            }
            return elements;
        }

        public static IEnumerable<XElement> AsXElements<T>(this IEnumerable<T> values, Encoding encoding, string defaultNamespace)
        {
            var elements = new List<XElement>();
            foreach (var value in values)
            {
                var serializer = new XmlSerializer(value.GetType(), defaultNamespace);
                var element = serializer.Serialize(value, encoding);
                elements.Add(element);
            }
            return elements;
        }

        public static IEnumerable<XElement> AsXElements<T>(this IEnumerable<T> values, Encoding encoding, XmlRootAttribute attribute)
        {
            var elements = new List<XElement>();
            foreach (var value in values)
            {
                var serializer = new XmlSerializer(value.GetType(), attribute);
                var element = serializer.Serialize(value, encoding);
                elements.Add(element);
            }
            return elements;
        }

        public static IEnumerable<XElement> AsXElements<T>(this IEnumerable<T> values, Encoding encoding, Type[] extraTypes)
        {
            var elements = new List<XElement>();
            foreach (var value in values)
            {
                var serializer = new XmlSerializer(value.GetType(), extraTypes);
                var element = serializer.Serialize(value, encoding);
                elements.Add(element);
            }
            return elements;
        }

        public static bool IsValid(this XElement element) => !string.IsNullOrEmpty((string)element);

        private static Task<T> DeserializeAsync<T>(this XmlSerializer serializer, XElement element)
        {
            return Task.FromResult(serializer.Deserialize<T>(element));
        }

        private static Task<XElement> SerializeAsync<T>(this XmlSerializer serializer, T value, Encoding encoding)
        {
            return Task.FromResult(serializer.Serialize(value, encoding));
        }

        private static Task<IEnumerable<XElement>> SerializeAsync<T>(this XmlSerializer serializer, IEnumerable<T> values, Encoding encoding)
        {
            return Task.FromResult(serializer.Serialize(values, encoding));
        }

        public static Task<T> AsAsync<T>(this XElement element)
        {
            return Task.FromResult(element.As<T>());
        }

        public static Task<T> AsAsync<T>(this XElement element, XmlRootAttribute attribute)
        {
            return Task.FromResult(element.As<T>(attribute));
        }

        public static Task<T> AsAsync<T>(this XElement element, Type[] extraTypes)
        {
            return Task.FromResult(element.As<T>(extraTypes));
        }

        public static Task<IEnumerable<T>> AsAsync<T>(this IEnumerable<XElement> elements, string defaultNamespace)
        {
            return Task.FromResult(elements.As<T>(defaultNamespace));
        }

        public static Task<IEnumerable<T>> AsAsync<T>(this IEnumerable<XElement> elements, XmlRootAttribute attribute)
        {
            return Task.FromResult(elements.As<T>(attribute));
        }

        public static Task<IEnumerable<T>> AsAsync<T>(this IEnumerable<XElement> elements, Type[] extraTypes)
        {
            return Task.FromResult(elements.As<T>(extraTypes));
        }

        public static Task<XElement> AsXElementAsync<T>(this T value, Encoding encoding)
        {
            return Task.FromResult(value.AsXElement(encoding));
        }

        public static Task<XElement> AsXElementAsync<T>(this T value, Encoding encoding, string defaultNamespace)
        {
            return Task.FromResult(value.AsXElement(encoding, defaultNamespace));
        }

        public static Task<XElement> AsXElementAsync<T>(this T value, Encoding encoding, XmlRootAttribute attribute)
        {
            return Task.FromResult(value.AsXElement(encoding, attribute));
        }

        public static Task<XElement> AsXElementAsync<T>(this T value, Encoding encoding, Type[] extraTypes)
        {
            return Task.FromResult(value.AsXElement(encoding, extraTypes));
        }

        public static Task<IEnumerable<XElement>> AsXElementsAsync<T>(this IEnumerable<T> values, Encoding encoding)
        {
            return Task.FromResult(values.AsXElements(encoding));
        }

        public static Task<IEnumerable<XElement>> AsXElementsAsync<T>(this IEnumerable<T> values, Encoding encoding, string defaultNamespace)
            => Task.FromResult(values.AsXElements(encoding, defaultNamespace));

        public static Task<IEnumerable<XElement>> AsXElementsAsync<T>(this IEnumerable<T> values, Encoding encoding, XmlRootAttribute attribute)
            => Task.FromResult(values.AsXElements(encoding, attribute));

        public static Task<IEnumerable<XElement>> AsXElementsAsync<T>(this IEnumerable<T> values, Encoding encoding, Type[] extraTypes)
            => Task.FromResult(values.AsXElements(encoding, extraTypes));
    }
}
