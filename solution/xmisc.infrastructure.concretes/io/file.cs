using reexjungle.xmisc.foundation.concretes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace reexjungle.xmisc.infrastructure.concretes.io
{
    /// <summary>
    /// Extensions to File-related operations
    /// </summary>
    public static class FileExtensions
    {
        /// <summary>
        /// Browse a direction specified by a path and return a collection of files found inside the directory
        /// </summary>
        /// <param name="path">The directory path</param>
        /// <returns>The collection of files from the directory</returns>
        public static IEnumerable<FileInfo> GetFiles(this string path)
        {
            return new DirectoryInfo(path).GetFiles();
        }

        /// <summary>
        /// Browse a direction specified by a path and return a collection of files found inside the directory.
        /// The returned files are filtered according to a specified extension
        /// </summary>
        /// <param name="path">The directory path</param>
        /// <param name="filter">An extension filter that specifies that only files with the extension are selected</param>
        /// <returns>The collection of files in the specified directory that are filtered by the extension</returns>
        public static IEnumerable<FileInfo> GetFiles(this string path, string filter)
        {
            return new DirectoryInfo(path).GetFiles().Where(f => f.Extension.Equals(filter, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Browse a direction specified by a path and return a collection of files found inside the directory.
        /// The returned files are filtered by specified multiple extensions
        /// </summary>
        /// <param name="path">The directory path</param>
        /// <param name="filters">A collection of extensions to filter the file retrieval result </param>
        /// <returns>The collection of files in the specified directory that have been filtered by the collection of extensions</returns>
        public static IEnumerable<FileInfo> GetFiles(this string path, IEnumerable<string> filters)
        {
            return new DirectoryInfo(path).GetFiles().Where(f => f.Extension == filters.Select(l => l).FirstOrDefault()).Select(f => f);
        }

        /// <summary>
        /// Creates a relative path from one file or folder to another.
        /// </summary>
        /// <param name="origin">Contains the directory that defines the start of the relative path.</param>
        /// <param name="destination">Contains the path that defines the endpoint of the relative path.</param>
        /// <returns>The relative path from the start directory to the end path</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <remarks>Acknowledgements to: http://stackoverflow.com/questions/275689/how-to-get-relative-path-from-absolute-path</remarks>
        public static string GetRelativePath(this string origin, string destination)
        {
            if (origin == destination) return Path.GetFileName(origin);
            Uri relativeUri = new Uri(origin).MakeRelativeUri(new Uri(destination));
            var relativePath = Uri.UnescapeDataString(relativeUri.ToString());
            return relativePath.Replace('/', Path.DirectorySeparatorChar);
        }

        /// <summary>
        /// Appends a path to a root path
        /// </summary>
        /// <param name="root">The root path</param>
        /// <param name="path">The path to be appended to the root path</param>
        /// <returns>A new path consisting of the root path and the appendix</returns>
        public static string AppendPath(this string root, string path)
        {
            var sb = new StringBuilder(root);
            sb.AppendFormat("\\{0}", path);
            return sb.ToString();
        }

        /// <summary>
        /// Gets the checksum of a file that is specified by the given hash algorithm
        /// </summary>
        /// <param name="path">The full path to the file</param>
        /// <param name="algorithm">The has algorithm used ti derive the checksum</param>
        /// <returns>The cheksum of the file if the operation is successful; otherwise an empty string</returns>
        public static string GetCheckSum(this string path, HashAlgorithm algorithm)
        {
            using (var reader = new StreamReader(path))
            {
                var checksum = algorithm.ComputeHash(reader.BaseStream);
                return checksum.NullOrEmpty() ?
                    BitConverter.ToString(checksum) :
                    string.Empty;
            }
        }

        ///  <summary>
        /// Reads text from a file, whose path is specified by a <see cref="System.IO.FileInfo"/> instance
        ///  </summary>
        /// <param name="finfo">The instance specifying the file path of the file</param>
        /// <returns>The text read from the file</returns>
        public static string ReadText(this FileInfo finfo)
        {
            var sb = new StringBuilder();
            using (var sr = File.OpenText(finfo.Name))
            {
                string line;
                while ((line = sr.ReadLine()) != null) sb.Append(line);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Reads text from a <see cref="System.IO.Stream"/> instance
        /// </summary>
        /// <param name="stream">The stream containing textual information </param>
        /// <returns>The text read from teh stream</returns>
        public static string ReadText(this Stream stream)
        {
            var sb = new StringBuilder();
            using (var sr = new StreamReader(stream))
            {
                string line;
                while ((line = sr.ReadLine()) != null) sb.Append(line);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Reads raw bytes from a <see cref="System.IO.Stream"/> instance
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] ReadBytes(Stream stream)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                var read = 0;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0) ms.Write(buffer, 0, read);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Splits an array of bytes to blocks of a specified size
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="blocksize"></param>
        /// <returns></returns>
        public static IEnumerable<byte[]> Split(this IEnumerable<byte> bytes, int blocksize)
        {
            var offset = 0;
            var data = bytes.ToArray();
            var count = data.Length / blocksize;
            var rem = data.Length % blocksize;

            var blocks = (rem == 0)
                ? new List<byte[]>(count)
                : new List<byte[]>(count + 1);

            while (offset < data.Length)
            {
                var buffer = new byte[blocksize];
                Buffer.BlockCopy(data, offset, buffer, 0, blocksize);
                blocks.Add(buffer);
                offset += blocksize;
            }
            return blocks;
        }
    }
}