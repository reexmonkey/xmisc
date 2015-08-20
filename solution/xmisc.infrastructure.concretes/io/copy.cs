using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace reexjungle.xmisc.infrastructure.concretes.io
{
    /// <summary>
    /// Provides extended object copying functionalities
    /// </summary>
    public static class CopyExtensions
    {
        /// <summary>
        /// Creates a deep clone of an object specified by a generic type parameter
        /// </summary>
        /// <typeparam name="TInstance">The type parameter of the object to be cloned </typeparam>
        /// <param name="value">The object of a specified generic type parameter</param>
        /// <returns></returns>
        public static TInstance Clone<TInstance>(this TInstance value)
            where TInstance : ISerializable, new()
        {
            object copy;
            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, value);
                ms.Position = 0;
                copy = bf.Deserialize(ms);
            }
            return (TInstance)copy;
        }
    }
}