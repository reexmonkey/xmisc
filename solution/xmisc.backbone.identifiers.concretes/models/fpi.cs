using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.models
{
    /// <summary>
    /// Represents a Formal Public Identifier (Fpi) as defined in RFC 3151
    /// </summary>
    public sealed class Fpi : FpiBase, IEquatable<Fpi>
    {
        /// <summary>
        /// Gets the default Fpi.
        /// </summary>
        /// <returns>The default unique identifier.</returns>
        public static readonly Fpi Empty = new Fpi(ApprovalStatus.None, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

        /// <summary>
        /// Initializes a new instance of the <see cref="Fpi"/> class with details.
        /// </summary>
        /// <param name="status">The approval status of the Fpi.</param>
        /// <param name="author">The owner of the Fpi</param>
        /// <param name="product">The reference to the standard authority that approved the Fpi.</param>
        /// <param name="description">The product class of the Fpi</param>
        /// <param name="language">The langauage of the Fpi according to ISO 639</param>
        /// <param name="reference">The reference to the standard authority that approved the Fpi.</param>
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
        /// Returns a value that indicates whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">The object to compare with this instance.</param>
        /// <returns>true if o is a <see cref="Fpi"/> that has the same value as this instance; otherwise, false.</returns>
        public override bool Equals(object obj) => Equals(obj as Fpi);

        /// <summary>
        /// Returns a value indicating whether this instance and a specified <see cref="Fpi"/> object represent the same value.
        /// </summary>
        /// <param name="other"> An object to compare to this instance.</param>
        /// <returns>true if <paramref name="other"/> is equal to this instance; otherwise, false.</returns>
        public bool Equals(Fpi other)
        {
            return !(other is null) &&
                   Author == other.Author &&
                   Reference == other.Reference &&
                   Product == other.Product &&
                   Description == other.Description &&
                   Language == other.Language;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code for this instance.</returns>
        public override int GetHashCode()
        {
            int hashCode = 1285179832;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Author);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Reference);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Product);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Language);
            return hashCode;
        }

        /// <summary>
        /// Indicates whether the values of two specified <see cref="Fpi"/> objects are equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns> true if <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, false.</returns>
        public static bool operator ==(Fpi left, Fpi right)
        {
            return EqualityComparer<Fpi>.Default.Equals(left, right);
        }

        /// <summary>
        /// Indicates whether the values of two specified <see cref="Fpi"/> objects are not equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>true if <paramref name="left"/> and <paramref name="right"/> are not equal; otherwise, false.</returns>
        public static bool operator !=(Fpi left, Fpi right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Returns a string that represents this instance of the <see cref="Fpi"/> class.
        /// </summary>
        /// <returns>The equivalent string representation of this instance.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            switch (Status)
            {
                case ApprovalStatus.Standard:
                    sb.Append(Reference);
                    break;

                case ApprovalStatus.None:
                    sb.Append("-");
                    break;

                default:
                    sb.Append("+");
                    break;
            }
            sb.AppendFormat("//{0}", Author);
            sb.AppendFormat("//{0}", Product);
            if (!string.IsNullOrEmpty(Description)) sb.AppendFormat(" {0}", Description);
            sb.AppendFormat("//{0}", Language);
            return sb.ToString();
        }
    }

    /// <summary>
    /// Represents a base class for a Formal Public Identifier (Fpi) as defined in RFC 3151
    /// </summary>
    public abstract class FpiBase
    {
        /// <summary>
        /// Gets the approval status of the Fpi.
        /// </summary>
        public ApprovalStatus Status { get; protected set; }

        /// <summary>
        /// Gets the owner of the Fpi
        /// </summary>
        public string Author { get; protected set; }

        /// <summary>
        /// Gets the reference to the standard authority that approved the Fpi.
        /// </summary>
        public string Reference { get; protected set; }

        /// <summary>
        /// Gets or sets the product class of the Fpi
        /// </summary>
        public string Product { get; protected set; }

        /// <summary>
        /// Gets or sets the description of the Fpi
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// Gets or sets the langauage of the Fpi according to ISO 639
        /// </summary>
        public string Language { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FpiBase"/> class.
        /// </summary>
        protected FpiBase()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FpiBase"/> class with details.
        /// </summary>
        /// <param name="status">The approval status of the Fpi.</param>
        /// <param name="author">The owner of the Fpi</param>
        /// <param name="product">The reference to the standard authority that approved the Fpi.</param>
        /// <param name="description">The product class of the Fpi</param>
        /// <param name="language">The langauage of the Fpi according to ISO 639</param>
        /// <param name="reference">The reference to the standard authority that approved the Fpi.</param>
        protected FpiBase(ApprovalStatus status, string author, string product, string description, string language, string reference)
        {
            if (string.IsNullOrEmpty(author))
                throw new ArgumentException("Value cannot be null or empty.", nameof(author));
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(author));
            if (string.IsNullOrEmpty(product))
                throw new ArgumentException("Value cannot be null or empty.", nameof(product));
            if (string.IsNullOrWhiteSpace(product))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(product));
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("Value cannot be null or empty.", nameof(description));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(description));
            if (string.IsNullOrEmpty(language))
                throw new ArgumentException("Value cannot be null or empty.", nameof(language));
            if (string.IsNullOrWhiteSpace(language))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(language));

            Status = status;
            Author = author;
            Product = product;
            Description = description;
            Language = language;
            Reference = reference;
        }
    }

    /// <summary>
    /// Represents the status of an approval
    /// </summary>
    public enum ApprovalStatus
    {
        /// <summary>
        /// Approved formally by an authority
        /// </summary>
        Standard = 0x1,

        /// <summary>
        /// Approved informally by an authority
        /// </summary>
        Informal = 0x2,

        /// <summary>
        /// Not approved by any authority
        /// </summary>
        None = 0xf
    }
}