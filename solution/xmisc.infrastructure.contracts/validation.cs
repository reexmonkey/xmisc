using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Validators;

namespace reexjungle.xmisc.infrastructure.contracts
{
    /// <summary>
    /// Specifies a custom property validator for a collection of elements. This interface should not be immplemented directly since it is subject to change in the future. Please inherit from <see cref="PropertyValidator"/> instead.
    /// </summary>
    /// <typeparam name="TCollectionElement"></typeparam>
    public interface ICollectionPropertyValidatorAdaptor<TCollectionElement> : IPropertyValidator
    {
        /// <summary>
        /// A predicate to filter elememts of the collection
        /// </summary>
        Func<TCollectionElement, bool> Predicate { get; set; }
    }


    /// <summary>
    /// Specifies a custom property validator for validating a collection of elements with composite validators.
    /// Each composite validator validates instances of <typeparamref name="TCollectionElement"/>. 
    /// </summary>
    /// <typeparam name="TCollectionElement">The type of element in the collection.</typeparam>
    public interface IChildCollectionValidatorsAdaptor<TCollectionElement> : ICollectionPropertyValidatorAdaptor<TCollectionElement>
    {
        /// <summary>
        /// Gets the validators for the specified <paramref name="type"/> from the pool of composite validators.
        /// </summary>
        /// <param name="type">The type declaration of the instance to be validated.</param>
        /// <returns>The composite validator from the pool; otherwise null.</returns>
        IEnumerable<IValidator> GetValidators(Type type);

        /// <summary>
        /// Gets the validators for the specified type <typeparamref name="T"/>from the pool of composite validators.
        /// </summary>
        /// <typeparam name="T">The type of instance to be validated.</typeparam>
        /// <returns>The composite validator from the pool; otherwise null.</returns>
        IEnumerable<IValidator<T>> GetValidators<T>();

        /// <summary>
        /// Adds a validator to the pool of composite validators.
        /// </summary>
        /// <param name="type">The type declaration of the instance to be validated.</param>
        /// <param name="validator">A composite validator for instances of <paramref name="type"/> to be added. The validator should be capable of validating instances of type <typeparamref name="TCollectionElement"/>.</param>
        void Add(Type type, IValidator validator);

        /// <summary>
        ///  Adds a validator to the pool of composite validators.
        /// </summary>
        /// <param name="validator">The validator to be added. The validator should be capable of validating instances of type <typeparamref name="TCollectionElement"/>.</param>
        /// <typeparam name="T">The type of instance to be validated.</typeparam>
        void Add<T>(IValidator<T> validator) where T: TCollectionElement;

        /// <summary>
        /// Adds validators to the pool of composite validators.
        /// </summary>
        /// <param name="validators">The validators to be added. Only non-null validators capable of validating instances of type <typeparamref name="TCollectionElement"/> shall be added.</param>
        /// <typeparam name="T">The type of instance to be validated.</typeparam>
        void AddRange<T>(IEnumerable<IValidator<T>> validators) where T: TCollectionElement;

        /// <summary>
        /// Adds validators to the pool of composite validators.
        /// </summary>
        /// <param name="tuples">Tuples containing validators to be added. Each validator of the tuple targets instances of the type declaration. Only non-null validators capable of validating instances of type <typeparamref name="TCollectionElement"/> shall be added. </param>
        void AddRange(IEnumerable<Tuple<Type, IValidator>> tuples);

        /// <summary>
        /// Removes a validator from the composite validator pool.
        /// </summary>
        /// <typeparam name="T">The type of instance validated by the validator.</typeparam>
        void Remove<T>() where T: TCollectionElement;

        /// <summary>
        /// Removes a validator from the composite validator pool.
        /// </summary>
        /// <param name="type">The type declaration of the instance that is validated by the validator.</param>
        void Remove(Type type);

        /// <summary>
        /// Removes all validators from the composite validator pool.
        /// </summary>
        void RemoveAll();
    }
}
