using FluentValidation;
using xmisc.backbone.identity.concretes.infrastructure;
using xmisc.backbone.identity.contracts.infrastructure;

namespace xmisc.backbone.identity.concretes.validators
{
    public class FpiBaseValidator : AbstractValidator<FpiBase>
    {
        public FpiBaseValidator(CascadeMode mode = CascadeMode.Continue)
        {
            CascadeMode = mode;

            RuleFor(x => x.Reference)
                .Must(x => !string.IsNullOrEmpty(x) && !string.IsNullOrWhiteSpace(x))
                .When(x => x.Status == ApprovalStatus.Standard);

        }
    }
}
