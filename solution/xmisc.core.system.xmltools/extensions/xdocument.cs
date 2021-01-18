using System;
using System.Xml;
using System.Xml.Linq;

namespace reexmonkey.xmisc.core.system.xmltools.extensions
{
    public static class XDocumentExtensions
    {
        public static XDocument AsXDocument(this string xml) => XDocument.Parse(xml);

        public static XDocument AsXDocument(this string xml, LoadOptions options) => XDocument.Parse(xml, options);

        public static (bool status, XDocument document, Exception exception) TryAsXDocument(this string xml)
        {
            try
            {
                var document = XDocument.Parse(xml);
                return (true, document, null);
            }
            catch (XmlException ex)
            {
                return (false, default(XDocument), ex);
            }
            catch (Exception ex)
            {
                return (false, default(XDocument), ex);
            }
        }

        public static (bool status, XDocument document, Exception exception) TryAsXDocument(this string xml, LoadOptions options)
        {
            try
            {
                var document = XDocument.Parse(xml, options);
                return (true, document, null);
            }
            catch (XmlException ex)
            {
                return (false, default(XDocument), ex);
            }
            catch (Exception ex)
            {
                return (false, default(XDocument), ex);
            }
        }
    }
}