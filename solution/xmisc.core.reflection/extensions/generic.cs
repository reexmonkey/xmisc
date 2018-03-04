using reexmonkey.xmisc.core.linq.infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace reexmonkey.xmisc.core.reflection.extensions
{
    public static class GenericExtensions
    {
        public static bool PropertiesEquals<T>(this T instance, T other, BindingFlags flags, params string[] ignore) where T : class
        {
            if (instance != null && other != null)
            {
                var type = typeof(T);
                var diff =
                    from pi in type.GetProperties(flags)
                    where !ignore.Contains(pi.Name)
                    let self = type.GetProperty(pi.Name)?.GetValue(instance, null)
                    let foreign = type.GetProperty(pi.Name)?.GetValue(other, null)
                    where self != foreign && (self == null || !self.Equals(foreign))
                    select self;
                return !diff.Any();
            }
            return instance == other;
        }

        public static IEnumerable<object> Differences<T>(this T instance, T other, BindingFlags flags, params string[] ignore)
            where T : class, new()
        {
            if (instance != null && other != null)
            {
                var type = typeof(T);
                var ignoreList = new List<string>(ignore);
                var diff =
                    from pi in type.GetProperties(flags)
                    where !ignoreList.Contains(pi.Name)
                    let self = type.GetProperty(pi.Name)?.GetValue(instance, null)
                    let foreign = type.GetProperty(pi.Name)?.GetValue(other, null)
                    where self != foreign && (self == null || !self.Equals(foreign))
                    select self;
                return diff;
            }
            return Enumerable.Empty<object>();
        }

        public static IEnumerable<object> Differences<T>(this T instance, T other, BindingFlags flags, params Expression<Func<T, object>>[] ignore)
            where T : class, new() => instance.Differences(other, flags, ignore.Select(x => x.GetMemberName()).ToArray());

        public static bool IsConcrete<TSource>() => typeof(TSource).IsConcrete();

        public static bool IsConcrete<TSource>(this TSource source) => source.GetType().IsConcrete();

        public static TAttribute GetCustomAttribute<TSource, TAttribute>() where TAttribute : Attribute
            => typeof(TSource).GetCustomAttribute<TAttribute>();

        public static IEnumerable<TAttribute> GetCustomAttributes<TSource, TAttribute>() where TAttribute : Attribute
            => typeof(TSource).GetCustomAttributes<TAttribute>();

        public static KnownTypeAttribute GetKnownTypeAttribute<TSource>()
            => typeof(TSource).GetCustomAttribute<KnownTypeAttribute>();

        public static IEnumerable<KnownTypeAttribute> GetknownTypeAttributes<TSource>()
            => typeof(TSource).GetCustomAttributes<KnownTypeAttribute>();

        public static IEnumerable<(Type type, string MethodName)> GetMetaDataProperties<TSource>()
            => typeof(TSource).GetMetaDataProperties();

        public static IEnumerable<Type> GetMetaDataTypes<TSource>()
            => typeof(TSource).GetMetaDataTypes();

        public static IEnumerable<string> GetMetaDataMethodNames<TSource>()
            => typeof(TSource).GetMetaDataMethodNames();

        //TODO enumerate metadata from hierarchical objects and types-
    }
}
