using System;
using System.Globalization;
using System.Xml;

namespace reexmonkey.xmisc.core.system.xmltools.extensions
{
    public static class StringExtensions
    {
        public static bool? ToNullableBoolean(this string value)
        {
            try
            {
                return XmlConvert.ToBoolean(value);
            }
            catch (ArgumentNullException)
            {
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
            catch (OverflowException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static byte? ToNullableByte(this string value)
        {
            try
            {
                return XmlConvert.ToByte(value);
            }
            catch (ArgumentNullException)
            {
                return default;
            }
            catch (FormatException)
            {
                return default;
            }
            catch (OverflowException)
            {
                return default;
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static sbyte? ToNullableSByte(this string value)
        {
            try
            {
                return XmlConvert.ToSByte(value);
            }
            catch (ArgumentNullException)
            {
                return default;
            }
            catch (FormatException)
            {
                return default;
            }
            catch (OverflowException)
            {
                return default;
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static char? ToNullableChar(this string value)
        {
            try
            {
                return XmlConvert.ToChar(value);
            }
            catch (ArgumentNullException)
            {
                return default;
            }
            catch (FormatException)
            {
                return default;
            }
            catch (OverflowException)
            {
                return default;
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static DateTime? ToNullableDateTime(this string value, XmlDateTimeSerializationMode option)
        {
            try
            {
                return XmlConvert.ToDateTime(value, option);
            }
            catch (ArgumentNullException)
            {
                return default;
            }
            catch (FormatException)
            {
                return default;
            }
            catch (OverflowException)
            {
                return default;
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static DateTime? ToNullableDateTime(this string value, IFormatProvider provider, DateTimeStyles styles)
        {
            return DateTime.TryParse(value, provider, styles, out DateTime result) ? result : default;
        }

        public static DateTimeOffset? ToNullableDateTimeOffset(this string value, string format = null)
        {
            try
            {
                return !string.IsNullOrEmpty(format)
                    ? XmlConvert.ToDateTimeOffset(value, format)
                    : XmlConvert.ToDateTimeOffset(value);
            }
            catch (ArgumentNullException)
            {
                return default;
            }
            catch (FormatException)
            {
                return default;
            }
            catch (OverflowException)
            {
                return default;
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static double? ToNullableDouble(this string value)
        {
            try
            {
                return XmlConvert.ToDouble(value);
            }
            catch (ArgumentNullException)
            {
                return default;
            }
            catch (FormatException)
            {
                return default;
            }
            catch (OverflowException)
            {
                return default;
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static double? ToNullableDouble(this string value, IFormatProvider provider)
        {
            return double.TryParse(value, NumberStyles.Float, provider, out double result) ? result : default;
        }

        public static float? ToNullableFloat(this string value)
        {
            try
            {
                return XmlConvert.ToSingle(value);
            }
            catch (ArgumentNullException)
            {
                return default;
            }
            catch (FormatException)
            {
                return default;
            }
            catch (OverflowException)
            {
                return default;
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static float? ToNullableFloat(this string value, IFormatProvider provider)
        {
            return float.TryParse(value, NumberStyles.Float, provider, out float result) ? result : default;
        }

        public static decimal? ToNullableDecimal(this string value)
        {
            try
            {
                return XmlConvert.ToDecimal(value);
            }
            catch (ArgumentNullException)
            {
                return default;
            }
            catch (FormatException)
            {
                return default;
            }
            catch (OverflowException)
            {
                return default;
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static decimal? ToNullableDecimal(this string value, IFormatProvider provider)
        {
            return decimal.TryParse(value, NumberStyles.Float, provider, out decimal result) ? result : default;
        }

        public static short? ToNullableInt16(this string value)
        {
            try
            {
                return XmlConvert.ToInt16(value);
            }
            catch (ArgumentNullException)
            {
                return default;
            }
            catch (FormatException)
            {
                return default;
            }
            catch (OverflowException)
            {
                return default;
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static short? ToNullableInt16(this string value, IFormatProvider provider)
        {
            return short.TryParse(value, NumberStyles.Integer, provider, out short result) ? result : default;
        }

        public static int? ToNullableInt32(this string value)
        {
            try
            {
                return XmlConvert.ToInt32(value);
            }
            catch (ArgumentNullException)
            {
                return default;
            }
            catch (FormatException)
            {
                return default;
            }
            catch (OverflowException)
            {
                return default;
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static int? ToNullableInt32(this string value, IFormatProvider provider)
        {
            return int.TryParse(value, NumberStyles.Integer, provider, out int result) ? result : default;
        }

        public static long? ToNullableInt64(this string value)
        {
            try
            {
                return XmlConvert.ToInt64(value);
            }
            catch (ArgumentNullException)
            {
                return default;
            }
            catch (FormatException)
            {
                return default;
            }
            catch (OverflowException)
            {
                return default;
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static long? ToNullableInt64(this string value, IFormatProvider provider)
        {
            return long.TryParse(value, NumberStyles.Integer, provider, out long result) ? result : default;
        }

        public static TimeSpan? ToNullableTimeSpan(this string value)
        {
            try
            {
                return XmlConvert.ToTimeSpan(value);
            }
            catch (ArgumentNullException)
            {
                return default;
            }
            catch (FormatException)
            {
                return default;
            }
            catch (OverflowException)
            {
                return default;
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static Guid? ToNullableGuid(this string value)
        {
            try
            {
                return XmlConvert.ToGuid(value);
            }
            catch (ArgumentNullException)
            {
                return default;
            }
            catch (FormatException)
            {
                return default;
            }
            catch (OverflowException)
            {
                return default;
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static ushort? ToNullableUInt16(this string value)
        {
            try
            {
                return XmlConvert.ToUInt16(value);
            }
            catch (ArgumentNullException)
            {
                return default;
            }
            catch (FormatException)
            {
                return default;
            }
            catch (OverflowException)
            {
                return default;
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static ushort? ToNullableUInt16(this string value, IFormatProvider provider)
        {
            return ushort.TryParse(value, 
                NumberStyles.AllowLeadingWhite 
                | NumberStyles.AllowTrailingWhite 
                | NumberStyles.AllowTrailingSign 
                | NumberStyles.AllowThousands, 
                provider, out ushort result) ? result : default;
        }

        public static uint? ToNullableUInt32(this string value)
        {
            try
            {
                return XmlConvert.ToUInt32(value);
            }
            catch (ArgumentNullException)
            {
                return default;
            }
            catch (FormatException)
            {
                return default;
            }
            catch (OverflowException)
            {
                return default;
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static uint? ToNullableUInt32(this string value, IFormatProvider provider)
        {
            return uint.TryParse(value,
                NumberStyles.AllowLeadingWhite
                | NumberStyles.AllowTrailingWhite
                | NumberStyles.AllowTrailingSign
                | NumberStyles.AllowThousands,
                provider, out uint result) ? result : default;
        }

        public static ulong? ToNullableUInt64(this string value)
        {
            try
            {
                return XmlConvert.ToUInt64(value);
            }
            catch (ArgumentNullException)
            {
                return default;
            }
            catch (FormatException)
            {
                return default;
            }
            catch (OverflowException)
            {
                return default;
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static ulong? ToNullableUInt64(this string value, IFormatProvider provider)
        {
            return ulong.TryParse(value,
                NumberStyles.AllowLeadingWhite
                | NumberStyles.AllowTrailingWhite
                | NumberStyles.AllowTrailingSign
                | NumberStyles.AllowThousands,
                provider, out ulong result) ? result : default;
        }

        public static T? ToNullableEnum<T>(this string value, bool ignoreCase = true) where T : struct, Enum
        {
            var success = Enum.TryParse(value, ignoreCase, out T result);
            return success ? result : default;
        }

        public static T ToEnum<T>(this string value, bool ignoreCase = true) where T : struct, Enum
        {
            var success = Enum.TryParse(value, ignoreCase, out T result);
            return success ? result : default;
        }
    }
}