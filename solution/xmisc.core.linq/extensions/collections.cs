using System;
using System.Collections.Generic;
using System.Linq;

namespace reexmonkey.xmisc.core.linq.extensions
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
        public static bool Empty<TSource>(this IEnumerable<TSource> source) => !source.Any();

        /// <summary>
        /// Safely checks if the sequence of elements is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of elements.</typeparam>
        /// <param name="source">The sequence of elements.</param>
        /// <returns>True if the sequence is neither null nor empty; otherwise false.</returns>
        public static bool SafeEmpty<TSource>(this IEnumerable<TSource> source) => source != null && !source.Any();

        /// <summary>
        /// Checks whether a sequence is null or empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequence</typeparam>
        /// <param name="source">A sequence that contains elements to be tested and counted</param>
        /// <returns>True if the sequence is null or empty.</returns>
        public static bool NullOrEmpty<TSource>(this IEnumerable<TSource> source) => !(source != null && source.Any());

        /// <summary>
        /// Checks whether a sequence is a set. That is, if the sequence contains ONLY distinct elements
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequence</typeparam>
        /// <param name="source">A sequence that contains elements to be tested and counted</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>True, if the sequence is a set; otherwise false.</returns>
        public static bool IsSet<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer = null) => source.Distinct(comparer).Count() == source.Count();

        /// <summary>
        /// Checks whether a sequence is equal to another sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequence</typeparam>
        /// <param name="first">A sequence that contains elements to be tested</param>
        /// <param name="second">Another sequence that contains elements to be tested</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>True, if the sequence is a set; otherwise false</returns>
        public static bool IsEquivalentOf<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer = null)
            => first.Intersect(second, comparer).Count() == first.Count();

        /// <summary>
        /// Checks whether a sequence is a super sequence of another seeuence
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequence</typeparam>
        /// <param name="first">A sequence that contains elements to be tested</param>
        /// <param name="second">Another sequence that contains elements to be tested</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>True, if the sequence is a sequence; otherwise false</returns>
        public static bool IsSuperSequenceOf<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer = null)
            => first.Except(second, comparer).Any();

        /// <summary>
        /// Checks whthere a sequence is a proper super sequence of another sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequence</typeparam>
        /// <param name="first">A sequence that contains elements to be tested</param>
        /// <param name="second">Another sequence that contains elements to be tested</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>True, if the sequence is a proper super sequence of the other; otherwise false</returns>
        public static bool IsProperSuperSequenceOf<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer = null)
            => !first.IsEquivalentOf(second, comparer) && first.Except(second, comparer).Any();

        /// <summary>
        /// Checks whether a sequence is a sub sequence of another sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequence</typeparam>
        /// <param name="first">A sequence that contains elements to be tested</param>
        /// <param name="second">Another sequence that contains elements to be tested</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>True, if the sequence is a  sub sequence of the other; otherwise false</returns>
        public static bool IsSubSequenceOf<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer = null)
            => !second.Except(first, comparer).Any();

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
            => first.IsEquivalentOf(second, comparer) && !second.Except(first, comparer).Any();

        /// <summary>
        /// Checks whether a sequence does not intersect another sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequence</typeparam>
        /// <param name="first">A sequence that contains elements to be tested</param>
        /// <param name="second">Another sequence that contains elements to be tested</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>True, if the sequence does not intersect the other; otherwise false</returns>
        public static bool IsDisjointOf<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer = null)
            => !first.Intersect(second, comparer).Any();

        /// <summary>
        /// Checks whether a sequence does not intersect another sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequences</typeparam>
        /// <param name="first">A sequence that contains elements to be tested</param>
        /// <param name="second">Another sequence that contains elements to be tested</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>True, if the sequence does not intersect the other; otherwise false</returns>
        public static bool Intersects<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer = null) => first.Intersect(second, comparer).Any();

        /// <summary>
        /// Produces a symmetric difference between two sequences
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequences</typeparam>
        /// <param name="first">A sequence that contains elements</param>
        /// <param name="second">Another sequence that contains elements</param>
        /// <param name="comparer">An IEqualityComparer to compare values. If it is null, the default equality comparer is used</param>
        /// <returns>The symmetric difference of the two sequences</returns>
        public static IEnumerable<TSource> SymmetricExcept<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer = null)
            => first.Union(second, comparer).Except(first.Intersect(second, comparer), comparer);

        /// <summary>
        /// Produces the set intersection of two sequences by using the specified IEqualityComparer{T} to compare values.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequences</typeparam>
        /// <param name="first">An IEnumerable{T} whose distinct elements do not appear in second.</param>
        /// <param name="second">An IEnumerable{T} whose distinct elements do not appear in first</param>
        /// <param name="comparer">An System.Collections.Generic.IEqualityComparer{T} to compare values</param>
        /// <returns>A sequence that contains the elements that form the set intersection of two sequences</returns>
        public static IEnumerable<TSource> NonIntersect<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer = null)
            => second
            .Union(first, comparer)
            .Except(second.Intersect(first, comparer), comparer);

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
            => !second.NullOrEmpty() ? first.Union(second.Where(predicate), comparer) : first;

        /// <summary>
        /// Produces the union of two sequences, where the elements of the second sequence are selected based on a predicate.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="first">A sequence that contains elements</param>
        /// <param name="second">Another sequence that contains elements</param>
        /// <param name="predicate">A filter to select elements from the second sequence</param>
        /// <returns>The union of the two sequences after filtration of the second sequence</returns>
        public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, bool> predicate) => first.Concat(second).Where(predicate);

        /// <summary>
        /// Gets the value for a given key if the key exists, otherwise the key and value are automatically added to the dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of key used for the retrieval or adding of the value.</typeparam>
        /// <typeparam name="TValue">The type of value to retrieve or add.</typeparam>
        /// <param name="source">The current <see cref="IDictionary{Key, TValue}"/> instance.</param>
        /// <param name="key">The key used to either retrieve or add a value to the dictionary.</param>
        /// <param name="value">The retrieved or added value of the dictionary.</param>
        /// <returns>The retrieved or added value for the key in the dictionary.</returns>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue value)
        {
            if (!source.TryGetValue(key, out TValue val))
            {
                source.Add(key, val = value);
            }
            return val;
        }

        /// <summary>
        /// Gets the value for a given key if the key exists, otherwise the key and value are automatically added to the dictionary.The type of value to retrieve or add.
        /// </summary>
        /// <typeparam name="TKey">The type of key used for the retrieval or adding of the value.</typeparam>
        /// <typeparam name="TValue">The type of value to retrieve or add.</typeparam>
        /// <param name="source">The current <see cref="IDictionary{TKey, TValue}"/> instance.</param>
        /// <param name="key">The key used to either retrieve or add a value to the dictionary.</param>
        /// <param name="func">The function used to compute the value, that is to be retrieved or added.</param>
        /// <returns>The retrieved or added value for the key in the dictionary.</returns>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, Func<TKey, TValue> func)
        {
            if (!source.TryGetValue(key, out TValue value))
            {
                source.Add(key, value = func(key));
            }
            return value;
        }

        /// <summary>
        /// Gets the value for a given key if the key exists, otherwise the default value is retrieved.
        /// </summary>
        /// <typeparam name="TKey">The type of key used for the retrieval.</typeparam>
        /// <typeparam name="TValue">The type of value to retrieve.</typeparam>
        /// <param name="source">The current <see cref="IDictionary{TKey, TValue}"/> instance.</param>
        /// <param name="key">The key used to either retrieve the value from the dictionary.</param>
        /// <returns>The retrieved value in the dictionary if it exists; otherwise the default value of <typeparamref name="TValue"/> is retrieved.</returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key)
        {
            return !source.TryGetValue(key, out TValue value)
    ? value
    : default(TValue);
        }

        /// <summary>
        /// Produces a concatenation of a sequence and a singleton based on a precondition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the source sequence.</typeparam>
        /// <param name="source">A sequence of elements.</param>
        /// <param name="value">The element to be concatenated.</param>
        /// <param name="predicate">The condition that determines whether the element is concatenated to the sequence.</param>
        /// <returns>A non-distinct sequence of elements that contains the concatenated <paramref name="value"/>.</returns>
        public static IEnumerable<TSource> ConcatOnCondition<TSource>(this IEnumerable<TSource> source, TSource value, Func<bool> predicate)
            => predicate() ? source.Concat(value.AsSingleton()) : source;

        /// <summary>
        /// Produces a distinct union of a sequence and a singleton based on a precondition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the source sequence.</typeparam>
        /// <param name="source">A sequence of elements.</param>
        /// <param name="value">The element to be added.</param>
        /// <param name="predicate">The condition that determines whether the element is added to the sequence.</param>
        /// <returns>A distinct sequence of elements that contains the added <paramref name="value"/>.</returns>
        public static IEnumerable<TSource> UnionOnCondition<TSource>(this IEnumerable<TSource> source, TSource value, Func<bool> predicate)
            => predicate() ? source.Union(value.AsSingleton()) : source;

        /// <summary>
        /// Produces a singleton from an element
        /// </summary>
        /// <typeparam name="TSource">The type of the element</typeparam>
        /// <param name="source">The element to be converted to a singleton</param>
        /// <returns>A singleton</returns>
        public static IEnumerable<TSource> AsSingleton<TSource>(this TSource source) => Enumerable.Repeat(source, 1);

        /// <summary>
        /// Produces a singleton from an element
        /// </summary>
        /// <typeparam name="TSource">The type of the element</typeparam>
        /// <param name="source">The element to be converted to a singleton</param>
        /// <param name="fast">Should the conversion to a singleton be the fast?
        /// <para/> The faster variant internally converts the <paramref name="source"/> to an array; meanwhile the slower variant uses the <see cref="Enumerable"/> class.
        /// </param>
        /// <returns>A singleton</returns>
        public static IEnumerable<TSource> AsSingleton<TSource>(this TSource source, bool fast) => fast ? new[] { source } : source.AsSingleton();

        private static IEnumerable<TSource> OptimizedSkip<TSource>(this IList<TSource> source, int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return source[i];
            }
            yield break;
        }
        private static IEnumerable<TSource> OptimizedTake<TSource>(this List<TSource> source, int offset, int count)
        {
            return source.GetRange(offset, count);
        }

        /// <summary>
        /// Bypasses a specified number of elements in a sequence and then returns the remaining elements.
        /// <para/> This method is an optimzation of the <see cref="IEnumerable{T}"/>.Skip(int) method. 
        /// <para/> Use this method if the <paramref name="source"/> sequence is a potential <see cref="IList{T}"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return elements from.</param>
        /// <param name="count">The number of elements to skip before returning the remaining elements.</param>
        /// <returns>An <see cref="IEnumerable{T}"/>that contains the elements that occur after the specified index in the input sequence.</returns>
        public static IEnumerable<TSource> FastSkip<TSource>(this IEnumerable<TSource> source, int count)
        {
            if (count <= 0) return source;
            var list = source as IList<TSource>;
            return list != null ? list.OptimizedSkip(count) : source.Skip(count);
        }

        /// <summary>
        /// Returns a specified number of contiguous elements from the start of a sequence.
        /// <para/> This method is an optimzation of the <see cref="IEnumerable{T}"/>.Take(int) method.
        /// <para/> Use this method if the <paramref name="source"/> sequence is a potential <see cref="List{T}"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">The sequence to return elements from.</param>
        /// <param name="count">The number of elements to return.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that contains the specified number of elements from the start of the input sequence.</returns>
        public static IEnumerable<TSource> FastTake<TSource>(this IEnumerable<TSource> source, int count)
        {
            if (count <= 0) return Enumerable.Empty<TSource>();
            if (count >= source.Count()) return source;
            var list = source as List<TSource>;
            return list != null ? list.GetRange(0, count) : source.Take(count);
        }


    }
}