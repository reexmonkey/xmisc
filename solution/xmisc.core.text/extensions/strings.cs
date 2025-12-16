using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace reexmonkey.xmisc.core.text.extensions
{
    /// <summary>
    /// Extends the standard text-related features of the <see cref="string"/> class.
    /// </summary>
    public static class StringExtensions
    {
        private static readonly char[] base64Padding = ['='];

        /// <summary>
        /// Extracts hexadecimal digits from a string
        /// </summary>
        /// <param name="source">The hexadecimal string from which the digits are extracted</param>
        /// <returns>An array of extracted hexadecimal digits</returns>
        public static char[] ExtractHexDigits(this string source)
        {
            var regex = new Regex("[abcdefABECDEF\\d]+", RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
            var extracted = from hex in source
                            where regex.IsMatch(hex.ToString())
                            select hex;

            return extracted.ToArray();
        }

        /// <summary>
        /// Replaces substrings in a string using a regular expresion pattern.
        /// </summary>
        /// <param name="text">The string, whose substrings are identified through pattern-recognition.</param>
        /// <param name="pattern">
        /// The regular expression pattern used in recognizing the substring in the string.
        /// </param>
        /// <param name="replacement">The string to replaces each found substring in the string.</param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string RegexReplace(this string text, string pattern, string replacement, RegexOptions options)
            => Regex.Replace(text, pattern, replacement, options);

        /// <summary>
        /// Checks if a string matches the specified pattern.
        /// </summary>
        /// <param name="text">The string to be checked.</param>
        /// <param name="pattern">
        /// The regular expression pattern used in recognizing the substring in the string.
        /// </param>
        /// <param name="options">Regular expression options to be used in the check.</param>
        /// <returns></returns>
        public static bool Match(this string text, string pattern, RegexOptions options) => Regex.IsMatch(text, pattern, options);

        /// <summary>
        /// Finds substrings in a string based on a pattern and appends a prefix to each found substring.
        /// </summary>
        /// <param name="text">The string to be searched</param>
        /// <param name="prefix">The string to be added at the beginning of each found substring</param>
        /// <param name="pattern">
        /// The regular expression pattern used in recognizing the substrings in the string.
        /// </param>
        /// <param name="options">Regular expression options</param>
        /// <returns>The original string that is appended wih <paramref name="prefix"/></returns>
        public static string FindAndPrepend(this string text, string prefix, string pattern, RegexOptions options)
        {
            var builder = new StringBuilder(text);
            var matches = Regex.Matches(text, pattern, options);
            for (var i = 0; i < matches.Count; i++)
            {
                var match = matches[i];
                if (string.IsNullOrEmpty(match.Value)) continue;
                builder.Replace(match.Value, $"{prefix}{match.Value}");
            }
            return builder.ToString();
        }

        /// <summary>
        /// Finds and substrings in a string based on a pattern and appends a suffix to each found substring.
        /// </summary>
        /// <param name="text">The string to be searched</param>
        /// <param name="suffix">The string to be added at the beginning of each found substring</param>
        /// <param name="pattern">
        /// The regular expression pattern used in recognizing the substrings in the string.
        /// </param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string FindAndAppend(this string text, string suffix, string pattern, RegexOptions options)
        {
            var builder = new StringBuilder(text);
            var matches = Regex.Matches(text, pattern);
            for (var i = 0; i < matches.Count; i++)
            {
                var match = matches[i];
                if (string.IsNullOrEmpty(match.Value)) continue;
                builder.Replace(match.Value, $"{match.Value}{suffix}");
            }
            return builder.ToString();
        }

        /// <summary>
        /// Delimits the lines of a string, when they are longer than the allowed maximum length.
        /// </summary>
        /// <param name="text">The string, whose lines are folded.</param>
        /// <param name="max">The maximum limit allowed for each line of the string</param>
        /// <param name="encoding">The character encoding used.</param>
        /// <param name="newline">
        /// The carriage-return-line-feed (CRLF) string to delimit <paramref name="text"/>.
        /// </param>
        /// <param name="whitespace">
        /// The linear whitespace (space or horizontal tab) string that follows <paramref name="newline"/>.
        /// </param>
        /// <returns>The string, whose lines are folded</returns>
        public static string FoldLines(this string text, int max, Encoding encoding, string newline = "\r\n", string whitespace = " ")
        {
            var lines = text.Split([newline], StringSplitOptions.RemoveEmptyEntries);
            using var stream = new MemoryStream(text.Length);
            var crlf = encoding.GetBytes(newline); //CRLF
            var crlfs = encoding.GetBytes($"{newline}" + whitespace); //CRLF and (SPACE or HTAB)
            for (var index = 0; index < lines.Length; index++)
            {
                var line = lines[index];
                var bytes = encoding.GetBytes(line);
                if (bytes.Length <= max)
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Write(crlf, 0, crlf.Length);
                }
                else
                {
                    var blocks = bytes.Length / max; //calculate block length
                    var remainder = bytes.Length % max; //calculate remaining length
                    var b = 0;
                    while (b < blocks)
                    {
                        stream.Write(bytes, (b++) * max, max);
                        stream.Write(crlfs, 0, crlfs.Length);
                    }

                    if (remainder > 0) stream.Write(bytes, blocks * max, remainder);
                }
            }
            return encoding.GetString(stream.ToArray());
        }

        /// <summary>
        /// Unfolds the lines of a string, which have been delimited to a specified length.
        /// </summary>
        /// <param name="text">The string, whose lines are unfolded.</param>
        /// <param name="newline">
        /// The newline characters, which were used to delimit the string to lines.
        /// </param>
        /// <param name="whitespace">
        /// The linear whitespace (space or horizontal tab) string that follows <paramref name="newline"/>.
        /// </param>
        /// <returns>The string, whose lines are unfolded.</returns>
        public static string UnfoldLines(this string text, string newline = "\r\n", string whitespace = " ")
            => text.Replace(newline + whitespace, string.Empty);

        /// <summary>
        /// Replaces the occurences of a first item of each tuple of strings in the current string
        /// instance with the second item of the tuple.
        /// </summary>
        /// <param name="text">The current string instance.</param>
        /// <param name="pairs">A enumerable collection of string tuples.</param>
        /// <returns>The string, in which specified substrings are replaced</returns>
        public static string Replace(this string text, params (string first, string second)[] pairs)
        {
            foreach (var pair in pairs)
            {
                if (string.IsNullOrEmpty(pair.first) || string.IsNullOrEmpty(pair.second)) continue;
                text = text.Replace(pair.first, pair.second);
            }
            return text;
        }

        /// <summary>
        /// Replaces all the occurence of each first Unicode characters from a pair in the <paramref name="text"/> with the second Unicode chararcter of the pair.
        /// </summary>
        /// <param name="text">The text where the character replacement takes place.</param>
        /// <param name="pairs">A tuple consisting of the character to replace, and its replacement.</param>
        /// <returns>A string where all occurences of each first character in the pair is replaced with the second character of the pair.</returns>
        public static string Replace(this string text, params (char first, char second)[] pairs)
        {
            foreach (var (first, second) in pairs)
            {
                text = text.Replace(first, second);
            }
            return text;
        }

        /// <summary>
        /// Escapes all the occurence of each specified target string in a text with the specified escape string.
        /// </summary>
        /// <param name="text">The text where the target strings are escaped.</param>
        /// <param name="escapeString">The string to escape each specified target string.</param>
        /// <param name="targets">The selected strings within <paramref name="text"/> to escape.</param>
        /// <returns>The string where each target instance has been escaped with <paramref name="escapeString"/>. </returns>
        public static string Escape(this string text, string escapeString, params string[] targets)
        {
            foreach (var target in targets)
            {
                text = text.Replace(target, escapeString + target);
            }
            return text;
        }

        /// <summary>
        /// Escapes all the occurence of each specified target char in a text with the specified escape char.
        /// </summary>
        /// <param name="text">The text where the target chars are escaped.</param>
        /// <param name="escapeChar">The char to escape each specified target char.</param>
        /// <param name="targets">The selected chars within <paramref name="text"/> to escape.</param>
        /// <returns>The char where each target instance has been escaped with <paramref name="escapeChar"/>. </returns>
        public static string Escape(this string text, char escapeChar, params char[] targets)
        {
            foreach (var target in targets)
            {
                text = text.Replace(new string([target]), new string([escapeChar, target]));
            }
            return text;
        }

        /// <summary>
        /// Converts text to another string format using the provided character encoding.
        /// </summary>
        /// <param name="text">The string to convert.</param>
        /// <param name="encoding">The encoding used to convert the given <paramref name="text"/>.</param>
        /// <returns>The encoded text equivalent.</returns>
        public static string AsEncoded(this string text, Encoding encoding)
            => encoding.GetString(encoding.GetBytes(text));

        /// <summary>
        /// Converts text to another string using the UTF-16 with big endian byte order character encoding.
        /// </summary>
        /// <param name="text">The string to convert.</param>
        /// <returns>The Unicode (UTF-16 using big endian byte order) text equivalent.</returns>
        public static string AsBigEndianUnicode(this string text)
            => text.AsEncoded(Encoding.BigEndianUnicode);

        /// <summary>
        /// Encodes text to its Base-64 encoded equivalent.
        /// </summary>
        /// <param name="text">The string to encode.</param>
        /// <param name="encoding">The character enoding to obtain the binary form of the text.</param>
        /// <returns>The Base-64 encoded equivalent of the string.</returns>
        public static string AsBase64(this string text, Encoding encoding)
            => Convert.ToBase64String(encoding.GetBytes(text));

        /// <summary>
        /// Decodes a specified Base64-encoded string to a string of a specified character encoding.
        /// </summary>
        /// <param name="base64">The Base-64 encoded string to decode.</param>
        /// <param name="encoding">
        /// The character encoding to obtain the string from the Base-64.
        /// </param>
        /// <returns>The string equivalent of the decoded Base-64 string.</returns>
        public static string FromBase64String(this string base64, Encoding encoding)
            => encoding.GetString(Convert.FromBase64String(base64));

        /// <summary>
        /// Encodes a specified text to its URL-safe Base64-encoded equivalent representation.
        /// </summary>
        /// <param name="text">The string to encode.</param>
        /// <param name="encoding">The character enoding to obtain the binary form of the text.</param>
        /// <returns>The URL-safe Base64-encoded equivalent representation of the string.</returns>
        public static string AsBase64Url(this string text, Encoding encoding)
        {
            return text.AsBase64(encoding)
                .TrimEnd(base64Padding)
                .Replace('+', '-')
                .Replace('/', '_');
        }

        /// <summary>
        /// Decodes a specified URL-safe Base64-encoded string to a string of a specified character encoding.
        /// </summary>
        /// <param name="base64">The URL-safe Base64-encoded string to decode.</param>
        /// <param name="encoding">
        /// The character encoding to obtain the string from the Base-64 value.
        /// </param>
        /// <returns>The string equivalent of the decoded URL-safe Base64 string.</returns>
        public static string FromBase64UrlString(this string base64, Encoding encoding)
        {
            var value = base64
                .Replace('_', '/')
                .Replace('-', '+');

            switch (value.Length % 4)
            {
                case 2: value += "=="; break;
                case 3: value += "="; break;
            }
            return value.FromBase64String(encoding);
        }

        /// <summary>
        /// Inserts line breaks after every number of characters in the provided string representation.
        /// </summary>
        /// <param name="text">The string representation, where the insert line break is inserted.</param>
        /// <param name="num">The number of characters to skip before inserting a line break. </param>
        /// <returns>The string representation with the line breaks</returns>
        public static string InsertLineBreaks(this string text, int num)
        {
            using var sw = new StringWriter();
            var chars = text.ToCharArray();
            var blocks = chars.Length / num;
            var remainder = chars.Length % num;
            var rounds = remainder > 0 ? blocks + 1 : blocks;
            var counter = 0;
            for (var i = 0; i < chars.Length; i += num)  // output as Base64 with lines chopped at line break
            {
                var limit = Math.Min(num, chars.Length - i);
                sw.Write(chars, i, limit);
                if (++counter < rounds) sw.Write("\n");
            }
            return sw.ToString();
        }
    }
}