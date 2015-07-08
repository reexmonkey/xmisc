using System;

namespace reexjungle.xmisc.infrastructure.contracts
{
    /// <summary>
    /// Specifies a factory contract to create instances of a given type based on the registered constructor of the type.
    /// </summary>
    public interface IFactory
    {
        /// <summary>
        /// Creates an instance of a type based on its registered constructor.
        /// </summary>
        /// <typeparam name="TValue">The type of instances to be created by the factory.</typeparam>
        /// <returns>The created instance of the type.</returns>
        TValue Create<TValue>();

        /// <summary>
        /// Registers the constructor of the type to be created.
        /// Note: Only a single constructor for a particular type can be registered with this method!
        /// </summary>
        /// <param name="ctor">The constructor of the type to be created.</param>
        /// <typeparam name="TValue">The type of instances to be created by the factory.</typeparam>
        void Register<TValue>(Func<TValue> ctor);

        /// <summary>
        /// Unregisters a type constructor from the factory.
        /// </summary>
        /// <typeparam name="TValue">The type of the constructor, which is to be unregistered.</typeparam>
        void Unregister<TValue>();
    }

    /// <summary>
    /// Specifies a factory contract to create keyed instances of a given type based on the registered constructors of the type.
    /// </summary>
    /// <typeparam name="TKey">The type of the key used to associate constructors to a given type.</typeparam>
    public interface IKeyedFactory<in TKey>
    {
        /// <summary>
        /// Creates an instance of a type based on its registered constructors.
        /// The specific constructor is resolved through the key.
        /// </summary>
        /// <param name="key">The key to find the constructor of the type</param>
        /// <typeparam name="TValue">The type of instances to be created by the factory.</typeparam
        /// <returns>The created instance of the type</returns>
        TValue Create<TValue>(TKey key);

        /// <summary>
        /// Registers the constructors of a type.
        /// Note: Multiple constructors are allowed, as long as the key for each registered constructor is unique
        /// </summary>
        /// <param name="key">The key to associate to the constructor of the given type.</param>
        /// <param name="ctor">The constructor of the given type.</param>
        /// <typeparam name="TValue">The type of instances to be created by the factory.</typeparam>
        void Register<TValue>(TKey key, Func<TValue> ctor);

        /// <summary>
        /// Unregisters a keyed type constructor from the factory.
        /// </summary>
        /// <param name="key">The key to resolve the type constructor.</param>
        void Unregister(TKey key);
    }
}