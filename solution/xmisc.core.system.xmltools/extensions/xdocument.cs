using System;
using System.Xml;
using System.Xml.Linq;

namespace reexmonkey.xmisc.core.system.xmltools.extensions
{
    public static class XDocumentExtensions
    {
        public static XDocument AsXDocument(this string xml) => XDocument.Parse(xml);

        public static XDocument AsXDocument(this string xml, LoadOptions options) => XDocument.Parse(xml, options);

        public static bool TryAsXDocument(this string xml, out XDocument document)
        {
            try
            {
                document = XDocument.Parse(xml);
                return true;
            }
            catch (XmlException)
            {
                document = default;
                return false;
            }
            catch (Exception)
            {
                document = default;
                return false;                
            }
        }

        public static bool TryAsXDocument(this string xml, LoadOptions options, out XDocument document)
        {
            try
            {
                document = XDocument.Parse(xml, options);
                return true;
            }
            catch (XmlException)
            {
                document = default;
                return false;
            }
            catch (Exception)
            {
                document = default;
                return false;
            }
        }
    }
}