using reexjungle.xmisc.infrastructure.contracts;
using System;
using System.Collections.Generic;

namespace reexjungle.xmisc.infrastructure.concretes.operations
{
    /// <summary>
    /// Provides a facory for creating instances of given type.
    /// </summary>
    public class Factory :
        ISimpleFactory,
        IKeyedFactory<string>,
        IKeyedFactory<Guid>,
        IKeyedFactory<int>,
        IKeyedFactory<ulong>,
        IKeyedFactory<ushort>
    {
        private readonly IDictionary<Type, Func<object>> tmap;
        private readonly IDictionary<string, Func<object>> smap;
        private readonly IDictionary<Guid, Func<object>> gmap;
        private readonly IDictionary<int, Func<object>> imap;
        private readonly IDictionary<ulong, Func<object>> ulmap;
        private readonly IDictionary<ushort, Func<object>> usmap;

        /// <summary>
        /// Constructor
        /// </summary>
        public Factory()
        {
            tmap = new Dictionary<Type, Func<object>>();
            smap = new Dictionary<string, Func<object>>();
            gmap = new Dictionary<Guid, Func<object>>();
            imap = new Dictionary<int, Func<object>>();
            ulmap = new Dictionary<ulong, Func<object>>();
            usmap = new Dictionary<ushort, Func<object>>();
        }

        /// <summary>
        /// Creates an instance of a type based on its registered constructor.
        /// </summary>
        /// <typeparam name="TValue">The type of instances to be created by the factory.</typeparam>
        /// <returns>The created instance of the type.</returns>
        public TValue Create<TValue>()
        {
            var type = typeof(TValue);
            return tmap.ContainsKey(type)
                ? (TValue)tmap[type]()
                : default(TValue);
        }

        /// <summary>
        /// Registers the constructor of the type to be created.
        /// Note: Only a single constructor for a particular type can be registered with this method!
        /// </summary>
        /// <param name="ctor">The constructor of the type to be created.</param>
        /// <typeparam name="TValue">The type of instances to be created by the factory.</typeparam>
        public void Register<TValue>(Func<TValue> ctor)
        {
            if (ctor == null) throw new ArgumentNullException("ctor");
            var type = typeof(TValue);
            if (!tmap.ContainsKey(type))
            {
                Func<object> f = () => ctor();
                tmap.Add(type, f);
            }
        }

        /// <summary>
        /// Unregisters a type constructor from the factory.
        /// </summary>
        /// <typeparam name="TValue">The type of the constructor, which is to be unregistered.</typeparam>
        public void Unregister<TValue>()
        {
            var key = typeof(TValue);
            if (tmap.ContainsKey(key))
                tmap.Remove(key);
        }

        /// <summary>
        /// Creates an instance of a type based on its registered constructors.
        /// The specific constructor is resolved through the key.
        /// </summary>
        /// <param name="key">The key to find the constructor of the type</param>
        /// <typeparam name="TValue">The type of instances to be created by the factory.</typeparam
        /// <returns>The created instance of the type</returns>
        public TValue Create<TValue>(string key)
        {
            if (key == null) throw new ArgumentNullException("key");
            return smap.ContainsKey(key)
                ? (TValue)smap[key]()
                : default(TValue);
        }

        /// <summary>
        /// Registers the constructors of a type.
        /// Note: Multiple constructors are allowed, as long as the key for each registered constructor is unique
        /// </summary>
        /// <param name="key">The key to associate to the constructor of the given type.</param>
        /// <param name="ctor">The constructor of the given type.</param>
        /// <typeparam name="TValue">The type of instances to be created by the factory.</typeparam>
        public void Register<TValue>(string key, Func<TValue> ctor)
        {
            if (key == null) throw new ArgumentNullException("key");
            if (ctor == null) throw new ArgumentNullException("ctor");
            if (!smap.ContainsKey(key))
            {
                Func<object> f = () => ctor();
                smap.Add(key, f);
            }
        }

        /// <summary>
        /// Unregisters a keyed type constructor from the factory.
        /// </summary>
        /// <param name="key">The key to resolve the type constructor.</param>
        public void Unregister(string key)
        {
            if (key == null) throw new ArgumentNullException("key");
            if (smap.ContainsKey(key))
                smap.Remove(key);
        }

        /// <summary>
        /// Creates an instance of a type based on its registered constructors.
        /// The specific constructor is resolved through the key.
        /// </summary>
        /// <param name="key">The key to find the constructor of the type</param>
        /// <typeparam name="TValue">The type of instances to be created by the factory.</typeparam
        /// <returns>The created instance of the type</returns>
        public TValue Create<TValue>(Guid key)
        {
            return gmap.ContainsKey(key)
                ? (TValue)gmap[key]()
                : default(TValue);
        }

        /// <summary>
        /// Registers the constructors of a type.
        /// Note: Multiple constructors are allowed, as long as the key for each registered constructor is unique
        /// </summary>
        /// <param name="key">The key to associate to the constructor of the given type.</param>
        /// <param name="ctor">The constructor of the given type.</param>
        /// <typeparam name="TValue">The type of instances to be created by the factory.</typeparam>
        public void Register<TValue>(Guid key, Func<TValue> ctor)
        {
            if (ctor == null) throw new ArgumentNullException("ctor");
            if (!gmap.ContainsKey(key))
            {
                Func<object> f = () => ctor();
                gmap.Add(key, f);
            }
        }

        /// <summary>
        /// Unregisters a keyed type constructor from the factory.
        /// </summary>
        /// <param name="key">The key to resolve the type constructor.</param>
        public void Unregister(Guid key)
        {
            if (gmap.ContainsKey(key))
                gmap.Remove(key);
        }

        /// <summary>
        /// Creates an instance of a type based on its registered constructors.
        /// The specific constructor is resolved through the key.
        /// </summary>
        /// <param name="key">The key to find the constructor of the type</param>
        /// <typeparam name="TValue">The type of instances to be created by the factory.</typeparam>
        /// <returns>The created instance of the type</returns>
        public TValue Create<TValue>(int key)
        {
            return imap.ContainsKey(key)
                ? (TValue)imap[key]()
                : default(TValue);
        }

        /// <summary>
        /// Registers the constructors of a type.
        /// Note: Multiple constructors are allowed, as long as the key for each registered constructor is unique
        /// </summary>
        /// <param name="key">The key to associate to the constructor of the given type.</param>
        /// <param name="ctor">The constructor of the given type.</param>
        /// <typeparam name="TValue">The type of instances to be created by the factory.</typeparam>
        public void Register<TValue>(int key, Func<TValue> ctor)
        {
            if (ctor == null) throw new ArgumentNullException("ctor");
            if (!imap.ContainsKey(key))
            {
                Func<object> f = () => ctor();
                imap.Add(key, f);
            }
        }

        /// <summary>
        /// Unregisters a keyed type constructor from the factory.
        /// </summary>
        /// <param name="key">The key to resolve the type constructor.</param>
        public void Unregister(int key)
        {
            if (imap.ContainsKey(key))
                imap.Remove(key);
        }

        /// <summary>
        /// Creates an instance of a type based on its registered constructors.
        /// The specific constructor is resolved through the key.
        /// </summary>
        /// <param name="key">The key to find the constructor of the type</param>
        /// <typeparam name="TValue">The type of instances to be created by the factory.</typeparam
        /// <returns>The created instance of the type</returns>
        public TValue Create<TValue>(ulong key)
        {
            return ulmap.ContainsKey(key)
                ? (TValue)ulmap[key]()
                : default(TValue);
        }

        /// <summary>
        /// Registers the constructors of a type.
        /// Note: Multiple constructors are allowed, as long as the key for each registered constructor is unique
        /// </summary>
        /// <param name="key">The key to associate to the constructor of the given type.</param>
        /// <param name="ctor">The constructor of the given type.</param>
        /// <typeparam name="TValue">The type of instances to be created by the factory.</typeparam>
        public void Register<TValue>(ulong key, Func<TValue> ctor)
        {
            if (ctor == null) throw new ArgumentNullException("ctor");
            if (!ulmap.ContainsKey(key))
            {
                Func<object> f = () => ctor();
                ulmap.Add(key, f);
            }
        }

        /// <summary>
        /// Unregisters a keyed type constructor from the factory.
        /// </summary>
        /// <param name="key">The key to resolve the type constructor.</param>
        public void Unregister(ulong key)
        {
            if (ulmap.ContainsKey(key))
                ulmap.Remove(key);
        }

        /// <summary>
        /// Creates an instance of a type based on its registered constructors.
        /// The specific constructor is resolved through the key.
        /// </summary>
        /// <param name="key">The key to find the constructor of the type</param>
        /// <typeparam name="TValue">The type of instances to be created by the factory.</typeparam
        /// <returns>The created instance of the type</returns>
        public TValue Create<TValue>(ushort key)
        {
            return usmap.ContainsKey(key)
                ? (TValue)usmap[key]()
                : default(TValue);
        }

        /// <summary>
        /// Registers the constructors of a type.
        /// Note: Multiple constructors are allowed, as long as the key for each registered constructor is unique
        /// </summary>
        /// <param name="key">The key to associate with the constructor of the given type.</param>
        /// <param name="ctor">The constructor of the given type.</param>
        /// <typeparam name="TValue">The type of instances to be created by the factory.</typeparam>
        public void Register<TValue>(ushort key, Func<TValue> ctor)
        {
            if (ctor == null) throw new ArgumentNullException("ctor");
            if (!usmap.ContainsKey(key))
            {
                Func<object> f = () => ctor();
                usmap.Add(key, f);
            }
        }

        /// <summary>
        /// Unregisters a keyed type constructor from the factory.
        /// </summary>
        /// <param name="key">The key to resolve the type constructor.</param>
        public void Unregister(ushort key)
        {
            if (usmap.ContainsKey(key))
                usmap.Remove(key);
        }
    }
}