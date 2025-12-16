using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace reexmonkey.xmisc.core.reflection.extensions
{
    /// <summary>
    /// Provides extended features for custom attributes.
    /// </summary>
    public static class AttributeExtensions
    {
        /// <summary>
        /// Gets custom attributes from a custom attribute provider.
        /// </summary>
        /// <typeparam name="TAttribute">The type of custom attribute.</typeparam>
        /// <param name="provider">The provider that offers custom attributes.</param>
        /// <param name="inherit">Indicates whether the custom attribute can be inherited by derived classes and overriding members.</param>
        /// <returns>A sequence of custom attributes.</returns>
        public static IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(this ICustomAttributeProvider provider, bool inherit) where TAttribute : Attribute
            => provider.GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>();

        /// <summary>
        /// Gets the properties of the given attribute.
        /// <para/>Each property is a tuple consisting of a type and name of a method 
        /// that returns a list of types that should be recognized during serialization or deserialization.
        /// <para/>The System.Runtime.Serialization.DataContractSerializer or a custom serializer could be reponsible for the serialization or deserialization.
        /// </summary>
        /// <param name="attribute">The attribute, whose properties are determined.</param>
        /// <returns>The properties of the given <paramref name="attribute"/>.</returns>
        public static (Type type, string MethodName) GetProperties(this KnownTypeAttribute attribute)
            => (attribute.Type, attribute.MethodName);

        /// <summary>
        /// Gets the properties of the given sequence of attributes.
        /// <para/>Each property is a tuple consisting of a type and name of a method 
        /// that returns a list of types that should be recognized during serialization or deserialization.
        /// <para/>The System.Runtime.Serialization.DataContractSerializer or a custom serializer could be reponsible for the serialization or deserialization.
        /// </summary>
        /// <param name="attributes">The sequence of attributes, whose properties are determined.</param>
        /// <returns>The properties of the given <paramref name="attributes"/>.</returns>
        public static IEnumerable<(Type type, string MethodName)> GetProperties(this IEnumerable<KnownTypeAttribute> attributes)
            => attributes.Select(x => x.GetProperties());

        /// <summary>
        /// Gets the types that should be recognized during serialization or deserialization from the given <paramref name="attributes"/>.
        /// <para/>The System.Runtime.Serialization.DataContractSerializer or a custom serializer could be reponsible for the serialization of deserialization.
        /// </summary>
        /// <param name="attributes">The attributes that contains declared data types that shall be recognized during serialization or deserialization.</param>
        /// <returns>The declared types of <paramref name="attributes"/>.</returns>
        public static IEnumerable<Type> GetTypes(this IEnumerable<KnownTypeAttribute> attributes)
            => attributes.Select(x => x.Type);

        /// <summary>
        /// Gets the method names that will each return a list of types that should be recognized during serialization or deserialization.
        /// <para/>The System.Runtime.Serialization.DataContractSerializer or a custom serializer could be reponsible for the serialization or deserialization.
        /// </summary>
        /// <param name="attributes">The attributes that contains declared data types that shall be recognized during serialization or deserialization.</param>
        /// <returns>The method names that will each return a list of types that should be recognized during serialization or deserialization.</returns>
        public static IEnumerable<string> GetMethodNames(this IEnumerable<KnownTypeAttribute> attributes)
            => attributes.Select(x => x.MethodName);

        /// <summary>
        /// Gets the properties of the given sequence of known type attributes and filters the results based on a <paramref name="predicate"/>.
        /// <para/>Each property is a tuple consisting of a type and name of a method 
        /// that returns a list of types that should be recognized during serialization or deserialization.
        /// <para/>The System.Runtime.Serialization.DataContractSerializer or a custom serializer could be reponsible for the serialization or deserialization.
        /// </summary>
        /// <param name="attributes">The sequence of attributes, whose properties are determined.</param>
        /// <param name="predicate">A function to test each known type attribute for a condition.</param>
        /// <returns>The found properties of known type attributes that match the search condition specified by the <paramref name="predicate"/>.</returns>
        public static IEnumerable<(Type type, string MethodName)> FindProperties(this IEnumerable<KnownTypeAttribute> attributes, Func<KnownTypeAttribute, bool> predicate)
            => attributes.Where(predicate)?.Select(x => x.GetProperties());

        /// <summary>
        /// Gets the sequence of types that should be recognized during serialization or deserialization of known type attributes 
        /// and filters the results based on a <paramref name="predicate"/>.
        /// <para/>The System.Runtime.Serialization.DataContractSerializer or a custom serializer could be reponsible for the serialization or deserialization.
        /// </summary>
        /// <param name="attributes">The sequence of attributes, whose declared tpes are determined.</param>
        /// <param name="predicate">A function to test each known type attribute for a condition.</param>
        /// <returns>The found declared types of known type attributes that match the search condition specified by the <paramref name="predicate"/>.</returns>
        public static IEnumerable<Type> FindTypes(this IEnumerable<KnownTypeAttribute> attributes, Func<KnownTypeAttribute, bool> predicate)
            => attributes.Where(predicate)?.Select(x => x.Type);

        /// <summary>
        /// Gets the method names that will each return a list of types that should be recognized during serialization or deserialization of known type attributes 
        /// and filters the results based on a <paramref name="predicate"/>.
        /// <para/>The System.Runtime.Serialization.DataContractSerializer or a custom serializer could be reponsible for the serialization or deserialization.
        /// </summary>
        /// <param name="attributes">The sequence of attributes, whose declared method names are determined.</param>
        /// <param name="predicate">A function to test each known type attribute for a condition.</param>
        /// <returns>The found method names of known type attributes that match the search condition specified by the <paramref name="predicate"/>.</returns>
        public static IEnumerable<string> FindMethodNames(this IEnumerable<KnownTypeAttribute> attributes, Func<KnownTypeAttribute, bool> predicate)
            => attributes.Where(predicate)?.Select(x => x.MethodName);
    }
}
