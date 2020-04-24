using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace reexmonkey.xmisc.core.cryptography.extensions
{
    public static class StringCryptographicExtensions
    {
        public static string GetHash(this string value, Encoding encoding, HashAlgorithm cipher)
            => BytesCryptographicExtensions.ToString(encoding.GetBytes(value).GetHash(cipher));

        public static string GetHash(this string value, int startIndex, int length, Encoding encoding, HashAlgorithm cipher)
            => BytesCryptographicExtensions.ToString(encoding.GetBytes(value.Substring(startIndex, length)).GetHash(cipher));

        private static string GetBase64Checksum(this byte[] base64)
            => base64 != null && base64.Any() ? Convert.ToBase64String(base64) : string.Empty;

        public static string GetBase64Hash(this string value, HashAlgorithm cipher)
            => Convert.FromBase64String(value).GetHash(cipher).GetBase64Checksum();

        public static string GetBase64Hash(this string value, int startIndex, int length, Encoding encoding, HashAlgorithm cipher)
            => encoding.GetBytes(value.Substring(startIndex, length)).GetHash(cipher).GetBase64Checksum();

        public static string GetSaltedHash(this string value, Encoding encoding, RandomNumberGenerator sprinkler, int saltLength, HashAlgorithm cipher)
            => BytesCryptographicExtensions.ToString(encoding.GetBytes(value).GetSaltedHash(sprinkler, saltLength, cipher));

        public static string GetSaltedHash(this string value, int startIndex, int length, Encoding encoding, HashAlgorithm cipher, RandomNumberGenerator sprinkler, int saltLength)
            => BytesCryptographicExtensions.ToString(encoding.GetBytes(value.Substring(startIndex, length)).GetSaltedHash(sprinkler, saltLength, cipher));

        public static string Getbase64SaltedHash(this string value, RandomNumberGenerator sprinkler, int saltLength, HashAlgorithm cipher)
            => Convert.FromBase64String(value).GetSaltedHash(sprinkler, saltLength, cipher).GetBase64Checksum();

        public static string GetBase64SaltedHash(this string value, HashAlgorithm cipher, RandomNumberGenerator sprinkler, int saltLength)
            => Convert.FromBase64String(value).GetSaltedHash(sprinkler, saltLength, cipher).GetBase64Checksum();
    }
}