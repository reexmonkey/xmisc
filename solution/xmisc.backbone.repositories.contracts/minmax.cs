using System;
using System.Linq.Expressions;
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
        /// Evaluates a scalar expression and returns the smallest value for a specified attribute.
        /// </summary>
        /// <typeparam name="T">The type of scalar result.</typeparam>
        /// <param name="field">The attribute in the scalar expression, for which the smallest value shall be evaluated.</param>
        /// <returns>The smallest value of the selected attribute.</returns>
        T Min<T>(Expression<Func<TModel, object>> field)
            where T : IEquatable<T>, IComparable, IComparable<T>;

        /// <summary>
        /// Evaluates a scalar expression and returns the smallest value for a specified attribute for a filtered set of data models.
        /// </summary>
        /// <typeparam name="T">The type of scalar result</typeparam>
        /// <param name="field">The attribute in the scalar expression, for which the smallest value shall be evaluated.</param>
        /// <param name="predicate">Filters the set of data models for which the scalar expression shall be evaluated.</param>
        /// <returns>The smallest value of the selected attribute.</returns>
        T Min<T>(Expression<Func<TModel, object>> field, Expression<Func<TModel, bool>> predicate)
            where T : IEquatable<T>, IComparable, IComparable<T>;

        /// <summary>
        /// Evaluates a scalar expression and returns the largest value for a specified attribute.
        /// </summary>
        /// <typeparam name="T">The type of scalar result.</typeparam>
        /// <param name="field">The attribute in the scalar expression, for which the largest value shall be evaluated.</param>
        /// <returns>The largest value of the selected attribute.</returns>
        T Max<T>(Expression<Func<TModel, object>> field)
            where T : IEquatable<T>, IComparable, IComparable<T>;

        /// <summary>
        /// Evaluates a scalar expression and returns the largest value for a specified attribute for a filtered set of data models.
        /// </summary>
        /// <typeparam name="T">The type of scalar result</typeparam>
        /// <param name="field">The attribute in the scalar expression, for which the largest value shall be evaluated.</param>
        /// <param name="predicate">Filters the set of data models for which the scalar expression shall be evaluated.</param>
        /// <returns>The largest value of the selected attribute.</returns>
        T Max<T>(Expression<Func<TModel, object>> field, Expression<Func<TModel, bool>> predicate)
            where T : IEquatable<T>, IComparable, IComparable<T>;

        /// <summary>
        /// Evaluates a scalar expression asynchronously and returns the smallest value for a specified attribute.
        /// </summary>
        /// <typeparam name="T">The type of scalar result.</typeparam>
        /// <param name="field">The attribute in the scalar expression, for which the smallest value shall be evaluated.</param>
        /// <returns>The smallest value of the selected attribute.</returns>

        Task<T> MinAsync<T>(Expression<Func<TModel, object>> field)
            where T : IEquatable<T>, IComparable, IComparable<T>;

        /// <summary>
        /// Evaluates a scalar expression asynchronously and returns the smallest value for a specified attribute for a filtered set of data models.
        /// </summary>
        /// <typeparam name="T">The type of scalar result</typeparam>
        /// <param name="field">The attribute in the scalar expression, for which the smallest value shall be evaluated.</param>
        /// <param name="predicate">Filters the set of data models for which the scalar expression shall be evaluated.</param>
        /// <returns>The smallest value of the selected attribute.</returns>
        Task<T> MinAsync<T>(Expression<Func<TModel, object>> field, Expression<Func<TModel, bool>> predicate)
            where T : IEquatable<T>, IComparable, IComparable<T>;

        /// <summary>
        /// Evaluates a scalar expression asynchronously and returns the largest value for a specified attribute.
        /// </summary>
        /// <typeparam name="T">The type of scalar result.</typeparam>
        /// <param name="field">The attribute in the scalar expression, for which the largest value shall be evaluated.</param>
        /// <returns>The largest value of the selected attribute.</returns>
        Task<T> MaxAsync<T>(Expression<Func<TModel, object>> field)
            where T : IEquatable<T>, IComparable, IComparable<T>;

        /// <summary>
        /// Evaluates a scalar expression asynchronously and returns the largest value for a specified attribute for a filtered set of data models.
        /// </summary>
        /// <typeparam name="T">The type of scalar result</typeparam>
        /// <param name="field">The attribute in the scalar expression, for which the largest value shall be evaluated.</param>
        /// <param name="predicate">Filters the set of data models for which the scalar expression shall be evaluated.</param>
        /// <returns>The largest value of the selected attribute.</returns>
        Task<T> MaxAsync<T>(Expression<Func<TModel, object>> field, Expression<Func<TModel, bool>> predicate)
            where T : IEquatable<T>, IComparable, IComparable<T>;
    }
}
