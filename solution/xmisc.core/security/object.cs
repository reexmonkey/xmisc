using reexmonkey.xmisc.core.io.infrastructure;
using System.Security.Cryptography;
using System.Text;

namespace reexmonkey.xmisc.core.security
{
    public static class ObjectCryptographicExtensions
    {
        public static byte[] GetBinaryHash<TValue, TSerializer>(this TValue value, TSerializer serializer, HashAlgorithm cipher)
            where TSerializer : BinarySerializerBase => serializer.Serialize(value).GetHash(cipher);

        public static string GetTextualHash<TValue, TSerializer>(this TValue value, TSerializer serializer, Encoding encoding, HashAlgorithm cipher)
            where TSerializer : TextSerializerBase => serializer.Serialize(value).GetHash(encoding, cipher);

        public static TValue GetSaltedBinaryHash<TValue, TSerializer>(this TValue value, TSerializer serializer, HashAlgorithm cipher, RandomNumberGenerator sprinkler, int saltLength, decimal separator)
            where TSerializer : BinarySerializerBase
            => serializer.Deserialize<TValue>(serializer.Serialize(value).GetSaltedHash(sprinkler, saltLength, cipher));

        public static TValue GetSaltedTextualHash<TValue, TSerializer>(this TValue value, TSerializer serializer, Encoding encoding, HashAlgorithm cipher, RandomNumberGenerator sprinkler, int saltLength, decimal separator)
            where TSerializer : TextSerializerBase
            => serializer.Deserialize<TValue>(serializer.Serialize(value).GetSaltedHash(encoding, sprinkler, saltLength, cipher));


    }
}
