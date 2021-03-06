﻿using reexmonkey.xmisc.core.authentication.types;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace reexmonkey.xmisc.core.authentication.keys
{
    /// <summary>
    /// JSON Web Key (JWK)
    /// <para /> Represents a cryptographic key.
    /// </summary>
    [DataContract]
    public abstract class Jwk
    {
        /// <summary>
        /// Key Type
        /// <para />Identifies the cryptographic algorithm family used with the key, such as "RSA" or "EC"
        /// </summary>
        [DataMember]
        public Kty Kty { get; set; }

        /// <summary>
        /// Public Key Use
        /// <para />Identifies the intended use of the public key
        /// </summary>
        [DataMember]
        public Use Use { get; set; }

        /// <summary>
        /// Key Operations
        /// <para />Identifies the operation(s) for which the key is intended to be used
        /// </summary>
        [DataMember]
        public KeyOps? KeyOps { get; set; }

        /// <summary>
        /// Algorithm
        /// <para />Identifies the algorithm intended for use with the key
        /// </summary>
        [DataMember]
        public string Alg { get; set; }

        /// <summary>
        /// Key ID
        /// <para />Used to match a specific key.
        /// <para /> This is used, for instance, to choose among a set of keys within a JWK Set during key rollover
        /// </summary>
        [DataMember]
        public string Kid { get; set; }

        /// <summary>
        /// X.509 URL
        /// <para />Resource for an X.509 public key certificate or certificate chain
        /// <para/> The identified resource MUST provide a representation of the certificate or certificate chain that conforms to RFC 528 [RFC5280] in PEM-encoded form, with each certificate delimited as specified in Section 6.1 of RFC 4945 [RFC4945]
        /// <para/> The key in the first certificate MUST match the public key represented by other members of the JWK.
        /// </summary>
        [DataMember]
        public string X5u { get; set; }

        /// <summary>
        /// X.509 Certificate Chain
        /// <para />The key in the first  certificate MUST match the public key represented by other members of the JWK.
        ///<para /> The PKIX certificate containing the key value MUST be the first certificate.This MAY be followed by additional certificates, with each subsequent certificate being the one used to certify the previous one.
        /// </summary>
        [DataMember]
        public List<string> X5c { get; set; }

        /// <summary>
        /// X.509 Certificate SHA-1 Thumbprint
        /// <para/> Base64url-encoded SHA-1 thumbprint (a.k.a. digest) of the DER encoding of an X.509 certificate
        /// </summary>
        [DataMember]
        public string X5t { get; set; }

        /// <summary>
        /// X.509 Certificate SHA-256 Thumbprint
        /// <para/>Base64url-encoded SHA-256 thumbprint (a.k.a. digest) of the DER encoding of an X.509 certificate
        /// </summary>
        [DataMember(Name = "x5t#256")]
        public string X5t256 { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Jwk"/> class.
        /// </summary>
        protected Jwk()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Jwk"/> class from another instance.
        /// </summary>
        /// <param name="other">The instance used for the initialization.</param>
        protected Jwk(Jwk other)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));
            Kty = other.Kty;
            Use = other.Use;
            KeyOps = other.KeyOps;
            Alg = other.Alg;
            Kid = other.Kid;
            X5u = other.X5u;
            X5t = other.X5t;
            X5t256 = other.X5t256;
            if (other.X5c != null && other.X5c.Any())
                X5c = new List<string>(other.X5c);
        }

        /// <summary>
        /// Returns a JSON serialization string that represents the current object.
        /// </summary>
        /// <returns>A JSON serialization string that represents the current object.</returns>
        public override string ToString()
        {
            using (JsConfig.CreateScope("EmitLowercaseUnderscoreNames"))
            {
                return this.ToJson();
            }
        }
    }
}