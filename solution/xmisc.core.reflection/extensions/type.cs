using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace reexmonkey.xmisc.core.reflection.extensions
{

    public static class TypeExtensions
    {
        public static bool IsConcrete(this Type type)
        {
            var ti = type.GetTypeInfo();
            return ti.IsValueType || (ti.IsClass && !ti.IsAbstract);
        }

        public static TAttribute GetCustomAttribute<TAttribute>(this Type source) where TAttribute : Attribute
        {
            var ti = source.GetTypeInfo();
            return (TAttribute)ti.GetCustomAttribute(typeof(TAttribute));
        }

        public static IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(this Type source) where TAttribute : Attribute
        {
            var ti = source.GetTypeInfo();
            return ti.GetCustomAttributes().OfType<TAttribute>();
        }

        public static KnownTypeAttribute GetKnownTypeAttribute(this Type source) => source.GetCustomAttribute<KnownTypeAttribute>();

        public static IEnumerable<KnownTypeAttribute> GetknownTypeAttributes(this Type source) => source.GetCustomAttributes<KnownTypeAttribute>();

        public static IEnumerable<(Type type, string MethodName)> GetMetaDataProperties(this Type source)
        {
            return source.GetknownTypeAttributes().GetProperties();
        }

        public static IEnumerable<Type> GetMetaDataTypes(this Type source)
        {
            return source.GetknownTypeAttributes().GetTypes();
        }

        public static IEnumerable<string> GetMetaDataMethodNames(this Type source)
        {
            return source.GetknownTypeAttributes().GetMethodNames();
        }
    }
}
