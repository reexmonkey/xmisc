using System;
using System.Linq.Expressions;
using System.Numerics;

namespace reexjungle.xmisc.foundation.concretes
{
    /// <summary>
    /// Provides extended functionality to mathematcal functions
    /// </summary>
    public static class MathExtensions
    {
        #region expression arithmetic operations

        private static Expression AddExpression<T>(this T a, T b)
            where T : struct, IComparable<T>, IComparable, IConvertible, IEquatable<T>
        {
            return Expression.AddChecked(Expression.Parameter(typeof(T)), Expression.Parameter(typeof(T)));
        }

        private static Expression SubstractExpression<T>(this T a, T b)
    where T : struct, IComparable<T>, IComparable, IConvertible, IEquatable<T>
        {
            return Expression.SubtractChecked(Expression.Parameter(typeof(T)), Expression.Parameter(typeof(T)));
        }

        private static Expression MultiplyByExpression<T>(this T a, T b)
    where T : struct, IComparable<T>, IComparable, IConvertible, IEquatable<T>
        {
            return Expression.MultiplyChecked(Expression.Parameter(typeof(T)), Expression.Parameter(typeof(T)));
        }

        private static Expression DivideByExpression<T>(this T a, T b)
    where T : struct, IComparable<T>, IComparable, IConvertible, IEquatable<T>
        {
            return Expression.Divide(Expression.Parameter(typeof(T)), Expression.Parameter(typeof(T)));
        }

        private static Expression IntegralDivideByExpression<T>(this T a, T b)
            where T : struct, IComparable<T>, IComparable, IConvertible, IEquatable<T>
        {
            var dividend = Expression.Parameter(typeof(T)); ;
            var divisor = Expression.Parameter(typeof(T)); ;
            var temp = Expression.Variable(typeof(T));
            var result = Expression.Label(typeof(T));
            return Expression.Block
                (
                    Expression.IfThen(Expression.LessThan(dividend, divisor), //if (n < d)
                    Expression.Return(result, Expression.Constant(a.Zero(), typeof(T)))), // return 0;
                    Expression.IfThenElse(Expression.Or(Expression.TypeIs(dividend, typeof(double)), Expression.TypeIs(dividend, typeof(decimal))),
                    Expression.Block
                    (
                        new[] { temp },
                        Expression.Assign(temp, Expression.Divide(dividend, divisor)),
                        Expression.Return(result, Expression.Call(typeof(Math).GetMethod("Truncate"), temp))
                    ),
                    Expression.IfThenElse(Expression.TypeIs(dividend, typeof(float)),
                    Expression.Return(result, Expression.Convert(Expression.Divide(dividend, divisor), typeof(int))),
                    Expression.Return(result, Expression.Divide(dividend, divisor), typeof(T))))
                );
        }

        private static Expression AbsoluteExpression<T>(this T a)
    where T : struct, IComparable<T>, IComparable, IConvertible, IEquatable<T>
        {
            var x = Expression.Parameter(typeof(T));
            var result = Expression.Label(typeof(T));
            return Expression.Block
                (
                    Expression.IfThenElse(Expression.LessThan(x, Expression.Constant(a.Zero(), typeof(T))),
                    Expression.Return(result, Expression.NegateChecked(x)),
                    Expression.Return(result, x))
                );
        }

        private static Expression IntegralExpression<T>(this T a)
    where T : struct, IComparable<T>, IComparable, IConvertible, IEquatable<T>
        {
            var x = Expression.Parameter(typeof(T), "x");
            var temp = Expression.Variable(typeof(T), "temp");
            var result = Expression.Label(typeof(T), "result");
            return Expression.Block
                (
                    Expression.IfThenElse(Expression.Or(Expression.TypeIs(x, typeof(double)), Expression.TypeIs(x, typeof(decimal))),
                    Expression.Return(result, Expression.Call(typeof(Math).GetMethod("Truncate"), temp)),
                    Expression.IfThenElse(Expression.TypeIs(x, typeof(float)),
                    Expression.Return(result, Expression.Convert(x, typeof(int))),
                    Expression.Return(result, x, typeof(T))))
                );
        }

        private static Expression NegativeModuloExpression<T>(this T a, T b)
            where T : struct, IComparable<T>, IComparable, IConvertible, IEquatable<T>
        {
            var x = Expression.Parameter(typeof(T), "x");
            var y = Expression.Parameter(typeof(T), "y");
            var m = Expression.Increment(Expression.Divide(a.Abs().IntegralExpression(), (b).IntegralExpression()));
            return Expression.AddChecked(x, Expression.Multiply(y, m));
        }

        private static Expression PositiveModuloExpression<T>(this T a, T b)
    where T : struct, IComparable<T>, IComparable, IConvertible, IEquatable<T>
        {
            var x = Expression.Parameter(typeof(T), "x");
            var y = Expression.Parameter(typeof(T), "y");
            return Expression.Modulo(x, y);
        }

        private static Expression ModuloExpression<T>(this T a, T b)
    where T : struct, IComparable<T>, IComparable, IConvertible, IEquatable<T>
        {
            var zero = Expression.Constant(a.Zero(), typeof(T));
            var x = Expression.Parameter(typeof(T), "x");
            var y = Expression.Parameter(typeof(T), "y");
            var temp = Expression.Parameter(typeof(T), "temp");
            var result = Expression.Label(typeof(T), "result");
            //
            return Expression.IfThenElse(Expression.Equal(x, zero), //a < 0 but a is a multiple of b. ie a = b*m; m >0
            Expression.Return(result, zero),
            Expression.IfThenElse(Expression.LessThan(x, zero),
            Expression.Block
            (
                   new[] { temp },
                   Expression.Assign(temp, a.Abs().PositiveModuloExpression(b)), //pos_mod(abs(a),b);
                   Expression.IfThenElse(Expression.Equal(temp, zero),
                   Expression.Return(result, zero),
                   Expression.Block
                   (
                      Expression.IfThenElse(Expression.GreaterThan(y, zero), //a < 0; b > 0; a != b*m; m >0
                      Expression.Return(result, a.NegativeModuloExpression(b)),
                      Expression.IfThenElse(Expression.LessThan(y, zero), //a < 0; b < 0; a != b*m; m >0
                      Expression.Negate(Expression.Modulo(a.AbsoluteExpression(), b.AbsoluteExpression())), //get the positve mod but with a negative answer
                      Expression.Throw(Expression.Constant(new DivideByZeroException("Divisor must not be a zero-value!")))))
                   ))
            ),
            Expression.Block // a>0
            (
                   Expression.IfThenElse(Expression.GreaterThan(y, zero), //pos_mod(a, b); // a > 0; b > 0; normal case
                   Expression.Return(result, a.PositiveModuloExpression(b)), //return a % b
                   Expression.IfThenElse(Expression.LessThan(y, zero), //a < 0; b > 0; a != b*m; m >0
                   Expression.Block
                   (
                      new[] { temp },
                      Expression.Assign(temp, Expression.Modulo(x, b.AbsoluteExpression())),
                      Expression.IfThenElse(Expression.Equal(temp, zero),
                      Expression.Return(result, zero),
                      Expression.Return(result, a.Negate().NegativeModuloExpression(b.Abs())))

                   ),
                   Expression.Throw(Expression.Constant(new DivideByZeroException("Divisor must not be a zero-value!")))))

            )));
        }

        #endregion expression arithmetic operations

        #region lambda functions

        /// <summary>
        /// Performs an arithmetic addition of two IComparable generic operands
        /// </summary>
        /// <typeparam name="T">The type parameter of the added operands</typeparam>
        /// <param name="a">This operand needed for the addition operation</param>
        /// <param name="b">The other operand needed for the addition operation</param>
        /// <returns>The sum of the two operands</returns>
        /// <exception cref="ArgumentNullException">a or b is null</exception>
        /// <exception cref="InvalidOperationException">The addition operator is not defined for a.Type and b.Type</exception>
        public static T Add<T>(this T a, T b)
            where T : struct, IComparable<T>, IComparable, IConvertible, IEquatable<T>
        {
            var func = Expression.Lambda<Func<T, T, T>>(a.AddExpression(b), Expression.Parameter(typeof(T)), Expression.Parameter(typeof(T)));
            return func.Compile()(a, b);
        }

        /// <summary>
        /// Performs an arithmetic substraction of two IComparable generic operands
        /// </summary>
        /// <typeparam name="T">The type parameter of the substracted operands</typeparam>
        /// <param name="a">This operand needed for the substraction operation</param>
        /// <param name="b">The other operand needed for the substraction operation</param>
        /// <returns>The difference of the two operands</returns>
        /// <exception cref="ArgumentNullException">a or b is null</exception>
        /// <exception cref="InvalidOperationException">The substraction operator is not defined for a.Type and b.Type</exception>
        public static T Substract<T>(this T a, T b)
            where T : struct, IComparable<T>, IComparable, IConvertible, IEquatable<T>
        {
            var func = Expression.Lambda<Func<T, T, T>>(a.SubstractExpression(b), Expression.Parameter(typeof(T)), Expression.Parameter(typeof(T)));
            return func.Compile()(a, b);
        }

        /// <summary>
        /// Performs an arithmetic multiplication of two IComparable generic operands
        /// </summary>
        /// <typeparam name="T">The type parameter of the multiplied operands</typeparam>
        /// <param name="a">This operand needed for the multiplication operation</param>
        /// <param name="b">The other operand needed for the multiplication operation</param>
        /// <returns>The product of the two operands</returns>
        /// <exception cref="ArgumentNullException">a or b is null</exception>
        /// <exception cref="InvalidOperationException">The multiplication operator is not defined for a.Type and b.Type</exception>
        public static T MultiplyBy<T>(this T a, T b)
            where T : struct, IComparable<T>, IComparable, IConvertible, IEquatable<T>
        {
            var func = Expression.Lambda<Func<T, T, T>>(a.MultiplyByExpression(b), Expression.Parameter(typeof(T)), Expression.Parameter(typeof(T)));
            return func.Compile()(a, b);
        }

        /// <summary>
        /// Performs an arithmetic division of two IComparable generic operands
        /// </summary>
        /// <typeparam name="T">The type parameter of the divided operands</typeparam>
        /// <param name="a">This operand needed for the division operation</param>
        /// <param name="b">The other operand needed for the division operation</param>
        /// <returns>The quotient of the two operands</returns>
        /// <exception cref="ArgumentNullException">a or b is null</exception>
        /// <exception cref="InvalidOperationException">The division operator is not defined for a.Type and b.Type</exception>
        public static T DivideBy<T>(this T a, T b)
            where T : struct, IComparable<T>, IComparable, IConvertible, IEquatable<T>
        {
            if (b.Equal(b.Zero())) throw new DivideByZeroException("Divisor must not be a zero-value!");
            var func = Expression.Lambda<Func<T, T, T>>(a.DivideByExpression(b), Expression.Parameter(typeof(T)), Expression.Parameter(typeof(T)));
            return func.Compile()(a, b);
        }

        /// <summary>
        /// Performs a greater-than comparison between two generic IComparable operands.
        /// </summary>
        /// <typeparam name="T">The type of operand</typeparam>
        /// <param name="first">An operand to be tested</param>
        /// <param name="second">Another operand to be tested</param>
        /// <returns>True if the first operand is greater than the second</returns>
        public static bool GreaterThan<T>(this T first, T second)
            where T : struct, IComparable<T>, IComparable, IConvertible, IEquatable<T>
        {
            var x = Expression.Parameter(typeof(T));
            var y = Expression.Parameter(typeof(T));
            var func = Expression.Lambda<Func<T, T, bool>>(Expression.GreaterThan(x, y), x, y);
            return func.Compile()(first, second);
        }

        /// <summary>
        /// Performs a greater-than-or-equal-to comparison between two generic IComparable operands.
        /// </summary>
        /// <typeparam name="T">The type of operand</typeparam>
        /// <param name="first">An operand to be tested</param>
        /// <param name="second">Another operand to be tested</param>
        /// <returns>True if the first operand is greater than or equal to the second</returns>
        public static bool GreaterThanOrEqual<T>(this T first, T second)
            where T : struct, IComparable<T>, IComparable, IConvertible, IEquatable<T>
        {
            var x = Expression.Parameter(typeof(T));
            var y = Expression.Parameter(typeof(T));
            var func = Expression.Lambda<Func<T, T, bool>>(Expression.GreaterThanOrEqual(x, y), x, y);
            return func.Compile()(first, second);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool LessThan<T>(this T a, T b)
            where T : struct, IComparable<T>, IComparable, IConvertible, IEquatable<T>
        {
            var x = Expression.Parameter(typeof(T));
            var y = Expression.Parameter(typeof(T));
            var func = Expression.Lambda<Func<T, T, bool>>(Expression.LessThan(x, y), x, y);
            return func.Compile()(a, b);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool LessThanOrEqual<T>(this T a, T b)
            where T : struct, IComparable<T>, IComparable, IConvertible, IEquatable<T>
        {
            var x = Expression.Parameter(typeof(T));
            var y = Expression.Parameter(typeof(T));
            var func = Expression.Lambda<Func<T, T, bool>>(Expression.LessThanOrEqual(x, y), x, y);
            return func.Compile()(a, b);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Equal<T>(this T a, T b)
            where T : struct, IComparable<T>, IComparable, IConvertible, IEquatable<T>
        {
            var x = Expression.Parameter(typeof(T));
            var y = Expression.Parameter(typeof(T));
            var func = Expression.Lambda<Func<T, T, bool>>(Expression.GreaterThan(x, y), x, y);
            return func.Compile()(a, b);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <returns></returns>
        public static T Zero<T>(this T a)
    where T : struct, IComparable<T>, IComparable, IConvertible, IEquatable<T>
        {
            return default(T);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static T IntegralDivideBy<T>(this T a, T b)
    where T : struct, IComparable<T>, IComparable, IConvertible, IEquatable<T>
        {
            if (b.Equal(b.Zero())) throw new DivideByZeroException("Divisor must not be a zero-value!"); ;
            var func = Expression.Lambda<Func<T, T, T>>(a.IntegralDivideByExpression(b), Expression.Parameter(typeof(T)), Expression.Parameter(typeof(T)));
            return func.Compile()(a, b);
        }

        /// <summary>
        /// Finds the arithmetic absolute value of this IComparable generic operand
        /// </summary>
        /// <typeparam name="T">The type parameter of the operand</typeparam>
        /// <param name="a">The operand needed for the absolute operation</param>
        /// <returns>The absolute value of the operation</returns>
        /// <exception cref="ArgumentNullException">a is null</exception>
        /// <exception cref="InvalidOperationException">The modulo operator is not defined for a.Type</exception>
        public static T Abs<T>(this T a)
            where T : struct, IComparable<T>, IComparable, IConvertible, IEquatable<T>
        {
            return Expression.Lambda<Func<T, T>>(a.AbsoluteExpression(), Expression.Parameter(typeof(T))).Compile()(a);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <returns></returns>
        public static T Negate<T>(this T a)
    where T : struct, IComparable<T>, IComparable, IConvertible, IEquatable<T>
        {
            return Expression.Lambda<Func<T, T>>(Expression.NegateChecked(Expression.Parameter(typeof(T))), Expression.Parameter(typeof(T))).Compile()(a);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <returns></returns>
        public static T Truncate<T>(this T a)
    where T : struct, IComparable<T>, IComparable, IConvertible, IEquatable<T>
        {
            if (!(a is float) && !(a is double)) return a;
            return Expression.Lambda<Func<T, T>>(a.IntegralExpression(), Expression.Parameter(typeof(T))).Compile()(a);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static T NegativeModulo<T>(this T a, T b)
    where T : struct, IComparable<T>, IComparable, IConvertible, IEquatable<T>
        {
            var func = Expression.Lambda<Func<T, T, T>>(a.NegativeModuloExpression(b), Expression.Parameter(typeof(T)), Expression.Parameter(typeof(T)));
            return func.Compile()(a, b);
        }

        /// <summary>
        /// Performs an arithmetic modulo of two IComparable generic operands
        /// </summary>
        /// <typeparam name="T">The type parameter of the operands</typeparam>
        /// <param name="a">This operand needed for the modulo operation</param>
        /// <param name="b">The other operand needed for the modulo operation</param>
        /// <returns>The modulo of operand a, with respect to b</returns>
        /// <exception cref="ArgumentNullException">a or b is null</exception>
        /// <exception cref="InvalidOperationException">The modulo operator is not defined for a.Type and b.Type</exception>
        private static T PositiveModulo<T>(this T a, T b)
            where T : struct, IComparable<T>, IComparable, IConvertible, IEquatable<T>
        {
            ParameterExpression arg0 = Expression.Parameter(typeof(T));
            ParameterExpression arg1 = Expression.Parameter(typeof(T));
            return Expression.Lambda<Func<T, T, T>>(Expression.Modulo(arg0, arg1), arg0, arg1).Compile()(a, b);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static T Modulo<T>(this T a, T b)
    where T : struct, IComparable<T>, IComparable, IConvertible, IEquatable<T>
        {
            var func = Expression.Lambda<Func<T, T, T>>(a.ModuloExpression(b), Expression.Parameter(typeof(T)), Expression.Parameter(typeof(T)));
            return func.Compile()(a, b);
        }

        #endregion lambda functions

        #region other functions

        /// <summary>
        /// Maps a pair of natural numbers to a unique number by use of the Cantor pairing function.
        /// </summary>
        /// <param name="x">The first number of the pair.</param>
        /// <param name="y">The second number of the pair.</param>
        /// <returns>The unique number resulting from the mapping.</returns>
        public static uint CantorPairMap(ushort x, ushort y)
        {
            return (uint)((((x + y) * (x + y + 1)) / 2) + y);
        }

        /// <summary>
        /// Maps a pair of natural numbers to a unique number by use of the Cantor pairing function.
        /// </summary>
        /// <param name="x">The first number of the pair.</param>
        /// <param name="y">The second number of the pair.</param>
        /// <returns>The unique number resulting from the mapping.</returns>
        public static ulong CantorPairMap(uint x, uint y)
        {
            return (((x + y) * (x + y + 1)) / (2)) + y;
        }

        /// <summary>
        /// Maps a pair of natural numbers to a unique number by use of the Cantor pairing function.
        /// </summary>
        /// <param name="a">The first number of the pair.</param>
        /// <param name="b">The second number of the pair.</param>
        /// <returns>The unique number resulting from the mapping.</returns>
        public static BigInteger CantorPairMap(ulong a, ulong b)
        {
            return (((a + b) * (a + b + 1)) / (2)) + b;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Tuple<BigInteger, BigInteger> InverseCantorPairMap(BigInteger z)
        {
            var temp = new BigInteger(8) * z;
            var root = Math.Exp(BigInteger.Log(temp) * 0.5) + 1;
            var w = (root - 1) / 2;
            var x = new BigInteger(((w * w) + w) / 2);
            var y = (z - x);
            return Tuple.Create(x, y);
        }

        public static Tuple<ulong, ulong> InverseCantorPairMap(ulong value)
        {
            var root = (ulong)((Math.Sqrt((8 * value) + 1) - 1) / 2);
            var a = ((root * root) + root) / 2;
            var c = value - a;
            var b = root - c;
            return Tuple.Create(a, b);
        }

        public static Tuple<uint, uint> InverseCantorPairMap(uint z)
        {
            var root = (uint)((Math.Sqrt((8 * z) + 1) - 1) / 2);
            var w = ((root * root) + root) / 2;
            var x = z - w;
            var y = root - x;
            return Tuple.Create(x, y);
        }

        /// <summary>
        /// Maps a pair of natural numbers to a unique number by use of the Elegant pairing function.
        /// </summary>
        /// <param name="x">The first number of the pair.</param>
        /// <param name="y">The second number of the pair.</param>
        /// <returns>The unique number resulting from the mapping.</returns>
        public static uint ElegantPairMap(ushort x, ushort y)
        {
            return x > y
                ? (uint)((x * x) + x + y)
                : (uint)(x + (y * y));
        }

        /// <summary>
        /// Maps a pair of natural numbers to a unique number by use of the Elegant pairing function.
        /// </summary>
        /// <param name="x">The first number of the pair.</param>
        /// <param name="y">The second number of the pair.</param>
        /// <returns>The unique number resulting from the mapping.</returns>
        public static ulong ElegantPairMap(uint x, uint y)
        {
            return x > y
                ? (x * x) + x + y
                : x + (y * y);
        }

        /// <summary>
        /// Maps a pair of natural numbers to a unique number by use of the Elegant pairing function.
        /// </summary>
        /// <param name="x">The first number of the pair.</param>
        /// <param name="y">The second number of the pair.</param>
        /// <returns>The unique number resulting from the mapping.</returns>
        public static BigInteger ElegantPairMap(ulong x, ulong y)
        {
            return x > y
                ? (x * x) + x + y
                : x + (y * y);
        }

        #endregion other functions
    }
}