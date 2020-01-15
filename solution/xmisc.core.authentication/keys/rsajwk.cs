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
    /// Represents a cryptographic RSA public key.
    /// </summary>
    public class RsaPublicJwk : Jwk
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RsaPublicJwk"/> class.
        /// </summary>
        public RsaPublicJwk()
        {
            Kty = Kty.RSA;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RsaPublicJwk"/> class from another instance.
        /// </summary>
        /// <param name="other">The instance used for the initialization.</param>
        public RsaPublicJwk(RsaPublicJwk other) : base(other)
        {
        }

        /// <summary>
        /// Modulus
        /// <para/> Modulus value for the RSA public key.
        /// </summary>
        public string N { get; set; }

        /// <summary>
        /// Exponent
        /// <para /> Exponent value for the RSA public key.
        /// </summary>
        public string E { get; set; }

        /// <summary>
        /// Parses a <see cref="RsaPublicJwk"/> value from its string representation.
        /// </summary>
        /// <param name="value">The string representation of an RSA public key.</param>
        /// <returns>The equivalent <see cref="RsaPublicJwk"/> representation.</returns>
        public static RsaPublicJwk Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("message", nameof(value));
            }
            return Parse(JsonObject.Parse(value));
        }

        /// <summary>
        /// Parses a <see cref="RsaPublicJwk"/> value from its <see cref="JsonObject"/> representation.
        /// </summary>
        /// <param name="o">The <see cref="JsonObject"/> representation of an <see cref="RsaPublicJwk"/> instance</param>
        /// <returns>The equivalent <see cref="RsaPublicJwk"/> representation.</returns>
        public static RsaPublicJwk Parse(JsonObject o)
        {
            var jwk = new RsaPublicJwk
            {
                Alg = o.Get("alg"),
                Kty = o.Get("kty").AsEnum<Kty>(),
                Use = o.Get("use").AsEnum<Use>(),
                KeyOps = o.Get("key_ops").AsNullableEnum<KeyOps>(),
                Kid = o.Get("kid"),
                X5u = o.Get<string>("x5u"),
                X5t = o.Get<string>("x5t"),
                X5t256 = o.Get<string>("x5t#256"),
                N = o.Get<string>("n"),
                E = o.Get<string>("e")
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
    }

    /// <summary>
    /// Other Primes
    /// </summary>
    public sealed class OthElement
    {
        /// <summary>
        /// Prime factor
        /// <para/> Value of a subsequent prime factor
        /// </summary>
        public string R { get; set; }

        /// <summary>
        /// Factor CRT exponent
        /// <para/> CRT exponent of the corresponding prime factor
        /// </summary>
        public string D { get; set; }

        /// <summary>
        /// Factor CRT Coefficient
        /// <para/> CRT coefficient of the corresponding prime factor
        /// </summary>
        public string T { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OthElement"/> class.
        /// </summary>
        public OthElement()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OthElement"/> class from another instance.
        /// </summary>
        /// <param name="other">The instance used for the initialization.</param>
        public OthElement(OthElement other)
        {
            R = other.R;
            D = other.D;
            T = other.T;
        }

        /// <summary>
        /// Parses a <see cref="OthElement"/> value from its string representation.
        /// </summary>
        /// <param name="value">The string representation of an <see cref="OthElement"/> instance.</param>
        /// <returns>The equivalent <see cref="OthElement"/> representation.</returns>
        public static OthElement Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("message", nameof(value));
            }
            return Parse(JsonObject.Parse(value));
        }

        /// <summary>
        /// Parses a <see cref="OthElement"/> value from its <see cref="JsonObject"/> representation.
        /// </summary>
        /// <param name="o">The <see cref="JsonObject"/> representation of an <see cref="OthElement"/> instance</param>
        /// <returns>The equivalent <see cref="OthElement"/> representation.</returns>
        public static OthElement Parse(JsonObject o)
        {
            return new OthElement
            {
                R = o.Get<string>("r"),
                D = o.Get<string>("d"),
                T = o.Get<string>("t")
            };
        }
    }

    /// <summary>
    /// Represents a cryptographic RSA private key.
    /// </summary>
    public class RsaPrivateJwk : RsaPublicJwk
    {
        /// <summary>
        /// Private Exponent
        /// <para/> Private exponent value for the RSA private key
        /// </summary>
        public string D { get; set; }

        /// <summary>
        /// First Prime Factor
        /// </summary>
        public string P { get; set; }

        /// <summary>
        /// Second Prime Factor
        /// </summary>
        public string Q { get; set; }

        /// <summary>
        /// First Factor CRT Exponent
        /// <para/> Chinese Remainder Theorem (CRT) exponent of the first factor
        /// </summary>
        public string Dp { get; set; }

        /// <summary>
        /// Second Factor CRT Exponent
        /// <para/> Chinese Remainder Theorem (CRT) exponent of the second factor
        /// </summary>
        public string Dq { get; set; }

        /// <summary>
        /// First CRT Coefficient
        /// <para/> CRT coefficient of the second factor.
        /// </summary>
        public string Qi { get; set; }

        /// <summary>
        /// Other Primes Info
        /// List of information about any third and subsequent primes
        /// </summary>
        public List<OthElement> Oth { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RsaPrivateJwk"/> class.
        /// </summary>
        public RsaPrivateJwk()
        {
            Oth = new List<OthElement>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RsaPrivateJwk"/> class from another instance.
        /// </summary>
        /// <param name="other">The instance used for the initialization.</param>
        public RsaPrivateJwk(RsaPrivateJwk other) : base(other)
        {
            Oth = new List<OthElement>();

            N = other.N;
            E = other.E;
            D = other.D;
            P = other.P;
            Q = other.Q;
            Dp = other.Dp;
            Dq = other.Dq;
            Qi = other.Qi;

            if (other.Oth != null && other.Oth.Any())
                Oth.AddRange(other.Oth);
        }

        /// <summary>
        /// Parses a <see cref="RsaPrivateJwk"/> value from its string representation.
        /// </summary>
        /// <param name="value">The string representation of an <see cref="RsaPrivateJwk"/> instance.</param>
        /// <returns>The equivalent <see cref="RsaPrivateJwk"/> representation.</returns>
        public static new RsaPrivateJwk Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("message", nameof(value));
            }
            return Parse(JsonObject.Parse(value));
        }

        /// <summary>
        /// Parses a <see cref="RsaPrivateJwk"/> value from its <see cref="JsonObject"/> representation.
        /// </summary>
        /// <param name="o">The <see cref="JsonObject"/> representation of an <see cref="RsaPrivateJwk"/> instance</param>
        /// <returns>The equivalent <see cref="RsaPrivateJwk"/> representation.</returns>
        public static new RsaPrivateJwk Parse(JsonObject o)
        {
            var jwk = new RsaPrivateJwk
            {
                Alg = o.Get("alg"),
                Kty = o.Get("kty").AsEnum<Kty>(),
                Use = o.Get("use").AsEnum<Use>(),
                KeyOps = o.Get("key_ops").AsNullableEnum<KeyOps>(),
                Kid = o.Get("kid"),
                X5u = o.Get<string>("x5u"),
                X5t = o.Get<string>("x5t"),
                X5t256 = o.Get<string>("x5t#256"),
                N = o.Get<string>("n"),
                E = o.Get<string>("e"),
                D = o.Get<string>("d"),
                P = o.Get<string>("p"),
                Q = o.Get<string>("q"),
                Dp = o.Get<string>("dp"),
                Dq = o.Get<string>("dq"),
                Qi = o.Get<string>("qi"),
            };

            var x5cjson = o.Get<string>("x5c");
            if (!string.IsNullOrEmpty(x5cjson))
            {
                var certs = x5cjson.FromJson<string[]>();
                foreach (var cert in certs)
                    jwk.X5c.Add(cert);
            }

            var othjson = o.Get<string>("oth");
            if (!string.IsNullOrEmpty(othjson))
            {
                var othmaps = JsonObject.ParseArray(othjson);
                foreach (var othmap in othmaps)
                {
                    var info = OthElement.Parse(othmap);
                    if (info != null) jwk.Oth.Add(info);
                }
            }

            return jwk;
        }
    }
}