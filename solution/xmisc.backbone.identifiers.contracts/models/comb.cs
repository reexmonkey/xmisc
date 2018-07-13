using reexmonkey.xmisc.backbone.identifiers.contracts.extensions;
using reexmonkey.xmisc.core.cryptography.extensions;
using System;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace reexmonkey.xmisc.backbone.identifiers.contracts.models
{
    /// <summary>
    /// Represents a time-based (version 1) globally unique identifier (GUID) that uses <see cref="MD5"/> hashing as defined in RFC 4122 (https://tools.ietf.org/html/rfc4122)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct SequentialGuid : IEquatable<SequentialGuid>, IComparable, IComparable<SequentialGuid>, IFormattable
    {
        /// <summary>
        ///  A read-only instance of the  <see cref="SequentialGuid"/> structure whose all 128 bits are set to zeros.
        /// </summary>
        public static readonly SequentialGuid Empty;

        private readonly Guid guid;
        private static readonly object mutex = new object();
        private static ulong last;
        private static ushort sequence = RandomNumberGenerator.Create().GenerateUInt16();

        /// <summary>
        /// Initializes a new instance of the <see cref="SequentialGuid"/> structure by using the specified unsigned integers and bytes.
        /// </summary>
        /// <param name="low">The low field of the timestamp (i.e. the first 4 bytes of the <see cref="SequentialGuid"/>).</param>
        /// <param name="mid">The middle field of the timestamp (i.e. the next 2 bytes of the <see cref="SequentialGuid"/>).</param>
        /// <param name="hi">The The high field of the timestamp multiplexed with the version number (i.e. the next 2 bytes of the <see cref="SequentialGuid"/>).</param>
        /// <param name="csl">The high field of the clock sequence multiplexed with the variant (i.e. the next byte of the <see cref="SequentialGuid"/>).</param>
        /// <param name="csh">The low field of the clock sequence (i.e. the next byte of the <see cref="SequentialGuid"/>).</param>
        /// <param name="n0">The first byte of a spatially unique node identifier of the <see cref="SequentialGuid"/> that is constructed from a name.</param>
        /// <param name="n1">The next byte of the spatially unique node identifier of the <see cref="SequentialGuid"/> that is constructed from a name.</param>
        /// <param name="n2">The next byte of the spatially unique node identifier of the <see cref="SequentialGuid"/> that is constructed from a name.</param>
        /// <param name="n3">The next byte of the spatially unique node identifier of the <see cref="SequentialGuid"/> that is constructed from a name.</param>
        /// <param name="n4">The next byte of the spatially unique node identifier of the <see cref="SequentialGuid"/> that is constructed from a name.</param>
        /// <param name="n5">The last byte of the spatially unique node identifier of the <see cref="SequentialGuid"/> that is constructed from a name.</param>
        public SequentialGuid(uint low, ushort mid, ushort hi, byte csl, byte csh, byte n0, byte n1, byte n2, byte n3, byte n4, byte n5)
        {
            guid = new Guid(low, mid, hi, csl, csh, n0, n1, n2, n3, n4, n5);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SequentialGuid"/> structure by using the specified unsigned integers and bytes.
        /// </summary>
        /// <param name="low">The low field of the timestamp (i.e. the first 4 bytes of the <see cref="SequentialGuid"/>).</param>
        /// <param name="mid">The middle field of the timestamp (i.e. the next 2 bytes of the <see cref="SequentialGuid"/>).</param>
        /// <param name="hi">The The high field of the timestamp multiplexed with the version number (i.e. the next 2 bytes of the <see cref="SequentialGuid"/>).</param>
        /// <param name="cs">The clock sequence of the <see cref="SequentialGuid"/> (i.e. the next 2 bytes of the <see cref="SequentialGuid"/>).</param>
        /// <param name="n">The spatially unique node identifier of the <see cref="SequentialGuid"/> that is constructed from a name.</param>
        public SequentialGuid(uint low, ushort mid, ushort hi, byte[] cs, byte[] n)
        {
            if (cs == null) throw new ArgumentNullException(nameof(cs));
            if (n == null) throw new ArgumentNullException(nameof(n));

            if (cs.Length != 2) throw new ArgumentException("Length of clock sequence array must be 2");
            if (n.Length != 6) throw new ArgumentException("Length of node array must be 6");

            guid = new Guid(low, mid, hi, cs[0], cs[1], n[0], n[1], n[2], n[3], n[4], n[5]);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SequentialGuid"/> structure by using the specified bytes.
        /// </summary>
        /// <param name="bytes"></param>
        public SequentialGuid(byte[] bytes) => guid = new Guid(bytes);

        /// <summary>
        /// Initializes a new instance of the <see cref="SequentialGuid"/> structure by using the specified <see cref="Guid"/>.
        /// <para /> Note the specified <paramref name="guid"/> should be compliant to a version 5 GUID as defined in RFC 4122.
        /// </summary>
        /// <param name="guid">The globally unique identifier (GUID) to initialize this instance.</param>
        public SequentialGuid(Guid guid) => this.guid = guid;

        /// <summary>
        /// Converts the string representation of a version 5 GUID to the equivalent <see cref="SequentialGuid"/> structure.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>A structure that contains the value that was parsed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        /// <exception cref="FormatException"><paramref name="value"/> is not in a recognized format.</exception>
        public static SequentialGuid Parse(string value)
        {
            var result = Guid.Parse(value);
            if (!result.IsVersion1Variant()) throw new FormatException(value + " is not in a recognized format.");
            return result;
        }

        /// <summary>
        /// Converts the string representation of a GUID to the equivalent <see cref="SequentialGuid"/> structure,
        /// provided that the string is in the specified format.
        /// </summary>
        /// <param name="value">The version 5 GUID to convert.</param>
        /// <param name="format">One of the following specifiers that indicates the exact format to use when interpreting <paramref name="value"/>:
        /// "N", "D", "B", "P", or "X".
        /// </param>
        /// <returns>A structure that contains the value that was parsed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> or <paramref name="value"/> is null.</exception>
        /// <exception cref="FormatException"><paramref name="value"/> is not in a recognized format.</exception>
        public static SequentialGuid ParseExact(string value, string format)
        {
            var result = Guid.ParseExact(value, format);
            if (!result.IsVersion1Variant()) throw new FormatException(value + " is not in a recognized format.");
            return result;
        }

        /// <summary>
        ///  Converts the string representation of a GUID to the equivalent <see cref="SequentialGuid"/> structure.
        /// </summary>
        /// <param name="value">The version GUID to convert.</param>
        /// <param name="result">The structure that will contain the parsed value. If the method returns true,
        /// result contains a valid <see cref="SequentialGuid"/>. If the method returns false, result equals
        /// <see cref="Empty"/> </param>
        /// <returns>true if the parse operation was successful; otherwise, false.</returns>
        public static bool TryParse(string value, out SequentialGuid result)
        {
            bool success = false;
            if (Guid.TryParse(value, out Guid guid) && guid.IsVersion1Variant())
            {
                success = true;
                result = guid;
            }
            else result = Empty;
            return success;
        }

        /// <summary>
        /// Converts the string representation of a GUID to the equivalent <see cref="SequentialGuid"/> structure,
        /// provided that the string is in the specified format.
        /// </summary>
        /// <param name="value">The version 5 GUID to convert</param>
        /// <param name="format">One of the following specifiers that indicates the exact format to use when interpreting <paramref name="value"/>:
        /// "N", "D", "B", "P", or "X".
        /// </param>
        /// <param name="result">The structure that will contain the parsed value. If the method returns true,
        /// result contains a valid <see cref="SequentialGuid"/>. If the method returns false, result equals
        /// <see cref="Empty"/> </param>
        /// <returns>true if the parse operation was successful; otherwise, false.</returns>
        public static bool TryParseExact(String value, String format, out SequentialGuid result)
        {
            bool success = false;
            if (Guid.TryParseExact(value, format, out Guid guid) && guid.IsVersion1Variant())
            {
                success = true;
                result = guid;
            }
            else result = Empty;
            return success;
        }

        private static ulong GetTimestamp()
        {
            var timestamp = (ulong)(DateTime.UtcNow - new DateTime(1582, 10, 15).ToUniversalTime()).Ticks;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="SequentialGuid"/> structure using an internal randomly or pseudo-randomly generated value.
        /// </summary>
        /// <returns>A new <see cref="SequentialGuid"/> object</returns>
        public static SequentialGuid NewGuid()
        {
            var node = RandomNumberGenerator.Create().Generate(6);
            node[0] |= 1; //multicast bit set to one, in order to avoid conflict with IEEE 802 network cards
            return Create(GetTimestamp(), sequence, node[0], node[1], node[2], node[3], node[4], node[5]);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SequentialGuid"/> structure
        /// using the IEEE 802 MAC address assigned to the specified network interface controller (NIC).
        /// </summary>
        /// <param name="nic">The network interface controller whose assigned IEEE 802 MAC address shall be used in the generation of the GUID.</param>
        /// <returns>A new <see cref="SequentialGuid"/> object</returns>
        public static SequentialGuid NewGuid(NetworkInterface nic)
        {
            if (nic == null) throw new ArgumentNullException(nameof(nic));

            var node = nic.GetPhysicalAddress().GetAddressBytes();
            return Create(GetTimestamp(), sequence, node[0], node[1], node[2], node[3], node[4], node[5]);
        }

        /// <summary>
        /// Returns a 16-element byte array that contains the value of this instance.
        /// </summary>
        /// <returns></returns>
        public byte[] ToByteArray() => guid.ToByteArray();

        /// <summary>
        /// Converts this <see cref="SequentialGuid"/> instance to the equiavlent <see cref="Guid"/> structure.
        /// </summary>
        /// <returns>A <see cref="Guid"/> structure that contains the value that was converted.</returns>
        public Guid AsGuid() => guid;

        /// <summary>
        /// Returns a value indicating whether this instance and a specified <see cref="SequentialGuid"/> object represent the same value.
        /// </summary>
        /// <param name="other"> An object to compare to this instance.</param>
        /// <returns>true if <paramref name="other"/> is equal to this instance; otherwise, false.</returns>
        public bool Equals(SequentialGuid other)
        {
            return guid.Equals(other.guid);
        }

        /// <summary>
        /// Returns a value that indicates whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="o">The object to compare with this instance.</param>
        /// <returns>true if o is a <see cref="SequentialGuid"/> that has the same value as this instance; otherwise, false.</returns>
        public override bool Equals(object o)
        {
            if (ReferenceEquals(null, o)) return false;
            return o is SequentialGuid && Equals((SequentialGuid)o);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code for this instance.</returns>
        public override int GetHashCode() => guid.GetHashCode();

        /// <summary>
        /// Returns a string representation of the value of this instance in registry format.
        /// </summary>
        /// <returns>
        /// The value of this <see cref="SequentialGuid"/> , formatted by using the "D" format specifier as follows:
        /// <para/> xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
        /// <para/>where the value of the GUID is represented as a series of lowercase hexadecimal digits in groups of 8, 4, 4,
        /// 4, and 12 digits and separated by hyphens.
        /// <para/>An example of a return value is "382c74c3-721d-4f34-80e5-57657b6cbc27".
        /// <para/>To convert the hexadecimal digits from a through f to uppercase, call the <see cref="String.ToUpper"/>
        /// method on the returned string.
        /// </returns>
        public override string ToString() => AsGuid().ToString();

        /// <summary>
        /// Returns a string representation of the value of this instance in registry format.
        /// </summary>
        /// <param name="format">A single format specifier that indicates how to format the value of this <see cref="SequentialGuid"/>.
        /// <para/> The format parameter can be "N", "D", "B", "P", or "X". If format is null or an empty string (""), "D" is used.
        /// </param>
        /// <returns>The value of this <see cref="SequentialGuid"/> , represented as a series of lowercase hexadecimal digits in the specified format.</returns>
        public string ToString(string format) => guid.ToString(format);

        /// <summary>
        /// Returns a string representation of the value of this instance in registry format.
        /// </summary>
        /// <param name="format">A single format specifier that indicates how to format the value of this <see cref="SequentialGuid"/>.
        /// <para/> The format parameter can be "N", "D", "B", "P", or "X". If format is null or an empty string (""), "D" is used.
        /// </param>
        /// <param name="formatProvider">Provides a mechanism to control the formatting of the GUID.
        /// Currently, it is ignored. Please see "https://referencesource.microsoft.com/#mscorlib/system/guid.cs,a6547a472def7796" </param>
        /// <returns>The value of this <see cref="SequentialGuid"/> , represented as a series of lowercase hexadecimal digits in the specified format.</returns>
        public string ToString(string format, IFormatProvider formatProvider) => guid.ToString(format);

        /// <summary>
        /// Compares this instance to a specified <see cref="SequentialGuid"/>  object and returns an indication of their relative values.
        /// </summary>
        /// <param name="o">An object to compare to this instance.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and <paramref name="o"/>.
        /// <para/> Return value Description:
        /// <para/> A negative integer: This instance is less than <paramref name="o"/>.
        /// <para/> Zero: This instance is equal to <paramref name="o"/>.
        /// <para/> A positive integer: This instance is greater than <paramref name="o"/>.
        /// </returns>
        public int CompareTo(object o)
        {
            if (ReferenceEquals(o, null)) return 1;
            if (!(o is SequentialGuid)) throw new ArgumentException("Argument must be SequentialGuid");
            return CompareTo((SequentialGuid)o);
        }

        /// <summary>
        /// Compares this instance to a specified <see cref="SequentialGuid"/>  object and returns an indication of their relative values.
        /// </summary>
        /// <param name="other">An object to compare to this instance.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and <paramref name="other"/>.
        /// <para/> Return value Description:
        /// <para/> A negative integer: This instance is less than <paramref name="other"/>.
        /// <para/> Zero: This instance is equal to <paramref name="other"/>.
        /// <para/> A positive integer: This instance is greater than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(SequentialGuid other) => guid.CompareTo(other.guid);

        /// <summary>
        /// Indicates whether the values of two specified <see cref="SequentialGuid"/> objects are equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns> true if <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, false.</returns>
        public static bool operator ==(SequentialGuid left, SequentialGuid right) => left.Equals(right);

        /// <summary>
        /// Indicates whether the values of two specified <see cref="SequentialGuid"/> objects are not equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>true if <paramref name="left"/> and <paramref name="right"/> are not equal; otherwise, false.</returns>
        public static bool operator !=(SequentialGuid left, SequentialGuid right) => !left.Equals(right);

        /// <summary>
        /// Indicates whether the value of this instance is less than the value of the specified <see cref="SequentialGuid"/> instance.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>true if <paramref name="left"/> is less than <paramref name="right"/>; otherwise, false.</returns>
        public static bool operator <(SequentialGuid left, SequentialGuid right) => left.CompareTo(right) < 0;

        /// <summary>
        /// Indicates whether the value of this instance is greater than the value of the specified <see cref="SequentialGuid"/> instance.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>true if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, false</returns>
        public static bool operator >(SequentialGuid left, SequentialGuid right) => left.CompareTo(right) > 0;

        /// <summary>
        /// Indicates whether the value of this instance is less than or equal to the value of the specified <see cref="SequentialGuid"/> instance.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>true if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise, false.</returns>
        public static bool operator <=(SequentialGuid left, SequentialGuid right) => left.CompareTo(right) <= 0;

        /// <summary>
        /// Indicates whether the value of this instance is greater than or equal to the value of the specified <see cref="SequentialGuid"/> instance.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>true if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise, false.</returns>
        public static bool operator >=(SequentialGuid left, SequentialGuid right) => left.CompareTo(right) >= 0;

        /// <summary>
        /// Converts a <see cref="SequentialGuid"/> structure to an equivalent <see cref="Guid"/> structure.
        /// </summary>
        /// <param name="guid">The <see cref="SequentialGuid"/> instance to convert.</param>
        public static implicit operator Guid(SequentialGuid guid) => guid.AsGuid();

        /// <summary>
        /// Converts a <see cref="Guid"/> structure to an equivalent <see cref="SequentialGuid"/> structure.
        /// </summary>
        /// <param name="guid">The <see cref="Guid"/> instance to convert.</param>
        public static implicit operator SequentialGuid(Guid guid) => new SequentialGuid(guid);
    }
}