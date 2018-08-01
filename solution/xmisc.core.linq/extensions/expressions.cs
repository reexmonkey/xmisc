using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace reexmonkey.xmisc.core.linq.extensions
{
    /// <summary>
    /// Extends the features of the <see cref="Expression"/> class.
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Produces the name of a member in a lambda expression
        /// </summary>
        /// <param name="expression">The lambda expression containing a membe</param>
        /// <returns>The name of the member</returns>
        public static string GetMemberName(this LambdaExpression expression)
        {
            string Selector(Expression e)
            {
                switch (e.NodeType)
                {
                    case ExpressionType.Parameter:
                        return ((ParameterExpression)e).Name;

                    case ExpressionType.MemberAccess:
                        return ((MemberExpression)e).Member.Name;

                    case ExpressionType.Call:
                        return ((MethodCallExpression)e).Method.Name;

                    case ExpressionType.Convert:
                    case ExpressionType.ConvertChecked:
                        return Selector(((UnaryExpression)e).Operand);

                    case ExpressionType.Invoke:
                        return Selector(((InvocationExpression)e).Expression);

                    case ExpressionType.ArrayLength:
                        return "Length";

                    default:
                        throw new Exception("not a proper member selector");
                }
            }

            return Selector(expression.Body);
        }

        /// <summary>
        /// Produces a sequence of names of mebers found in a lambda expression
        /// </summary>
        /// <param name="expression">The lambda expression containing members</param>
        /// <returns>The sequence of member names</returns>
        public static IEnumerable<string> GetMemberNames(this LambdaExpression expression)
        {
            IEnumerable<string> Selector(Expression e)
            {
                switch (e.NodeType)
                {
                    case ExpressionType.Parameter:
                        return ((ParameterExpression)e).Name.AsSingleton();

                    case ExpressionType.MemberAccess:
                        return ((MemberExpression)e).Member.Name.AsSingleton();

                    case ExpressionType.New:
                        return ((NewExpression)e).Members.Select(x => x.Name);

                    case ExpressionType.Call:
                        return ((MethodCallExpression)e).Method.Name.AsSingleton();

                    case ExpressionType.Convert:
                    case ExpressionType.ConvertChecked:
                        return Selector(((UnaryExpression)e).Operand);

                    case ExpressionType.Invoke:
                        return Selector(((InvocationExpression)e).Expression);

                    case ExpressionType.ArrayLength:
                        return "Length".AsSingleton();

                    default:
                        throw new Exception("not a proper member selector");
                }
            }

            return Selector(expression.Body);
        }

        /// <summary>
        /// Combines two expression that encapsulate lambda functions <see cref="Func{T, TResult}"/> using the "OR" logic.
        /// </summary>
        /// <typeparam name="T">The type specified in the lambda functions.</typeparam>
        /// <param name="this">The first exprsession to combine.</param>
        /// <param name="other">The second expression to combine.</param>
        /// <returns>The "OR" combination of the expressions.</returns>
        public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> @this, Expression<Func<T, bool>> other)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(@this.Parameters[0], parameter);
            var left = leftVisitor.Visit(@this.Body);

            var rightVisitor = new ReplaceExpressionVisitor(other.Parameters[0], parameter);
            var right = rightVisitor.Visit(other.Body);

            return Expression.Lambda<Func<T, bool>>(
                Expression.OrElse(left, right), parameter);
        }

        /// <summary>
        /// Combines two expression that encapsulate lambda functions <see cref="Func{T, TResult}"/> using the "AND" logic.
        /// </summary>
        /// <typeparam name="T">The type specified in the lambda functions.</typeparam>
        /// <param name="this">The first exprsession to combine.</param>
        /// <param name="other">The second expression to combine.</param>
        /// <returns>The "AND" combination of the expressions.</returns>
        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> @this, Expression<Func<T, bool>> other)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(@this.Parameters[0], parameter);
            var left = leftVisitor.Visit(@this.Body);

            var rightVisitor = new ReplaceExpressionVisitor(other.Parameters[0], parameter);
            var right = rightVisitor.Visit(other.Body);

            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(left, right), parameter);
        }

        private class ReplaceExpressionVisitor : ExpressionVisitor
        {
            private readonly Expression oldValue;
            private readonly Expression newValue;

            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                this.oldValue = oldValue;
                this.newValue = newValue;
            }

            public override Expression Visit(Expression node)
            {
                if (node == oldValue)
                    return newValue;
                return base.Visit(node);
            }
        }
    }
}
