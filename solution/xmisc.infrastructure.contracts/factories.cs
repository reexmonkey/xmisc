using reexjungle.xmisc.foundation.contracts;
using System;

namespace reexjungle.xmisc.infrastructure.contracts
{
    /// <summary>
    /// Specifies an interface for a factory creating instances of a given type
    /// </summary>
    public interface IFactory
    {
        /// <summary>
        /// Creates an instance from a  given type.
        /// </summary>
        /// <typeparam name="TValue">The type of the instance to be created.</typeparam>
        /// <returns>The created instance if successful; the default value of TValue.</returns>
        TValue Create<TValue>() where TValue : class, new();

        /// <summary>
        /// Creates an instance from a  given type, whose constructor has initializers.
        /// </summary>
        /// <typeparam name="TValue">The type of the instance to be created.</typeparam>
        /// <param name="args">Constructor arguments to initialize the instance during creation.</param>
        /// <returns>The created instance if successful; the default value of TValue.</returns>
        TValue Create<TValue>(params object[] args) where TValue : class, new();
    }

    /// <summary>
    /// Specifies an interface for a factory creating keyed instances of a given type.
    /// </summary>
    /// <typeparam name="TKey">The type of identifier(key) of the instance</typeparam>
    public interface IFactory<in TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Creates a keyed instance from a given type.
        /// </summary>
        /// <typeparam name="TValue">The type of the instance to be created.</typeparam>
        /// <returns>The created instance if successful; otherwise the default value of TValue.</returns>
        TValue Create<TValue>() where TValue : class;

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TValue">The type of the instance to be created.</typeparam>
        /// <param name="otherArgs">Other constructor arguments to initialize the instance during creation.
        /// Note the other constructor arguments should not include the key as argument!
        /// </param>
        /// <returns>The created instance if successful; otherwise the default value of TValue.</returns>
        TValue Create<TValue>(params object[] otherArgs) where TValue : class;
    }
}