﻿using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace reexjungle.xmisc.infrastructure.concretes.io
{
    public static class XmlExtensions
    {
        #region xml serialization

        private static void SerializeToXml<TValue>(this TValue value, XmlWriter xw, XmlSerializer xs, bool warning, bool flush)
        {
            if (warning) xw.WriteComment("warning");
            xs.Serialize(xw, value);
            if (flush) xw.Flush();
        }

        public static Stream WriteToXml<TValue>(this TValue value, Stream stream)
        {
            var type = typeof(TValue);
            try
            {
                var xs = new XmlSerializer(type);
                var settings = new XmlWriterSettings { Indent = true, IndentChars = "    " };
                var xw = XmlWriter.Create(stream, settings);
                value.SerializeToXml(xw, xs, true, true);
            }
            catch (SerializationException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return stream;
        }

        public static TextWriter WriteToXml<TValue>(this TValue value, TextWriter writer)
        {
            var type = typeof(TValue);
            try
            {
                var xs = new XmlSerializer(type);
                var settings = new XmlWriterSettings { Indent = true, IndentChars = "    " };
                var xw = XmlWriter.Create(writer, settings);
                value.SerializeToXml(xw, xs, true, true);
            }
            catch (SerializationException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return writer;
        }

        public static void WriteToXml<TValue>(this TValue value, string path)
        {
            try
            {
                using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write)) value.WriteToXml(fs);
            }
            catch (UnauthorizedAccessException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        #endregion xml serialization

        #region xml document helpers

        /// <summary>
        /// Converts this XDocument to an XmlDocument
        /// </summary>
        /// <param name="document">The XDocument that is converted</param>
        /// <returns>The resulting XmlDocument</returns>
        public static XmlDocument ToXmlDocument(this XDocument document)
        {
            var xdoc = new XmlDocument();
            using (var reader = document.CreateReader())
            {
                xdoc.Load(reader);
            }
            return xdoc;
        }

        /// <summary>
        /// Converts this XmlDocument to an XDocument
        /// </summary>
        /// <param name="document">The XmlDocument to convert</param>
        /// <returns>The corresponding XDocument</returns>
        public static XDocument ToXDocument(this XmlDocument document)
        {
            XDocument xdoc = null;
            using (var reader = new XmlNodeReader(document))
            {
                reader.MoveToContent();
                xdoc = XDocument.Load(reader);
            }
            return xdoc;
        }

        #endregion xml document helpers
    }
}