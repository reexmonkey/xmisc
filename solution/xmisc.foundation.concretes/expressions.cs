using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace reexjungle.xmisc.foundation.concretes
{
    /// <summary>
    /// Provides extended funtionality to lambda epxressions
    /// </summary>
    public static class LambdaExpressionExtensions
    {
        /// <summary>
        /// Produces the name of a member in a lambda expression
        /// </summary>
        /// <param name="expression">The lambda expression containing a membe</param>
        /// <returns>The name of the member</returns>
        public static string GetMemberName(this LambdaExpression expression)
        {
            Func<Expression, string> selector = null;  //recursive func
            selector = e => //or move the entire thing to a separate recursive method
            {
                switch (e.NodeType)
                {
                    case ExpressionType.Parameter: return ((ParameterExpression)e).Name;
                    case ExpressionType.MemberAccess: return ((MemberExpression)e).Member.Name;
                    case ExpressionType.Call: return ((MethodCallExpression)e).Method.Name;
                    case ExpressionType.Convert:
                    case ExpressionType.ConvertChecked: return selector(((UnaryExpression)e).Operand);
                    case ExpressionType.Invoke: return selector(((InvocationExpression)e).Expression);
                    case ExpressionType.ArrayLength: return "Length";
                    default:
                        throw new Exception("not a proper member selector");
                }
            };

            return selector(expression.Body);
        }

        /// <summary>
        /// Produces a sequence of names of mebers found in a lambda expression
        /// </summary>
        /// <param name="expression">The lambda expression containing members</param>
        /// <returns>The sequence of member names</returns>
        public static IEnumerable<string> GetMemberNames(this LambdaExpression expression)
        {
            Func<Expression, IEnumerable<string>> selector = null;
            selector = e =>
            {
                switch (e.NodeType)
                {
                    case ExpressionType.Parameter: return ((ParameterExpression)e).Name.ToSingleton();
                    case ExpressionType.MemberAccess: return ((MemberExpression)e).Member.Name.ToSingleton();
                    case ExpressionType.New: return ((NewExpression)e).Members.Select(x => x.Name);
                    case ExpressionType.Call: return ((MethodCallExpression)e).Method.Name.ToSingleton();
                    case ExpressionType.Convert:
                    case ExpressionType.ConvertChecked: return selector(((UnaryExpression)e).Operand);
                    case ExpressionType.Invoke: return selector(((InvocationExpression)e).Expression);
                    case ExpressionType.ArrayLength: return "Length".ToSingleton();
                    default:
                        throw new Exception("not a proper member selector");
                }
            };

            return selector(expression.Body);
        }
    }
}