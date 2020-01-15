using reexmonkey.xmisc.core.authentication.extensions;
using reexmonkey.xmisc.core.authentication.types;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.Collections.Generic;

namespace reexmonkey.xmisc.core.authentication.tokens
{
    /// <summary>
    /// Represents a JOSE header for a JSON Web Signature (JWS).
    /// </summary>
    public sealed class JwsHeader : JoseHeader
    {
        /// <summary>
        /// Algorithm
        /// <para/> The cryptographic algorithm used to secure or encrypt the JWT.
        /// </summary>
        public JwsAlg? Alg { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="JwsHeader"/> class.
        /// </summary>
        public JwsHeader()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JwsHeader"/> class from another instance.
        /// </summary>
        /// <param name="other">The instance used for the initialization.</param>
        public JwsHeader(JwsHeader other) : base(other)
        {
            Alg = other.Alg;
        }

        /// <summary>
        /// Parses a <see cref="JwsHeader"/> value from its string representation.
        /// </summary>
        /// <param name="value">The string representation of an <see cref="JwsHeader"/> instance.</param>
        /// <returns>The equivalent <see cref="JwsHeader"/> representation.</returns>
        public static JwsHeader Parse(string value)
        {
            var json = value.FromBase64UrlSafe().FromUtf8Bytes();
            var o = JsonObject.Parse(json);
            var header = new JwsHeader
            {
                Alg = o.Get("alg", "none").AsNullableEnum<JwsAlg>(),
                Jku = o.Get<string>("jku"),
                Jwk = o.Get<string>("jwk"),
                Kid = o.Get<string>("kid"),
                X5u = o.Get<string>("x5u"),
                X5t = o.Get<string>("x5t"),
                X5t256 = o.Get<string>("x5t#256"),
                Typ = o.Get<string>("typ"),
                Cty = o.Get<string>("cty"),
                Crit = o.Get<string>("crit")
            };

            var x5cjson = o.Get<string>("x5c");
            if (!string.IsNullOrEmpty(x5cjson))
            {
                var certs = x5cjson.FromJson<string[]>();
                foreach (var cert in certs)
                    header.X5c.Add(cert);
            }

            return header;
        }
    }

    /// <summary>
    /// Repesents the content (payload) of a secure or encrypted JWT.
    /// </summary>
    public class Payload
    {
        /// <summary>
        /// Issuer Claim
        /// <para/> This claim identifies the principal that issued the JWT.
        /// </summary>
        public static readonly string Iss = "iss";

        /// <summary>
        /// Subject Claim
        /// <para/>This claim identifies the principal that is the subject of the JWT.
        /// </summary>
        public static readonly string Sub = "sub";

        /// <summary>
        /// Audience Claim
        /// <para/> This claim identifies the recipients that the JWT is intended for.
        /// </summary>
        public static readonly string Aud = "aud";

        /// <summary>
        /// Expiration Time Claim
        /// <para/> claim identifies the expiration time on or after which the JWT MUST NOT be accepted for processing.
        /// </summary>
        public static readonly string Exp = "exp";

        /// <summary>
        /// Not Before Claim
        /// <para/> This claim identifies the time before which the JWT MUST NOT be accepted for processing.
        /// </summary>
        public static readonly string Nbf = "nbf";

        /// <summary>
        /// Issued At
        /// <para/> This claim identifies the time at which the JWT was issued.
        /// </summary>
        public static readonly string Iat = "iat";

        /// <summary>
        /// JSON Web Token (JWT) ID
        /// <para/> This claim provides a unique identifier for the JWT.
        /// </summary>
        public static readonly string Jti = "jti";

        /// <summary>
        /// Gets the claims of the payload.
        /// <para /> To access a standard claim, please use the static properties.
        /// </summary>
        public Dictionary<string, string> Claims { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Payload"/> class.
        /// </summary>
        public Payload()
        {
            Claims = new Dictionary<string, string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Payload"/> class from another instance.
        /// </summary>
        /// <param name="other">The instance used for the initialization.</param>
        public Payload(Payload other)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));
            Claims = new Dictionary<string, string>(other.Claims);
        }

        /// <summary>
        /// Parses a <see cref="Payload"/> value from its string representation.
        /// </summary>
        /// <param name="value">The string representation of an <see cref="Payload"/> instance.</param>
        /// <returns>The equivalent <see cref="Payload"/> representation.</returns>
        public static Payload Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"{nameof(value)} can neither be null nor empty.", nameof(value));
            }
            var content = value.FromBase64UrlSafe().FromUtf8Bytes();
            var o = JsonObject.Parse(content);

            return new Payload
            {
                Claims = new Dictionary<string, string>(o.ToStringDictionary())
            };
        }
    }

    /// <summary>
    /// Represents a JSON Web Signature (JWS).
    /// </summary>
    public class Jws : Jwt<JwsHeader>
    {
        /// <summary>
        /// Payload of the JWS.
        /// </summary>
        public Payload Payload { get; set; }

        /// <summary>
        /// Signature of the JWS.
        /// </summary>
        public byte[] Signature { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Jws"/> class.
        /// </summary>
        public Jws()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Jws"/> class from another instance.
        /// </summary>
        /// <param name="other">The instance used for the initialization.</param>
        public Jws(Jws other) : base(other)
        {
            Header = other.Header != null ? new JwsHeader(other.Header) : default;
            Payload = other.Payload != null ? new Payload(other.Payload) : default;
            Signature = other.Signature;
        }

        /// <summary>
        /// Parses a <see cref="Jws"/> value from its string representation.
        /// </summary>
        /// <param name="value">The string representation of an <see cref="Jws"/> instance.</param>
        /// <returns>The equivalent <see cref="Jws"/> representation.</returns>
        public static Jws Parse(string value)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            var parts = value.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0) throw new FormatException("Invalid JWT Compact JSON Serialiazion Format: No parts found");

            if (string.IsNullOrEmpty(parts[0])) throw new FormatException("Invalid JWT Compact JSON Serialiazion Format: Missing Protected Header");

            var jws = new Jws();

            jws.Header = JwsHeader.Parse(parts[0]);

            if (parts.Length > 1) jws.Payload = Payload.Parse(parts[1]);

            if (parts.Length > 2) jws.Signature = parts[2].FromBase64UrlSafe();

            return jws;
        }
    }
}