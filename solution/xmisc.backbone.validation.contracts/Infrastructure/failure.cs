using FluentValidation.Results;
using xmisc.backbone.validation.contracts.Core;

namespace xmisc.backbone.validation.contracts.Infrastructure
{
    public class AbstractValidationFailure : ValidationFailure, IValidationFailure
    {
        public AbstractValidationFailure(string propertyName, string error, AbstractValidationState state = null, AbstractValidationResult parent = null)
            : base(propertyName, error)
        {
            State = state;
            Parent = parent;
        }

        public AbstractValidationFailure(string propertyName, string error, object attemptedValue, AbstractValidationState state = null, AbstractValidationResult parent = null)
            : base(propertyName, error, attemptedValue)
        {
            State = state;
            Parent = parent;
        }

        public AbstractValidationFailure(string propertyName, string error, AbstractValidationResult parent)
            : this(propertyName, error, null, parent)
        {
        }

        public AbstractValidationFailure(string propertyName, string error, object attemptedValue, AbstractValidationResult parent)
            : this(propertyName, error, attemptedValue, null, parent)
        {
        }

        public AbstractValidationFailure(ValidationFailure failure) : base(failure?.PropertyName, failure?.ErrorMessage, failure?.AttemptedValue)
        {
        }

        public AbstractValidationFailure(AbstractValidationFailure other) : base(other?.PropertyName, other?.ErrorMessage, other?.AttemptedValue)
        {
            State = other?.State;
            Parent = other?.Parent;
        }

        public AbstractValidationState State { get; }

        public AbstractValidationResult Parent { get; }

        IValidationState IValidationFailure.State => State;

        IValidationResult IValidationFailure.Parent => Parent;
    }
}
