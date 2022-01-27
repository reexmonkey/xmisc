using System;
using FluentValidation.Results;

namespace xmisc.backbone.validation.contracts.Infrastructure
{
    /// <summary>
    /// Represents a base extension of the <see cref="ValidationFailure"/> class.
    /// </summary>
    public abstract class AbstractValidationFailure : ValidationFailure
    {
        /// <summary>
        /// Creates a new instance of the <see cref="AbstractValidationFailure"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property that caused the failure.</param>
        /// <param name="error">The error message.</param>
        /// <param name="parent">
        /// Optional: the <see cref="AbstractValidationResult"/> that owns this failure.
        /// </param>
        protected AbstractValidationFailure(string propertyName, string error, AbstractValidationResult parent = null)
            : base(propertyName, error)
        {
            Parent = parent;
        }

        /// <summary> Creates a new instance of the <see cref="AbstractValidationFailure"/> class.
        /// </summary> <param name="propertyName">The name of the property that caused the failure.
        /// </param> <param name="error">The error message.</param>
        /// <param name="attemptedValue">The property value that caused the failure.</param> 
        /// <param name="parent">Optional: the <see cref="AbstractValidationResult"/> that owns this failure.</param>
        protected AbstractValidationFailure(string propertyName, string error, object attemptedValue, AbstractValidationResult parent = null)
            : base(propertyName, error, attemptedValue)
        {
            Parent = parent;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AbstractValidationFailure"/> class.
        /// </summary>
        /// <param name="failure">The source of the failure to initialize this instance with.</param>
        protected AbstractValidationFailure(ValidationFailure failure)
            : base(failure?.PropertyName, failure?.ErrorMessage, failure?.AttemptedValue)
        {
            CustomState = failure?.CustomState;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AbstractValidationFailure"/> class.
        /// </summary>
        /// <param name="other">
        /// Another instance of the <see cref="AbstractValidationFailure"/> to initiate this instance with.
        /// </param>
        protected AbstractValidationFailure(AbstractValidationFailure other)
            : base(other?.PropertyName, other?.ErrorMessage, other?.AttemptedValue)
        {
            CustomState = other?.CustomState;
            Parent = other?.Parent;
        }

        /// <summary>
        /// The <see cref="AbstractValidationResult"/> that owns this failure.
        /// </summary>
        public AbstractValidationResult Parent { get; set; }
    }

    /// <summary>
    /// Represents a base extension of the <see cref="AbstractValidationFailure"/> class with a
    /// specified custom state.
    /// </summary>
    /// <typeparam name="TState">The type of custom state associated to the failure.</typeparam>
    public abstract class AbstractValidationFailure<TState> : AbstractValidationFailure
    {
        /// <summary>
        /// Creates a new instance of the <see cref="AbstractValidationFailure{TState}"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property that caused the failure.</param>
        /// <param name="error">The error message.</param>
        /// <param name="state">The custom state associated with the failure.</param>
        /// <param name="parent">
        /// Optional: the <see cref="AbstractValidationResult"/> that owns this failure.
        /// </param>
        protected AbstractValidationFailure(string propertyName, string error, TState state = default, AbstractValidationResult parent = null)
            : base(propertyName, error, parent)
        {
            CustomState = state;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AbstractValidationFailure{TState}"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property that caused the failure.</param>
        /// <param name="error">The error message.</param>
        /// <param name="attemptedValue">The property value that caused the failure.</param>
        /// <param name="state">The custom state associated with the failure.</param>
        /// <param name="parent">
        /// Optional: the <see cref="AbstractValidationResult"/> that owns this failure.
        /// </param>
        protected AbstractValidationFailure(string propertyName, string error, object attemptedValue, TState state = default, AbstractValidationResult parent = null) : base(propertyName, error, attemptedValue, parent)
        {
            CustomState = state;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AbstractValidationFailure{TState}"/> class.
        /// </summary>
        /// <param name="failure">The source of the failure to initialize this instance with.</param>
        /// <param name="transform">
        /// The function that transforms a given object data into a custom state.
        /// </param>
        protected AbstractValidationFailure(ValidationFailure failure, Func<object, TState> transform) : base(failure)
        {
            if (transform == null) throw new ArgumentNullException(nameof(transform));
            CustomState = transform(failure?.CustomState);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AbstractValidationFailure"/> class.
        /// </summary>
        /// <param name="other">
        /// Another instance of the <see cref="AbstractValidationFailure"/> to initiate this instance with.
        /// </param>
        protected AbstractValidationFailure(AbstractValidationFailure other) : base(other)
        {
        }

        /// <summary>
        /// Gets or sets the custom state associated to this validation failure.
        /// </summary>
        public new TState CustomState
        {
            get => (TState)base.CustomState;
            set => base.CustomState = value;
        }
    }
}
