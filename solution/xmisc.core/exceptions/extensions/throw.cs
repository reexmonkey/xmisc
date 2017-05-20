using System;
using System.Linq.Expressions;

namespace reexmonkey.xmisc.core.exceptions.extensions
{
    /// <summary>
    /// Provides extensions for  the <see cref="Exception"/> class.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Throws the specified <paramref name="exception"/> when the <paramref name="predicate"/> is true.
        /// </summary>
        /// <typeparam name="TSource">The type of entity that is a potential source of the exception.</typeparam>
        /// <typeparam name="TException">The type of exception to throw.</typeparam>
        /// <param name="source">The source of the exception.</param>
        /// <param name="predicate">The condition that triggers the exception.</param>
        /// <param name="exception">The exception to throw, when the <paramref name="predicate"/> evaluates to true.</param>
        public static void ThrowOnCondition<TSource, TException>(this TSource source, Func<TSource, bool> predicate, TException exception)
            where TException : Exception
        {
            if (predicate(source)) throw exception;
        }

        /// <summary>
        /// Throws the specified <paramref name="exception"/> when the <paramref name="predicate"/> is true.
        /// </summary>
        /// <typeparam name="TSource">The type of entity, whose property is a potential source of the exception.</typeparam>
        /// <typeparam name="TException">The type of exception to throw.</typeparam>
        /// <param name="expression">The lambda expression that evaluates to a specified property of the source entity.</param>
        /// <param name="predicate">The condition that triggers the exception.</param>
        /// <param name="exception">The exception to throw, when the <paramref name="predicate"/> evaluates to true.</param>
        public static void ThrowOnCondition<TSource, TException>(
            this Expression<Func<TSource, object>> expression,
            Func<Expression<Func<TSource, object>>, bool> predicate,
            TException exception) where TException : Exception
        {
            if (predicate(expression)) throw exception;
        }

        /// <summary>
        /// Throws the specified <paramref name="exception"/> when the <paramref name="predicate"/> is
        /// </summary>
        /// <typeparam name="TSource">The type of entity, whose property a potential source of the exception.</typeparam>
        /// <typeparam name="TProperty">The type of property of the source entity.</typeparam>
        /// <typeparam name="TException">The type of exception to throw.</typeparam>
        /// <param name="expression">The lambda expression that evaluates to a specified property of the source entity.</param>
        /// <param name="predicate">The condition that triggers the exception.</param>
        /// <param name="exception">The exception to throw, when the <paramref name="predicate"/> evaluates to true.</param>
        public static void ThrowOnCondition<TSource, TProperty, TException>(
            this Expression<Func<TSource, TProperty>> expression,
            Func<Expression<Func<TSource, TProperty>>, bool> predicate,
            TException exception) where TException : Exception
        {
            if (predicate(expression)) throw exception;
        }

        /// <summary>
        /// Throws an <see cref="NullReferenceException"/> when the <paramref name="source"/> is a null reference.
        /// </summary>
        /// <typeparam name="TSource">The type of entity that is the source of the exception.</typeparam>
        /// <param name="source">The source of the exception.</param>
        public static void ThrowWhenNull<TSource>(this TSource source) where TSource : class
            => source.ThrowOnCondition(x => x == null, new NullReferenceException());

        /// <summary>
        /// Throws an <see cref="NullReferenceException"/> when the <paramref name="expression"/> evaluates to null.
        /// </summary>
        /// <typeparam name="TSource">The type of entity, whose property is the potential source of the exception.</typeparam>
        /// <param name="source">The entity whose property is a potential source of the exception.</param>
        /// <param name="expression">The lambda expression that evaluates to a specified property of the source entity.</param>
        public static void ThrowWhenNull<TSource>(this TSource source, Expression<Func<TSource, object>> expression)
            where TSource : class => expression.ThrowOnCondition(x => x.Compile()(source) == null, new NullReferenceException());

        /// <summary>
        /// Throws an <see cref="NullReferenceException"/> when the <paramref name="expression"/> evaluates to null.
        /// </summary>
        /// <typeparam name="TSource">The type of entity, whose property is the potential source of the exception.</typeparam>
        /// <typeparam name="TProperty">The type of property that is the potential source of the exceptiion.</typeparam>
        /// <param name="source">The entity whose property is a potential source of the exception.</param>
        /// <param name="expression">The lambda expression that evaluates to a specified property of the source entity.</param>
        public static void ThrowWhenNull<TSource, TProperty>(this TSource source, Expression<Func<TSource, TProperty>> expression)
            where TSource : class => expression.ThrowOnCondition(x => x.Compile()(source) == null, new NullReferenceException());

        /// <summary>
        /// Throws an <see cref="NullReferenceException"/> with a specified error message and the inner exception that triggered this null exception.
        /// </summary>
        /// <typeparam name="TSource">The type of exception source.</typeparam>
        /// <param name="source">The source of the exception.</param>
        /// <param name="message">A message that describes the error.</param>
        public static void ThrowWhenNull<TSource>(this TSource source, string message) where TSource : class
        {
            source.ThrowOnCondition(x => x == null, new NullReferenceException(message));
        }

        /// <summary>
        /// Throws an <see cref="NullReferenceException"/> with a specified error message, when the <paramref name="expression"/> evaluates to null.
        /// </summary>
        /// <typeparam name="TSource">The type of entity, whose property is the potential source of the exception.</typeparam>
        /// <param name="source">The entity whose property causes the exception.</param>
        /// <param name="expression">The lambda expression that evaluates to a specified property of the source entity.</param>
        /// <param name="message">A message that describes the error.</param>
        public static void ThrowWhenNull<TSource>(this TSource source, Expression<Func<TSource, object>> expression, string message) where TSource : class
            => expression.ThrowOnCondition(x => x.Compile()(source) == null, new NullReferenceException(message));

        /// <summary>
        /// Throws an <see cref="NullReferenceException"/> with a specified error message, when the <paramref name="expression"/> evaluates to null.
        /// </summary>
        /// <typeparam name="TSource">The type of entity, whose property is the potential source of the exception.</typeparam>
        /// <typeparam name="TProperty">The type of property that is the potential source of the exceptiion.</typeparam>
        /// <param name="source">The entity whose property causes the exception.</param>
        /// <param name="expression">The lambda expression that evaluates to a specified property of the source entity.</param>
        /// <param name="message">A message that describes the error.</param>
        public static void ThrowWhenNull<TSource, TProperty>(this TSource source, Expression<Func<TSource, TProperty>> expression, string message) where TSource : class
            => expression.ThrowOnCondition(x => x.Compile()(source) == null, new NullReferenceException(message));
    }
}