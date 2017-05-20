using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.core.reflection.infrastructure
{
    /// <summary>
    /// Represents a basic property structure of an object
    /// </summary>
    public class Property
    {
        private readonly List<object> backstore;
        /// <summary>
        /// Gets the name of the property
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the type of the property
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Gets the values of the property in case the property is indexed, otherwise the first element is the value
        /// </summary>
        public ReadOnlyCollection<object> Values => backstore.AsReadOnly();

        /// <summary>
        /// Checks if the property has indexed values
        /// </summary>
        public bool IsIndexed => Values.Any();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <param name="type">The type of the property</param>
        /// <param name="values">The values of the property in case the property is indexed, otherwise the first element is the value</param>
        public Property(string name, Type type, IEnumerable<object> values)
        {
            Name = name;
            Type = type;
            backstore = values != null ? new List<object>(values) : new List<object>();
        }

    }


    public class PropertyInfo<TValue> : Property
    {
        public PropertyInfo(string name, IEnumerable<TValue> values) : base(name, typeof(TValue), values.Cast<object>())
        {
        }

        public new ReadOnlyCollection<TValue> Values => base.Values.Cast<TValue>().ToList().AsReadOnly();
    }
}
