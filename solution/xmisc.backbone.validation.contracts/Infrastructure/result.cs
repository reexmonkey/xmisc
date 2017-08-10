using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using xmisc.backbone.validation.contracts.Core;

namespace xmisc.backbone.validation.contracts.Infrastructure
{
    public abstract class AbstractValidationResult : ValidationResult, IValidationResult
    {
        protected AbstractValidationResult()
        {
        }

        protected AbstractValidationResult(IEnumerable<AbstractValidationFailure> failures) : base(failures)
        {
        }

        protected AbstractValidationResult(ValidationResult result, Func<ValidationFailure, AbstractValidationFailure> func)
        {
            foreach (var error in result.Errors)
            {
                AddFailure(func(error));
            }
        }

        public new IList<AbstractValidationFailure> Errors => base.Errors.Cast<AbstractValidationFailure>().ToList();


        public void AddFailure(AbstractValidationFailure failure)
        {
            if (!Errors.Contains(failure)) Errors.Add(failure);
        }

        public void AddFailures(IEnumerable<AbstractValidationFailure> failures)
        {
            foreach (var failure in failures.Where(x => !Errors.Contains(x)))
                Errors.Add(failure);
        }

        public void RemoveFailure(AbstractValidationFailure failure) => Errors.Remove(failure);

        public void RemoveFailures(IEnumerable<AbstractValidationFailure> failures)
        {
            foreach (var failure in failures) Errors.Remove(failure);
        }

        public IEnumerable<AbstractValidationFailure> GetFailures(int? skip, int? take)
            => skip != null && take != null ? Errors.Skip(skip.Value).Take(take.Value) : Errors;

        IEnumerable<IValidationFailure> IValidationResult.GetFailures(int? skip, int? take)
            => GetFailures(skip, take);

        public IEnumerable<AbstractValidationFailure> FindFailures(Func<AbstractValidationFailure, bool> predicate, int? skip, int? take)
            => skip != null && take != null
            ? Errors.Where(predicate).Skip(skip.Value).Take(take.Value)
            : Errors.Where(predicate);

        IEnumerable<IValidationFailure> IValidationResult.FindFailures(Func<IValidationFailure, bool> predicate, int? skip, int? take)
            => FindFailures(predicate, skip, take);
    }
}
