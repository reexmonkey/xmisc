using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using reexjungle.xmisc.foundation.concretes;
using reexjungle.xmisc.infrastructure.contracts;

namespace reexjungle.xmisc.infrastructure.concretes.operations
{
    /// <summary>
    /// Represents an abstract class for building cache keys. 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class CacheKeyBuilderBase<TKey>: ICacheKeyBuilder<TKey>
        where TKey: IEquatable<TKey>
    {

        /// <summary>
        /// Combines multiple keys to a single key.
        /// </summary>
        /// <param name="keys">The given multiple keys to be combined.</param>
        /// <returns>A single key, resulting from the combination of multiple keys.</returns>
        protected abstract TKey Aggregate(IEnumerable<TKey> keys);

        /// <summary>
        /// Creates a key from the selected attribute of the given data value. 
        /// </summary>
        /// <typeparam name="TValue">The type of data value.</typeparam>
        /// <typeparam name="TProperty">The type of attribute.</typeparam>
        /// <param name="value">The given data value, whose attribute is to be selected.</param>
        /// <param name="selector">The selected attribute of the data value.</param>
        /// <returns>A cache key created from the selected attribute of the data value.</returns>
        protected abstract TKey CreateKey<TValue, TProperty>(TValue value, Expression<Func<TValue, TProperty>> selector);

        /// <summary>
        /// Creates a key from the selected attributes of the given data value. 
        /// </summary>
        /// <typeparam name="TValue">The type of data value.</typeparam>
        /// <typeparam name="TProperty">The type of each attribute</typeparam>
        /// <param name="value">The given data value, whose attributes are to be selected</param>
        /// <param name="selectors">The selected attributes of the data value.</param>
        /// <returns>>A cache key created from the selected attributes of the data value</returns>
        protected abstract TKey CreateKey<TValue, TProperty>(TValue value, IEnumerable<Expression<Func<TValue, TProperty>>> selectors);

        /// <summary>
        /// Gets the default key of the cache key builder.
        /// </summary>
        public abstract TKey NullKey { get; }

        /// <summary>
        /// Builds a cache key based on a single data value constrained by chosen attributes.
        /// </summary>
        /// <typeparam name="TValue">The type of data value, whose selected attributes are used in building the cache key.</typeparam>
        /// <param name="value">The data value, whose selected attributes are used in building the cache key. </param>
        /// <param name="selectors">The seleted attributes of the data value, whoich are used to build the cache key. At least one selector must be provided!</param>
        /// <returns>A cache key created from the chosen attributes of the data value.</returns>
        public TKey Build<TValue>(TValue value, params Expression<Func<TValue, object>>[] selectors)
        {
            if (value == null) throw new ArgumentNullException("value");
            if (selectors == null) throw new ArgumentNullException("selectors");

            return Aggregate(CreateKey(value, selectors).ToSingleton());
        }

        /// <summary>
        /// Builds a cache key based on multiple data values constrained by chosen attributes.  
        /// </summary>
        /// <typeparam name="TValue">The type of data value, whose selected attributes are used in building the cache key.</typeparam>
        /// <param name="instances">The data values, whose selected attributes are used in building the cache key. At least one data value must be provided.</param>
        /// <param name="selectors">The seleted attributes of each data value, whoich are used to build the cache key. At least one selector must be provided!</param>
        /// <returns>A cache key created from the chosen attributes of each data value.</returns>
        public TKey Build<TValue>(IEnumerable<TValue> instances, params Expression<Func<TValue, object>>[] selectors)
        {
            if (instances == null) throw new ArgumentNullException("instances");
            if (selectors == null) throw new ArgumentNullException("selectors");

            var keys = instances.Distinct().Where(i => i != null).Select(x => CreateKey(x, selectors));
            return Aggregate(keys);
        }
    }

    /// <summary>
    /// Represents a cache key builder that produces string-based keys.
    /// </summary>
    public sealed class StringCacheKeyBuilder: CacheKeyBuilderBase<string>
    {
        private readonly string nullKey;
        public override string NullKey
        {
            get { return nullKey; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringCacheKeyBuilder"/> class.
        /// </summary>
        public StringCacheKeyBuilder()
        {
            nullKey = Guid.NewGuid().ToString();
        }
        
        private string CreateKey(object value)
        {
            if (value == null) 
                return nullKey;

            var convertible = value as IConvertible;
            if (convertible != null)
            {
                return convertible.ToString(CultureInfo.InvariantCulture);
            }

            if (value is Guid) 
                return ((Guid) value).ToString();

            if (value is DateTime)
                return string.Format("{0}", ((DateTime) value).Ticks);

            var builder = value as ICacheKeyBuilder<string>;
            if (builder != null)
            {
                return CreateKey(builder, x=>x.NullKey);
            }

            var enumerable = value as IEnumerable;
            if (enumerable != null)
            {
                var keyparts = (from object element in enumerable
                                select CreateKey(element))
                                .ToList();

                return Aggregate(keyparts);
            }

            var type = value as Type;
            if (type != null)
            {
                return string.Format("{0}", type.GUID);
            }

            throw new ArgumentException(string.Format("{0} is not cacheable", value.GetType()));
        }

        protected override string Aggregate(IEnumerable<string> keys)
        {
            return string.Join(string.Empty, keys);
        }

        protected override string CreateKey<TValue, TProperty>(TValue value, Expression<Func<TValue, TProperty>> selector)
        {
           return string.Format("{{{0}:{{{1}:{2}}}}}", typeof(TValue).Name, selector.GetMemberName(), CreateKey(selector.Compile()(value)));
        }

        protected override string CreateKey<TValue, TProperty>(TValue value, IEnumerable<Expression<Func<TValue, TProperty>>> selectors)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{{{0}:", typeof (TValue).Name);
            var names = new List<string>();
            foreach (var selector in selectors)
            {
                var name = selector.GetMemberName();
                if(names.Contains(name, StringComparer.OrdinalIgnoreCase)) continue;
                sb.AppendFormat("{{{0}:{1}}}", name, CreateKey(selector.Compile()(value)));
                names.Add(name);
            }
            sb.Append("}");
            return sb.ToString();
        }

    }

    /// <summary>
    /// Represents a cache key builder that produces Global Unique Identifier (GUID)-based keys.
    /// </summary>
    public class GuidCacheKeyBuilder : CacheKeyBuilderBase<Guid>
    {
        private readonly Guid nullKey;
        public override Guid NullKey
        {
            get { return nullKey; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GuidCacheKeyBuilder"/> class.
        /// </summary>
        public GuidCacheKeyBuilder()
        {
            nullKey = Guid.NewGuid();
        }

        private Guid CreateKey(object value)
        {
            if (value == null) 
                return nullKey;

            var convertible = value as IConvertible;
            if (convertible != null)
            {
                var valuestring = convertible.ToString(CultureInfo.InvariantCulture);
                var bytes = Encoding.Unicode.GetBytes(valuestring);
                return new Guid(bytes.Hash(new MD5CryptoServiceProvider()));
            }

            if (value is Guid) return (Guid) value;

            if (value is DateTime)
            {
                var datetime = ((DateTime)value).Ticks;
                var bytes = BitConverter.GetBytes(datetime);
                return new Guid(bytes.Hash(new MD5CryptoServiceProvider()));
            }

            var builder = value as ICacheKeyBuilder<Guid>;
            if (builder != null)
            {
                return builder.Build(builder, x => x.NullKey);
            }

            var enumerable = value as IEnumerable;
            if (enumerable != null)
            {
                var keyparts = (from object element in enumerable
                                select CreateKey(element))
                                .ToList();

                return Aggregate(keyparts);
            }

            var type = value as Type;
            if (type != null)
            {
                return type.GUID;
            }

            throw new ArgumentException(string.Format("{0} is not cacheable", value.GetType()));
        }

        protected override Guid Aggregate(IEnumerable<Guid> keys)
        {
            return keys.Aggregate(Guid.Empty, (@this, other) => @this.Combine(other));
        }

        protected override Guid CreateKey<TValue, TProperty>(TValue value, Expression<Func<TValue, TProperty>> selector)
        {
            return CreateKey(selector.Compile()(value));
        }

        protected override Guid CreateKey<TValue, TProperty>(TValue value, IEnumerable<Expression<Func<TValue, TProperty>>> selectors)
        {
            var keys = selectors.Where(s => s != null).Select(x => CreateKey(x.Compile()(value)));
            return Aggregate(keys.Distinct());
        }
    }
}
