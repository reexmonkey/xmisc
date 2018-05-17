using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.validation.contracts.validators
{
    /// <summary>
    /// Represents a validator that composes multiple validators and for the validation of an entity.
    /// </summary>
    /// <typeparam name="T">The type of entity to validate</typeparam>
    /// <remarks>See: "http://www.jeremyskinner.co.uk/2011/01/13/using-fluentvalidation-to-validate-types-with-multiple-interfaces/"</remarks>
    public abstract class AbstractCompositeValidator<T> : AbstractValidator<T>
    {
        private List<IValidator> validators;

        /// <summary>
        /// Creates a new instance of the <see cref="AbstractCompositeValidator{T}"/> class.
        /// </summary>
        public AbstractCompositeValidator()
        {
            validators = new List<IValidator>();
        }

        /// <summary>
        /// Registers a compatible validator that can validate instances of the type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="validator">A validator that can vlidate instances of the type <typeparamref name="T"/>.</param>
        protected void RegisterValidator(IValidator validator)
        {
            if (validator.CanValidateInstancesOfType(typeof(T))) validators.Add(validator);
            else throw new NotSupportedException($"The validator cannot validate instance of the type {typeof(T).Name}");
        }

        /// <summary>
        /// Validates the specified instance.
        /// </summary>
        /// <param name="context">Validation Context</param>
        /// <returns>A ValidationResult object containing any validation failures.</returns>
        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var primaryErrors = base.Validate(context).Errors;
            var secondaryErrors = validators.SelectMany(x => x.Validate(context).Errors);
            return new ValidationResult(primaryErrors.Concat(secondaryErrors));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public override async Task<ValidationResult> ValidateAsync(ValidationContext<T> context, CancellationToken cancellation = default(CancellationToken))
        {
            var primaryErrors = (await base.ValidateAsync(context)).Errors;
            var secondaryErrors = await ValidateAsync(validators, context, cancellation);
            return new ValidationResult(primaryErrors.Concat(secondaryErrors));
        }

        private async Task<IList<ValidationFailure>> ValidateAsync(
            IEnumerable<IValidator> validators,
            ValidationContext<T> context,
            CancellationToken cancellation = default(CancellationToken))
        {
            var errors = new List<ValidationFailure>();
            foreach (var validator in validators)
            {
                var result = await validator.ValidateAsync(context, cancellation);
                if (result.Errors.Any()) errors.AddRange(result.Errors);
            }
            return errors;
        }
    }
}
