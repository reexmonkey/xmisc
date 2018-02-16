using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace xmisc.backbone.validation.contracts.Infrastructure
{
    /// <summary>
    /// Represents a base extension of the <see cref="ValidationResult"/> class.
    /// </summary>
    public abstract class AbstractValidationResult : ValidationResult
    {
        /// <summary>
        /// Creates a new instance of the <see cref="AbstractValidationResult"/> class.
        /// </summary>
        protected AbstractValidationResult()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractValidationResult"/> class with a
        /// sequence of failures.
        /// </summary>
        /// <param name="failures">The sequeence of failures to initialize this instance with.</param>
        protected AbstractValidationResult(IEnumerable<AbstractValidationFailure> failures) : base(failures)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractValidationResult"/> class with
        /// failures derived from a specified result.
        /// </summary>
        /// <param name="result">The result, from which the failures are derived.</param>
        /// <param name="transform">
        /// The function that transforms each error from the specified <paramref name="result"/> into
        /// a validation failure.
        /// </param>
        protected AbstractValidationResult(ValidationResult result, Func<ValidationFailure, AbstractValidationFailure> transform)
        {
            if (transform == null) throw new ArgumentNullException(nameof(transform));
            AddErrors(result.Errors.Select(transform));
        }

        /// <summary>
        /// Gets the validation failures from this result.
        /// </summary>
        public new IList<AbstractValidationFailure> Errors => base.Errors.Cast<AbstractValidationFailure>().ToList();

        /// <summary>
        /// Adds a failure to the collection of errors owned by this result.
        /// </summary>
        /// <param name="error"></param>
        public void AddError(AbstractValidationFailure error)
        {
            error.Parent = this;
            Errors.Add(error);
        }

        /// <summary>
        /// Adds a sequence of failures to the collection of errors owned by this result.
        /// </summary>
        /// <param name="errors"></param>
        public void AddErrors(IEnumerable<AbstractValidationFailure> errors)
        {
            foreach (var error in errors) Errors.Add(error);
        }

        /// <summary>
        /// Removes the specified failure from the collection of errors owned by this result.
        /// </summary>
        /// <param name="error"></param>
        public void RemoveError(AbstractValidationFailure error) => Errors.Remove(error);

        /// <summary>
        /// Removes the specified sequence of failures from the collection of errors owned by this result.
        /// </summary>
        /// <param name="errors"></param>
        public void RemoveErrors(IEnumerable<AbstractValidationFailure> errors)
        {
            foreach (var error in errors) Errors.Remove(error);
        }

        /// <summary>
        /// Gets the sequence of failures owned by this result.
        /// <para/>
        /// The retrieval can be paged in sets by providing values for the <paramref name="count"/>
        /// and <paramref name="offset"/> parameters.
        /// </summary>
        /// <param name="offset">
        /// The number of failures to bypass from the collection of failures owned by this result.
        /// </param>
        /// <param name="count">
        /// The number of contiguous failures from the specified <paramref name="offset"/> position
        /// in the collection of failures.
        /// </param>
        /// <returns></returns>
        public IEnumerable<AbstractValidationFailure> GetErrors(int? offset, int? count)
            => offset != null && count != null ? Errors.Skip(offset.Value).Take(count.Value) : Errors;

        /// <summary>
        /// Filters the sequence of failures based on a predicate.
        /// <para/>
        /// The retrieval can be paged in sets by providing values for the <paramref name="count"/>
        /// and <paramref name="offset"/> parameters.
        /// </summary>
        /// <param name="predicate">A function to test each failure for a condition.</param>
        /// <param name="offset">
        /// The number of failures to bypass from the collection of failures owned by this result.
        /// </param>
        /// <param name="count">
        /// The number of contiguous failures from the specified <paramref name="offset"/> position
        /// in the collection of failures.
        /// </param>
        /// <returns></returns>
        public IEnumerable<AbstractValidationFailure> FindErrors(Func<AbstractValidationFailure, bool> predicate, int? offset, int? count)
            => offset != null && count != null
            ? Errors.Where(predicate).Skip(offset.Value).Take(count.Value)
            : Errors.Where(predicate);
    }

    /// <summary>
    /// Represents a base extension of the <see cref="AbstractValidationResult"/> class with a
    /// specified custom state.
    /// </summary>
    /// <typeparam name="TState">
    /// The type of custom state associated to all of the failures owned by this result.
    /// </typeparam>
    public abstract class AbstractValidationResult<TState> : AbstractValidationResult
    {
        /// <summary>
        /// Creates a new instance of the <see cref="AbstractValidationResult{TState}"/> class.
        /// </summary>
        protected AbstractValidationResult()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractValidationResult{TState}"/> class
        /// with a sequence of failures.
        /// </summary>
        /// <param name="failures">The sequeence of failures to initialize this instance with.</param>
        protected AbstractValidationResult(IEnumerable<AbstractValidationFailure<TState>> failures)
            : base(failures)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractValidationResult"/> class with
        /// failures derived from a specified result.
        /// </summary>
        /// <param name="result">The result, from which the failures are derived.</param>
        /// <param name="transform">
        /// The function that transforms each error from the specified <paramref name="result"/> into
        /// a validation failure.
        /// </param>
        protected AbstractValidationResult(ValidationResult result, Func<ValidationFailure, AbstractValidationFailure<TState>> transform)
            : base(result, transform)
        {
        }
    }
}
