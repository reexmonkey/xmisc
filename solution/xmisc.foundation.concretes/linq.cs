using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace reexjungle.xmisc.foundation.concretes
{
    /// <summary>
    /// Provides extended Linq functionalities
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// Checks whether a sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequence</typeparam>
        /// <param name="source">A sequence that contains elements to be tested and counted</param>
        /// <returns>True if the sequence is empty; otherwise false.</returns>
        public static bool Empty<TSource>(this IEnumerable<TSource> source)
        {
            return (source.Count() == 0);
        }

        /// <summary>
        /// Checks whether a sequence is empty after its nullness has been tested.
        /// </summary>
        /// <typeparam name="TValue">The type of the elements of the input sequence</typeparam>
        /// <param name="source">A sequence that contains elements to be tested and counted</param>
        /// <returns>True if the sequence is empty; otherwise false.</returns>
        public static bool SafeEmpty<TValue>(this IEnumerable<TValue> source)
        {
            return (source != null) ? source.Count() == 0 : false;
        }

        /// <summary>
        /// Checks whether a sequence is null or empty.
        /// </summary>
        /// <typeparam name="TValue">The type of the elements of the input sequence</typeparam>
        /// <param name="source">A sequence that contains elements to be tested and counted</param>
        /// <returns>True if the sequence is null or empty.</returns>
        public static bool NullOrEmpty<TValue>(this IEnumerable<TValue> source)
        {
            return (!(source != null && source.Count() != 0));
        }

        /// <summary>
        /// Checks whether a sequence is a set. That is, if the sequence contains ONLY distinct elements
        /// </summary>
        /// <typeparam name="TValue">The type of the elements of the input sequence</typeparam>
        /// <param name="source">A sequence that contains elements to be tested and counted</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>True, if the sequence is a set; otherwise false.</returns>
        public static bool IsSet<TValue>(this IEnumerable<TValue> source, IEqualityComparer<TValue> comparer = null)
        {
            return source.Distinct(comparer).Count() == source.Count();
        }

        /// <summary>
        /// Checks whether a sequence is equal to another sequence.
        /// </summary>
        /// <typeparam name="TValue">The type of the elements of the input sequence</typeparam>
        /// <param name="first">A sequence that contains elements to be tested</param>
        /// <param name="second">Another sequence that contains elements to be tested</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>True, if the sequence is a set; otherwise false</returns>
        public static bool IsEqualOf<TValue>(this IEnumerable<TValue> first, IEnumerable<TValue> second, IEqualityComparer<TValue> comparer = null)
        {
            return first.Intersect(second, comparer).Count() == first.Count();
        }

        /// <summary>
        /// Checks whether a sequence is a super sequence of another seeuence
        /// </summary>
        /// <typeparam name="TValue">The type of the elements of the input sequence</typeparam>
        /// <param name="first">A sequence that contains elements to be tested</param>
        /// <param name="second">Another sequence that contains elements to be tested</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>True, if the sequence is a sequence; otherwise false</returns>
        public static bool IsSuperSequence<TValue>(this IEnumerable<TValue> first, IEnumerable<TValue> second, IEqualityComparer<TValue> comparer = null)
        {
            return first.Except(second, comparer).Count() > 0;
        }

        /// <summary>
        /// Checks whthere a sequence is a proper super sequence of another sequence.
        /// </summary>
        /// <typeparam name="TValue">The type of the elements of the input sequence</typeparam>
        /// <param name="first">A sequence that contains elements to be tested</param>
        /// <param name="second">Another sequence that contains elements to be tested</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>True, if the sequence is a proper super sequence of the other; otherwise false</returns>
        public static bool IsProperSuperSequenceOf<TValue>(this IEnumerable<TValue> first, IEnumerable<TValue> second, IEqualityComparer<TValue> comparer = null)
        {
            return !first.IsEqualOf(second, comparer) && first.Except(second, comparer).Count() > 0;
        }

        /// <summary>
        /// Checks whether a sequence is a sub sequence of another sequence.
        /// </summary>
        /// <typeparam name="TValue">The type of the elements of the input sequence</typeparam>
        /// <param name="first">A sequence that contains elements to be tested</param>
        /// <param name="second">Another sequence that contains elements to be tested</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>True, if the sequence is a  sub sequence of the other; otherwise false</returns>
        public static bool IsSubSequenceOf<TValue>(this IEnumerable<TValue> first, IEnumerable<TValue> second, IEqualityComparer<TValue> comparer = null)
        {
            return second.Except(first, comparer).Count() == 0;
        }

        /// <summary>
        /// Checks whether a sequence is a proper sub sequence of another sequence.
        /// A sequence is a proper sub sequence of another, if it is not the same as the other and is a s
        /// </summary>
        /// <typeparam name="TValue">The type of the elements of the input sequence</typeparam>
        /// <param name="first">A sequence that contains elements to be tested</param>
        /// <param name="second">Another sequence that contains elements to be tested</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>True, if the sequence is a proper sub sequence of the other; otherwise false</returns>
        public static bool IsProperSubSequenceOf<TValue>(this IEnumerable<TValue> first, IEnumerable<TValue> second, IEqualityComparer<TValue> comparer = null)
        {
            return first.IsEqualOf(second, comparer) && second.Except(first, comparer).Count() == 0;
        }

        /// <summary>
        /// Checks whether a sequence does not intersect another sequence.
        /// </summary>
        /// <typeparam name="TValue">The type of the elements of the input sequence</typeparam>
        /// <param name="first">A sequence that contains elements to be tested</param>
        /// <param name="second">Another sequence that contains elements to be tested</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>True, if the sequence does not intersect the other; otherwise false</returns>
        public static bool NonIntersects<TValue>(this IEnumerable<TValue> first, IEnumerable<TValue> second, IEqualityComparer<TValue> comparer = null)
        {
            return first.Intersect(second, comparer).Count() == 0;
        }

        /// <summary>
        /// Checks whether a sequence does not intersect another sequence.
        /// </summary>
        /// <typeparam name="TValue">The type of the elements of the input sequences</typeparam>
        /// <param name="first">A sequence that contains elements to be tested</param>
        /// <param name="second">Another sequence that contains elements to be tested</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>True, if the sequence does not intersect the other; otherwise false</returns>
        public static bool Intersects<TValue>(this IEnumerable<TValue> first, IEnumerable<TValue> second, IEqualityComparer<TValue> comparer = null)
        {
            return first.Intersect(second, comparer).Count() > 0;
        }

        /// <summary>
        /// Produces a symmetric difference between two sequences
        /// </summary>
        /// <typeparam name="TValue">The type of the elements of the input sequences</typeparam>
        /// <param name="first">A sequence that contains elements</param>
        /// <param name="second">Another sequence that contains elements</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>The symmetric difference of the two sequences</returns>
        public static IEnumerable<TValue> SymmetricExcept<TValue>(this IEnumerable<TValue> first, IEnumerable<TValue> second, IEqualityComparer<TValue> comparer = null)
        {
            return (first.Union(second)).Except(first.Intersect(second));
        }

        /// <summary>
        /// Produces the set intersection of two sequences by using the specified IEqualityComparer<T> to compare values.
        /// </summary>
        /// <typeparam name="TValue">The type of the elements of the input sequences</typeparam>
        /// <param name="first">An IEnumerable<T> whose distinct elements do not appear in second.</param>
        /// <param name="second">An IEnumerable<T> whose distinct elements do not appear in first</param>
        /// <param name="comparer">An System.Collections.Generic.IEqualityComparer<T> to compare values</param>
        /// <returns>A sequence that contains the elements that form the set intersection of two sequences</returns>
        public static IEnumerable<TValue> NonIntersect<TValue>(this IEnumerable<TValue> first, IEnumerable<TValue> second, IEqualityComparer<TValue> comparer = null)
        {
            return (second.Union(first, comparer)).Except(second.Intersect(first, comparer), comparer);
        }

        /// <summary>
        /// Produces the union of two sequences, where the elements of the second sequence are filtered based on a predicate.
        /// </summary>
        /// <typeparam name="TValue">The type of the elements of the input sequences</typeparam>
        /// <param name="first">A sequence that contains elements</param>
        /// <param name="second">Another sequence that contains elements</param>
        /// <param name="predicate">A filter to select elements from the second sequence</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>The union of the two sequences after filtration of the second sequence</returns>
        public static IEnumerable<TValue> Union<TValue>(this IEnumerable<TValue> first, IEnumerable<TValue> second, Func<TValue, bool> predicate, IEqualityComparer<TValue> comparer = null)
        {
            return !second.NullOrEmpty() ? first.Union(second.Where(x => predicate(x)), comparer) : first;
        }

        /// <summary>
        /// Produces the union of two sequences, where the elements of the second sequence are selected based on a predicate.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="first">A sequence that contains elements</param>
        /// <param name="second">Another sequence that contains elements</param>
        /// <param name="predicate">A filter to select elements from the second sequence</param>
        /// <returns>The union of the two sequences after filtration of the second sequence</returns>
        public static IEnumerable<TValue> Concat<TValue>(this IEnumerable<TValue> first, IEnumerable<TValue> second, Func<TValue, bool> predicate)
        {
            return first.Concat(second).Where(x => predicate(x));
        }

        /// <summary>
        /// Merges the elements of two sequences. Only non-existing elements of the first sequence are selected from the second sequence.
        /// </summary>
        /// <typeparam name="TValue">The type of the elements of the input sequences</typeparam>
        /// <param name="first">A sequence that contains elements</param>
        /// <param name="second">Another sequence that contains elements</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>A union of the two sequences, where only non-existing elements of the first sequence are selected from the second sequence</returns>
        public static IEnumerable<TValue> Merge<TValue>(this IEnumerable<TValue> first, IEnumerable<TValue> second, IEqualityComparer<TValue> comparer = null)
        {
            var difference = second.Except(first.Distinct(), comparer);
            return !difference.NullOrEmpty() ? first.Union(difference) : first;
        }

        /// <summary>
        /// Produces a union of a sequence and a singleton based on a precondition.
        /// </summary>
        /// <typeparam name="TValue">The type of the elements of the source sequence</typeparam>
        /// <param name="source">A sequence of elements</param>
        /// <param name="value">The element to be added</param>
        /// <param name="precondition">The filter to determine whether the element is added to the sequence</param>
        /// <returns></returns>
        public static IEnumerable<TValue> Union<TValue>(this IEnumerable<TValue> source, TValue value, bool precondition)
        {
            return precondition ? source.Union(value.ToSingleton()) : source;
        }

        /// <summary>
        /// Produces a singleton from an element
        /// </summary>
        /// <typeparam name="TValue">The type of the element</typeparam>
        /// <param name="value">The element to be converted to a singleton</param>
        /// <returns>A singleton</returns>
        public static IEnumerable<TValue> ToSingleton<TValue>(this TValue value)
        {
            return new TValue[] { value };
        }
    }
}