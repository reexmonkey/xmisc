using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace reexjungle.xmisc.infrastructure.concretes.operations
{
    public static class CryptographicExtensions
    {

        #region GUIDs

        public static Guid Combine(this Guid @this, Guid other)
        {
            var first = @this.ToByteArray();
            var second = other.ToByteArray();

            return new Guid(BitConverter.GetBytes(BitConverter.ToUInt64(first, 0) ^ BitConverter.ToUInt64(second, 8))
                .Concat(BitConverter.GetBytes(BitConverter.ToUInt64(first, 8) ^ BitConverter.ToUInt64(second, 0))).ToArray());
        }

        #endregion

        #region Hashing

        /// <summary>
        ///
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="cipher"></param>
        /// <returns></returns>
        public static byte[] Hash(this byte[] bytes, HashAlgorithm cipher)
        {
            return bytes != null
                ? cipher.ComputeHash(bytes)
                : null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="encoding"></param>
        /// <param name="cipher"></param>
        /// <returns></returns>
        public static string Hash(this string value, Encoding encoding, HashAlgorithm cipher)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var hash = cipher.ComputeHash(encoding.GetBytes(value));
                return BitConverter.ToString(hash);
            }

            return string.Empty;
        }

        public static Guid Hash(this Guid value, HashAlgorithm cipher)
        {
            var hash = cipher.ComputeHash(value.ToByteArray());
            return new Guid(hash);
        }

        public static short Hash(this short value, HashAlgorithm cipher)
        {
            var hash = cipher.ComputeHash(BitConverter.GetBytes(value));
            return BitConverter.ToInt16(hash, 0);
        }

        public static ushort Hash(this ushort value, HashAlgorithm cipher)
        {
            var hash = cipher.ComputeHash(BitConverter.GetBytes(value));
            return BitConverter.ToUInt16(hash, 0);
        }

        public static int Hash(this int value, HashAlgorithm cipher)
        {
            var hash = cipher.ComputeHash(BitConverter.GetBytes(value));
            return BitConverter.ToInt32(hash, 0);
        }

        public static uint Hash(this uint value, HashAlgorithm cipher)
        {
            var hash = cipher.ComputeHash(BitConverter.GetBytes(value));
            return BitConverter.ToUInt32(hash, 0);
        }

        public static long Hash(this long value, HashAlgorithm cipher)
        {
            var hash = cipher.ComputeHash(BitConverter.GetBytes(value));
            return BitConverter.ToInt64(hash, 0);
        }

        public static ulong Hash(this ulong value, HashAlgorithm cipher)
        {
            var hash = cipher.ComputeHash(BitConverter.GetBytes(value));
            return BitConverter.ToUInt64(hash, 0);
        }

        #endregion [Hashing]

        #region Salting

        /// <summary>
        ///
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] Salt(uint length)
        {
            var buffer = new byte[length];

            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetBytes(buffer);
                return buffer;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Base64StringSalt(uint length)
        {
            return Convert.ToBase64String(Salt(length));
        }

        #endregion [Salting]

        #region Salted Hashing

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="bytes"></param>
        ///  <param name="cipher"></param>
        ///  <param name="saltLength"></param>
        /// <param name="appendSalt"></param>
        /// <returns></returns>
        public static byte[] SaltedHash(this byte[] bytes, HashAlgorithm cipher, uint saltLength, byte separator)
        {
            var hash = bytes.Hash(cipher);
            var salt = Salt(saltLength);
            var buffer = new byte[hash.Length + salt.Length + 1];

            Buffer.BlockCopy(salt, 0, buffer, 0, salt.Length);
            buffer[saltLength] = separator;
            Buffer.BlockCopy(hash, 0, buffer, buffer.Length + 1, hash.Length);

            return buffer;
        }


        ///    <summary>
        ///
        ///    </summary>
        ///    <param name="value"></param>
        ///    <param name="encoding"></param>
        ///    <param name="cipher"></param>
        ///    <param name="saltLength"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string SaltedHash(this string value, Encoding encoding, HashAlgorithm cipher,
            uint saltLength, string separator = ":")
        {
            var hash = value.Hash(encoding, cipher);
            var salt = Base64StringSalt(saltLength);
            return string.Join(separator, salt, hash);
        }

        ///    <summary>
        ///
        ///    </summary>
        ///    <param name="bytes"></param>
        ///    <param name="cipher"></param>
        ///    <param name="saltLength"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static byte[] StrongSaltedHash(this byte[] bytes, HashAlgorithm cipher, uint saltLength, byte separator)
        {
            var saltedhash = SaltedHash(bytes, cipher, saltLength, separator);
            return Hash(saltedhash, cipher);
        }


        ///   <summary>
        ///
        ///   </summary>
        ///   <param name="value"></param>
        ///   <param name="encoding"></param>
        ///   <param name="cipher"></param>
        ///   <param name="saltLength"></param>
        /// <returns></returns>
        public static string StrongSaltedHash(this string value, Encoding encoding, HashAlgorithm cipher,
            uint saltLength)
        {
            var saltedhash = SaltedHash(value, encoding, cipher, saltLength);
            return Hash(saltedhash, encoding, cipher);
        }

        #endregion [Salting Hash]
    }
}
