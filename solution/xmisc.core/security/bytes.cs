using System;
using System.Linq;
using System.Security.Cryptography;

namespace reexmonkey.xmisc.core.security
{
    public static class BytesCryptographicExtensions
    {
        public static byte[] GetHash(this byte[] bytes, HashAlgorithm cipher) => cipher.ComputeHash(bytes);

        public static byte[] GetHash(this byte[] bytes, int offset, int count, HashAlgorithm cipher) => cipher.ComputeHash(bytes, offset, count);

        private static byte[] AddSalt(this RandomNumberGenerator sprinkler, byte[] bytes, int saltLength)
        {
            var buffer = new byte[bytes.Length + saltLength];
            var salt = sprinkler.Generate(saltLength);
            Buffer.BlockCopy(salt, 0, buffer, 0, salt.Length);
            Buffer.BlockCopy(bytes, 0, buffer, salt.Length, bytes.Length);
            return buffer;
        }

        public static byte[] GetSaltedHash(this byte[] bytes, RandomNumberGenerator sprinkler, int saltLength, HashAlgorithm cipher)
            => sprinkler.AddSalt(bytes, saltLength).GetHash(cipher);

        public static byte[] GetSaltedHash(this byte[] bytes, int offset, int count, RandomNumberGenerator sprinkler, int saltLength, HashAlgorithm cipher)
            => sprinkler.AddSalt(bytes, saltLength).GetHash(offset, count, cipher);

        public static string GetChecksum(this byte[] hash) => hash != null && hash.Any() ? BitConverter.ToString(hash) : string.Empty;

    }
}