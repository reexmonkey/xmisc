using reexjungle.xmisc.foundation.concretes;
using reexjungle.xmisc.foundation.contracts;
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

        public TValue Create<TValue>(params object[] args) where TValue : class, new()
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
    /// Represents a factory creating keyed instances of a given type.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class Factory<TKey> : IFactory<TKey> where TKey : IEquatable<TKey>
    {
        private readonly IKeyGenerator<TKey> keyGenerator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="keyGenerator">Key generator to provide keys for the created instances</param>
        public Factory(IKeyGenerator<TKey> keyGenerator)
        {
            this.keyGenerator.ThrowIfNull();
            this.keyGenerator = keyGenerator;
        }

        /// <summary>
        /// Creates a keyed instance from a given type.
        /// </summary>
        /// <typeparam name="TValue">The type of the instance to be created.</typeparam>
        /// <returns>The created instance if successful; otherwise the default value of TValue.</returns>
        public TValue Create<TValue>() where TValue : class
        {
            try
            {
                return (TValue)Activator.CreateInstance(typeof(TValue), keyGenerator.GetNext());
            }
            catch (MissingMethodException)
            {
                return default(TValue);
            }
        }

        public TValue Create<TValue>(params object[] otherArgs) where TValue : class
        {
            try
            {
                return (TValue)Activator.CreateInstance(typeof(TValue), keyGenerator.GetNext(), otherArgs);
            }
            catch (MissingMethodException)
            {
                return default(TValue);
            }
        }
    }
}