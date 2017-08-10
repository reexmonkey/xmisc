using System;
using System.Text.RegularExpressions;
using xmisc.backbone.identity.contracts.infrastructure;

namespace xmisc.backbone.identity.concretes.infrastructure
{
    public class Fpi : FpiBase, IEquatable<Fpi>
    {
        public static readonly Fpi NullFpi = new Fpi(ApprovalStatus.None, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);


        public Fpi(ApprovalStatus status, string author, string product, string description, string language, string reference = null)
            : base(status, author, product, description, language, reference)
        {
        }

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

        public bool Equals(Fpi other)
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

        public static bool operator ==(Fpi left, Fpi right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Fpi left, Fpi right)
        {
            return !Equals(left, right);
        }
    }
}
