using System;
using System.Runtime.Serialization;

namespace reexmonkey.xmisc.core.authentication.types
{
    /// <summary>
    /// Represents the type of cryptographic algorithm used to secure a JWS.
    /// </summary>
    public enum JwsAlg
    {
        /// <summary>
        /// HMAC using SHA-256
        /// <para /> Required
        /// </summary>
        HS256,

        /// <summary>
        /// HMAC using SHA-384
        /// <para /> Optional
        /// </summary>
        HS384,

        /// <summary>
        /// HMAC using SHA-512
        /// <para /> Optional
        /// </summary>
        HS512,

        /// <summary>
        ///  RSASSA-PKCS1-v1_5 using SHA-256
        ///  <para /> Recommended
        /// </summary>
        RS256,

        /// <summary>
        ///  RSASSA-PKCS1-v1_5 using SHA-384
        ///  <para /> Optional
        /// </summary>
        RS384,

        /// <summary>
        ///  RSASSA-PKCS1-v1_5 using SHA-512
        ///  <para /> Optional
        /// </summary>
        RS512,

        /// <summary>
        ///  ECDSA using P-256 and SHA-256
        ///  <para /> Recommended+
        /// </summary>
        ES256,

        /// <summary>
        ///  ECDSA using P-384 and SHA-384
        ///  <para /> Optional
        /// </summary>
        ES384,

        /// <summary>
        /// ECDSA using P-521 and SHA-512
        /// <para /> Optional
        /// </summary>
        ES512,

        /// <summary>
        ///  RSASSA-PSS using SHA-256 and MGF1 with SHA-256
        ///  <para /> Optional
        /// </summary>
        PS256,

        /// <summary>
        ///  RSASSA-PSS using SHA-384 and MGF1 with SHA-384
        ///  <para /> Optional
        /// </summary>
        PS384,

        /// <summary>
        ///  RSASSA-PSS using SHA-512 and MGF1 with SHA-512
        ///  <para /> Optional
        /// </summary>
        PS512,

        /// <summary>
        ///  No digital signature or MAC performed
        /// </summary>
        none
    }

    /// <summary>
    /// Represents the type of cryptographic algorithm used to secure a JWE.
    /// </summary>
    [DataContract]
    public enum JweAlg
    {
        /// <summary>
        /// RSAES-PKCS1-v1_5
        /// <para /> Recommended-
        /// </summary>
        [EnumMember]
        RSA1_5,

        /// <summary>
        /// RSAES OAEP using default parameters
        /// <para /> Recommended+
        /// </summary>
        [EnumMember(Value = "RSA-OAEP")]
        RSA_OAEP,

        /// <summary>
        /// RSAES OAEP using SHA-256 and MGF1 with SHA-256
        /// <para /> Optional
        /// </summary>
        [EnumMember(Value = "RSA-OAEP-256")]
        RSA_OAEP_256,

        /// <summary>
        ///  AES Key Wrap with default initial value using 128-bit key
        ///  <para /> Recommended
        /// </summary>
        [EnumMember]
        A128KW,

        /// <summary>
        ///  AES Key Wrap with default initial value using 192-bit key
        ///  <para /> Optional
        /// </summary>
        [EnumMember]
        A192KW,

        /// <summary>
        ///  AES Key Wrap with default initial value using 256-bit key
        ///  <para /> Recommended
        /// </summary>
        [EnumMember]
        A256KW,

        /// <summary>
        ///  Direct use of a shared symmetric key as the CEK
        ///  <para /> Recommended
        /// </summary>
        [EnumMember(Value = "dir")]        
        Dir,

        /// <summary>
        ///  Elliptic Curve Diffie-Hellman Ephemeral Static key agreement using Concat KDF
        /// <para /> Header parameters: "epk", "apu", "apv"
        /// <para /> Recommended+
        /// </summary>
        [EnumMember(Value = "ECDH-ES")]
        ECDH_ES,

        /// <summary>
        ///  ECDH-ES using Concat KDF and CEK wrapped with "A128KW"
        /// <para /> Header parameters: "epk", "apu", "apv"
        /// <para /> Recommended
        /// </summary>
        [EnumMember(Value = "ECDH-ES+A128KW")]
        ECDH_ESA128KW,

        /// <summary>
        ///  ECDH-ES using Concat KDF and CEK wrapped with "A192KW"
        /// <para /> Header parameters: "epk", "apu", "apv"
        /// <para /> Optional
        /// </summary>
        [EnumMember(Value = "ECDH-ES+A192KW")]
        ECDH_ESA192KW,

        /// <summary>
        ///  ECDH-ES using Concat KDF and CEK wrapped with "A256KW"
        /// <para /> Header parameters: "epk", "apu", "apv"
        /// <para /> Recommended
        /// </summary>
        [EnumMember(Value = "ECDH-ES+A256KW")]
        ECDH_ESA256KW,

        /// <summary>
        ///  Key wrapping with AES GCM using 128-bit key
        /// <para /> Header parameters: "iv", "tag"
        /// <para /> Optional
        /// </summary>
        [EnumMember]
        A128GCMKW,

        /// <summary>
        ///  Key wrapping with AES GCM using 192-bit key
        /// <para /> Header parameters: "iv", "tag"
        /// <para /> Optional
        /// </summary>
        [EnumMember]
        A192GCMKW,

        /// <summary>
        ///  Key wrapping with AES GCM using 256-bit key
        /// <para /> Header parameters: "iv", "tag"
        /// <para /> Optional
        /// </summary>
        [EnumMember]
        A256GCMKW,

        /// <summary>
        ///  PBES2 with HMAC SHA-256 and "A128KW" wrapping
        /// <para /> Header parameters: "p2s", "p2c"
        /// <para /> Optional
        /// </summary>
        [EnumMember(Value = "PBES2-HS256+A128KW")]
        PBES2_HS256A128KW,

        /// <summary>
        ///  PBES2 with HMAC SHA-384 and "A192KW" wrapping
        /// <para /> Header parameters: "p2s", "p2c"
        /// <para /> Optional
        /// </summary>
        [EnumMember(Value = "PBES2-HS384+A192KW")]
        PBES2_HS384A192KW,

        /// <summary>
        ///  PBES2 with HMAC SHA-512 and "A256KW" wrapping
        /// <para /> Header parameters: "p2s", "p2c"
        /// <para /> Optional
        /// </summary>
        [EnumMember(Value = "PBES2-HS512+A256KW")]
        PBES2_HS512A256KW,
    }

    /// <summary>
    /// Represents the type of intended public use of a JWK.
    /// </summary>
    [Flags]
    public enum Use
    {
        /// <summary>
        /// Signature
        /// </summary>
        sig = 0x0001,

        /// <summary>
        /// Encryption
        /// </summary>
        enc = 0x0010
    }

    /// <summary>
    /// Represents the type of operation a JWK is intended to be used.
    /// </summary>
    [Flags]
    public enum KeyOps
    {
        /// <summary>
        /// Compute digital signature or MAC
        /// </summary>
        sign = 0x00000001,

        /// <summary>
        /// Verify digital signature or MAC
        /// </summary>
        verify = 0x00000010,

        /// <summary>
        /// Encrypt content
        /// </summary>
        encrypt = 0x00000100,

        /// <summary>
        /// Decrypt content and validate decryption, if applicable
        /// </summary>
        decrypt = 0x00001000,

        /// <summary>
        /// Encrypt key
        /// </summary>
        wrapKey = 0x00010000,

        /// <summary>
        /// Decrypt key and validate decryption, if applicable
        /// </summary>
        unwrapKey = 0x00100000,

        /// <summary>
        /// Derive key
        /// </summary>
        deriveKey = 0x01000000,

        /// <summary>
        /// Derive bits not to be used as a key
        /// </summary>
        deriveBits = 0x10000000
    }

    /// <summary>
    /// Represesents the type of cryptographic algorithm family used with the key
    /// </summary>
    public enum Kty
    {
        /// <summary>
        /// Elliptic Curve 
        /// </summary>
        EC,

        /// <summary>
        /// Rivest-Shamir-Adleman
        /// </summary>
        RSA,

        /// <summary>
        /// Octet sequence
        /// </summary>
        oct
    }
}