namespace xmisc.backbone.validation.contracts.Core
{
    public interface IValidationFailure
    {
        IValidationState State { get; }

        IValidationResult Parent { get; }
    }
}
