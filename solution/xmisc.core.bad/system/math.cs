using System;
using System.Linq.Expressions;
using System.Numerics;

namespace reexmonkey.xmisc.core.system
{
    /// <summary>
    /// Extends stanard Math features
    /// </summary>
    public static class MathExtensions
    {

        /// <summary>
        /// Maps a pair of natural numbers to a unique number by use of the Cantor pairing function.
        /// </summary>
        /// <param name="x">The first number of the pair.</param>
        /// <param name="y">The second number of the pair.</param>
        /// <returns>The unique number resulting from the mapping.</returns>
        public static uint CantorPairMap(this ushort x, ushort y)
        {
            return (uint)((((x + y) * (x + y + 1)) / 2) + y);
        }

        /// <summary>
        /// Maps a pair of natural numbers to a unique number by use of the Cantor pairing function.
        /// </summary>
        /// <param name="x">The first number of the pair.</param>
        /// <param name="y">The second number of the pair.</param>
        /// <returns>The unique number resulting from the mapping.</returns>
        public static ulong CantorPairMap(this uint x, uint y)
        {
            return (((x + y) * (x + y + 1)) / (2)) + y;
        }

        /// <summary>
        /// Maps a pair of natural numbers to a unique number by use of the Cantor pairing function.
        /// </summary>
        /// <param name="a">The first number of the pair.</param>
        /// <param name="b">The second number of the pair.</param>
        /// <returns>The unique number resulting from the mapping.</returns>
        public static BigInteger CantorPairMap(this ulong a, ulong b)
        {
            return (((a + b) * (a + b + 1)) / (2)) + b;
        }

        /// <summary>
        /// Finds the Cantor pair, which were mapped to a number.
        /// </summary>
        /// <param name="z">The number that resulted from a Cantor map.</param>
        /// <returns>The original pair of numbers that were used to produce the Cantor pair. </returns>
        public static Tuple<BigInteger, BigInteger> InverseCantorPairMap(this BigInteger z)
        {
            var temp = new BigInteger(8) * z;
            var root = Math.Exp(BigInteger.Log(temp) * 0.5) + 1;
            var w = (root - 1) / 2;
            var x = new BigInteger(((w * w) + w) / 2);
            var y = (z - x);
            return Tuple.Create(x, y);
        }

        /// <summary>
        /// Finds the Cantor pair, which were mapped to a number.
        /// </summary>
        /// <param name="z">The number that resulted from a Cantor map.</param>
        /// <returns>The original pair of numbers that were used to produce the Cantor pair. </returns>
        public static Tuple<ulong, ulong> InverseCantorPairMap(this ulong z)
        {
            var root = (ulong)((Math.Sqrt((8 * z) + 1) - 1) / 2);
            var w = ((root * root) + root) / 2;
            var x = z - w;
            var y = root - x;
            return Tuple.Create(x, y);
        }

        /// <summary>
        /// Finds the Cantor pair, which were mapped to a number.
        /// </summary>
        /// <param name="z">The number that resulted from a Cantor map.</param>
        /// <returns>The original pair of numbers that were used to produce the Cantor pair. </returns>
        public static Tuple<uint, uint> InverseCantorPairMap(this uint z)
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

        /// <summary>
        /// Finds the Elegant pair, which was mapped to a number.
        /// </summary>
        /// <param name="z">The number that resulted from an Elegant Pair mapping.</param>
        /// <returns>The original pair of numbers that were used to produce the Cantor pair. </returns>
        public static Tuple<BigInteger, BigInteger> InverseElegantPairMap(this BigInteger z)
        {
            var root = Math.Exp(BigInteger.Log(z) * 0.5) + 1;
            var rootsqr = root * root;

            if (root < rootsqr)
            {
                var x = BigInteger.Subtract(z, new BigInteger(rootsqr));
                var y = new BigInteger(root);
                return Tuple.Create(x, y);
            }
            else
            {
                var x = new BigInteger(root);
                var y = BigInteger.Subtract(z, BigInteger.Subtract(new BigInteger(rootsqr), x));
                return Tuple.Create(x, y);
            }
        }

        /// <summary>
        /// Finds the Elegant pair, which was mapped to a number.
        /// </summary>
        /// <param name="z">The number that resulted from the Elegant mapping.</param>
        /// <returns>The original pair of numbers used to produce the Elegant pair. </returns>
        public static Tuple<ulong, ulong> InverseElegantPairMap(this ulong z)
        {
            var root = Math.Sqrt(z);
            var rootsqr = root * root;

            if (root < rootsqr)
            {
                var x = (ulong)(z - rootsqr);
                var y = (ulong)root;
                return Tuple.Create(x, y);
            }
            else
            {
                var x = (ulong)root;
                var y = (ulong)(z - rootsqr - root);
                return Tuple.Create(x, y);
            }
        }

        /// <summary>
        /// Finds the Elegant pair, which was mapped to a number.
        /// </summary>
        /// <param name="z">The number that resulted from the Elegant mapping.</param>
        /// <returns>The original pair of numbers used to produce the Elegant pair.</returns>
        public static Tuple<uint, uint> InverseElegantPairMap(this uint z)
        {
            var root = Math.Sqrt(z);
            var rootsqr = root * root;

            if (root < rootsqr)
            {
                var x = (uint)(z - rootsqr);
                var y = (uint)root;
                return Tuple.Create(x, y);
            }
            else
            {
                var x = (uint)root;
                var y = (uint)(z - rootsqr - root);
                return Tuple.Create(x, y);
            }
        }

    }
}