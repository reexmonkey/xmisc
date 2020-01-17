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
                header.X5c = new List<string>();
                var certs = x5cjson.FromJson<string[]>();
                if(certs.Any()) header.X5c.AddRange(certs);
            }

            return header;
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
        public byte[] Payload { get; set; }

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
            Payload = other.Payload;
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

            var jws = new Jws
            {
                Header = JwsHeader.Parse(parts[0])
            };

            if (parts.Length > 1) jws.Payload = parts[1].FromBase64UrlSafe();

            if (parts.Length > 2) jws.Signature = parts[2].FromBase64UrlSafe();

            return jws;
        }
    }
}