using reexmonkey.xmisc.core.authentication.extensions;
using reexmonkey.xmisc.core.authentication.types;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;

namespace reexmonkey.xmisc.core.authentication.tokens
{
    /// <summary>
    /// Represents a JOSE header for a JSON Web Encryption (JWS).
    /// </summary>
    public sealed class JweHeader : JoseHeader
    {
        /// <summary>
        /// Algorithm
        /// <para/> The cryptographic algorithm used to secure or encrypt the JWT.
        /// </summary>
        public JweAlg? Alg { get; set; }

        /// <summary>
        /// Encryption Algorithm
        /// <para/> Identifies the content encryption algorithm used to perform authenticated encryption on the plaintext to produce the ciphertext and the Authentication Tag.
        /// <para /> The encrypted content is not usable if the "enc" value does
        /// not represent a supported algorithm. "enc" values should either be
        /// registered in the IANA "JSON Web Signature and Encryption Algorithms"
        /// registry established by[JWA] or be a value that contains a Collision-Resistant Name
        /// </summary>
        public string Enc { get; set; }

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
    }

    /// <summary>
    /// Represents a JSON Web Encryption.
    /// </summary>
    public sealed class Jwe : Jwt<JweHeader>
    {
        /// <summary>
        /// Encrypted Content Encryption Key value.
        /// <para/> Note that for some algorithms, the JWE Encrypted Key value is specified as being the empty octet sequence
        /// </summary>
        public byte[] EncryptedKey { get; set; }

        /// <summary>
        /// Initialization Vector value used when encrypting the plaintext
        /// </summary>
        public byte[] InitializationVector { get; set; }

        /// <summary>
        /// Ciphertext value resulting from authenticated encryption of the plaintext with Additional Authenticated Data (AAD)
        /// </summary>
        public byte[] Ciphertext { get; set; }

        /// <summary>
        /// Authentication Tag value resulting from authenticated encryption of the plaintext with Additional Authenticated Data (AAD)
        /// </summary>
        public byte[] AuthenticationTag { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Jwe"/> class from another instance.
        /// </summary>
        /// <param name="other">The instance used for the initialization.</param>
        public Jwe(Jwe other) : base(other)
        {
            Header = other.Header != null ? new JweHeader(other.Header) : default;
            EncryptedKey = other.EncryptedKey;
            InitializationVector = other.InitializationVector;
            Ciphertext = other.Ciphertext;
            AuthenticationTag = other.AuthenticationTag;
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

            if (parts.Length == 0) throw new FormatException("Invalid JWT Compact JSON Serialiazion Format: No parts found");

            if (string.IsNullOrEmpty(parts[0])) throw new FormatException("Invalid JWT Compact JSON Serialiazion Format: Missing Protected Header");

            var jwe = new Jwe
            {
                Header = JweHeader.Parse(parts[0])
            };

            if (parts.Length > 1) jwe.EncryptedKey = parts[1].FromBase64UrlSafe();
            if (parts.Length > 2) jwe.InitializationVector = parts[2].FromBase64UrlSafe();
            if (parts.Length > 3) jwe.Ciphertext = parts[3].FromBase64UrlSafe();
            if (parts.Length > 4) jwe.AuthenticationTag = parts[4].FromBase64UrlSafe();

            return jwe;
        }
    }
}