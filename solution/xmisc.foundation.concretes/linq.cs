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
            return (!source.Any());
        }

        /// <summary>
        /// Checks whether a sequence is empty after its nullness has been tested.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequence</typeparam>
        /// <param name="source">A sequence that contains elements to be tested and counted</param>
        /// <returns>True if the sequence is empty; otherwise false.</returns>
        public static bool SafeEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return (source != null) && !source.Any();
        }

        /// <summary>
        /// Checks whether a sequence is null or empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequence</typeparam>
        /// <param name="source">A sequence that contains elements to be tested and counted</param>
        /// <returns>True if the sequence is null or empty.</returns>
        public static bool NullOrEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return (!(source != null && source.Any()));
        }

        /// <summary>
        /// Checks whether a sequence is a set. That is, if the sequence contains ONLY distinct elements
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequence</typeparam>
        /// <param name="source">A sequence that contains elements to be tested and counted</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>True, if the sequence is a set; otherwise false.</returns>
        public static bool IsSet<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer = null)
        {
            return source.Distinct(comparer).Count() == source.Count();
        }

        /// <summary>
        /// Checks whether a sequence is equal to another sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequence</typeparam>
        /// <param name="first">A sequence that contains elements to be tested</param>
        /// <param name="second">Another sequence that contains elements to be tested</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>True, if the sequence is a set; otherwise false</returns>
        public static bool IsEquivalentOf<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer = null)
        {
            return first.Intersect(second, comparer).Count() == first.Count();
        }

        /// <summary>
        /// Checks whether a sequence is a super sequence of another seeuence
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequence</typeparam>
        /// <param name="first">A sequence that contains elements to be tested</param>
        /// <param name="second">Another sequence that contains elements to be tested</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>True, if the sequence is a sequence; otherwise false</returns>
        public static bool IsSuperSequenceOf<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer = null)
        {
            return first.Except(second, comparer).Any();
        }

        /// <summary>
        /// Checks whthere a sequence is a proper super sequence of another sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequence</typeparam>
        /// <param name="first">A sequence that contains elements to be tested</param>
        /// <param name="second">Another sequence that contains elements to be tested</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>True, if the sequence is a proper super sequence of the other; otherwise false</returns>
        public static bool IsProperSuperSequenceOf<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer = null)
        {
            return !first.IsEquivalentOf(second, comparer) && first.Except(second, comparer).Any();
        }

        /// <summary>
        /// Checks whether a sequence is a sub sequence of another sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequence</typeparam>
        /// <param name="first">A sequence that contains elements to be tested</param>
        /// <param name="second">Another sequence that contains elements to be tested</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>True, if the sequence is a  sub sequence of the other; otherwise false</returns>
        public static bool IsSubSequenceOf<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer = null)
        {
            return !second.Except(first, comparer).Any();
        }

        /// <summary>
        /// Checks whether a sequence is a proper sub sequence of another sequence.
        /// A sequence is a proper sub sequence of another, if it is not the same as the other and is a s
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequence</typeparam>
        /// <param name="first">A sequence that contains elements to be tested</param>
        /// <param name="second">Another sequence that contains elements to be tested</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>True, if the sequence is a proper sub sequence of the other; otherwise false</returns>
        public static bool IsProperSubSequenceOf<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer = null)
        {
            return first.IsEquivalentOf(second, comparer) && !second.Except(first, comparer).Any();
        }

        /// <summary>
        /// Checks whether a sequence does not intersect another sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequence</typeparam>
        /// <param name="first">A sequence that contains elements to be tested</param>
        /// <param name="second">Another sequence that contains elements to be tested</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>True, if the sequence does not intersect the other; otherwise false</returns>
        public static bool NonIntersects<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer = null)
        {
            return !first.Intersect(second, comparer).Any();
        }

        /// <summary>
        /// Checks whether a sequence does not intersect another sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequences</typeparam>
        /// <param name="first">A sequence that contains elements to be tested</param>
        /// <param name="second">Another sequence that contains elements to be tested</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>True, if the sequence does not intersect the other; otherwise false</returns>
        public static bool Intersects<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer = null)
        {
            return first.Intersect(second, comparer).Any();
        }

        /// <summary>
        /// Produces a symmetric difference between two sequences
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequences</typeparam>
        /// <param name="first">A sequence that contains elements</param>
        /// <param name="second">Another sequence that contains elements</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>The symmetric difference of the two sequences</returns>
        public static IEnumerable<TSource> SymmetricExcept<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer = null)
        {
            return (first.Union(second)).Except(first.Intersect(second));
        }

        /// <summary>
        /// Produces the set intersection of two sequences by using the specified IEqualityComparer<T> to compare values.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequences</typeparam>
        /// <param name="first">An IEnumerable<T> whose distinct elements do not appear in second.</param>
        /// <param name="second">An IEnumerable<T> whose distinct elements do not appear in first</param>
        /// <param name="comparer">An System.Collections.Generic.IEqualityComparer<T> to compare values</param>
        /// <returns>A sequence that contains the elements that form the set intersection of two sequences</returns>
        public static IEnumerable<TSource> NonIntersect<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer = null)
        {
            return (second.Union(first, comparer)).Except(second.Intersect(first, comparer), comparer);
        }

        /// <summary>
        /// Produces the union of two sequences, where the elements of the second sequence are filtered based on a predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequences</typeparam>
        /// <param name="first">A sequence that contains elements</param>
        /// <param name="second">Another sequence that contains elements</param>
        /// <param name="predicate">A filter to select elements from the second sequence</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>The union of the two sequences after filtration of the second sequence</returns>
        public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, bool> predicate, IEqualityComparer<TSource> comparer = null)
        {
            return !second.NullOrEmpty() ? first.Union(second.Where(x => predicate(x)), comparer) : first;
        }

        /// <summary>
        /// Produces the union of two sequences, where the elements of the second sequence are selected based on a predicate.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="first">A sequence that contains elements</param>
        /// <param name="second">Another sequence that contains elements</param>
        /// <param name="predicate">A filter to select elements from the second sequence</param>
        /// <returns>The union of the two sequences after filtration of the second sequence</returns>
        public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, bool> predicate)
        {
            return first.Concat(second).Where(x => predicate(x));
        }

        /// <summary>
        /// Merges the elements of two sequences. Only non-existing elements of the first sequence are selected from the second sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequences</typeparam>
        /// <param name="first">A sequence that contains elements</param>
        /// <param name="second">Another sequence that contains elements</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>A union of the two sequences, where only non-existing elements of the first sequence are selected from the second sequence</returns>
        public static IEnumerable<TSource> Merge<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer = null)
        {
            if (second != null) return first.Union(second, comparer);
            return first;
        }

        /// <summary>
        /// Merges the elements of a sequence to those of a <see cref="System.Collections.Generic.List&lt;TSource&gt;"/>. Only non-existing elements of the list are selected from the given sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the list and the input sequence</typeparam>
        /// <param name="origin">A list that contains elements</param>
        /// <param name="values">Another sequence that contains elements</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        public static void MergeRange<TSource>(this List<TSource> origin, IEnumerable<TSource> values, IEqualityComparer<TSource> comparer = null)
        {
            if (values == null) throw new ArgumentNullException("values", "Sequence must be not null");

            var incoming = values.Except(origin.Distinct(), comparer);
            if (!incoming.NullOrEmpty()) origin.AddRange(incoming);
        }

        /// <summary>
        /// Gets the value for a given key if the key exists, otherwise the key and value are automatically added to the dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of key used for the retrieval or adding of the value.</typeparam>
        /// <typeparam name="TValue">The type of value to retrieve or add.</typeparam>
        /// <param name="source">The current <see cref="IDictionary{Key, TValue}"/> instance.</param>
        /// <param name="key">The key used to either retrieve or add a value to the dictionary.</param>
        /// <param name="value">The retrieved or added value of the dictionary.</param>
        /// <returns>The retrieved or added value for the key in the dictionary.</returns>
        /// <seealso cref="http://www.codeducky.org/10-utilities-c-developers-should-know-part-two/"/>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> source,  TKey key, TValue value)
        {
            TValue val;
            if (!source.TryGetValue(key, out val))
            {
               source.Add(key, val = value); 
            }
            return val;
        }

        /// <summary>
        /// Gets the computed value for a given key if the key exists, otherwise the key and value are automatically added to the dictionary.The type of value to retrieve or add.
        /// </summary>
        /// <typeparam name="TKey">The type of key used for the retrieval or adding of the value.</typeparam>
        /// <typeparam name="TValue">The type of value to retrieve or add.</typeparam>
        /// <param name="source">The current <see cref="IDictionary{TKey, TValue}"/> instance.</param>
        /// <param name="key">The key used to either retrieve or add a value to the dictionary.</param>
        /// <param name="func">The function used to compute the value, that is to be retrieved or added.</param>
        /// <returns>The retrieved or added value for the key in the dictionary.</returns>
        /// <seealso cref="http://www.codeducky.org/10-utilities-c-developers-should-know-part-two/"/>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, Func<TKey, TValue> func)
        {
            TValue value;
            if (!source.TryGetValue(key, out value))
            {
                source.Add(key, value = func(key));
            }
            return value;
        }

        /// <summary>
        /// Produces a union of a sequence and a singleton based on a precondition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the source sequence</typeparam>
        /// <param name="source">A sequence of elements</param>
        /// <param name="value">The element to be added</param>
        /// <param name="precondition">The filter to determine whether the element is added to the sequence</param>
        /// <returns></returns>
        public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> source, TSource value, bool precondition)
        {
            return precondition ? source.Union(value.ToSingleton()) : source;
        }

        /// <summary>
        /// Produces a singleton from an element
        /// </summary>
        /// <typeparam name="TSource">The type of the element</typeparam>
        /// <param name="value">The element to be converted to a singleton</param>
        /// <returns>A singleton</returns>
        public static IEnumerable<TSource> ToSingleton<TSource>(this TSource value)
        {
            return new TSource[] { value };
        }
    }
}