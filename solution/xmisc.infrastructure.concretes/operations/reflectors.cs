using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace reexjungle.xmisc.infrastructure.concretes.operations
{
    /// <summary>
    /// Represents a basic property structure of an object
    /// </summary>
    public class PropertyElement
    {
        /// <summary>
        /// Gets or sets the name of the property
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the property
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets the values of the property in case the property is indexed, otherwise the first element is the value
        /// </summary>
        public List<object> Values { get; set; }

        /// <summary>
        /// Checks if the property has indexed values
        /// </summary>
        public bool IsIndexed
        {
            get { return Values.Count > 1; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public PropertyElement()
        {
            Name = string.Empty;
            Type = typeof(object);
            Values = new List<object>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <param name="type">The type of the property</param>
        /// <param name="values">The values of the property in case the property is indexed, otherwise the first element is the value</param>
        public PropertyElement(string name, Type type, IEnumerable<object> values)
        {
            Name = name;
            Type = type;
            Values = new List<object>(values);
        }

        /// <summary>
        /// Clones this instance memberwise
        /// </summary>
        /// <returns>A clone of this instance</returns>
        public PropertyElement Clone()
        {
            return new PropertyElement(this.Name, this.Type, this.Values);
        }
    }

    /// <summary>
    /// Represents a basic property structure of a generic instance
    /// </summary>
    public class PropertyElement<TInstance>
    {
        /// <summary>
        /// Gets or sets the name of the property
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the values of the property in case the property is indexed, otherwise the first element is the value
        /// </summary>
        public IEnumerable<TInstance> Values { get; set; }

        /// <summary>
        /// Checks if the property has indexed values
        /// </summary>
        public bool IsIndexed
        {
            get { return Values.Count() > 1; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public PropertyElement()
        {
            Name = string.Empty;
            Values = new List<TInstance>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <param name="values">The values of the property in case the property is indexed, otherwise the first element is the value</param>
        public PropertyElement(string name, IEnumerable<TInstance> values)
        {
            Name = name;
            Values = new List<TInstance>(values);
        }

        /// <summary>
        /// Clones this instance
        /// </summary>
        /// <returns>A clone of this instance</returns>
        public PropertyElement<TInstance> Clone()
        {
            return new PropertyElement<TInstance>(this.Name, this.Values);
        }
    }

    /// <summary>
    /// Provides reflection helper functionality
    /// </summary>
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Gets the properties of an object
        /// </summary>
        /// <param name="source">The source object</param>
        /// <returns>A key-value pair collection of properties, where the key is the name of the property and the value a property element </returns>
        public static IDictionary<string, PropertyElement> GetProperties(this object source)
        {
            IDictionary<string, PropertyElement> properties = new Dictionary<string, PropertyElement>();

            foreach (var prop in source.GetType().GetProperties())
            {
                PropertyElement element = new PropertyElement();
                element.Name = prop.Name;
                element.Type = prop.PropertyType;
                var piinfos = prop.GetIndexParameters();
                if (piinfos.Length != 0)
                {
                    for (int p = 0; p < piinfos.Length; ++p) element.Values.Add(prop.GetValue(source, new object[] { p }));
                }
                else element.Values.Add(prop.GetValue(source, null));

                if (!properties.ContainsKey(prop.Name)) properties.Add(prop.Name, element);
            }

            return properties;
        }

        /// <summary>
        /// Sets the properties of a target object from a given key-value pair collection of property elements
        /// </summary>
        /// <param name="target">The object target</param>
        /// <param name="property_elements">A key-value pair collection of property elements</param>
        /// <returns>The target object, whose properties have been assigned</returns>
        public static object SetProperties(this object target, IDictionary<string, PropertyElement> property_elements)
        {
            if (property_elements.Count == 0) return target;

            Type type = target.GetType();
            foreach (var pair in property_elements)
            {
                try
                {
                    if (pair.Value.Values.Count == 0) continue;
                    if (pair.Value.IsIndexed)
                    {
                        for (int p = 0; p < pair.Value.Values.Count; ++p) type.GetProperty(pair.Key).SetValue(target, pair.Value.Values[p], new object[] { p });
                    }
                    else type.GetProperty(pair.Key).SetValue(target, pair.Value.Values[0], null);
                }
                catch (ArgumentNullException) { throw; }
                catch (TargetException) { throw; }
                catch (TargetParameterCountException) { throw; }
                catch (MethodAccessException) { throw; }
                catch (TargetInvocationException) { throw; }
                catch (ArgumentException) { throw; }
                catch (AmbiguousMatchException) { throw; }
                catch (Exception) { throw; }
            }

            return target;
        }

        /// <summary>
        /// Copies the properties to a target object from this object
        /// </summary>
        /// <param name="source">The source object</param>
        /// <param name="target">The target object</param>
        /// <returns>The converted target object</returns>
        public static object CopyPropertiesTo(this object source, object target)
        {
            if (target == null) throw new NullReferenceException();

            Type type = source.GetType();

            foreach (var prop in type.GetProperties())
            {
                var piinfos = prop.GetIndexParameters();
                try
                {
                    if (piinfos.Length != 0)
                    {
                        for (int p = 0; p < piinfos.Length; ++p)
                        {
                            var pindex = new object[] { p };
                            if (target is string) target = prop.GetValue(source, pindex);
                            else target.GetType().GetProperty(prop.Name).SetValue(target, prop.GetValue(source, pindex), pindex);
                        }
                    }
                    else
                    {
                        if (target is string) target = prop.GetValue(source, null);
                        else target.GetType().GetProperty(prop.Name).SetValue(target, prop.GetValue(source, null), null);
                    }
                }
                catch (ArgumentNullException) { throw; }
                catch (TargetException) { throw; }
                catch (TargetParameterCountException) { throw; }
                catch (MethodAccessException) { throw; }
                catch (TargetInvocationException) { throw; }
                catch (ArgumentException) { throw; }
                catch (AmbiguousMatchException) { throw; }
                catch (Exception) { throw; }
            }

            return target;
        }

        /// <summary>
        /// Copies poperties from a source object to this object
        /// </summary>
        /// <param name="instance">The target object</param>
        /// <param name="other">The source object</param>
        /// <returns>The converted target object</returns>
        public static object CopyPropertiesFrom(this object instance, object other)
        {
            if (instance == null) throw new NullReferenceException();
            var type = other.GetType();
            foreach (var prop in type.GetProperties())
            {
                var piinfos = prop.GetIndexParameters();
                try
                {
                    if (piinfos.Length != 0)
                    {
                        for (int p = 0; p < piinfos.Length; ++p)
                        {
                            var pindex = new object[] { p };
                            if (instance is string) instance = prop.GetValue(other, pindex);
                            else instance.GetType().GetProperty(prop.Name).SetValue(instance, prop.GetValue(other, pindex), pindex);
                        }
                    }
                    else
                    {
                        if (instance is string) instance = prop.GetValue(other, null);
                        else instance.GetType().GetProperty(prop.Name).SetValue(instance, prop.GetValue(other, null), null);
                    }
                }
                catch (ArgumentNullException) { throw; }
                catch (TargetException) { throw; }
                catch (TargetParameterCountException) { throw; }
                catch (MethodAccessException) { throw; }
                catch (TargetInvocationException) { throw; }
                catch (ArgumentException) { throw; }
                catch (AmbiguousMatchException) { throw; }
                catch (Exception) { throw; }
            }

            return instance;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="other"></param>
        /// <param name="ignore"></param>
        /// <returns></returns>
        public static bool PropertiesEquals<T>(this T instance, T other, params string[] ignore) where T : class
        {
            if (instance != null && other != null)
            {
                var type = typeof(T);
                var ignoreList = new List<string>(ignore);
                var diff =
                    from pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    where !ignoreList.Contains(pi.Name)
                    let selfValue = type.GetProperty(pi.Name).GetValue(instance, null)
                    let toValue = type.GetProperty(pi.Name).GetValue(other, null)
                    where selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue))
                    select selfValue;
                return !diff.Any();
            }
            return instance == other;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="other"></param>
        /// <param name="ignore"></param>
        /// <returns></returns>
        public static IEnumerable<object> Differences<T>(this T instance, T other, params string[] ignore)
            where T : class, new()
        {
            if (instance != null && other != null)
            {
                var type = typeof(T);
                var ignoreList = new List<string>(ignore);
                var diff =
                    from pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    where !ignoreList.Contains(pi.Name)
                    let selfValue = type.GetProperty(pi.Name).GetValue(instance, null)
                    let toValue = type.GetProperty(pi.Name).GetValue(other, null)
                    where selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue))
                    select selfValue;
                return diff;
            }

            return new List<object>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="other"></param>
        /// <param name="ignore"></param>
        /// <returns></returns>
        public static IEnumerable<object> Differences<T>(this T instance, T other, params Expression<Func<T, object>>[] ignore)
            where T : class, new()
        {
            if (instance != null && other != null)
            {
                var type = typeof(T);
                var iparams = new List<Expression<Func<T, object>>>(ignore);
                var ignoreList = new List<string>(iparams.Count);
                iparams.ForEach(x =>
                {
                    var name = (x != null) ? (x.Body as MemberExpression).Member.Name : null;
                    if (name != null) ignoreList.Add(name);
                });

                var diff =
                    from pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    where !ignoreList.Contains(pi.Name)
                    let selfValue = type.GetProperty(pi.Name).GetValue(instance, null)
                    let toValue = type.GetProperty(pi.Name).GetValue(other, null)
                    where selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue))
                    select selfValue;
                return diff;
            }

            return new List<object>();
        }

        /// <summary>
        /// Gets the assembly for a specified type
        /// </summary>
        /// <param name="type">The specified type, whose assembly is being searched</param>
        /// <returns></returns>
        public static Assembly GetAssembly(this Type type)
        {
            Assembly assembly = null;
            try
            {
                assembly = Assembly.GetAssembly(type);
            }
            catch (System.ArgumentNullException)
            {
                throw;
            }

            return assembly;
        }

        public static Assembly GetAssembly<TInstance>(this TInstance value)
        {
            Assembly assembly = null;
            try
            {
                assembly = Assembly.GetAssembly(typeof(TInstance));
            }
            catch (ArgumentNullException) { throw; }
            catch (Exception) { throw; }
            return assembly;
        }

        public static Assembly GetAssembly<TInstance>()
        {
            Assembly assembly = null;
            try
            {
                assembly = Assembly.GetAssembly(typeof(TInstance));
            }
            catch (ArgumentNullException) { throw; }
            catch (Exception) { throw; }
            return assembly;
        }

        public static string GetAssemblyPath(this Type type)
        {
            string path = null;
            try
            {
                path = Assembly.GetAssembly(type).Location;
            }
            catch (ArgumentNullException) { throw; }
            catch (Exception) { throw; }
            return path;
        }

        public static string GetAssemblyPath<TInstance>(this TInstance value)
        {
            string path = string.Empty;
            try
            {
                path = Assembly.GetAssembly(typeof(TInstance)).Location;
            }
            catch (System.ArgumentNullException)
            {
                throw;
            }
            return path;
        }

        public static string GetAssemblyPath<T>()
        {
            string path = string.Empty;
            try
            {
                path = Assembly.GetAssembly(typeof(T)).Location;
            }
            catch (System.ArgumentNullException)
            {
                throw;
            }
            return path;
        }

        public static string[] GetAssembyPaths(this Type[] types)
        {
            IEnumerable<string> paths = null;
            try
            {
                paths = types.Select(x => Assembly.GetAssembly(x).Location);
            }
            catch (ArgumentNullException) { throw; }
            catch (Exception) { throw; }

            return paths.ToArray();
        }

        public static string[] GetAssembyPaths<TInstance>(this TInstance[] objects)
        {
            IEnumerable<string> paths = null;
            try
            {
                paths = objects.Select(x => Assembly.GetAssembly(typeof(TInstance)).Location);
            }
            catch (ArgumentNullException) { throw; }
            catch (Exception) { throw; }

            return paths.ToArray();
        }

        public static string[] GetReferencedAssemblyNames(this Assembly assembly)
        {
            string[] references = null;
            try
            {
                references = assembly.GetReferencedAssemblies().Select(x => string.Format("{0}.dll", x.Name)).ToArray();
            }
            catch (ArgumentNullException) { throw; }
            catch (Exception) { throw; }
            return references;
        }

        public static string[] GetReferencedAssemblyNamesFromEntryAssembly()
        {
            string[] references = null;
            try
            {
                references = Assembly.GetEntryAssembly().GetReferencedAssemblies().Select(x => string.Format("{0}.dll", x.Name)).ToArray();
            }
            catch (ArgumentNullException) { throw; }
            catch (Exception) { throw; }
            return references;
        }

        public static string[] GetReferencedAssemblyNamesFromExecutingAssembly()
        {
            string[] references = null;
            try
            {
                references = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(x => string.Format("{0}.dll", x.Name)).ToArray();
            }
            catch (ArgumentNullException) { throw; }
            catch (Exception) { throw; }
            return references;
        }

        public static FileVersionInfo GetFileVersionInfo(this Assembly assembly)
        {
            return FileVersionInfo.GetVersionInfo(assembly.Location);
        }

        public static Version GetVersionInfo(this Assembly assembly)
        {
            return assembly.GetName().Version;
        }

        /// <summary>
        /// Gets the application path of current executing application
        /// </summary>
        /// <returns>The application path</returns>
        public static string GetExecutingAssemblyApplicationPath()
        {
            return Assembly.GetExecutingAssembly().Location;
        }

        /// <summary>
        /// Gets the application path of first executable
        /// </summary>
        /// <returns>The application path</returns>
        public static string GetEntryAssemblyApplicationPath()
        {
            return Assembly.GetEntryAssembly().Location;
        }
    }
}