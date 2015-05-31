using reexjungle.xmisc.foundation.contracts;
using System.Text;

namespace reexjungle.xmisc.foundation.concretes
{
    /// <summary>
    /// Represents a Formal Public Identifier class
    /// </summary>
    public class Fpi : IFpiOwner, IFpiText
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
        public Fpi(ApprovalStatus status, string author, string product, string description, string language)
        {
            if (status == ApprovalStatus.Standard)
                author.ThrowIfNullOrEmpty("author");

            Status = status;
            Author = author;

            product.ThrowIfNullOrEmpty("product");
            Product = product;

            description.ThrowIfNullOrEmpty("description");
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
        public Fpi(string author, string product, string description, string language) :
            this(ApprovalStatus.Standard, author, product, description, language)
        {
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            switch (Status)
            {
                case ApprovalStatus.Standard:
                    sb.Append(Author);
                    break;

                case ApprovalStatus.None:
                    sb.Append("-");
                    break;

                default:
                    sb.Append("+");
                    break;
            }

            sb.AppendFormat("//{0}", Product);
            sb.AppendFormat("//{0}", Description);
            sb.AppendFormat("//{0}", Language);
            return sb.ToString();
        }
    }
}