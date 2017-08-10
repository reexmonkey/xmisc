namespace xmisc.backbone.validation.contracts.Core
{
    /// <summary>
    /// Specifies an interface for a custom validation state.
    /// </summary>
    public interface IValidationState
    {
        /// <summary>
        /// Gets the validation failure that associates to this custom state.
        /// </summary>
        IValidationFailure Parent { get; }
    }
}
