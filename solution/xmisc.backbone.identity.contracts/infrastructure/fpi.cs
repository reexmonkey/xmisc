using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xmisc.backbone.identity.contracts.infrastructure
{
    public abstract class FpiBase: IFpiOwner, IFpiText, IFpiUrnConverter, IEquatable<FpiBase>
    {
        public ApprovalStatus Status { get; protected set; }
        public string Author { get; protected set; }
        public string Reference { get; protected set; }
        public string Product { get; protected set; }
        public string Description { get; protected set; }
        public string Language { get; protected set; }

        protected FpiBase() { }

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

        public string ToUrn() => $"urn:{ToString().Replace("//", ":")}";
        public abstract void FromUrn(string urn);

        public bool Equals(FpiBase other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Status == other.Status && string.Equals(Author, other.Author, StringComparison.OrdinalIgnoreCase) 
                && string.Equals(Reference, other.Reference, StringComparison.OrdinalIgnoreCase) 
                && string.Equals(Product, other.Product, StringComparison.OrdinalIgnoreCase) 
                && string.Equals(Description, other.Description, StringComparison.OrdinalIgnoreCase) 
                && string.Equals(Language, other.Language, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FpiBase) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) Status;
                hashCode = (hashCode * 397) ^ (Author != null ? Author.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Reference != null ? Reference.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Product != null ? Product.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Language != null ? Language.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(FpiBase left, FpiBase right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FpiBase left, FpiBase right)
        {
            return !Equals(left, right);
        }
    }
}
