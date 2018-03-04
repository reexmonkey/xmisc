using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace reexmonkey.xmisc.core.reflection.extensions
{
    public static class AttributeExtensions
    {
        public static IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(this ICustomAttributeProvider provider, bool inherit) where TAttribute : Attribute
            => provider.GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>();

        public static (Type type, string MethodName) GetProperties(this KnownTypeAttribute attribute)
            => (attribute.Type, attribute.MethodName);

        public static IEnumerable<(Type type, string MethodName)> GetProperties(this IEnumerable<KnownTypeAttribute> attributes)
            => attributes.Select(x => x.GetProperties());

        public static IEnumerable<Type> GetTypes(this IEnumerable<KnownTypeAttribute> attributes)
            => attributes.Select(x => x.Type);

        public static IEnumerable<string> GetMethodNames(this IEnumerable<KnownTypeAttribute> attributes)
            => attributes.Select(x => x.MethodName);

        public static IEnumerable<(Type type, string MethodName)> FindProperties(this IEnumerable<KnownTypeAttribute> attributes, Func<KnownTypeAttribute, bool> predicate)
            => attributes.Where(predicate)?.Select(x => x.GetProperties());

        public static IEnumerable<Type> FindTypes(this IEnumerable<KnownTypeAttribute> attributes, Func<KnownTypeAttribute, bool> predicate)
            => attributes.Where(predicate)?.Select(x => x.Type);

        public static IEnumerable<string> FindMethodNames(this IEnumerable<KnownTypeAttribute> attributes, Func<KnownTypeAttribute, bool> predicate)
            => attributes.Where(predicate)?.Select(x => x.MethodName);
    }
}
