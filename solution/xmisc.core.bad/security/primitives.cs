using reexmonkey.xmisc.core.io.infrastructure;
using System;
using System.Security.Cryptography;

namespace reexmonkey.xmisc.core.security
{
    public static class PrimitiveCryptographicExtensions
    {
        #region Hash Functions

        public static short GetHashValue(this short value, HashAlgorithm cipher) => BitConverter.ToInt16(BitConverter.GetBytes(value).GetHash(cipher), 0);

        public static ushort GetHashValue(this ushort value, HashAlgorithm cipher) => BitConverter.ToUInt16(BitConverter.GetBytes(value).GetHash(cipher), 0);

        public static int GetHashValue(this int value, HashAlgorithm cipher) => BitConverter.ToInt32(BitConverter.GetBytes(value).GetHash(cipher), 0);

        public static uint GetHashValue(this uint value, HashAlgorithm cipher) => BitConverter.ToUInt32(BitConverter.GetBytes(value).GetHash(cipher), 0);

        public static long GetHashValue(this long value, HashAlgorithm cipher) => BitConverter.ToInt64(BitConverter.GetBytes(value).GetHash(cipher), 0);

        public static ulong GetHashValue(this ulong value, HashAlgorithm cipher) => BitConverter.ToUInt64(BitConverter.GetBytes(value).GetHash(cipher), 0);

        public static float GetHashValue(this float value, HashAlgorithm cipher) => BitConverter.ToSingle(BitConverter.GetBytes(value).GetHash(cipher), 0);

        public static double GetHashValue(this double value, HashAlgorithm cipher) => BitConverter.ToDouble(BitConverter.GetBytes(value).GetHash(cipher), 0);

        public static decimal GetHashValue<TSerializer>(this decimal value, TSerializer serializer, HashAlgorithm cipher)
            where TSerializer : BinarySerializerBase => serializer.Deserialize<decimal>(value.GetBinaryHash(serializer, cipher));

        #endregion Hash Functions

        #region Salt Functions

        public static short GetSaltedHashValue(this short value, HashAlgorithm cipher, RandomNumberGenerator sprinkler, int saltLength)
            => BitConverter.ToInt16(BitConverter.GetBytes(value).GetSaltedHash(sprinkler, saltLength, cipher), 0);

        public static ushort GetSaltedHashValue(this ushort value, HashAlgorithm cipher, RandomNumberGenerator sprinkler, int saltLength)
            => BitConverter.ToUInt16(BitConverter.GetBytes(value).GetSaltedHash(sprinkler, saltLength, cipher), 0);

        public static int GetSaltedHashValue(this int value, HashAlgorithm cipher, RandomNumberGenerator sprinkler, int saltLength)
            => BitConverter.ToInt32(BitConverter.GetBytes(value).GetSaltedHash(sprinkler, saltLength, cipher), 0);

        public static uint GetSaltedHashValue(this uint value, HashAlgorithm cipher, RandomNumberGenerator sprinkler, int saltLength)
            => BitConverter.ToUInt32(BitConverter.GetBytes(value).GetSaltedHash(sprinkler, saltLength, cipher), 0);

        public static long GetSaltedHashValue(this long value, HashAlgorithm cipher, RandomNumberGenerator sprinkler, int saltLength)
            => BitConverter.ToInt64(BitConverter.GetBytes(value).GetSaltedHash(sprinkler, saltLength, cipher), 0);

        public static ulong GetSaltedHashValue(this ulong value, HashAlgorithm cipher, RandomNumberGenerator sprinkler, int saltLength)
            => BitConverter.ToUInt64(BitConverter.GetBytes(value).GetSaltedHash(sprinkler, saltLength, cipher), 0);

        public static float GetSaltedHashValue(this float value, HashAlgorithm cipher, RandomNumberGenerator sprinkler, int saltLength)
            => BitConverter.ToSingle(BitConverter.GetBytes(value).GetSaltedHash(sprinkler, saltLength, cipher), 0);

        public static double GetSaltedHashValue(this double value, HashAlgorithm cipher, RandomNumberGenerator sprinkler, int saltLength)
            => BitConverter.ToDouble(BitConverter.GetBytes(value).GetSaltedHash(sprinkler, saltLength, cipher), 0);

        public static decimal GetSaltedHashValue<TSerializer>(this decimal value, TSerializer serializer, HashAlgorithm cipher, RandomNumberGenerator sprinkler, int saltLength)
            where TSerializer : BinarySerializerBase
            => serializer.Deserialize<decimal>(value.GetBinaryHash(serializer, cipher).GetSaltedHash(sprinkler, saltLength, cipher));

        #endregion Salt Functions
    }
}