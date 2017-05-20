using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace reexmonkey.xmisc.core.io.extensions
{
    public static class StreamExtensions
    {
        public static byte[] ReadBytes(this Stream stream, int buffersize = 4096)
        {
            var memorystream = stream as MemoryStream;
            if (memorystream != null) return memorystream.ToArray();

            var buffer = new byte[buffersize];
            using (var mstream = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0) mstream.Write(buffer, 0, read);
                return mstream.ToArray();
            }
        }

        public static string GetChecksum(this Stream stream, HashAlgorithm algorithm)
        {
            var hash = algorithm.ComputeHash(stream);
            return hash.Any() ? BitConverter.ToString(hash) : string.Empty;
        }

        public static string GetMd5Checksum(this Stream stream) => stream.GetChecksum(new MD5CryptoServiceProvider());

        public static string GetSha1Checksum(this Stream stream) => stream.GetChecksum(new SHA1CryptoServiceProvider());

        public static string GetSha256Checksum(this Stream stream) => stream.GetChecksum(new SHA256CryptoServiceProvider());

        public static string GetSha384Checksum(this Stream stream) => stream.GetChecksum(new SHA384CryptoServiceProvider());

        public static string GetSha512Checksum(this Stream stream) => stream.GetChecksum(new SHA512CryptoServiceProvider());

        public static string GetRipemdChecksum(this Stream stream) => stream.GetChecksum(new RIPEMD160Managed());
    }
}