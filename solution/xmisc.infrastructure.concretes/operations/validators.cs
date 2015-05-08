using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace reexjungle.xmisc.infrastructure.concretes.operations
{
    /// <summary>
    /// Base dvalidator class for composite validators
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractCompositeValidator<T> : AbstractValidator<T>
    {
        private readonly List<IValidator> validators = new List<IValidator>();

        protected void RegisterBaseValidator<TBase>(IValidator<TBase> validator)
        {
            if (validator.CanValidateInstancesOfType(typeof(T))) validators.Add(validator);
            else throw new NotSupportedException(string.Format("Type {0} is not a base-class or interface implemented by {1}.", typeof(TBase).Name, typeof(T).Name));
        }

        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var berrors = base.Validate(context).Errors;
            var oerrors = validators.SelectMany(x => x.Validate(context).Errors);
            var combined = berrors.Concat(oerrors);
            return new ValidationResult(combined);
        }
    }

    /// <summary>
    /// Polymorphic dvalidator class for collections. Useful for collections of base objects.
    /// </summary>
    /// <typeparam name="TBase"></typeparam>
    public class PolymorphicCollectionValidator<TBase> : NoopPropertyValidator
    {
        private readonly IValidator<TBase> validator;
        private readonly Dictionary<Type, IValidator> deriveds;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="validator">Specifies the validator of the base class</param>
        public PolymorphicCollectionValidator(IValidator<TBase> validator = null)
        {
            this.validator = validator;
            this.deriveds = new Dictionary<Type, IValidator>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <typeparam name="TDerived">The type of the derived class</typeparam>
        /// <param name="dvalidator">Specifies the specialized validtor of the dervived class</param>
        /// <returns></returns>
        public PolymorphicCollectionValidator<TBase> Add<TDerived>(IValidator<TDerived> dvalidator)
            where TDerived : TBase
        {
            if (!this.deriveds.ContainsKey(typeof(TDerived)))
                this.deriveds.Add(typeof(TDerived), dvalidator);
            return this;
        }

        public override IEnumerable<ValidationFailure> Validate(PropertyValidatorContext context)
        {
            var collection = context.PropertyValue as IEnumerable<TBase>;
            if (collection == null) return Enumerable.Empty<ValidationFailure>();
            if (!collection.Any()) return Enumerable.Empty<ValidationFailure>();

            foreach (var item in collection)
            {
                if (!deriveds.ContainsKey(item.GetType())) continue;
                var derived = deriveds[item.GetType()];
                var collectionValidator = new ChildCollectionValidatorAdaptor(derived);
                return collectionValidator.Validate(context);
            }

            if (this.validator != null)
            {
                var baseCollectionValidator = new ChildCollectionValidatorAdaptor(this.validator);
                return baseCollectionValidator.Validate(context);
            }

            return Enumerable.Empty<ValidationFailure>();
        }
    }
}