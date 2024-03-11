using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using System;

namespace reexmonkey.xmisc.backbone.identifiers.contracts.extensions
{
    /// <summary>
    /// Provides extensions to globally unique identifiers (GUIDs).
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        /// Determines the version of the specified GUID variant.
        /// </summary>
        /// <param name="guid">The GUID, whose version is being determined.</param>
        /// <returns>The version of the GUID variant.</returns>
        public static int GetVersion(this Guid guid)
        {
            var bytes = guid.ToByteArray();
            return (ushort)(bytes[7] >> 4);
        }

        private static bool IsValidVersion(this Guid guid, int version) => guid.GetVersion() == version;

        /// <summary>
        /// Checks if this GUID is a valid time-based GUID as defined in RFC 4122
        /// </summary>
        /// <param name="guid">The GUID, whose version is verified.</param>
        /// <returns>true if it is a time-based GUID; otherwise false.</returns>
        public static bool IsVersion1Variant(this Guid guid) => guid.IsValidVersion(1);

        /// <summary>
        /// Checks if this GUID is a valid DCE Security GUID as defined in RFC 4122
        /// </summary>
        /// <param name="guid">The GUID, whose version is verified.</param>
        /// <returns>true if it is a DCE Security GUID; otherwise false.</returns>
        public static bool IsVersion2Variant(this Guid guid) => guid.IsValidVersion(2);

        /// <summary>
        /// Checks if this GUID is a valid name-based GUID that uses MD5 hashing, as defined in RFC 4122
        /// </summary>
        /// <param name="guid">The GUID, whose version is verified.</param>
        /// <returns>true if it is a name-based GUID that uses MD5 hashing; otherwise false.</returns>
        public static bool IsVersion3Variant(this Guid guid) => guid.IsValidVersion(3);

        /// <summary>
        /// Checks if this GUID is a valid randomly or pseudo-randomly generated GUID as defined in RFC 4122
        /// </summary>
        /// <param name="guid">The GUID, whose version is verified.</param>
        /// <returns>true if it is a randomly or pseudo-randomly generated GUID; otherwise false.</returns>
        public static bool IsVersion4Variant(this Guid guid) => guid.IsValidVersion(4);

        /// <summary>
        /// Checks if this GUID is a valid name-based GUID that uses SHA1 hashing, as defined in RFC 4122
        /// </summary>
        /// <param name="guid">The GUID, whose version is verified.</param>
        /// <returns>true if it is a name-based GUID that uses SHA1 hashing; otherwise false.</returns>
        public static bool IsVersion5Variant(this Guid guid) => guid.IsValidVersion(5);

        /// <summary>
        /// Checks if this GUID is an invalid GUID as defined in RFC 4122.
        /// </summary>
        /// <param name="guid">The GUID, whose version is verified.</param>
        /// <returns>true if its version cannot be associated to any version variant defined in RFC 4122; otherwise false.</returns>
        public static bool IsUnknownVariant(this Guid guid)
        {
            var version = guid.GetVersion();
            return version < 1 && version > 5;
        }
    }
}