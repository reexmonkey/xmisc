using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace reexmonkey.xmisc.core.reflection.extensions
{
    /// <summary>
    /// Extends reflection features for generic types.
    /// </summary>
    public static class GenericExtensions
    {
        /// <summary>
        /// Determines whether this data type is a concrete type.
        /// </summary>
        /// <typeparam name="T">The type of data to check.</typeparam>
        /// <returns>
        ///   <c>true</c> if this datatype is concrete; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsConcrete<T>() => typeof(T).IsConcrete();

        /// <summary>
        /// Determines whether this instance is concrete.
        /// </summary>
        /// <typeparam name="T">The type of data.</typeparam>
        /// <param name="source">The instance of the data type to check.</param>
        /// <returns>
        ///   <c>true</c> if the specified source is concrete; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsConcrete<T>(this T source) => source.GetType().IsConcrete();

        /// <summary>
        /// Gets the custom attribute declared for the specified data type.
        /// </summary>
        /// <typeparam name="T">The type of the data, for which the custom attribute is declared.</typeparam>
        /// <typeparam name="TAttribute">The type of custom attribute.</typeparam>
        /// <returns>The custom attribute declared for the specified data type.</returns>
        public static TAttribute GetCustomAttribute<T, TAttribute>() where TAttribute : Attribute
            => typeof(T).GetCustomAttribute<TAttribute>();

        /// <summary>
        /// Gets the custom attributes declared for the specified data type.
        /// </summary>
        /// <typeparam name="T">The type of the data, for which the custom attributes are declared.</typeparam>
        /// <typeparam name="TAttribute">The type of custom attributes.</typeparam>
        /// <returns>The custom attributes declared for the specified data type.</returns>
        public static IEnumerable<TAttribute> GetCustomAttributes<T, TAttribute>() where TAttribute : Attribute
            => typeof(T).GetCustomAttributes<TAttribute>();

        /// <summary>
        /// Gets the known type attribute declared for the specified data type.
        /// </summary>
        /// <typeparam name="T">The type of the data, for which known type attributes are declared.</typeparam>
        /// <returns>The known type attribute declared for the specified data type.</returns>
        public static KnownTypeAttribute GetKnownTypeAttribute<T>()
            => typeof(T).GetCustomAttribute<KnownTypeAttribute>();

        /// <summary>
        /// Gets known type attributes declared for the specified data type.
        /// </summary>
        /// <typeparam name="T">The type of the data, for which known type attributes are declared.</typeparam>
        /// <returns>The known type attributes declared for the specified data type.</returns>
        public static IEnumerable<KnownTypeAttribute> GetknownTypeAttributes<T>()
            => typeof(T).GetCustomAttributes<KnownTypeAttribute>();

        /// <summary>
        /// Gets the metadata properties declared for the given type of data.
        /// <para/>Each metadata property is a tuple consisting of a type and name of a method,
        /// which returns a list of types that should be recognized during serialization or deserialization.
        /// <para/>The System.Runtime.Serialization.DataContractSerializer or a custom serializer could be reponsible for the serialization or deserialization.
        /// /// </summary>
        /// <typeparam name="T">The type of data.</typeparam>
        /// <returns>The meta data properties declared for the type of data <typeparamref name="T"/>.</returns>
        public static IEnumerable<(Type type, string MethodName)> GetMetaDataProperties<T>()
            => typeof(T).GetMetaDataProperties();

        /// <summary>
        /// Gets the metadata types declared for the given type of data.
        /// <para/>The System.Runtime.Serialization.DataContractSerializer or a custom serializer could be reponsible for the serialization or deserialization.
        /// /// </summary>
        /// <typeparam name="T">The type of data.</typeparam>
        /// <returns>The metadata types declared for the type <typeparamref name="T"/>.</returns>
        public static IEnumerable<Type> GetMetaDataTypes<T>()
            => typeof(T).GetMetaDataTypes();

        /// <summary>
        /// Gets the names of methods (returns a list of types that should be recognized during serialization or deserialization) declared for the given type of data.
        /// <para/>The System.Runtime.Serialization.DataContractSerializer or a custom serializer could be reponsible for the serialization or deserialization.
        /// /// </summary>
        /// <typeparam name="T">The type of data.</typeparam>
        /// <returns>The meta data properties declared for the type <typeparamref name="T"/>.</returns>
        public static IEnumerable<string> GetMetaDataMethodNames<T>()
            => typeof(T).GetMetaDataMethodNames();

        //TODO enumerate metadata from hierarchical objects and types-
    }
}
