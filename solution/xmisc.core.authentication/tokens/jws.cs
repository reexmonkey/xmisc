using reexmonkey.xmisc.core.authentication.extensions;
using reexmonkey.xmisc.core.authentication.types;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace reexmonkey.xmisc.core.authentication.tokens
{
    /// <summary>
    /// Represents a JOSE header for a JSON Web Signature (JWS).
    /// </summary>
    [DataContract]
    public sealed class JwsHeader : JoseHeader
    {
        /// <summary>
        /// Algorithm
        /// <para/> The cryptographic algorithm used to secure or encrypt the JWT.
        /// </summary>
        [DataMember(Order = 12)]
        public JwsAlg? Alg { get; set; }

        /// <summary>
        /// Protected Header
        /// <para/> JSON object that contains the Header Parameters that are integrity protected by the JWS Signature digital signature or MAC operation.
        /// For the JWS Compact Serialization, this comprises the entire JOSE Header.
        /// For the JWS JSON Serialization, this is one component of the JOSE Header.
        /// </summary>
        [DataMember(Order = 13)]
        public string Protected { get; set; }

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
            var enc = new UTF8Encoding(false, true);
            var json = enc.GetString(value.FromBase64UrlSafe()); //value.FromBase64UrlSafe().FromUtf8Bytes();
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
                Crit = o.Get<string>("crit"),
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
    /// Represents a JSON Web Signature (JWS).
    /// </summary>
    [DataContract]
    public class Jws : Jwt<JwsHeader>
    {
        /// <summary>
        /// Payload of the JWS.
        /// </summary>
        [DataMember(Order = 0)]
        public byte[] Payload { get; set; }

        /// <summary>
        /// Signature of the JWS.
        /// </summary>
        [DataMember(Order = 15)]
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

            if (parts.Length == 0)
            {
                return value.FromJson<Jws>();
            }
            else
            {
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

        /// <summary>
        /// Returns a JWS JSON serialization string that represents the current JWS object.
        /// </summary>
        /// <returns>A JWS JSON serialization string that represents the current JWS object.</returns>
        public override string ToString() => ToString(false);

        /// <summary>
        /// Returns a JWS JSON serialization string that represents the current JWS object.
        /// </summary>
        /// <param name="compact">
        /// Determines whether this current JWS object should be serialized in the compact or general JSON form.
        /// <para/> true if the current JWS object is serialized in the compact JSON form.
        /// <para/> false if the current JWS object is serialized in the general JSON form.
        /// </param>
        /// <returns>A JWS JSON serialization string that represents the current JWS object.</returns>
        public string ToString(bool compact)
        {
            using (JsConfig.CreateScope("EmitLowercaseUnderscoreNames,ExcludeTypeInfo"))
            {
                if (!compact) return this.ToJson().IndentJson();
                return $"{Header.ToString().ToUtf8Bytes().ToBase64UrlSafe()}.{Payload.ToBase64UrlSafe()}.{Signature.ToBase64UrlSafe()}";
            }
        }
    }
}