using reexjungle.xmisc.infrastructure.contracts;
using System;

namespace reexjungle.xmisc.infrastructure.concretes.operations
{
    /// <summary>
    ///Represents a factory that creates instances of a given type
    /// </summary>
    public class Factory : IFactory
    {
        /// <summary>
        /// Creates an instance from a  given type
        /// </summary>
        /// <typeparam name="TValue">The type of the instance to be created</typeparam>
        /// <returns>The created instance if successful; the default value of TValue</returns>
        public TValue Create<TValue>() where TValue : class, new()
        {
            try
            {
                return Activator.CreateInstance<TValue>();
            }
            catch (MissingMethodException)
            {
                return default(TValue);
            }
        }

        public TValue Create<TValue>(params object[] args) where TValue : class
        {
            try
            {
                return (TValue)Activator.CreateInstance(typeof(TValue), args);
            }
            catch (MissingMethodException)
            {
                return default(TValue);
            }
        }
    }

    /// <summary>
    /// Specifies a factory for creating instances constrained by a given interface.
    /// </summary>
    /// <typeparam name="TInterface">The contract by which the created instance is constrained.</typeparam>
    public class Factory<TInterface> : IFactory<TInterface>
    {
        /// <summary>
        /// Creates an instance of a given type that is constrained by an interface.
        /// </summary>
        /// <typeparam name="TValue">The type of instance to create.</typeparam>
        /// <returns>The contract by which the created type is constrained.</returns>
        public TInterface Create<TValue>() where TValue : class, TInterface, new()
        {
            return Activator.CreateInstance<TValue>();
        }

        /// <summary>
        /// Creates and initializes an instance of a given type that is constrained by an interface.
        /// </summary>
        /// <typeparam name="TValue">The type of instance to create.</typeparam>
        /// <param name="args">The constructor arguments to initialize the created instanced.</param>
        /// <returns>The contract by which the created type is constrained.</returns>
        public TInterface Create<TValue>(params object[] args) where TValue : class, TInterface
        {
            try
            {
                return (TValue)Activator.CreateInstance(typeof(TValue), args);
            }
            catch (MissingMethodException)
            {
                return default(TValue);
            }
        }
    }
}