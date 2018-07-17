using reexmonkey.xmisc.backbone.identifiers.concretes.infrastructure;
using reexmonkey.xmisc.backbone.identifiers.contracts.infrastructure;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.models
{
    /// <summary>
    /// Represents a Formal Public Identifier (FPI) as defined in RFC 3151
    /// </summary>
    public class Fpi : FpiBase, IEquatable<Fpi>
    {
        /// <summary>
        /// Gets the default FPI.
        /// </summary>
        /// <returns>The default unique identifier.</returns>
        public static readonly Fpi NullFpi = new Fpi(ApprovalStatus.None, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

        /// <summary>
        /// Initializes a new instance of the <see cref="Fpi"/> class with details.
        /// </summary>
        /// <param name="status">The approval status of the FPI.</param>
        /// <param name="author">The owner of the FPI</param>
        /// <param name="product">The reference to the standard authority that approved the FPI.</param>
        /// <param name="description">The product class of the FPI</param>
        /// <param name="language">The langauage of the FPI according to ISO 639</param>
        /// <param name="reference">The reference to the standard authority that approved the FPI.</param>
        public Fpi(ApprovalStatus status, string author, string product, string description, string language, string reference = null)
            : base(status, author, product, description, language, reference)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Fpi"/> class.
        /// </summary>
        public Fpi(string value)
        {
            const RegexOptions options = RegexOptions.IgnoreCase
                                         | RegexOptions.CultureInvariant
                                         | RegexOptions.ExplicitCapture
                                         | RegexOptions.Compiled;

            const string pattern = @"^(?<prefix>)//(?<product>)//(?<desc>)//(?<lang>)*$";
            foreach (Match match in Regex.Matches(value, pattern, options))
            {
                if (match.Groups["prefix"].Success)
                {
                    switch (match.Groups["prefix"].Value)
                    {
                        case "+": Status = ApprovalStatus.Informal; break;

                        case "-": Status = ApprovalStatus.None; break;

                        default:
                            Status = ApprovalStatus.Standard;
                            Reference = match.Groups["prefix"].Value;
                            break;
                    }
                }
                if (match.Groups["author"].Success) Author = match.Groups["author"].Value;
                if (match.Groups["product"].Success) Product = match.Groups["product"].Value;
                if (match.Groups["desc"].Success && !string.IsNullOrWhiteSpace(match.Groups["desc"].Value))
                    Description = match.Groups["desc"].Value.TrimStart();
                if (match.Groups["lang"].Success) Language = match.Groups["lang"].Value;
            }
        }

        /// <summary>
        /// Returns a value indicating whether this instance and a specified <see cref="Fpi"/> object represent the same value.
        /// </summary>
        /// <param name="other"> An object to compare to this instance.</param>
        /// <returns>true if <paramref name="other"/> is equal to this instance; otherwise, false.</returns>
        public bool Equals(Fpi other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;
            return Status == other.Status && string.Equals(Author, other.Author, StringComparison.OrdinalIgnoreCase)
                   && string.Equals(Reference, other.Reference, StringComparison.OrdinalIgnoreCase)
                   && string.Equals(Product, other.Product, StringComparison.OrdinalIgnoreCase)
                   && string.Equals(Description, other.Description, StringComparison.OrdinalIgnoreCase)
                   && string.Equals(Language, other.Language, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Returns a value that indicates whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="o">The object to compare with this instance.</param>
        /// <returns>true if o is a <see cref="Fpi"/> that has the same value as this instance; otherwise, false.</returns>
        public override bool Equals(object o)
        {
            if (ReferenceEquals(this, o)) return true;
            if (ReferenceEquals(o, null)) return false;
            return o is Fpi && Equals((Fpi)o);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code for this instance.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int)Status;
                hashCode = (hashCode * 397) ^ (Author != null ? Author.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Reference != null ? Reference.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Product != null ? Product.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Language != null ? Language.GetHashCode() : 0);
                return hashCode;
            }
        }

        /// <summary>
        /// Indicates whether the values of two specified <see cref="Fpi"/> objects are equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns> true if <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, false.</returns>
        public static bool operator ==(Fpi left, Fpi right)
            => EqualityComparer<Fpi>.Default.Equals(left, right);

        /// <summary>
        /// Indicates whether the values of two specified <see cref="Fpi"/> objects are not equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>true if <paramref name="left"/> and <paramref name="right"/> are not equal; otherwise, false.</returns>
        public static bool operator !=(Fpi left, Fpi right)
            => !EqualityComparer<Fpi>.Default.Equals(left, right);
    }
}