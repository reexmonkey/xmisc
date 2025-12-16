using reexmonkey.xmisc.backbone.identifiers.contracts.extensions;
using reexmonkey.xmisc.backbone.identifiers.contracts.helpers;
using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace reexmonkey.xmisc.backbone.identifiers.contracts.models
{
    /// <summary>
    /// Represents a name-based (version 3) globally unique identifier (GUID) that uses <see cref="MD5"/> hashing as defined in RFC 9562 (https://tools.ietf.org/html/rfc4122)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public readonly record struct Md5Guid : IComparable, IComparable<Md5Guid>, IFormattable
    {
        /// <summary>
        ///  A read-only instance of the  <see cref="Md5Guid"/> structure whose all 128 bits are set to zeros.
        /// </summary>
        public static readonly Md5Guid Empty = new();

        /// <summary>
        /// Gets the default fully-qualified Distinguished Name (DNs) namespace ID  as defined in RFC 9562.
        /// </summary>
        public static readonly Guid DnsNamespaceId = new("6ba7b810-9dad-11d1-80b4-00c04fd430c8");

        /// <summary>
        /// Gets the default uniform resource locator (URL) namespace ID  as defined in RFC 9562.
        /// </summary>
        public static readonly Guid UrlNamespaceId = new("6ba7b811-9dad-11d1-80b4-00c04fd430c8");

        /// <summary>
        /// Gets the default ISO OID namepace ID as defined in RFC 9562.
        /// </summary>
        public static readonly Guid OidNamespaceId = new("6ba7b812-9dad-11d1-80b4-00c04fd430c8");

        /// <summary>
        /// Gets the default X500 namepace ID as defined in RFC 9562.
        /// </summary>
        public static readonly Guid X500NamespaceId = new("6ba7b814-9dad-11d1-80b4-00c04fd430c8");

        private readonly Guid guid;

        /// <summary>
        /// Initializes a new instance of the <see cref="Md5Guid"/> structure by using the specified unsigned integers and bytes.
        /// </summary>
        /// <param name="low">The low field of the timestamp (i.e. the first 4 bytes of the <see cref="Md5Guid"/>).</param>
        /// <param name="mid">The middle field of the timestamp (i.e. the next 2 bytes of the <see cref="Md5Guid"/>).</param>
        /// <param name="hi">The The high field of the timestamp multiplexed with the version number (i.e. the next 2 bytes of the <see cref="Md5Guid"/>).</param>
        /// <param name="csl">The high field of the clock sequence multiplexed with the variant (i.e. the next byte of the <see cref="Md5Guid"/>).</param>
        /// <param name="csh">The low field of the clock sequence (i.e. the next byte of the <see cref="Md5Guid"/>).</param>
        /// <param name="n0">The first byte of a spatially unique node identifier of the <see cref="Md5Guid"/> that is constructed from a name.</param>
        /// <param name="n1">The next byte of the spatially unique node identifier of the <see cref="Md5Guid"/> that is constructed from a name.</param>
        /// <param name="n2">The next byte of the spatially unique node identifier of the <see cref="Md5Guid"/> that is constructed from a name.</param>
        /// <param name="n3">The next byte of the spatially unique node identifier of the <see cref="Md5Guid"/> that is constructed from a name.</param>
        /// <param name="n4">The next byte of the spatially unique node identifier of the <see cref="Md5Guid"/> that is constructed from a name.</param>
        /// <param name="n5">The last byte of the spatially unique node identifier of the <see cref="Md5Guid"/> that is constructed from a name.</param>
        public Md5Guid(uint low, ushort mid, ushort hi, byte csl, byte csh, byte n0, byte n1, byte n2, byte n3, byte n4, byte n5)
        {
            guid = new Guid(low, mid, hi, csl, csh, n0, n1, n2, n3, n4, n5);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Md5Guid"/> structure by using the specified unsigned integers and bytes.
        /// </summary>
        /// <param name="low">The low field of the timestamp (i.e. the first 4 bytes of the <see cref="Md5Guid"/>).</param>
        /// <param name="mid">The middle field of the timestamp (i.e. the next 2 bytes of the <see cref="Md5Guid"/>).</param>
        /// <param name="hi">The The high field of the timestamp multiplexed with the version number (i.e. the next 2 bytes of the <see cref="Md5Guid"/>).</param>
        /// <param name="cs">The clock sequence of the <see cref="Md5Guid"/> (i.e. the next 2 bytes of the <see cref="Md5Guid"/>).</param>
        /// <param name="n">The spatially unique node identifier of the <see cref="Md5Guid"/> that is constructed from a name.</param>
        public Md5Guid(uint low, ushort mid, ushort hi, byte[] cs, byte[] n)
        {
            if (cs == null) throw new ArgumentNullException(nameof(cs));
            if (n == null) throw new ArgumentNullException(nameof(n));

            if (cs.Length != 2) throw new ArgumentException("Length of clock sequence array must be 2");
            if (n.Length != 6) throw new ArgumentException("Length of node array must be 6");

            guid = new Guid(low, mid, hi, cs[0], cs[1], n[0], n[1], n[2], n[3], n[4], n[5]);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Md5Guid"/> structure by using the specified bytes.
        /// </summary>
        /// <param name="bytes"></param>
        public Md5Guid(byte[] bytes) => guid = new Guid(bytes);

        /// <summary>
        /// Initializes a new instance of the <see cref="Md5Guid"/> structure by using the specified <see cref="Guid"/>.
        /// <para /> Note the specified <paramref name="guid"/> should be compliant to a version 5 GUID as defined in RFC 9562.
        /// </summary>
        /// <param name="guid">The globally unique identifier (GUID) to initialize this instance.</param>
        public Md5Guid(Guid guid) => this.guid = guid;

        /// <summary>
        /// Converts the string representation of a version 5 GUID to the equivalent <see cref="Md5Guid"/> structure.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>A structure that contains the value that was parsed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        /// <exception cref="FormatException"><paramref name="value"/> is not in a recognized format.</exception>
        public static Md5Guid Parse(string value)
        {
            var result = Guid.Parse(value);
            if (!result.IsVersion3Variant()) throw new FormatException(value + " is not in a recognized format.");
            return result;
        }

        /// <summary>
        /// Converts the string representation of a GUID to the equivalent <see cref="Md5Guid"/> structure,
        /// provided that the string is in the specified format.
        /// </summary>
        /// <param name="value">The version 5 GUID to convert.</param>
        /// <param name="format">One of the following specifiers that indicates the exact format to use when interpreting <paramref name="value"/>:
        /// "N", "D", "B", "P", or "X".
        /// </param>
        /// <returns>A structure that contains the value that was parsed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> or <paramref name="value"/> is null.</exception>
        /// <exception cref="FormatException"><paramref name="value"/> is not in a recognized format.</exception>
        public static Md5Guid ParseExact(string value, string format)
        {
            var result = Guid.ParseExact(value, format);
            if (!result.IsVersion3Variant()) throw new FormatException(value + " is not in a recognized format.");
            return result;
        }

        /// <summary>
        ///  Converts the string representation of a GUID to the equivalent <see cref="Md5Guid"/> structure.
        /// </summary>
        /// <param name="value">The version GUID to convert.</param>
        /// <param name="result">The structure that will contain the parsed value. If the method returns true,
        /// result contains a valid <see cref="Md5Guid"/>. If the method returns false, result equals
        /// <see cref="Empty"/> </param>
        /// <returns>true if the parse operation was successful; otherwise, false.</returns>
        public static bool TryParse(string value, out Md5Guid result)
        {
            bool success = false;
            if (Guid.TryParse(value, out Guid guid) && guid.IsVersion3Variant())
            {
                success = true;
                result = guid;
            }
            else result = Empty;
            return success;
        }

        /// <summary>
        /// Converts the string representation of a GUID to the equivalent <see cref="Md5Guid"/> structure,
        /// provided that the string is in the specified format.
        /// </summary>
        /// <param name="value">The version 5 GUID to convert</param>
        /// <param name="format">One of the following specifiers that indicates the exact format to use when interpreting <paramref name="value"/>:
        /// "N", "D", "B", "P", or "X".
        /// </param>
        /// <param name="result">The structure that will contain the parsed value. If the method returns true,
        /// result contains a valid <see cref="Md5Guid"/>. If the method returns false, result equals
        /// <see cref="Empty"/> </param>
        /// <returns>true if the parse operation was successful; otherwise, false.</returns>
        public static bool TryParseExact(string value, string format, out Md5Guid result)
        {
            bool success = false;
            if (Guid.TryParseExact(value, format, out Guid guid) && guid.IsVersion3Variant())
            {
                success = true;
                result = guid;
            }
            else result = Empty;
            return success;
        }

        private static Md5Guid Create(byte[] hash)
        {
            int version = 3;
            var guid = new byte[16];

            //copy the first 16 bytes
            Array.Copy(hash, 0, guid, 0, 16);

            // set version to 5:
            guid[6] = (byte)((guid[6] & 0x0F) | (version << 4));

            //clock sequence hi and reserved: 8
            guid[8] = (byte)((guid[8] & 0x3F) | 0x80);

            //reverse copy to match network byte order
            var swapped = guid.SwapByteOrder();
            return new Md5Guid(swapped);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Md5Guid"/> structure for a string-based name drawn from- and within a string-based namespace.
        /// </summary>
        /// <param name="namespaceId">The identifier of the namespace, from and within which a name is drawn.</param>
        /// <param name="name">The string value that is drawn from a namespace and serves as the basis for generating the version 5 GUID. </param>
        /// <param name="encoding">The character encoding that was used to format the specified <paramref name="namespaceId"/> and <paramref name="name"/>.</param>
        /// <returns>A new <see cref="Md5Guid"/> object.</returns>
        public static Md5Guid NewGuid(string namespaceId, string name, Encoding encoding) => NewGuid(encoding.GetBytes(namespaceId), encoding.GetBytes(name));

        /// <summary>
        /// Initializes a new instance of the <see cref="Md5Guid"/> structure for a string-based name drawn from- and within a GUID-based namespace.
        /// </summary>
        /// <param name="namespaceId">The identifier of the namespace, from and within which a name is drawn.</param>
        /// <param name="name">The string value that is drawn from a namespace and serves as the basis for generating the version 5 GUID. </param>
        /// <param name="encoding">The character encoding that was used to format the specified <paramref name="name"/>.</param>
        /// <returns>A new <see cref="Md5Guid"/> object.</returns>
        public static Md5Guid NewGuid(Guid namespaceId, string name, Encoding encoding) => NewGuid(namespaceId.ToByteArray(), encoding.GetBytes(name));

        /// <summary>
        /// Initializes a new instance of the <see cref="Md5Guid"/> structure for a byte-specified name drawn from- and within a byte-based namespace.
        /// </summary>
        /// <param name="namespaceId">The identifier of the namespace, from and within which a name is drawn.</param>
        /// <param name="name">The value that is drawn from a namespace and serves as the basis for generating the version 5 GUID.</param>
        /// <returns>A new <see cref="Md5Guid"/> object</returns>
        public static Md5Guid NewGuid(byte[] namespaceId, byte[] name)
        {
            var swapped = namespaceId.SwapByteOrder();
            var hash = MD5.HashData([.. swapped, .. name]);
            return Create(hash);
        }

        /// <summary>
        /// Returns a 16-element byte array that contains the value of this instance.
        /// </summary>
        /// <returns></returns>
        public byte[] ToByteArray() => guid.ToByteArray();

        /// <summary>
        /// Converts this <see cref="Md5Guid"/> instance to the equiavlent <see cref="Guid"/> structure.
        /// </summary>
        /// <returns>A <see cref="Guid"/> structure that contains the value that was converted.</returns>
        public Guid AsGuid() => guid;

        /// <summary>
        /// Returns a string representation of the value of this instance in registry format.
        /// </summary>
        /// <returns>
        /// The value of this <see cref="Md5Guid"/> , formatted by using the "D" format specifier as follows:
        /// <para/> xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
        /// <para/>where the value of the GUID is represented as a series of lowercase hexadecimal digits in groups of 8, 4, 4,
        /// 4, and 12 digits and separated by hyphens.
        /// <para/>An example of a return value is "382c74c3-721d-4f34-80e5-57657b6cbc27".
        /// <para/>To convert the hexadecimal digits from a through f to uppercase, call the <see cref="string.ToUpper()"/>
        /// method on the returned string.
        /// </returns>
        public override string ToString() => AsGuid().ToString();

        /// <summary>
        /// Returns a string representation of the value of this instance in registry format.
        /// </summary>
        /// <param name="format">A single format specifier that indicates how to format the value of this <see cref="Md5Guid"/>.
        /// <para/> The format parameter can be "N", "D", "B", "P", or "X". If format is null or an empty string (""), "D" is used.
        /// </param>
        /// <returns>The value of this <see cref="Md5Guid"/> , represented as a series of lowercase hexadecimal digits in the specified format.</returns>
        public string ToString(string format) => guid.ToString(format);

        /// <summary>
        /// Compares this instance to a specified <see cref="Md5Guid"/>  object and returns an indication of their relative values.
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
            if (!(o is Md5Guid)) throw new ArgumentException("Argument must be of type " + nameof(Md5Guid));
            return CompareTo((Md5Guid)o);
        }

        /// <summary>
        /// Compares this instance to a specified <see cref="Md5Guid"/>  object and returns an indication of their relative values.
        /// </summary>
        /// <param name="other">An object to compare to this instance.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and <paramref name="other"/>.
        /// <para/> Return value Description:
        /// <para/> A negative integer: This instance is less than <paramref name="other"/>.
        /// <para/> Zero: This instance is equal to <paramref name="other"/>.
        /// <para/> A positive integer: This instance is greater than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(Md5Guid other) => guid.CompareTo(other.guid);

        /// <summary>
        /// Returns a string representation of the value of this instance in registry format.
        /// </summary>
        /// <param name="format">A single format specifier that indicates how to format the value of this <see cref="Md5Guid"/>.
        /// <para/> The format parameter can be "N", "D", "B", "P", or "X". If format is null or an empty string (""), "D" is used.
        /// </param>
        /// <param name="formatProvider">Provides a mechanism to control the formatting of the GUID.
        /// Currently, it is ignored. Please see "https://referencesource.microsoft.com/#mscorlib/system/guid.cs,a6547a472def7796" </param>
        /// <returns>The value of this <see cref="SequentialGuid"/> , represented as a series of lowercase hexadecimal digits in the specified format.</returns>
        public string ToString(string format, IFormatProvider formatProvider) => guid.ToString(format);

        /// <summary>
        /// Indicates whether the value of this instance is less than the value of the specified <see cref="Md5Guid"/> instance.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>true if <paramref name="left"/> is less than <paramref name="right"/>; otherwise, false.</returns>
        public static bool operator <(Md5Guid left, Md5Guid right) => left.CompareTo(right) < 0;

        /// <summary>
        /// Indicates whether the value of this instance is greater than the value of the specified <see cref="Md5Guid"/> instance.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>true if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, false</returns>
        public static bool operator >(Md5Guid left, Md5Guid right) => left.CompareTo(right) > 0;

        /// <summary>
        /// Indicates whether the value of this instance is less than or equal to the value of the specified <see cref="Md5Guid"/> instance.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>true if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise, false.</returns>
        public static bool operator <=(Md5Guid left, Md5Guid right) => left.CompareTo(right) <= 0;

        /// <summary>
        /// Indicates whether the value of this instance is greater than or equal to the value of the specified <see cref="Md5Guid"/> instance.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>true if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise, false.</returns>
        public static bool operator >=(Md5Guid left, Md5Guid right) => left.CompareTo(right) >= 0;

        /// <summary>
        /// Converts a <see cref="Md5Guid"/> structure to an equivalent <see cref="Guid"/> structure.
        /// </summary>
        /// <param name="guid">The <see cref="Md5Guid"/> instance to convert.</param>
        public static implicit operator Guid(Md5Guid guid) => guid.AsGuid();

        /// <summary>
        /// Converts a <see cref="Guid"/> structure to an equivalent <see cref="Md5Guid"/> structure.
        /// </summary>
        /// <param name="guid">The <see cref="Guid"/> instance to convert.</param>
        public static implicit operator Md5Guid(Guid guid) => new(guid);
    }
}