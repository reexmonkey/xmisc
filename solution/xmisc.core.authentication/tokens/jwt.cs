using ServiceStack;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace reexmonkey.xmisc.core.authentication.tokens
{
    /// <summary>
    /// Specifies a JSON Object Signing and Encryption (JOSE) Header.
    /// </summary>
    [DataContract]
    public abstract class JoseHeader
    {
        /// <summary>
        /// JWK Set URL
        /// <para/> URI that refers to a resource for a set of JSON-encoded public keys, one of which corresponds to the key used to digitally sign the JWT.
        /// <para /> Use of this Header Parameter is OPTIONAL.
        /// </summary>
        [DataMember(Order = 1)]
        public string Jku { get; set; }

        /// <summary>
        /// JSON Web Key
        /// <para/>The public key that corresponds to the key used to digitally sign or encrypt the JWT.
        /// <para /> Use of this Header Parameter is OPTIONAL.
        /// </summary>
        [DataMember(Order = 2)]
        public string Jwk { get; set; }

        /// <summary>
        /// Key ID
        /// <para/> Hint indicating which key was used to secure or encrypt the JWT.
        /// <para /> Use of this Header Parameter is OPTIONAL.
        /// </summary>
        [DataMember(Order = 3)]
        public string Kid { get; set; }

        /// <summary>
        /// URI that refers to a resource for the X.509 public key certificate or certificate chain corresponding to the key used to digitally sign or secure the JWT.
        /// <para/> The identified resource MUST provide a representation of the certificate or certificate chain that conforms to RFC 5280 [RFC5280] in PEM-encoded form, with each certificate delimited as specified in Section 6.1 of RFC 4945 [RFC4945].
        /// <para /> The certificate containing the public key corresponding to the key used to digitally sign or secure the JWT MUST be the first certificate.
        /// <para /> Use of this Header Parameter is OPTIONAL.
        /// </summary>
        [DataMember(Order = 4)]
        public string X5u { get; set; }

        /// <summary>
        /// X.509 Certificate Chain
        /// <para/> X.509 public key certificate or certificate chain corresponding to the key used to digitally sign or secure the JWT.
        /// <para/> Each string in the array is a  base64-encoded(-- not base64url-encoded) DER  PKIX certificate value.
        /// <para /> The certificate containing the public key corresponding to the key used to digitally sign or secure the JWT MUST be the first certificate.
        /// <para />This MAY be followed by additional certificates, with each subsequent certificate being the one used to certify the previous one.
        /// <para /> Use of this Header Parameter is OPTIONAL.
        /// </summary>
        [DataMember(Order = 5)]
        public List<string> X5c { get; set; }

        /// <summary>
        /// X.509 Certificate SHA-1 Thumbprint
        /// <para/>Base64 url-encoded SHA-1 thumbprint(a.k.a.digest) of the DER encoding of the X.509 certificate corresponding to the key used to digitally sign or secure the JWT.
        /// <para /> Use of this Header Parameter is OPTIONAL.
        /// </summary>
        [DataMember(Order = 6)]
        public string X5t { get; set; }

        /// <summary>
        /// X.509 X.509 Certificate SHA-256 Thumbprint
        /// <para/>Base64 url-encoded SHA-256 thumbprint(a.k.a.digest) of the DER encoding of the X.509 certificate corresponding to the key used to digitally sign or secure the JWT.
        /// <para /> Use of this Header Parameter is OPTIONAL.
        /// </summary>
        [DataMember(Order = 7, Name = "x5t#256")]
        public string X5t256 { get; set; }

        /// <summary>
        /// Type of token
        /// <para /> Describes the media type [IANA.MediaTypes] of the complete JWT.
        /// </summary>
        [DataMember(Order = 8)]
        public string Typ { get; set; }

        /// <summary>
        /// Content Type
        /// <para /> DEscribes the media type [IANA.MediaTypes] of the secured or encrypted content (payload).
        /// <para /> Use of this Header Parameter is OPTIONAL.
        /// </summary>
        [DataMember(Order = 9)]
        public string Cty { get; set; }

        /// <summary>
        /// Critical
        /// <para /> Indicates that extensions to this specification and/or[JWA] are being used that MUST be understood and processed.
        /// <para /> Use of this Header Parameter is OPTIONAL.
        /// </summary>
        [DataMember(Order = 10)]
        public string Crit { get; set; }

        /// <summary>
        /// Compression Algorithm
        /// <para/>The "zip" (compression algorithm) applied to the plaintext before encryption, if any.
        /// <para /> Use of this Header Parameter is OPTIONAL.
        /// </summary>
        [DataMember(Order = 11)]
        public string Zip { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="JoseHeader"/> class.
        /// </summary>
        protected JoseHeader()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JoseHeader"/> class from another instance.
        /// </summary>
        /// <param name="other">The instance used for the initialization.</param>
        protected JoseHeader(JoseHeader other) : this()
        {
            if (other is null) throw new ArgumentNullException(nameof(other));
            Jku = other.Jku;
            Jwk = other.Jwk;
            Kid = other.Kid;
            X5u = other.X5u;
            X5t = other.X5t;
            X5t256 = other.X5t256;
            Typ = other.Typ;
            Cty = other.Cty;
            Crit = other.Crit;
            Zip = other.Zip;
            if (other.X5c != null && other.X5c.Any())
                X5c = new List<string>(other.X5c);
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
    /// Represents a base class for all JSON Web Token (JWTs) for a specified JOSE header.
    /// </summary>
    /// <typeparam name="THeader">The type of header used by this JWT</typeparam>
    [DataContract]
    public abstract class Jwt<THeader>
        where THeader : JoseHeader
    {
        /// <summary>
        /// The header part of the JWT
        /// </summary>
        [DataMember(Order = 14)]
        public THeader Header { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Jwt{THeader}"/> class.
        /// </summary>
        protected Jwt()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Jwt{THeader}"/> class from another instance.
        /// </summary>
        /// <param name="other">The instance used for the initialization.</param>
        protected Jwt(Jwt<THeader> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            Header = other.Header;
        }
    }
}