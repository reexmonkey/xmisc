using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.core.io.extensions
{
    /// <summary>
    /// Extends the standard file-related features of .NET
    /// </summary>
    public static class FileExtensions
    {
        /// <summary>
        /// Gets the file information of a file specified by the given <paramref name="uri"/>.
        /// </summary>
        /// <param name="uri">The URI to the file.</param>
        /// <returns>The file information if the URI is valid and the file exists; otherwise a null value.</returns>
        public static FileInfo GetFileInfo(this Uri uri) => new(uri.AbsolutePath);

        /// <summary>
        /// Gets the directory information from a directoy specified by the given <paramref name="uri"/>.
        /// </summary>
        /// <param name="uri">The URI to the directory.</param>
        /// <returns>The directory information if the URI is valid and the directory exists; otherwise a null value.</returns>
        public static DirectoryInfo GetDirectoryInfo(this Uri uri)
        {
            return File.Exists(uri.AbsolutePath) ? uri.GetFileInfo().Directory : new DirectoryInfo(uri.AbsolutePath);
        }

        /// <summary>
        /// Determines whether this instance of the <see cref="Uri"/> class is file.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>
        ///   <c>true</c> if the specified URI is file; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsFile(this Uri uri) => File.Exists(uri.AbsolutePath);

        /// <summary>
        /// Determines whether this instance of the <see cref="Uri"/> is directory.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>
        ///   <c>true</c> if the specified URI is directory; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDirectory(this Uri uri) => Directory.Exists(uri.AbsolutePath);

        /// <summary>
        /// Creates a relative path from one file to another.
        /// </summary>
        /// <param name="fi">The start of the reletive path.</param>
        /// <param name="other">The end of the relative path.</param>
        /// <returns>The relative path from <paramref name="fi"/> to <paramref name="other"/>.</returns>
        /// <remarks>Credits: http://stackoverflow.com/questions/275689/how-to-get-relative-path-from-absolute-path</remarks>
        public static Uri MakeRelative(this FileInfo fi, FileInfo other)
            => new Uri(fi.FullName).MakeRelativeUri(new Uri(other.FullName));

        /// <summary>
        /// Creates a relative path from one directory to another.
        /// </summary>
        /// <param name="di">The start of the reletive path.</param>
        /// <param name="other">The end of the relative path.</param>
        /// <returns>The relative path from <paramref name="di"/> to <paramref name="other"/>.</returns>
        /// <remarks>Credits: http://stackoverflow.com/questions/275689/how-to-get-relative-path-from-absolute-path</remarks>
        public static Uri MakeRelative(this DirectoryInfo di, DirectoryInfo other)
            => new Uri(di.FullName).MakeRelativeUri(new Uri(other.FullName));

        /// <summary>
        /// Enumerates all the files in the current directory that satisfy the given predicate.
        /// </summary>
        /// <param name="directory">The directory to enumerate.</param>
        /// <param name="predicate">The predicate that filters the results of the enumeration.</param>
        /// <returns>The filtered enumeration of files in the current directory.</returns>
        public static IEnumerable<FileInfo> EnumerateFiles(this DirectoryInfo directory, Func<FileInfo, bool> predicate)
            => directory.EnumerateFiles().Where(predicate);

        /// <summary>
        /// Reads textual content from the given file specified by <paramref name="fi"/>.
        /// </summary>
        /// <param name="fi">The file to read.</param>
        /// <param name="encoding">The encoding used during the reading process.</param>
        /// <returns>The text read from the file.</returns>
        public static string ReadText(this FileInfo fi, Encoding encoding)
        {
            var builder = new StringBuilder();
            using (var fstream = File.Open(fi.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using var reader = new StreamReader(fstream, encoding);
                string line;
                while ((line = reader.ReadLine()) != null) builder.AppendLine(line);
            }
            return builder.ToString();
        }

        /// <summary>
        /// Reads all text from the given file specified by <paramref name="fi"/>.
        /// </summary>
        /// <param name="fi">The file to read.</param>
        /// <returns>The text read from the file.</returns>
        public static string ReadAllText(this FileInfo fi) => File.ReadAllText(fi.FullName);

        /// <summary>
        /// Reads all bytes from the given file specified by <paramref name="fi"/>..
        /// </summary>
        /// <param name="fi">The The file to read.</param>
        /// <returns>The binary content from the file.</returns>
        public static byte[] ReadAllBytes(this FileInfo fi) => File.ReadAllBytes(fi.FullName);

        /// <summary>
        /// Writes data from the given file to a <see cref="Stream"/> <paramref name="destination"/>.
        /// </summary>
        /// <param name="fi">The file to write.</param>
        /// <param name="destination">The stream to which the contents of the file are streamed to.</param>
        /// <param name="buffersize">The size of the buffer during the streaming process.</param>
        /// <remarks>Credits: http://stackoverflow.com/a/2030971</remarks>
        public static void WriteToStream(this FileInfo fi, Stream destination, int buffersize = 4096)
        {
            using var fstream = File.Open(fi.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var buffer = new byte[buffersize];
            int read;
            while ((read = fstream.Read(buffer, 0, buffer.Length)) > 0) destination.Write(buffer, 0, read);
        }

        /// <summary>
        /// Asynchronously Enumerates all the files in the current directory that satisfy the given predicate.
        /// </summary>
        /// <param name="directory">The directory to enumerate.</param>
        /// <param name="predicate">The predicate that filters the results of the enumeration.</param>
        /// <returns>The filtered enumeration of files in the current directory.</returns>
        public static async Task<IEnumerable<FileInfo>> EnumerateFilesAsync(this DirectoryInfo directory, Func<FileInfo, bool> predicate)
            => await Task.FromResult(directory.EnumerateFiles(predicate));

        /// <summary>
        /// Asynchronously reads textual content from the given file specified by <paramref name="fi"/>.
        /// </summary>
        /// <param name="fi">The file to read.</param>
        /// <param name="encoding">The encoding used during the reading process.</param>
        /// <returns>The text read from the file.</returns>
        public static async Task<string> ReadTextAsync(this FileInfo fi, Encoding encoding)
        {
            var builder = new StringBuilder();
            using (var fstream = File.Open(fi.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using var reader = new StreamReader(fstream, encoding);
                string line;
                while ((line = await reader.ReadLineAsync()) != null) builder.AppendLine(line);
            }
            return builder.ToString();
        }

        /// <summary>
        /// Asynchronously reads all text from the given file specified by <paramref name="fi"/>.
        /// </summary>
        /// <param name="fi">The file to read.</param>
        /// <returns>The text read from the file.</returns>
        public static async Task<string> ReadAllTextAsync(this FileInfo fi) => await Task.FromResult(fi.ReadAllText());

        /// <summary>
        /// Asynchronously reads all bytes from the given file specified by <paramref name="fi"/>..
        /// </summary>
        /// <param name="fi">The The file to read.</param>
        /// <returns>The binary content from the file.</returns>
        public static async Task<byte[]> ReadAllBytesAsync(this FileInfo fi) => await Task.FromResult(fi.ReadAllBytes());

        /// <summary>
        /// Asynchronously writes data from the given file to a <see cref="Stream"/> <paramref name="destination"/>.
        /// </summary>
        /// <param name="fi">The file to write.</param>
        /// <param name="destination">The stream to which the contents of the file are streamed to.</param>
        /// <param name="buffersize">The size of the buffer during the streaming process.</param>
        /// <remarks>Credits: http://stackoverflow.com/a/2030971</remarks>
        public static async Task WriteToStreamAsync(this FileInfo fi, Stream destination, int buffersize = 4096)
        {
            using var fstream = File.Open(fi.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var buffer = new byte[buffersize];
            int read;
            while ((read = await fstream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                await destination.WriteAsync(buffer, 0, read);
        }
    }
}