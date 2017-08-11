using System;
using System.Security.Cryptography;
using System.Text;
using reexmonkey.xmisc.core.cryptography.extensions;

namespace reexmonkey.xmisc.backbone.identifiers.contracts.infrastructure
{
    public struct Sha1Guid
    {
        public static readonly Sha1Guid Empty = new Sha1Guid();
        private static readonly Guid NamespaceId = new Guid("3ff28409-45d1-11e7-5f80-432925f587fd");
        private readonly Guid guid;

        public Sha1Guid(uint low, ushort mid, ushort hi, byte csl, byte csh, byte n0, byte n1, byte n2, byte n3, byte n4, byte n5)
        {
            guid = new Guid(low, mid, hi, csl, csh, n0, n1, n2, n3, n4, n5);
        }

        public Sha1Guid(uint low, ushort mid, ushort hi, byte[] cs, byte[] n)
        {
            if (cs == null) throw new ArgumentNullException(nameof(cs));
            if (n == null) throw new ArgumentNullException(nameof(n));

            if (cs.Length != 2) throw new ArgumentException("Length of clock sequence array must be 2");
            if (n.Length != 6) throw new ArgumentException("Length of node array must be 6");

            guid = new Guid(low, mid, hi, cs[0], cs[1], n[0], n[1], n[2], n[3], n[4], n[5]);
        }

        public Sha1Guid(byte[] bytes) => guid = new Guid(bytes);

        public Sha1Guid(Guid guid) => this.guid = guid;

        public static Sha1Guid Parse(string value) => Guid.Parse(value);

        public static Sha1Guid ParseExact(string value, string format) => Guid.ParseExact(value, format);

        public static bool TryParse(string value, out Sha1Guid result)
        {
            var success = Guid.TryParse(value, out Guid guid);
            result = guid;
            return success;
        }

        public static bool TryParseExact(String value, String format, out Sha1Guid result)
        {
            var success = Guid.TryParseExact(value, format, out Guid guid);
            result = guid;
            return success;
        }

        private static Sha1Guid Create(byte[] hash)
        {
            var guid = new byte[16];

            //Assign time - low: 0-3
            guid[0] = hash[0];
            guid[1] = hash[1];
            guid[2] = hash[2];
            guid[3] = hash[3];

            //Assign times - mid: 4-5
            guid[4] = hash[4];
            guid[5] = hash[5];

            //Assign time - hi and version: 6-7
            guid[6] = hash[6];
            guid[7] = hash[7];

            // convert bytes to time_hi and version
            var hiver = (ushort)((guid[7] << 8 | guid[6]) & 0X0FFF | (5 << 12));
            guid[6] = (byte)hiver;
            guid[7] = (byte)(hiver >> 8);

            //clock sequence hi and reserved: 8
            guid[8] = (byte)(((hash[8] & 0x3F00) >> 8) | 0x80);

            //clock sequence - low: 9
            guid[9] = hash[9];

            guid[10] = hash[10];
            guid[11] = hash[11];
            guid[12] = hash[12];
            guid[13] = hash[13];
            guid[14] = hash[14];
            guid[15] = hash[15];
            return new Sha1Guid(guid);
        }


        public static Sha1Guid NewGuid(string name, Encoding encoding) => NewGuid(encoding.GetBytes(name));

        public static Sha1Guid NewGuid(byte[] name)
        {
            var namespaceId = NamespaceId.ToByteArray();
            var combined = new byte[namespaceId.Length + name.Length];
            Buffer.BlockCopy(namespaceId, 0, combined, 0, namespaceId.Length);
            Buffer.BlockCopy(name, 0, combined, namespaceId.Length, name.Length);
            var hash = combined.GetHash(SHA1.Create());
            return Create(hash);
        }

        public byte[] ToByteArray() => guid.ToByteArray();

        public Guid AsGuid() => guid;

        public bool Equals(Sha1Guid other)
        {
            return guid.Equals(other.guid);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Sha1Guid && Equals((Sha1Guid)obj);
        }

        public override int GetHashCode() => guid.GetHashCode();

        public string ToString(string format, IFormatProvider formatProvider) => guid.ToString(format);

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(obj, null)) return 1;
            if (!(obj is Sha1Guid)) throw new ArgumentException("Argument must be Md5Guid");
            return CompareTo((Sha1Guid)obj);
        }

        public int CompareTo(Sha1Guid other) => guid.CompareTo(other.guid);

        public static bool operator ==(Sha1Guid left, Sha1Guid right) => left.Equals(right);

        public static bool operator !=(Sha1Guid left, Sha1Guid right) => !left.Equals(right);

        public static bool operator <(Sha1Guid left, Sha1Guid right) => left.CompareTo(right) < 0;

        public static bool operator >(Sha1Guid left, Sha1Guid right) => left.CompareTo(right) > 0;

        public static bool operator <=(Sha1Guid left, Sha1Guid right) => left.CompareTo(right) <= 0;

        public static bool operator >=(Sha1Guid left, Sha1Guid right) => left.CompareTo(right) >= 0;

        public static implicit operator Guid(Sha1Guid comb) => comb.AsGuid();

        public static implicit operator Sha1Guid(Guid guid) => new Sha1Guid(guid);

        public override string ToString() => AsGuid().ToString();
    }
}
