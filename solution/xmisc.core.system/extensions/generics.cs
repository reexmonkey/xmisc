using System.Collections.Generic;

namespace reexmonkey.xmisc.core.system.extensions
{
    /// <summary>
    /// Extends standard generic features.
    /// </summary>
    public static class GenericExtensions
    {
        /// <summary>
        /// Swaps the values of two given variables.
        /// </summary>
        /// <typeparam name="TValue">The type of value to swap.</typeparam>
        /// <param name="value">The first value to swap.</param>
        /// <param name="other">The second value to swap.</param>
        /// <returns>The swapped values.</returns>
        public static (TValue value, TValue other) Swap<TValue>(this TValue value, TValue other)
        {
            (other, value) = (value, other);
            return (value, other);
        }

        /// <summary>
        /// Swaps the specified elements of a given <see cref="IList{T}"/>.
        /// </summary>
        /// <typeparam name="TElement">The type of element to swap.</typeparam>
        /// <param name="values">The list containing elements to swap.</param>
        /// <param name="i">An index to access the first element to swap.</param>
        /// <param name="j">An index to access the second element to swap.</param>
        public static void Swap<TElement>(this IList<TElement> values, int i, int j)
        {
            (values[j], values[i]) = (values[i], values[j]);
        }
    }
}
