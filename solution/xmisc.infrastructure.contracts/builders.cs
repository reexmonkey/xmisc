using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace reexjungle.xmisc.infrastructure.contracts
{
    /// <summary>
    /// Specifies a generic contract for building cache keys. 
    /// </summary>
    /// <typeparam name="TKey">The type of cache key to be generated.</typeparam>
    public interface ICacheKeyBuilder<out TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Gets the default key of the cache key builder.
        /// </summary>
        TKey NullKey { get; }

        /// <summary>
        /// Builds a cache key based on a single data value constrained by chosen attributes.
        /// </summary>
        /// <typeparam name="TValue">The type of data value, whose selected attributes are used in building the cache key.</typeparam>
        /// <param name="value">The data value, whose selected attributes are used in building the cache key. </param>
        /// <param name="selectors">The seleted attributes of the data value, whoich are used to build the cache key. At least one selector must be provided!</param>
        /// <returns>A cache key created from the chosen attributes of the data value.</returns>
        TKey Build<TValue>(TValue value, params Expression<Func<TValue, object>>[] selectors);

        /// <summary>
        /// Builds a cache key based on multiple data values constrained by chosen attributes.  
        /// </summary>
        /// <typeparam name="TValue">The type of data value, whose selected attributes are used in building the cache key.</typeparam>
        /// <param name="values">The data values, whose selected attributes are used in building the cache key. At least one data value must be provided.</param>
        /// <param name="selectors">The seleted attributes of each data value, whoich are used to build the cache key. At least one selector must be provided!</param>
        /// <returns>A cache key created from the chosen attributes of each data value.</returns>
        TKey Build<TValue>(IEnumerable<TValue> values, params Expression<Func<TValue, object>>[] selectors);

    }
}
