using System;

namespace reexjungle.xmisc.infrastructure.contracts.factories
{
    /// <summary>
    /// Specifies a factory contract to create instances of a given type based on the registered constructor of the type.
    /// </summary>
    public interface ISimpleFactory
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
}