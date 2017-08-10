using System;

namespace reexjungle.xmisc.infrastructure.contracts.factories
{
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