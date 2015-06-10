using System;
using System.Collections.Generic;
using System.Text;

namespace reexjungle.xmisc.infrastructure.concretes.operations
{
    /// <summary>
    /// Provides extended encoding functionalities
    /// </summary>
    public static class EncodingExtensions
    {
        /// <summary>
        /// Converts UTF-16 string to UTF-8 bytes.
        /// </summary>
        /// <param name="unicode">UTF-16 string</param>
        /// <returns>UTF-8 bytes</returns>
        public static byte[] ToUtf8Bytes(this string unicode)
        {
            return new UTF8Encoding().GetBytes(unicode);
        }

        /// <summary>
        /// Converts UTF-16 string to UTF-8 string.
        /// </summary>
        /// <param name="unicode">UTF-16 string</param>
        /// <returns>UTF-8 string</returns>
        public static string ToUtf8String(this string unicode)
        {
            var bytes = Encoding.UTF8.GetBytes(unicode);
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// Converts plain text to its equivalent encoded ASCII raw binary.
        /// </summary>
        /// <param name="unicode">The unicode to be byte-encoded.</param>
        /// <returns>The Base64-based binary value encoded from the plain text</returns>
        /// <exception cref="ArgumentNullException">Throw when the plain text argument is null</exception>
        /// <exception cref="EncoderFallbackException">Throw when encoding the plain text to Base64 fails</exception>
        public static byte[] ToAsciiBytes(this string unicode)
        {
            if (unicode == null) throw new ArgumentNullException();
            return Encoding.ASCII.GetBytes(unicode);
        }

        public static string ToAsciiString(this string unicode)
        {
            var bytes = Encoding.Default.GetBytes(unicode);
            return Encoding.ASCII.GetString(bytes);
        }

        /// <summary>
        /// Converts plain text to its equivalent encoded Base64 string
        /// </summary>
        /// <param name="unicode">The plain text (unencoded) that is to be encoded</param>
        /// <returns>The Base64 string encoded from the plain text</returns>
        public static string ToBase64String(this string unicode)
        {
            var bytes = Encoding.Unicode.GetBytes(unicode);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Converts a Base64 string to its equivalent encoded plain text
        /// </summary>
        /// <param name="base64">The base64 text (encoded) that is to be decoded</param>
        /// <returns>Plain text decoded from the Base64 text</returns>
        public static string ToUnicodeString(this string base64)
        {
            var bytes = Convert.FromBase64String(base64);
            return Encoding.Unicode.GetString(bytes);
        }

        /// <summary>
        /// Converts a Base64 string to its equivalent raw 8-bit unsigned integer array.
        /// </summary>
        /// <param name="base64">The base64 text (encoded) that is to be decoded</param>
        /// <returns>Raw binary data decoded from the Base64 text</returns>
        public static IEnumerable<byte> ToBytes(this string base64)
        {
            return Convert.FromBase64String(base64);
        }
    }
}