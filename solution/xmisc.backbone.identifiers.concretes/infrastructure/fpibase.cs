using reexmonkey.xmisc.backbone.identifiers.contracts.infrastructure;
using System;
using System.Text;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.infrastructure
{
    /// <summary>
    /// Represents a base class for a Formal Public Identifier (FPI) as defined in RFC 3151
    /// </summary>
    public abstract class FpiBase : IFpiOwner, IFpiText
    {
        /// <summary>
        /// Gets the approval status of the FPI.
        /// </summary>
        public ApprovalStatus Status { get; protected set; }

        /// <summary>
        /// Gets the owner of the FPI
        /// </summary>
        public string Author { get; protected set; }

        /// <summary>
        /// Gets the reference to the standard authority that approved the FPI.
        /// </summary>
        public string Reference { get; protected set; }

        /// <summary>
        /// Gets or sets the product class of the FPI
        /// </summary>
        public string Product { get; protected set; }

        /// <summary>
        /// Gets or sets the description of the FPI
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// Gets or sets the langauage of the FPI according to ISO 639
        /// </summary>
        public string Language { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FpiBase"/> class.
        /// </summary>
        protected FpiBase() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FpiBase"/> class with details.
        /// </summary>
        /// <param name="status">The approval status of the FPI.</param>
        /// <param name="author">The owner of the FPI</param>
        /// <param name="product">The reference to the standard authority that approved the FPI.</param>
        /// <param name="description">The product class of the FPI</param>
        /// <param name="language">The langauage of the FPI according to ISO 639</param>
        /// <param name="reference">The reference to the standard authority that approved the FPI.</param>
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

        /// <summary>
        /// Returns a string representation of this instance.
        /// </summary>
        /// <returns></returns>
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
