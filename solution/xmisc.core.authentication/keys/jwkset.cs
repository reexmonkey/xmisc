using reexmonkey.xmisc.core.authentication.extensions;
using reexmonkey.xmisc.core.authentication.types;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;

namespace reexmonkey.xmisc.core.authentication.keys
{
    /// <summary>
    /// Represents a JSON Web Key Set
    /// </summary>
    public sealed class JwkSet
    {
        /// <summary>
        /// List of JWKs belonging to this set.
        /// </summary>
        public List<Jwk> Keys { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="JwkSet"/> class.
        /// </summary>
        public JwkSet()
        {
            Keys = new List<Jwk>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JwkSet"/> class with a sequence of JWKs.
        /// </summary>
        /// <param name="keys">The sequence of JWKs used for the initialization.</param>
        public JwkSet(IEnumerable<Jwk> keys)
        {
            if (keys is null) throw new ArgumentNullException(nameof(keys));
            Keys = new List<Jwk>(keys);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JwkSet"/> class from another instance.
        /// </summary>
        /// <param name="other">The instance used for the initialization.</param>
        public JwkSet(JwkSet other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (other.Keys != null && other.Keys.Any())
                Keys = new List<Jwk>(other.Keys);
        }

        /// <summary>
        /// Parses a <see cref="JwkSet"/> value from its string representation.
        /// </summary>
        /// <param name="value">The string representation of an <see cref="JwkSet"/> instance.</param>
        /// <returns>The equivalent <see cref="JwkSet"/> representation.</returns>
        public static JwkSet Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("message", nameof(value));
            }

            var set = new JwkSet();
            var map = JsonObject.Parse(value);

            if (map == null) return default;

            if (map.TryGetValue("keys", out value))
            {
                var kmaps = JsonObject.ParseArray(value);
                foreach (var kmap in kmaps)
                {
                    var kty = kmap.Get("kty").AsEnum<Kty>();

                    Jwk jwk = null;

                    if (kty == Kty.EC) jwk = ParseEcJwk(kmap);
                    if (kty == Kty.RSA) jwk = ParseRsaJwk(kmap);
                    if (kty == Kty.oct) jwk = ParseOctJwk(kmap);
                    if (jwk != null) set.Keys.Add(jwk);
                }
            }
            return set;
        }

        private static Jwk ParseOctJwk(JsonObject map)
        {
            if (map.TryGetValue("k", out string k)
                && !string.IsNullOrEmpty(k)
                )
                return OctJwk.Parse(map);

            return default;
        }

        private static Jwk ParseEcJwk(JsonObject map)
        {
            if (map.TryGetValue("d", out string d)
                && !string.IsNullOrEmpty(d)
                )
                return RsaPrivateJwk.Parse(map);

            if (map.TryGetValue("crv", out string crv)
                && map.TryGetValue("x", out string x)
                && map.TryGetValue("y", out string y)
                && !string.IsNullOrEmpty(crv)
                && !string.IsNullOrEmpty(x)
                && !string.IsNullOrEmpty(y)
                )
                return RsaPublicJwk.Parse(map);

            return default;
        }

        private static Jwk ParseRsaJwk(JsonObject map)
        {
            if (map.TryGetValue("d", out string d)
                && map.TryGetValue("p", out string p)
                && map.TryGetValue("dp", out string dp)
                && map.TryGetValue("dq", out string dq)
                && !string.IsNullOrEmpty(d)
                && !string.IsNullOrEmpty(p)
                && !string.IsNullOrEmpty(dp)
                && !string.IsNullOrEmpty(dq)
                )
                return RsaPrivateJwk.Parse(map);

            if (map.TryGetValue("n", out string n)
                && map.TryGetValue("e", out string e)
                && !string.IsNullOrEmpty(n)
                && !string.IsNullOrEmpty(e))
                return RsaPublicJwk.Parse(map);

            return default;
        }
    }
}