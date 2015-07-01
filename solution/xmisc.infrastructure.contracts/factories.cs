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
        TValue Create<TValue>(params object[] args) where TValue : class;
    }

    /// <summary>
    /// Specifies a factory for creating instances constrained by a given interface.
    /// </summary>
    /// <typeparam name="TInterface">The contract by which the created instance is constrained.</typeparam>
    public interface IFactory<TInterface>
    {
        /// <summary>
        /// Creates an instance of a given type that is constrained by an interface.
        /// </summary>
        /// <typeparam name="TValue">The type of instance to create.</typeparam>
        /// <returns>The contract by which the created type is constrained.</returns>
        TInterface Create<TValue>() where TValue : class, TInterface, new();

        /// <summary>
        /// Creates and initializes an instance of a given type that is constrained by an interface.
        /// </summary>
        /// <typeparam name="TValue">The type of instance to create.</typeparam>
        /// <param name="args">The constructor arguments to initialize the created instanced.</param>
        /// <returns>The contract by which the created type is constrained.</returns>
        TInterface Create<TValue>(params object[] args) where TValue : class, TInterface;
    }
}