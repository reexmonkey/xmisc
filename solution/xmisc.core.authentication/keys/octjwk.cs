using reexmonkey.xmisc.core.authentication.extensions;
using reexmonkey.xmisc.core.authentication.types;
using reexmonkey.xmisc.core.text.extensions;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace reexmonkey.xmisc.core.authentication.keys
{
    /// <summary>
    /// Represents a symmetric key, whose value is a single octet sequence.
    /// </summary>
    [DataContract]
    public sealed class OctJwk : Jwk
    {
        /// <summary>
        /// Key Value
        /// <para/> Value of the symmetric (or other single-valued) key
        /// </summary>
        [DataMember]
        public List<byte> K { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OctJwk"/> class.
        /// </summary>
        public OctJwk()
        {
            Kty = Kty.EC;
            K = new List<byte>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OctJwk"/> class from another instance.
        /// </summary>
        /// <param name="other">The instance used for the initialization.</param>
        public OctJwk(OctJwk other) : base(other)
        {
            if (other.K != null && other.K.Any())
                K.AddRange(other.K);
        }

        /// <summary>
        /// Parses a <see cref="OctJwk"/> value from its string representation.
        /// </summary>
        /// <param name="value">The string representation of an <see cref="OctJwk"/> instance.</param>
        /// <returns>The equivalent <see cref="OctJwk"/> representation.</returns>
        public static OctJwk Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"{nameof(value)} can neither be null nor empty", nameof(value));
            }
            return Parse(JsonObject.Parse(value));
        }

        /// <summary>
        /// Parses a <see cref="OctJwk"/> value from its <see cref="JsonObject"/> representation.
        /// </summary>
        /// <param name="o">The <see cref="JsonObject"/> representation of an <see cref="OctJwk"/> instance</param>
        /// <returns>The equivalent <see cref="OctJwk"/> representation.</returns>
        public static OctJwk Parse(JsonObject o)
        {
            var jwk = new OctJwk
            {
                Alg = o.Get("alg"),
                Kty = o.Get("kty").AsEnum<Kty>(),
                Use = o.Get("use").AsEnum<Use>(),
                KeyOps = o.Get("key_ops").AsNullableEnum<KeyOps>(),
                Kid = o.Get("kid"),
                X5u = o.Get<string>("x5u"),
                X5t = o.Get<string>("x5t"),
                X5t256 = o.Get<string>("x5t#256")
            };

            var x5cjson = o.Get<string>("x5c");
            if (!string.IsNullOrEmpty(x5cjson))
            {
                jwk.X5c = new List<string>();
                var certs = x5cjson.FromJson<string[]>();
                if (certs.Any()) jwk.X5c.AddRange(certs);
            }

            var k = o.Get<string>("k");
            if (!string.IsNullOrEmpty(k)) jwk.K.AddRange(k.FromBase64UrlSafe());
            return jwk;
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
}