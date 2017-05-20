using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using xmisc.backbone.identity.contracts.infrastructure;

namespace xmisc.backbone.identity.contracts.validators
{
    public class FpiBaseValidator: AbstractValidator<FpiBase>
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
