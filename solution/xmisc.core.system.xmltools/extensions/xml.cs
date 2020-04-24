using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace reexmonkey.xmisc.core.system.xmltools.extensions
{
    public static class XmlExtensions
    {
        public static XDocument AsXDocument(this string xml) => XDocument.Parse(xml);

        public static XDocument AsXDocument(this string xml, LoadOptions options) => XDocument.Parse(xml, options);

        public static (bool status, XDocument document, Exception exception) TryAsXDocument(this string xml)
        {
            XDocument document = null;
            try
            {
                document = XDocument.Parse(xml);
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
            XDocument document = null;
            try
            {
                document = XDocument.Parse(xml, options);
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

        public static Task<(bool status, XDocument document, Exception exception)> TryAsXDocumentAsync(this string xml)
            => Task.FromResult(TryAsXDocument(xml));

        public static Task<(bool status, XDocument document, Exception exception)> TryAsXDocumentAsync(this string xml, LoadOptions options)
            => Task.FromResult(TryAsXDocument(xml, options));

        public static Task<XDocument> AsXDocumentAsync(this string xml) => Task.FromResult(AsXDocument(xml));

        public static Task<XDocument> AsXDocumentAsync(this string xml, LoadOptions options) => Task.FromResult(XDocument.Parse(xml, options));
    }
}
