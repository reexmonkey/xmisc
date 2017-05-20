using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.core.io.extensions
{
    public static class FileExtensions
    {
        public static FileInfo GetFileInfo(this Uri uri) => new FileInfo(uri.AbsolutePath);

        public static DirectoryInfo GetDirectoryInfo(this Uri uri)
        {
            return File.Exists(uri.AbsolutePath) ? uri.GetFileInfo().Directory : new DirectoryInfo(uri.AbsolutePath);
        }

        public static bool IsFile(this Uri uri) => File.Exists(uri.AbsolutePath);

        public static bool IsDirectory(this Uri uri) => Directory.Exists(uri.AbsolutePath);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fi"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        /// <remarks>Credits: http://stackoverflow.com/questions/275689/how-to-get-relative-path-from-absolute-path</remarks>
        public static Uri MakeRelative(this FileInfo fi, FileInfo other)
            => new Uri(fi.FullName).MakeRelativeUri(new Uri(other.FullName));

        public static Uri MakeRelative(this DirectoryInfo di, DirectoryInfo other)
            => new Uri(di.FullName).MakeRelativeUri(new Uri(other.FullName));

        public static IEnumerable<FileInfo> EnumerateFiles(this DirectoryInfo directory, Func<FileInfo, bool> predicate)
            => directory.EnumerateFiles().Where(predicate);

        public static string ReadText(this FileInfo fi, Encoding encoding)
        {
            var builder = new StringBuilder();
            using (var fstream = File.Open(fi.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = new StreamReader(fstream, encoding))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null) builder.AppendLine(line);
                }
            }
            return builder.ToString();
        }

        public static string ReadAllText(this FileInfo fi) => File.ReadAllText(fi.FullName);


        public static byte[] ReadAllBytes(this FileInfo fi) => File.ReadAllBytes(fi.FullName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fi"></param>
        /// <param name="destination"></param>
        /// <param name="buffersize"></param>
        /// <remarks>Credits: http://stackoverflow.com/a/2030971</remarks>
        public static void WriteToStream(this FileInfo fi, Stream destination, int buffersize = 4096)
        {
            using (var fstream = File.Open(fi.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var buffer = new byte[buffersize];
                int read;
                while ((read = fstream.Read(buffer, 0, buffer.Length)) > 0) destination.Write(buffer, 0, read);
            }
        }

        public static async Task<IEnumerable<FileInfo>> EnumerateFilesAsync(this DirectoryInfo directory) => await Task.FromResult(directory.EnumerateFiles());

        public static async Task<IEnumerable<FileInfo>> EnumerateFilesAsync(this DirectoryInfo directory,
            string searchpattern, SearchOption option)
            => await Task.FromResult(directory.EnumerateFiles(searchpattern, option));

        public static async Task<IEnumerable<FileInfo>> EnumerateFilesAsync(this DirectoryInfo directory, Func<FileInfo, bool> predicate)
            => await Task.FromResult(directory.EnumerateFiles(predicate));

        public static async Task<string> ReadTextAsync(this FileInfo fi, Encoding encoding)
        {
            var builder = new StringBuilder();
            using (var fstream = File.Open(fi.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = new StreamReader(fstream, encoding))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null) builder.AppendLine(line);
                }
            }
            return builder.ToString();
        }

        public static async Task<string> ReadAllTextAsync(this FileInfo fi) => await Task.FromResult(fi.ReadAllText());

        public static async Task<byte[]> ReadAllBytesAsync(this FileInfo fi) => await Task.FromResult(fi.ReadAllBytes());

        public static async Task<bool> WriteToStreamAsync(this FileInfo fi, Stream destination, int buffersize = 4096)
        {
            using (var fstream = File.Open(fi.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var buffer = new byte[buffersize];
                int read;
                while ((read = await fstream.ReadAsync(buffer, 0, buffer.Length)) > 0) await destination.WriteAsync(buffer, 0, read);
            }

            return await Task.FromResult(true);
        }
    }
}