using reexmonkey.xmisc.core.authentication.types;
using System;

namespace reexmonkey.xmisc.core.authentication.extensions
{
    /// <summary>
    /// Extends features of enumeration types.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Converts the string value to its equivalent nullable enumeration type.
        /// </summary>
        /// <typeparam name="TEnum">The type of enumeration.</typeparam>
        /// <param name="value">The string value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static TEnum? AsNullableEnum<TEnum>(this string value)
            where TEnum : struct
        {
            if (string.IsNullOrEmpty(value)) return default;
            return Enum.TryParse(value, true, out TEnum result)
                ? result
                : default;
        }

        /// <summary>
        /// Converts the string value to its equivalent enumeration type.
        /// </summary>
        /// <typeparam name="TEnum">The type of enumeration.</typeparam>
        /// <param name="value">The string value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static TEnum AsEnum<TEnum>(this string value)
            where TEnum : struct
        {
            if (string.IsNullOrEmpty(value)) return default;
            return Enum.TryParse(value, true, out TEnum result)
                ? result
                : throw new FormatException("Parsing error: Invalid eumeration format");
        }

        /// <summary>
        /// Converts the string value to its equivalent <see cref="JweAlg"/> representation.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static JweAlg AsJweAlg(this string value)
        {
            if (string.IsNullOrEmpty(value)) return default;

            if (value.Equals("RSA-OAEP", StringComparison.OrdinalIgnoreCase)) return JweAlg.RSA_OAEP;
            if (value.Equals("RSA-OAEP-256", StringComparison.OrdinalIgnoreCase)) return JweAlg.RSA_OAEP_256;
            if (value.Equals("ECDH-ES", StringComparison.OrdinalIgnoreCase)) return JweAlg.ECDH_ES;
            if (value.Equals("ECDH-ES+A128KW", StringComparison.OrdinalIgnoreCase)) return JweAlg.ECDH_ESA128KW;
            if (value.Equals("ECDH-ES+A192KW", StringComparison.OrdinalIgnoreCase)) return JweAlg.ECDH_ESA192KW;
            if (value.Equals("ECDH-ES+A256KW", StringComparison.OrdinalIgnoreCase)) return JweAlg.ECDH_ESA256KW;
            if (value.Equals("PBES2-HS256+A128KW", StringComparison.OrdinalIgnoreCase)) return JweAlg.PBES2_HS256A128KW;
            if (value.Equals("PBES2-HS384+A192KW", StringComparison.OrdinalIgnoreCase)) return JweAlg.PBES2_HS384A192KW;
            if (value.Equals("PBES2-HS512+A256KW", StringComparison.OrdinalIgnoreCase)) return JweAlg.PBES2_HS512A256KW;
            return (JweAlg)Enum.Parse(typeof(JweAlg), value, true);
        }

        /// <summary>
        /// Converts the string value to its equivalent nullable <see cref="JweAlg"/> representation.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static JweAlg? AsNullableJweAlg(this string value)
        {
            if (string.IsNullOrEmpty(value)) return default;
            try
            {
                return value.AsJweAlg();
            }
            catch (ArgumentNullException) { return null; }
            catch (ArgumentException) { return null; }
            catch (OverflowException) { return null; }
        }
    }
}