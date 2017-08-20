using System;
using FluentValidation.Results;


namespace xmisc.backbone.validation.contracts.Infrastructure
{
    public abstract class AbstractValidationFailure : ValidationFailure
    {
        protected AbstractValidationFailure(string propertyName, string error, AbstractValidationResult parent = null)
            : base(propertyName, error)
        {
            Parent = parent;
        }

        protected AbstractValidationFailure(string propertyName, string error, object attemptedValue, AbstractValidationResult parent = null)
            : base(propertyName, error, attemptedValue)
        {
            Parent = parent;
        }

        protected AbstractValidationFailure(ValidationFailure failure)
            : base(failure?.PropertyName, failure?.ErrorMessage, failure?.AttemptedValue)
        {
            CustomState = failure?.CustomState;
        }
        protected AbstractValidationFailure(AbstractValidationFailure other)
            : base(other?.PropertyName, other?.ErrorMessage, other?.AttemptedValue)
        {
            CustomState = other?.CustomState;
            Parent = other?.Parent;
        }

        public AbstractValidationResult Parent { get; set; }

    }

    public abstract class AbstractValidationFailure<TState> : AbstractValidationFailure
    {
        protected AbstractValidationFailure(string propertyName, string error, TState state = default(TState), AbstractValidationResult parent = null)
            : base(propertyName, error, parent)
        {
            CustomState = state;
        }

        protected AbstractValidationFailure(string propertyName, string error, object attemptedValue, TState state = default(TState), AbstractValidationResult parent = null) : base(propertyName, error, attemptedValue, parent)
        {
            CustomState = state;
        }

        protected AbstractValidationFailure(ValidationFailure failure, Func<object, TState> transform) : base(failure)
        {
            if (transform == null) throw new ArgumentNullException(nameof(transform));
            CustomState = transform(failure?.CustomState);
        }

        protected AbstractValidationFailure(AbstractValidationFailure other) : base(other)
        {
        }

        public new TState CustomState
        {
            get => (TState)base.CustomState;
            set => base.CustomState = value;
        }
    }
}
