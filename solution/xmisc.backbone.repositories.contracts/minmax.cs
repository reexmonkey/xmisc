using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies a repository that returns a minimum or maximum scalar result after the evaluation of a scalar expression.
    /// </summary>
    /// <typeparam name="TModel">The type of data model, for which the scalar expression is evaluated.</typeparam>
    public interface IMinMaxRepository<TModel>
    {
        /// <summary>
        /// Evaluates an internal scalar expression and returns the smallest value.
        /// <para /> The internal scalar expression is to be defined at the implementation of this method.
        /// </summary>
        /// <typeparam name="T">The type of scalar result.</typeparam>
        /// <returns>The smallest value from the internal evaluation of the scalar expression.</returns>
        T Min<T>() where T : IEquatable<T>, IComparable, IComparable<T>;

        /// <summary>
        /// Evaluates an internal scalar expression and returns the smallest value for a set of data models that satisfy the specified predicate.
        /// </summary>
        /// <typeparam name="T">The type of scalar result.</typeparam>
        /// <param name="predicate">Filters the set of data models for which the scalar expression shall be evaluated.</param>
        /// <returns>The smallest value from the internal evaluation of the scalar expression.</returns>
        T Min<T>(Expression<Func<TModel, bool>> predicate)
            where T : IEquatable<T>, IComparable, IComparable<T>;

        /// <summary>
        /// Evaluates an internal scalar expression and returns the largest value.
        /// <para /> The internal scalar expression is to be defined at the implementation of this method.
        /// </summary>
        /// <typeparam name="T">The type of scalar result.</typeparam>
        /// <returns>The largest value from the internal evaluation of the scalar expression.</returns>
        T Max<T>() where T : IEquatable<T>, IComparable, IComparable<T>;

        /// <summary>
        /// Evaluates an internal scalar expression and returns the largest value for a set of data models that satisfy the specified predicate.
        /// </summary>
        /// <typeparam name="T">The type of scalar result.</typeparam>
        /// <param name="predicate">Filters the set of data models for which the scalar expression shall be evaluated.</param>
        /// <returns>The largest value from the internal evaluation of the scalar expression.</returns>
        T Max<T>(Expression<Func<TModel, bool>> predicate)
            where T : IEquatable<T>, IComparable, IComparable<T>;

        /// <summary>
        /// Evaluates an internal scalar expression asynchronously and returns the smallest value.
        /// <para /> The internal scalar expression is to be defined at the implementation of this method.
        /// </summary>
        /// <typeparam name="T">The type of scalar result.</typeparam>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to return the smallest value from the internal evaluation of the scalar expression.</returns>
        Task<T> MinAsync<T>(CancellationToken token = default(CancellationToken))
            where T : IEquatable<T>, IComparable, IComparable<T>;

        /// <summary>
        /// Evaluates an internal scalar expression asynchronously and returns the smallest value for a set of data models that satisfy the specified predicate.
        /// </summary>
        /// <typeparam name="T">The type of scalar result.</typeparam>
        /// <param name="predicate">Filters the set of data models for which the scalar expression shall be evaluated.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to return the smallest value from the internal evaluation of the scalar expression.</returns>
        Task<T> MinAsync<T>(Expression<Func<TModel, bool>> predicate, CancellationToken token = default(CancellationToken))
            where T : IEquatable<T>, IComparable, IComparable<T>;

        /// <summary>
        /// Evaluates an internal scalar expression asynchronously and returns the largest value.
        /// <para /> The internal scalar expression is to be defined at the implementation of this method.
        /// </summary>
        /// <typeparam name="T">The type of scalar result.</typeparam>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to return the largest value from the internal evaluation of the scalar expression.</returns>
        Task<T> MaxAsync<T>(CancellationToken token = default(CancellationToken))
            where T : IEquatable<T>, IComparable, IComparable<T>;

        /// <summary>
        /// Evaluates an internal scalar expression asynchronously and returns the largest value for a set of data models that satisfy the specified predicate.
        /// </summary>
        /// <typeparam name="T">The type of scalar result.</typeparam>
        /// <param name="predicate">Filters the set of data models for which the scalar expression shall be evaluated.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to return the largest value from the internal evaluation of the scalar expression.</returns>
        Task<T> MaxAsync<T>(Expression<Func<TModel, bool>> predicate, CancellationToken token = default(CancellationToken))
            where T : IEquatable<T>, IComparable, IComparable<T>;
    }
}
