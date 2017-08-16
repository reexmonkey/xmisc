using System;
using System.Text;
using reexmonkey.xmisc.backbone.identifiers.contracts.infrastructure;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.infrastructure
{
    public abstract class FpiBase : IFpiOwner, IFpiText
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
}
