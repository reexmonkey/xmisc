using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using FluentValidation.Validators;
using reexjungle.xmisc.foundation.concretes;
using reexjungle.xmisc.infrastructure.concretes.extensions;
using reexjungle.xmisc.infrastructure.contracts;

namespace reexjungle.xmisc.infrastructure.concretes.operations
{
    /// <summary>
    /// Represents a custom property validator for validating a collection of elements with composite validators.
    /// Each composite validator validates instances of <typeparamref name="TCollectionElement"/>.
    /// </summary>
    /// <typeparam name="TCollectionElement">The type of element in the collection.</typeparam>
    public class ChildCollectionValidationsAdaptor<TCollectionElement>: NoopPropertyValidator, IChildCollectionValidatorsAdaptor<TCollectionElement>
    {
        private readonly IDictionary<Type, List<IValidator>> pool;
        private readonly Type elementType;
        private readonly IEqualityComparer<IValidator> comparer; 

        /// <summary>
        /// A predicate to filter elememts of the collection
        /// </summary>
        public Func<TCollectionElement, bool> Predicate { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="ChildCollectionValidationsAdaptor{TCollectionElement}"/> class.
        /// </summary>
        public ChildCollectionValidationsAdaptor()
        {
            pool = new Dictionary<Type, List<IValidator>>();
            elementType = typeof (TCollectionElement);
            comparer = new ValidatorEqualityComparer();
        }


        /// <summary>
        /// Validates the collection of elements using the validators in the composite validators pool.
        /// </summary>
        /// <param name="context">The context of the validation.</param>
        /// <returns>A sequence of validation failures, if the validation produced errors; otherwise an empty sequence. </returns>
        public override IEnumerable<ValidationFailure> Validate(PropertyValidatorContext context)
        {
            return PerformValidation(
                context,
                element => GetValidators(element.GetType()),
                items => items.SelectMany(tuple =>
                {
                    ValidationContext ctx = tuple.Item1;
                    IValidator validator = tuple.Item2;
                    return validator.Validate(ctx).Errors;
                }),
                Enumerable.Empty<ValidationFailure>());
        }

        private TResult PerformValidation<TResult>(PropertyValidatorContext context,
    Func<TCollectionElement, IEnumerable<IValidator>> provider,
    Func<IEnumerable<Tuple<ValidationContext, IValidator>>, TResult> validatorApplicator,
    TResult emptyResult)
        {
            if (context.Rule.Member == null)
                throw new InvalidOperationException("Nested validators can only be used with Member Expressions.");

            IEnumerable<TCollectionElement> collection = context.PropertyValue as IEnumerable<TCollectionElement>;
            if (collection.NullOrEmpty()) return emptyResult;

            Func<TCollectionElement, bool> predicate = Predicate ?? (Predicate ?? (x => true));

            //Items to validate
            IEnumerable<Tuple<ValidationContext, IValidator>> items = collection
                .Select((item, index) => new { item, index })
                .Where(x => x.item != null && predicate(x.item))
                .SelectMany(x =>
                {
                    ValidationContext newContext = context.ParentContext.CloneForChildValidator(x.item);
                    newContext.PropertyChain.Add(context.Rule.PropertyName);
                    newContext.PropertyChain.AddIndexer(x.index);
                    IEnumerable<IValidator> validators = provider(x.item);

                    return validators.NullOrEmpty()
                        ? Enumerable.Empty<Tuple<ValidationContext, IValidator>>()
                        : validators.Select(validator => Tuple.Create(newContext, validator));
                });

            return validatorApplicator(items);
        }

        /// <summary>
        /// Gets a validator from the pool of composite validators.
        /// </summary>
        /// <param name="type">The type declaration of the instance to be validated.</param>
        /// <returns>The composite validator from the pool; otherwise null.</returns>
        public IEnumerable<IValidator> GetValidators(Type type)
        {
            List<IValidator> validators;
            return pool.TryGetValue(type, out validators)
                ? validators.Distinct(comparer)
                : Enumerable.Empty<IValidator>();
        }

        /// <summary>
        /// Gets a validator from the pool of composite validators.
        /// </summary>
        /// <typeparam name="T">The type of instance to be validated.</typeparam>
        /// <returns>The composite validator from the pool; otherwise null.</returns>
        public IEnumerable<IValidator<T>> GetValidators<T>()
        {
            var key = typeof (T);
            List<IValidator> validators;
            return pool.TryGetValue(key, out validators)
                ? validators.Distinct(comparer).Cast<IValidator<T>>()
                : Enumerable.Empty<IValidator<T>>();
        }

        /// <summary>
        /// Adds a validator to the pool of composite validators.
        /// </summary>
        /// <param name="type">The type declaration of the instance to be validated.</param>
        /// <param name="validator">A composite validator for instances of <paramref name="type"/> to be added. The validator should be capable of validating instances of type <typeparamref name="TCollectionElement"/>.</param>
        public void Add(Type type, IValidator validator)
        {
            List<IValidator> validators;
            if (!pool.TryGetValue(type, out validators))
            {
                if (validator.CanValidateInstancesOfType(elementType))
                    pool.Add(type, new List<IValidator> { validator });
            }
            else
            {
                if (validator.CanValidateInstancesOfType(elementType))
                    validators.Add(validator);
            }
        }

        /// <summary>
        ///  Adds a validator to the pool of composite validators.
        /// </summary>
        /// <param name="validator">The validator to be added. The validator should be capable of validating instances of type <typeparamref name="TCollectionElement"/>.</param>
        /// <typeparam name="T">The type of instance to be validated.</typeparam>
        public void Add<T>(IValidator<T> validator) where T: TCollectionElement
        {
            var type = typeof (T);
            List<IValidator> validators;
            if (!pool.TryGetValue(type, out validators))
            {
                pool.Add(type, new List<IValidator> { validator });
            }
            else
            {
                validators.Add(validator);
            }
        }

        /// <summary>
        /// Adds validators to the pool of composite validators.
        /// </summary>
        /// <param name="validators">The validators to be added. Only non-null validators capable of validating instances of type <typeparamref name="TCollectionElement"/> shall be added.</param>
        /// <typeparam name="T">The type of instance to be validated.</typeparam>
        public void AddRange<T>(IEnumerable<IValidator<T>> validators) where T: TCollectionElement
        {
            Type key = typeof(T);
            foreach (IValidator<T> validator in validators)
            {
                List<IValidator> values;
                if (!pool.TryGetValue(key, out values))
                {
                    pool.Add(key, new List<IValidator> { validator });
                }
                else
                {
                    values.Add(validator);
                }
            }
        }

        /// <summary>
        /// Adds validators to the pool of composite validators.
        /// </summary>
        /// <param name="tuples">Tuples containing validators to be added. Each validator of the tuple targets instances of the type declaration. Only non-null validators capable of validating instances of type <typeparamref name="TCollectionElement"/> shall be added. </param>
        public void AddRange(IEnumerable<Tuple<Type, IValidator>> tuples)
        {
            foreach (Tuple<Type, IValidator> tuple in tuples)
            {
                if (tuple == null) continue;
                if (tuple.Item1 == null || tuple.Item2 == null) continue;

                List<IValidator> validators;
                if (!pool.TryGetValue(tuple.Item1, out validators))
                {
                    if (tuple.Item2.CanValidateInstancesOfType(elementType))
                        pool.Add(tuple.Item1, new List<IValidator> { tuple.Item2 });
                }
                else
                {
                    if (tuple.Item2.CanValidateInstancesOfType(elementType))
                        validators.Add(tuple.Item2);
                }
            }
        }

        /// <summary>
        /// Removes a validator from the composite validator pool.
        /// </summary>
        /// <typeparam name="T">The type of instance validated by the validator.</typeparam>
        public void Remove<T>() where T: TCollectionElement
        {
            pool.Remove(typeof (T));
        }

        /// <summary>
        /// Removes a validator from the composite validator pool.
        /// </summary>
        /// <param name="type">The type declaration of the instance that is validated by the validator.</param>
        public void Remove(Type type)
        {
            pool.Remove(type);
        }

        /// <summary>
        /// Removes all validators from the composite validator pool.
        /// </summary>
        public void RemoveAll()
        {
            pool.Clear();
        }
    }

    internal class CollectionValidatorRuleBuilder<T, TCollectionElement> :
        CollectionValidatorExtensions.ICollectionValidatorRuleBuilder<T, TCollectionElement>
    {
        private readonly IRuleBuilder<T, IEnumerable<TCollectionElement>> ruleBuilder;
        private readonly ICollectionPropertyValidatorAdaptor<TCollectionElement> adaptor;

        public CollectionValidatorRuleBuilder(
            IRuleBuilder<T, IEnumerable<TCollectionElement>> ruleBuilder,
            ICollectionPropertyValidatorAdaptor<TCollectionElement> adaptor)
        {
            if (ruleBuilder == null) throw new ArgumentNullException("ruleBuilder");
            if (adaptor == null) throw new ArgumentNullException("adaptor");

            this.ruleBuilder = ruleBuilder;
            this.adaptor = adaptor;
        }

        public IRuleBuilderOptions<T, IEnumerable<TCollectionElement>> SetValidator(IPropertyValidator validator)
        {
            return ruleBuilder.SetValidator(validator);
        }

        public IRuleBuilderOptions<T, IEnumerable<TCollectionElement>> SetValidator(
            IValidator<IEnumerable<TCollectionElement>> validator)
        {
            return ruleBuilder.SetValidator(validator);
        }

        public IRuleBuilderOptions<T, IEnumerable<TCollectionElement>> Configure(Action<PropertyRule> configurator)
        {
            return ((IRuleBuilderOptions<T, IEnumerable<TCollectionElement>>) ruleBuilder).Configure(configurator);
        }

        public CollectionValidatorExtensions.ICollectionValidatorRuleBuilder<T, TCollectionElement> Where(
            Func<TCollectionElement, bool> predicate)
        {
            if (predicate == null) throw new ArgumentNullException("predicate");
            adaptor.Predicate = predicate;
            return this;
        }
    }

    internal class ValidatorEqualityComparer: IEqualityComparer<IValidator>
    {
        public bool Equals(IValidator x, IValidator y)
        {
            if (ReferenceEquals(null, y)) return false;
            if (ReferenceEquals(x, y)) return true;
            return  x.GetType() == y.GetType();
        }

        public int GetHashCode(IValidator obj)
        {
            return obj.GetType().GetHashCode();
        }
    }
}
