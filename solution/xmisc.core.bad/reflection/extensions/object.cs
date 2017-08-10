using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using reexmonkey.xmisc.core.linq.expressions;
using reexmonkey.xmisc.core.reflection.infrastructure;

namespace reexmonkey.xmisc.core.reflection.extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="name">The name.</param>
        /// <param name="flags">The flags.</param>
        /// <returns></returns>
        public static Func<TSource, object> GetProperty<TSource>(string name, BindingFlags flags = BindingFlags.Public)
        {
            var type = typeof(TSource);
            var pi = type.GetProperty(name, flags);
            if (pi == null || !pi.CanRead) return null;
            var gettermi = pi.GetGetMethod();
            var entity = Expression.Parameter(type);
            var getter = Expression.Call(entity, gettermi);
            var o = Expression.Convert(getter, typeof(object));
            var lambda = Expression.Lambda(o, entity);
            return (Func<TSource, object>)lambda.Compile();
        }

        public static IEnumerable<Func<TSource, object>> GetProperties<TSource>(BindingFlags flags = BindingFlags.Public)
        {
            var type = typeof(TSource);
            var properties = type.GetProperties(flags).Where(x => x.CanRead);
            foreach (var pi in properties)
            {
                var gettermi = pi.GetGetMethod();
                var entity = Expression.Parameter(type);
                var getter = Expression.Call(entity, gettermi);
                var o = Expression.Convert(getter, typeof(object));
                var lambda = Expression.Lambda(o, entity);
                yield return (Func<TSource, object>) lambda.Compile();
            }
        }

        /// <summary>
        /// Gets the properties of an object
        /// </summary>
        /// <param name="source">The source object</param>
        /// <returns>A key-value pair collection of properties, where the key is the name of the property and the value a property element </returns>
        public static Dictionary<string, Property> GetProperties(this object source)
        {
            var properties = new Dictionary<string, Property>();
            foreach (var property in source.GetType().GetProperties())
            {
                var parameters = property.GetIndexParameters();
                var values = new List<object>(parameters.Length);
                if (parameters.Length != 0)
                {
                    for (var index = 0; index < parameters.Length; ++index)
                        values.Add(property.GetValue(source, new object[] { index }));
                }
                else values.Add(property.GetValue(source, null));
                if (!properties.ContainsKey(property.Name))
                    properties.Add(property.Name, new Property(property.Name, property.PropertyType, values));
            }
            return properties;
        }

       
        /// <summary>
        /// Sets the properties of a target object from a given key-value pair collection of property elements
        /// </summary>
        /// <param name="target">The object target</param>
        /// <param name="map">A key-value pair collection of property elements</param>
        /// <returns>The target object, whose properties have been assigned</returns>
        public static object SetProperties(this object target, IDictionary<string, Property> map)
        {
            if (map.Count == 0) return target;

            var type = target.GetType();
            foreach (var pair in map)
            {
                if (pair.Value.Values.Count == 0) continue;
                var pi = type.GetProperty(pair.Key);
                if (pi == null) continue;
                if (pair.Value.IsIndexed)
                {
                    for (var index = 0; index < pair.Value.Values.Count; ++index)
                    {
                        pi.SetValue(target, pair.Value.Values[index], new object[] { index });
                    }
                }
                else pi.SetValue(target, pair.Value.Values[0], null);
            }

            return target;
        }

        public static object SetProperties(this object target, IEnumerable<(string name, Property property)> tuples)
        {
            var tupleList = tuples as IList<(string name, Property property)> ?? tuples.ToList();
            if (!tupleList.Any()) return target;

            var type = target.GetType();
            foreach (var tuple in tupleList)
            {
                if (tuple.property.Values.Count == 0) continue;
                var pi = type.GetProperty(tuple.name);
                if (pi == null) continue;
                if (tuple.property.IsIndexed)
                {
                    for (var index = 0; index < tuple.property.Values.Count; ++index)
                        pi.SetValue(target, tuple.property.Values[index], new object[] {index});
                }
                else pi.SetValue(target, tuple.property.Values[0], null);
            }

            return target;
        }

        /// <summary>
        /// Copies the properties from the <paramref name="source"/> object to the <paramref name="target"/> object.
        /// </summary>
        /// <param name="source">The source object</param>
        /// <param name="target">The target object</param>
        /// <returns>The converted target object</returns>
        public static object CopyProperties(this object target, object source)
        {
            var stype = source.GetType();
            var ttype = target.GetType();
            foreach (var properties in stype.GetProperties())
            {
                var indexparams = properties.GetIndexParameters();
                if (indexparams.Length != 0)
                {
                    for (var index = 0; index < indexparams.Length; ++index)
                    {
                        var pindex = new object[] { index };
                        if (target is string) target = properties.GetValue(source, pindex);
                        else ttype.GetProperty(properties.Name)?.SetValue(target, properties.GetValue(source, pindex), pindex);
                    }
                }
                else
                {
                    if (target is string) target = properties.GetValue(source, null);
                    else target.GetType().GetProperty(properties.Name)?.SetValue(target, properties.GetValue(source, null), null);
                }
            }

            return target;
        }


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



    }
}
