using reexjungle.xmisc.foundation.contracts;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace reexjungle.xmisc.foundation.concretes
{
    /// <summary>
    /// Represents a Formal Public Identifier class
    /// </summary>
    public class Fpi : IFpiOwner, IFpiText, IFpiUrnConverter, IEquatable<Fpi>
    {
        /// <summary>
        /// Gets or sets the approval status of the FPI
        /// </summary>
        public ApprovalStatus Status { get; set; }

        /// <summary>
        ///  Gets or sets the owner of the FPI, if it is approved by a standard authority e.g. ISO
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the reference to the standard authority that approved the FPI
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the product class of the FPI
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// Gets or sets the description of the FPI
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the langauage of the FPI according to ISO 639
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="author">The owner of a formal FPI that is approved by a formal authority</param>
        /// <param name="product">The product class of the FPI</param>
        /// <param name="description">The description of the FPI</param>
        /// <param name="language">The language of the FPI</param>
        /// <param name="status">The approval status of the FPI</param>
        /// <param name="reference">The standard authority that formally approved the FPI</param>
        public Fpi(ApprovalStatus status, string author, string product, string description, string language, string reference = null)
        {

            Status = status;
            Reference = reference;

            if (status == ApprovalStatus.Standard)
                reference.ThrowIfNullOrEmpty("A reference (e.g. ISO) must be provided for the Standard approval status");

            Author = author;
            author.ThrowIfNullOrEmpty("author");

            product.ThrowIfNullOrEmpty("product");
            Product = product;

            Description = description;

            language.ThrowIfNullOrEmpty("language");
            Language = language;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="author">The owner of the formal FPI</param>
        /// <param name="product">The product class of the FPI</param>
        /// <param name="description">The description of the FPI</param>
        /// <param name="language">The language of the FPI</param>
        /// <param name="reference">The standard authority that formally approved the FPI</param>
        public Fpi(string author, string product, string description, string language, string reference) :
            this(ApprovalStatus.Standard, author, product, description, language, reference)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Serialized string of a Formal Public Identifier (FPI)</param>
        public Fpi(string value)
        {
            const string pattern = @"^(?<prefix>[\+|-]|\p{L}+)//(?<author>(\p{L}+\d*)*)//(?<product>(\p{L}+\d*)*)(?<description>(\s*\p{L}*\d*\s*\d*\p{P}*\d*)*)//(?<language>\p{L}{2})*$";
            if (!Regex.IsMatch(value, pattern, RegexOptions.ExplicitCapture | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase))
                throw new FormatException("Invalid FPI format");

            foreach (Match match in Regex.Matches(value, pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture))
            {
                if (match.Groups["prefix"].Success)
                {
                    switch (match.Groups["prefix"].Value)
                    {
                        case "+":
                            Status = ApprovalStatus.Informal;
                            break;

                        case "-":
                            Status = ApprovalStatus.None;
                            break;

                        default:
                            Status = ApprovalStatus.Standard;
                            Reference = match.Groups["prefix"].Value;
                            break;
                    }
                }
                if (match.Groups["author"].Success) Author = match.Groups["author"].Value;
                if (match.Groups["product"].Success) Product = match.Groups["product"].Value;
                if (match.Groups["description"].Success && !string.IsNullOrWhiteSpace(match.Groups["description"].Value))
                    Description = match.Groups["description"].Value.TrimStart();
                if (match.Groups["language"].Success) Language = match.Groups["language"].Value;
            }
        }

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

        public bool Equals(Fpi other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Status == other.Status &&
                string.Equals(Author, other.Author) &&
                string.Equals(Reference, other.Reference) &&
                string.Equals(Product, other.Product) &&
                string.Equals(Description, other.Description) &&
                string.Equals(Language, other.Language);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Fpi)obj);
        }

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
        /// The equality operator
        /// </summary>
        /// <param name="left">The FPI operand on the left side of the equality operator</param>
        /// <param name="right">The FPI operand on the right side of the equality operator</param>
        /// <returns>True, if both FPI instances are equal, otherwise false.</returns>
        public static bool operator ==(Fpi left, Fpi right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// The inequality operator
        /// </summary>
        /// <param name="left">The FPI operand on the left side of the equality operator</param>
        /// <param name="right">The FPI operand on the right side of the equality operator</param>
        /// <returns>True, if both FPI instances are inequal, otherwise false.</returns>
        public static bool operator !=(Fpi left, Fpi right)
        {
            return !Equals(left, right);
        }

        public string ToUrn()
        {
            return string.Format("urn:{0}", ToString().Replace("//", ":"));
        }

        public void FromUrn(string urn)
        {
            if (urn == null) throw new ArgumentNullException("urn");
            var fpi = new Fpi(string.Format("urn:{0}", urn.Substring(4).Replace(":", "//")));
            Status = fpi.Status;
            Author = fpi.Author;
            Product = fpi.Product;
            Description = fpi.Description;
        }
    }
}