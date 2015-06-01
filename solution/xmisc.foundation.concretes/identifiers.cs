using reexjungle.xmisc.foundation.contracts;
using System;
using System.Text;

namespace reexjungle.xmisc.foundation.concretes
{
    /// <summary>
    /// Represents a Formal Public Identifier class
    /// </summary>
    public class Fpi : IFpiOwner, IFpiText, IEquatable<Fpi>
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
            if (status == ApprovalStatus.Standard)
                reference.ThrowIfNullOrEmpty("reference");
            Reference = reference;

            Status = status;

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
            return Status == other.Status && string.Equals(Author, other.Author) && string.Equals(Reference, other.Reference) && string.Equals(Product, other.Product) && string.Equals(Description, other.Description) && string.Equals(Language, other.Language);
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
    }
}