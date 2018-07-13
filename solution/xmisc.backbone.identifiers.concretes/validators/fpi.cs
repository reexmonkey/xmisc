using FluentValidation;
using reexmonkey.xmisc.backbone.identifiers.concretes.models;
using reexmonkey.xmisc.backbone.identifiers.contracts.infrastructure;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.validators
{
    /// <summary>
    /// Represent a fluent validator for objects of the <see cref="FpiBase"/> type.
    /// </summary>
    public class FpiBaseValidator : AbstractValidator<FpiBase>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="FpiBaseValidator"/> class.
        /// </summary>
        public FpiBaseValidator()
        {
            RuleFor(x => x.Reference)
                .Must(x => !string.IsNullOrEmpty(x) && !string.IsNullOrWhiteSpace(x))
                .When(x => x.Status == ApprovalStatus.Standard);
        }
    }
}
