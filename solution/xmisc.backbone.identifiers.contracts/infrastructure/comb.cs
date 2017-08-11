using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using reexmonkey.xmisc.core.cryptography.extensions;

namespace reexmonkey.xmisc.backbone.identifiers.contracts.infrastructure
{
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct SequentialGuid : IEquatable<SequentialGuid>, IComparable, IComparable<SequentialGuid>, IFormattable
    {
        public static readonly SequentialGuid Empty;
        private readonly Guid guid;
        private static readonly object mutex = new object();
        private static ulong last;
        private static ushort sequence = RandomNumberGenerator.Create().GenerateUInt16();

        public SequentialGuid(uint low, ushort mid, ushort hi, byte csl, byte csh, byte n0, byte n1, byte n2, byte n3, byte n4, byte n5)
        {
            guid = new Guid(low, mid, hi, csl, csh, n0, n1, n2, n3, n4, n5);
        }

        public SequentialGuid(uint low, ushort mid, ushort hi, byte[] cs, byte[] n)
        {
            if (cs == null) throw new ArgumentNullException(nameof(cs));
            if (n == null) throw new ArgumentNullException(nameof(n));

            if (cs.Length != 2) throw new ArgumentException("Length of clock sequence array must be 2");
            if (n.Length != 6) throw new ArgumentException("Length of node array must be 6");

            guid = new Guid(low, mid, hi, cs[0], cs[1], n[0], n[1], n[2], n[3], n[4], n[5]);
        }

        public SequentialGuid(byte[] bytes) => guid = new Guid(bytes);

        public SequentialGuid(Guid guid) => this.guid = guid;

        public static SequentialGuid Parse(string value) => Guid.Parse(value);

        public static SequentialGuid ParseExact(string value, string format) => Guid.ParseExact(value, format);

        public static bool TryParse(string value, out SequentialGuid result)
        {
            var success = Guid.TryParse(value, out Guid guid);
            result = guid;
            return success;
        }

        public static bool TryParseExact(String value, String format, out SequentialGuid result)
        {
            var success = Guid.TryParseExact(value, format, out Guid guid);
            result = guid;
            return success;
        }

        private static ulong GetTimestamp()
        {
            var timestamp = (ulong)(DateTime.UtcNow - new DateTime(1582, 10, 15)).Ticks;
            lock (mutex)
            {
                if (last == timestamp) sequence++;
                last = timestamp;
            }
            return timestamp;
        }

        private static SequentialGuid Create(ulong timestamp, ushort clocksequence, byte n0, byte n1, byte n2, byte n3, byte n4, byte n5)
        {
            var guid = new byte[16];

            //Assign time - low: 0-3
            var tlow = timestamp & 0xFFFFFFFF;
            guid[0] = (byte)tlow;
            guid[1] = (byte)(tlow >> 8);
            guid[2] = (byte)(tlow >> 16);
            guid[3] = (byte)(tlow >> 24);

            //Assign times - mid: 4-5
            var tmid = (ushort)((timestamp >> 32) & 0xFFFF);
            guid[4] = (byte)tmid;
            guid[5] = (byte)(tmid >> 8);

            //Assign time - hi and version: 6-7
            var thi = (ushort)((timestamp >> 48) & 0X0FFF | (1 << 12));
            guid[6] = (byte)thi;
            guid[7] = (byte)(thi >> 8);

            //Assign clock sequence - low: 8
            guid[8] = (byte)(((clocksequence & 0x3F00) >> 8) | 0x80);

            //clock sequence hi and reserved: 9
            guid[9] = (byte)(clocksequence & 0xFF);

            guid[10] = n0;
            guid[11] = n1;
            guid[12] = n2;
            guid[13] = n3;
            guid[14] = n4;
            guid[15] = n5;
            return new SequentialGuid(guid);
        }

        public static SequentialGuid NewGuid()
        {
            var node = RandomNumberGenerator.Create().Generate(6);
            node[0] |= 1; //multicast bit set to one, in order to avoid conflict with IEEE 802 network cards
            return Create(GetTimestamp(), sequence, node[0], node[1], node[2], node[3], node[4], node[5]);
        }

        public static SequentialGuid NewGuidForSqlServer()
        {
            var bytes = NewGuid().ToByteArray();
            Array.Reverse(bytes, 0, 4);
            Array.Reverse(bytes, 4, 2);
            Array.Reverse(bytes, 6, 2);
            return new SequentialGuid(bytes);
        }


        public byte[] ToByteArray() => guid.ToByteArray();

        public Guid AsGuid() => guid;

        public bool Equals(SequentialGuid other)
        {
            return guid.Equals(other.guid);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is SequentialGuid && Equals((SequentialGuid)obj);
        }

        public override int GetHashCode() => guid.GetHashCode();

        public string ToString(string format, IFormatProvider formatProvider) => guid.ToString(format);

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(obj, null)) return 1;
            if (!(obj is SequentialGuid)) throw new ArgumentException("Argument must be SequentialGuid");
            return CompareTo((SequentialGuid)obj);
        }

        public int CompareTo(SequentialGuid other) => guid.CompareTo(other.guid);

        public static bool operator ==(SequentialGuid left, SequentialGuid right) => left.Equals(right);

        public static bool operator !=(SequentialGuid left, SequentialGuid right) => !left.Equals(right);

        public static bool operator <(SequentialGuid left, SequentialGuid right) => left.CompareTo(right) < 0;

        public static bool operator >(SequentialGuid left, SequentialGuid right) => left.CompareTo(right) > 0;

        public static bool operator <=(SequentialGuid left, SequentialGuid right) => left.CompareTo(right) <= 0;

        public static bool operator >=(SequentialGuid left, SequentialGuid right) => left.CompareTo(right) >= 0;

        public static implicit operator Guid(SequentialGuid comb) => comb.AsGuid();

        public static implicit operator SequentialGuid(Guid guid) => new SequentialGuid(guid);

        public override string ToString() => AsGuid().ToString();
    }
}