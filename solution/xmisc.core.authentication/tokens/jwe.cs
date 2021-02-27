using reexmonkey.xmisc.core.authentication.extensions;
using reexmonkey.xmisc.core.authentication.types;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace reexmonkey.xmisc.core.authentication.tokens
{
    /// <summary>
    /// Represents a JOSE header for a JSON Web Encryption (JWS).
    /// </summary>
    [DataContract]
    public sealed class JweHeader : JoseHeader
    {
        /// <summary>
        /// Algorithm
        /// <para/> The cryptographic algorithm used to secure or encrypt the JWT.
        /// </summary>
        [DataMember(Order = 12)]
        public JweAlg? Alg { get; set; }

        /// <summary>
        /// Encryption Algorithm
        /// <para/> Identifies the content encryption algorithm used to perform authenticated encryption on the plaintext to produce the ciphertext and the Authentication Tag.
        /// <para /> The encrypted content is not usable if the "enc" value does
        /// not represent a supported algorithm. "enc" values should either be
        /// registered in the IANA "JSON Web Signature and Encryption Algorithms"
        /// registry established by[JWA] or be a value that contains a Collision-Resistant Name
        /// </summary>
        [DataMember(Order = 13)]
        public string Enc { get; set; }

        /// <summary>
        /// Protected Header
        /// <para/>JSON object that contains the Header Parameters that are integrity protected by the authenticated encryption operation.
        /// These parameters apply to all recipients of the JWE.
        /// For the JWE Compact Serialization, this comprises the entire JOSE Header.
        /// For the JWE JSON Serialization, this is one component of the JOSE Header.
        /// </summary>
        [DataMember(Order = 0)]
        public string Protected { get; set; }

        /// <summary>
        /// Shared Unprotected Header
        /// <para/> JSON object that contains the Header Parameters that apply to all recipients of the JWE that are not integrity protected.
        /// This can only be present when using the JWE JSON Serialization.
        /// </summary>
        [DataMember(Order = 0)]
        public string Unprotected { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="JweHeader"/> class.
        /// </summary>
        public JweHeader()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JweHeader"/> class from another instance.
        /// </summary>
        /// <param name="other">The instance used for the initialization.</param>
        public JweHeader(JweHeader other) : base(other)
        {
            Alg = other.Alg;
            Enc = other.Enc;
        }

        /// <summary>
        /// Parses a <see cref="JweHeader"/> value from its string representation.
        /// </summary>
        /// <param name="value">The string representation of an <see cref="JweHeader"/> instance.</param>
        /// <returns>The equivalent <see cref="JweHeader"/> representation.</returns>
        public static JweHeader Parse(string value)
        {
            var json = value.FromBase64UrlSafe().FromUtf8Bytes();
            var o = JsonObject.Parse(json);

            var header = new JweHeader
            {
                Alg = o.Get("alg").AsNullableJweAlg(),
                Jku = o.Get<string>("jku"),
                Jwk = o.Get<string>("jwk"),
                Kid = o.Get<string>("kid"),
                X5u = o.Get<string>("x5u"),
                X5t = o.Get<string>("x5t"),
                X5t256 = o.Get<string>("x5t#256"),
                Typ = o.Get<string>("typ"),
                Cty = o.Get<string>("cty"),
                Crit = o.Get<string>("crit"),
                Enc = o.Get<string>("enc"),
                Zip = o.Get<string>("zip"),
            };

            var x5cjson = o.Get<string>("x5c");
            if (!string.IsNullOrEmpty(x5cjson))
            {
                header.X5c = new List<string>();
                var certs = x5cjson.FromJson<string[]>();
                if (certs.Any()) header.X5c.AddRange(certs);
            }

            return header;
        }

        /// <summary>
        /// Returns a JSON serialization string that represents the current object.
        /// </summary>
        /// <returns>A JSON serialization string that represents the current object.</returns>
        public override string ToString()
        {
            using (JsConfig.CreateScope("EmitLowercaseUnderscoreNames,ExcludeTypeInfo"))
            {
                return this.ToJson();
            }
        }
    }

    /// <summary>
    /// Represents a JSON Web Encryption.
    /// </summary>
    [DataContract]
    public sealed class Jwe : Jwt<JweHeader>
    {
        /// <summary>
        /// Encrypted Content Encryption Key value.
        /// <para/> Note that for some algorithms, the JWE Encrypted Key value is specified as being the empty octet sequence.
        /// </summary>
        [DataMember(Order = 14)]
        public byte[] EncryptedKey { get; set; }

        /// <summary>
        /// Additional Authenticated Data (AAD)
        /// <para/> An input to an Authenticated Encryption with Associated Data (AEAD) operation that is integrity protected but not encrypted.
        /// <para/> An AEAD algorithm is one that encrypts the plaintext, allows Additional Authenticated Data to be specified, and provides an
        /// integrated content integrity check over the ciphertext and Additional Authenticated Data. AEAD algorithms accept two inputs,
        /// the plaintext and the Additional Authenticated Data value, and produce two outputs, the ciphertext and the Authentication Tag
        /// value.AES Galois/Counter Mode(GCM) is one such algorithm.
        /// </summary>
        [DataMember(Order = 15)]
        public byte[] Aad { get; set; }

        /// <summary>
        /// Initialization Vector value used when encrypting the plaintext
        /// </summary>
        [DataMember(Order = 16)]
        public byte[] IV { get; set; }

        /// <summary>
        /// Ciphertext
        /// <para/> Ciphertext value resulting from authenticated encryption of the plaintext with Additional Authenticated Data (AAD).
        /// </summary>
        [DataMember(Order = 17)]
        public byte[] Ciphertext { get; set; }

        /// <summary>
        /// Authentication Tag
        /// <para/> An output of an AEAD operation that ensures the integrity of the ciphertext and the Additional Authenticated Data.
        /// Note that some algorithms may not use an Authentication Tag, in which case this value is the empty octet sequence.
        /// </summary>
        [DataMember(Order = 18)]
        public byte[] Tag { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Jwe"/> class from another instance.
        /// </summary>
        /// <param name="other">The instance used for the initialization.</param>
        public Jwe(Jwe other) : base(other)
        {
            Header = other.Header != null ? new JweHeader(other.Header) : default;
            EncryptedKey = other.EncryptedKey;
            IV = other.IV;
            Ciphertext = other.Ciphertext;
            Tag = other.Tag;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Jwe"/> class.
        /// </summary>
        public Jwe()
        {
        }

        /// <summary>
        /// Parses a <see cref="Jwe"/> value from its string representation.
        /// </summary>
        /// <param name="value">The string representation of an <see cref="Jwe"/> instance.</param>
        /// <returns>The equivalent <see cref="Jwe"/> representation.</returns>
        public static Jwe Parse(string value)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));

            var parts = value.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
            {
                return value.FromJson<Jwe>();
            }
            else
            {
                if (string.IsNullOrEmpty(parts[0])) throw new FormatException("Invalid JWT Compact JSON Serialiazion Format: Missing Protected Header");

                var jwe = new Jwe
                {
                    Header = JweHeader.Parse(parts[0])
                };

                if (parts.Length > 1) jwe.EncryptedKey = parts[1].FromBase64UrlSafe();
                if (parts.Length > 2) jwe.IV = parts[2].FromBase64UrlSafe();
                if (parts.Length > 3) jwe.Ciphertext = parts[3].FromBase64UrlSafe();
                if (parts.Length > 4) jwe.Tag = parts[4].FromBase64UrlSafe();

                return jwe;
            }
        }

        /// <summary>
        /// Returns a JWE JSON serialization string that represents the current JWE object.
        /// </summary>
        /// <returns>A JWE JSON serialization string that represents the current JWE object.</returns>
        public override string ToString() => ToString(false);

        /// <summary>
        /// Returns a JWE JSON serialization string that represents the current JWE object.
        /// </summary>
        /// <param name="compact">
        /// Determines whether this current JWE object should be serialized in the compact or general JSON form.
        /// <para/> true if the current JWE object is serialized in the compact JSON form.
        /// <para/> false if the current JWE object is serialized in the general JSON form.
        /// </param>
        /// <returns>A JWE JSON serialization string that represents the current JWE object.</returns>
        public string ToString(bool compact)
        {
            using (JsConfig.CreateScope("EmitLowercaseUnderscoreNames,ExcludeTypeInfo"))
            {
                if (!compact) return this.ToJson().IndentJson();
                return $"{Header.ToString().ToUtf8Bytes().ToBase64UrlSafe()}.{EncryptedKey.ToBase64UrlSafe()}.{IV.ToBase64UrlSafe()}.{Ciphertext.ToBase64UrlSafe()}.{Tag.ToBase64UrlSafe()}";
            }
        }
    }
}