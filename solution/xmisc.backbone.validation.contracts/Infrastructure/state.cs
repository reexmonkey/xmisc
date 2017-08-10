using System;
using xmisc.backbone.validation.contracts.Core;

namespace xmisc.backbone.validation.contracts.Infrastructure
{
    /// <summary>
    /// Represents a custom validation state.
    /// </summary>
    public abstract class AbstractValidationState : IValidationState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractValidationState"/> class with a custom validation failure that associates to this state.
        /// </summary>
        /// <param name="parent">The custom validation failure that associates to this custom validation state.</param>
        protected AbstractValidationState(AbstractValidationFailure parent)
            => Parent = parent ?? throw new ArgumentNullException(nameof(parent));

        /// <summary>
        /// Gets the validation failure that associates to this custom state.
        /// </summary>
        IValidationFailure IValidationState.Parent => Parent;

        /// <summary>
        /// Gets the validation failure that associates to this custom state.
        /// </summary>
        public AbstractValidationFailure Parent { get; }
    }
}
