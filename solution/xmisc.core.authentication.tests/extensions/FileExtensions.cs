using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace xmisc.core.authentication.tests.extensions
{
    public static class FileExtensions
    {
        public static string ReadFile(this FileInfo fi, Encoding encoding, int bufferSize = 262144)
        {
            using (var reader = new StreamReader(fi.FullName, encoding, true, bufferSize))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
