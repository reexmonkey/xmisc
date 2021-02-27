using reexmonkey.xmisc.core.authentication.extensions;
using reexmonkey.xmisc.core.authentication.types;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;

namespace reexmonkey.xmisc.core.authentication.keys
{
    /// <summary>
    /// Represents a public Elliptic Curve (Digital Signature Standard) key.
    /// </summary>
    public class EcPublicJwk : Jwk
    {
        /// <summary>
        /// Identifies the cryptographic curve used with the key
        /// </summary>
        public string Crv { get; set; }

        /// <summary>
        /// X coordinate for the Elliptic Curve point
        /// </summary>
        public string X { get; set; }

        /// <summary>
        /// Y coordinate for the Elliptic Curve point
        /// </summary>
        public string Y { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OctJwk"/> class.
        /// </summary>
        public EcPublicJwk()
        {
            Kty = Kty.EC;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EcPublicJwk"/> class from another instance.
        /// </summary>
        /// <param name="other">The instance used for the initialization.</param>
        public EcPublicJwk(EcPublicJwk other) : base(other)
        {
        }

        /// <summary>
        /// Parses a <see cref="EcPublicJwk"/> value from its string representation.
        /// </summary>
        /// <param name="value">The string representation of an <see cref="EcPublicJwk"/> instance.</param>
        /// <returns>The equivalent <see cref="EcPublicJwk"/> representation.</returns>
        public static EcPublicJwk Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"{nameof(value)} can neither be null nor empty", nameof(value));
            }
            return Parse(JsonObject.Parse(value));
        }

        /// <summary>
        /// Parses a <see cref="EcPublicJwk"/> value from its <see cref="JsonObject"/> representation.
        /// </summary>
        /// <param name="o">The <see cref="JsonObject"/> representation of an <see cref="EcPublicJwk"/> instance</param>
        /// <returns>The equivalent <see cref="EcPublicJwk"/> representation.</returns>
        public static EcPublicJwk Parse(JsonObject o)
        {
            var jwk = new EcPublicJwk
            {
                Alg = o.Get("alg"),
                Kty = o.Get("kty").AsEnum<Kty>(),
                Use = o.Get("use").AsEnum<Use>(),
                KeyOps = o.Get("key_ops").AsNullableEnum<KeyOps>(),
                Kid = o.Get("kid"),
                X5u = o.Get<string>("x5u"),
                X5t = o.Get<string>("x5t"),
                X5t256 = o.Get<string>("x5t#256"),
                Crv = o.Get<string>("crv"),
                X = o.Get<string>("x"),
                Y = o.Get<string>("y")
            };

            var x5cjson = o.Get<string>("x5c");
            if (!string.IsNullOrEmpty(x5cjson))
            {
                jwk.X5c = new List<string>();
                var certs = x5cjson.FromJson<string[]>();
                if (certs.Any()) jwk.X5c.AddRange(certs);
            }

            return jwk;
        }

        /// <summary>
        /// Returns a JWS JSON serialization string that represents the current object.
        /// </summary>
        /// <returns>A JWS JSON serialization string that represents the current object.</returns>
        public virtual string ToJsonString() => this.ToJson();
    }

    /// <summary>
    /// Represents a private Elliptic Curve (Digital Signature Standard) key.
    /// </summary>
    public class EcPrivateJwk : EcPublicJwk
    {
        /// <summary>
        /// X coordinate for the Elliptic Curve point
        /// </summary>
        public string D { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EcPrivateJwk"/> class.
        /// </summary>
        public EcPrivateJwk()
        {
            Kty = Kty.EC;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EcPrivateJwk"/> class from another instance.
        /// </summary>
        /// <param name="other">The instance used for the initialization.</param>
        public EcPrivateJwk(EcPrivateJwk other) : base(other)
        {
        }

        /// <summary>
        /// Parses a <see cref="EcPrivateJwk"/> value from its string representation.
        /// </summary>
        /// <param name="value">The string representation of an <see cref="EcPrivateJwk"/> instance.</param>
        /// <returns>The equivalent <see cref="EcPrivateJwk"/> representation.</returns>
        public static new EcPrivateJwk Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"{nameof(value)} can neither be null nor empty", nameof(value));
            }

            return Parse(JsonObject.Parse(value));
        }

        /// <summary>
        /// Parses a <see cref="EcPrivateJwk"/> value from its <see cref="JsonObject"/> representation.
        /// </summary>
        /// <param name="o">The <see cref="JsonObject"/> representation of an <see cref="EcPrivateJwk"/> instance</param>
        /// <returns>The equivalent <see cref="EcPrivateJwk"/> representation.</returns>
        public static new EcPrivateJwk Parse(JsonObject o)
        {
            var jwk = new EcPrivateJwk
            {
                Alg = o.Get("alg"),
                Kty = o.Get("kty").AsEnum<Kty>(),
                Use = o.Get("use").AsEnum<Use>(),
                KeyOps = o.Get("key_ops").AsNullableEnum<KeyOps>(),
                Kid = o.Get("kid"),
                X5u = o.Get<string>("x5u"),
                X5t = o.Get<string>("x5t"),
                X5t256 = o.Get<string>("x5t#256"),
                Crv = o.Get<string>("crv"),
                X = o.Get<string>("x"),
                Y = o.Get<string>("y"),
                D = o.Get<string>("d"),
            };

            var x5cjson = o.Get<string>("x5c");
            if (!string.IsNullOrEmpty(x5cjson))
            {
                var certs = x5cjson.FromJson<string[]>();
                foreach (var cert in certs)
                    jwk.X5c.Add(cert);
            }

            return jwk;
        }

        /// <summary>
        /// Returns a JWS JSON serialization string that represents the current object.
        /// </summary>
        /// <returns>A JWS JSON serialization string that represents the current object.</returns>
        public override string ToJsonString() => this.ToJson();
    }
}