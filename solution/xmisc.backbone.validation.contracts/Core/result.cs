using System;
using System.Collections.Generic;

namespace xmisc.backbone.validation.contracts.Core
{
    public interface IValidationResult
    {
        IEnumerable<IValidationFailure> GetFailures(int? skip = null, int? take = null);

        IEnumerable<IValidationFailure> FindFailures(Func<IValidationFailure, bool> predicate, int? skip = null, int? take = null);

    }
}
