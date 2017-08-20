using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace xmisc.backbone.validation.contracts.Infrastructure
{
    public abstract class AbstractValidationResult : ValidationResult
    {
        protected AbstractValidationResult()
        {
        }

        protected AbstractValidationResult(IEnumerable<AbstractValidationFailure> failures) : base(failures)
        {
        }

        protected AbstractValidationResult(ValidationResult result, Func<ValidationFailure, AbstractValidationFailure> transform)
        {
            if (transform == null) throw new ArgumentNullException(nameof(transform));
            AddErrors(result.Errors.Select(transform));
        }

        public new IList<AbstractValidationFailure> Errors => base.Errors.Cast<AbstractValidationFailure>().ToList();

        public void AddError(AbstractValidationFailure error)
        {
            error.Parent = this;
            Errors.Add(error);
        }

        public void AddErrors(IEnumerable<AbstractValidationFailure> errors)
        {
            foreach (var error in errors) Errors.Add(error);
        }

        public void RemoveError(AbstractValidationFailure error) => Errors.Remove(error);

        public void RemoveErrors(IEnumerable<AbstractValidationFailure> errors)
        {
            foreach (var error in errors) Errors.Remove(error);
        }

        public IEnumerable<AbstractValidationFailure> GetErrors(int? offset, int? count)
            => offset != null && count != null ? Errors.Skip(offset.Value).Take(count.Value) : Errors;

        public IEnumerable<AbstractValidationFailure> FindErrors(Func<AbstractValidationFailure, bool> predicate, int? offset, int? count)
            => offset != null && count != null
            ? Errors.Where(predicate).Skip(offset.Value).Take(count.Value)
            : Errors.Where(predicate);
    }

    public abstract class AbstractValidationResult<TState> : AbstractValidationResult
    {
        protected AbstractValidationResult()
        {
        }

        protected AbstractValidationResult(IEnumerable<AbstractValidationFailure<TState>> failures)
            : base(failures)
        {
        }

        protected AbstractValidationResult(ValidationResult result, Func<ValidationFailure, AbstractValidationFailure<TState>> transform)
            : base(result, transform)
        {
        }
    }
}
