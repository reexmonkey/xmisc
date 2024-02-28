using System;
using System.Collections.Generic;

namespace reexmonkey.xmisc.backbone.repositories.contracts.helpers
{
    /// <summary>
    /// Specifies a class that contains event data, when one or more parent data models have been modified.
    /// </summary>
    public abstract class ModificationEventArgs : EventArgs
    {
        /// <summary>
        /// Determines the type of action to execute when the parent data model of a dependent (child) data model is deleted or updated.
        /// </summary>
        public ReferentialIntegrityType Type { get; }

        /// <summary>
        /// Initializes the event argument with the type of referential integrity action to execute.
        /// </summary>
        /// <param name="type">The type of referential integrity action.</param>
        public ModificationEventArgs(ReferentialIntegrityType type)
        {
            Type = type;
        }
    }

    /// <summary>
    /// Represents a class that contains event data, when the specified parent data model has been deleted.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public sealed class DeleteParentModelEventArgs<TKey> : ModificationEventArgs
       where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// The primary key of the specified parent data model.
        /// </summary>
        public TKey Key { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteParentModelEventArgs{TKey}"/> class.
        /// </summary>
        /// <param name="key">The primary key of the parent data model.</param>
        /// <param name="type">The type of referential integrity action.</param>
        public DeleteParentModelEventArgs(TKey key, ReferentialIntegrityType type = ReferentialIntegrityType.CASCADE) : base(type)
        {
            Key = key;
        }
    }

    /// <summary>
    /// Represents a class that contains event data, when the specified parent data models have been deleted.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public sealed class DeleteManyParentModelsEventArgs<TKey> : ModificationEventArgs
       where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// The primary keys of the specified parent data models.
        /// </summary>
        public List<TKey> Keys { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteParentModelEventArgs{TKey}"/> class.
        /// </summary>
        /// <param name="keys">The primary keys of the parent data models.</param>
        /// <param name="type">The type of referential integrity action.</param>
        public DeleteManyParentModelsEventArgs(IEnumerable<TKey> keys, ReferentialIntegrityType type = ReferentialIntegrityType.CASCADE) : base(type)
        {
            Keys = new List<TKey>(keys);
        }
    }

    /// <summary>
    /// Represents a class that contains event data, when the specified parent data model has been updated.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public sealed class UpdateParentModelEventArgs<TKey> : ModificationEventArgs
       where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// The primary key of the parent data model.
        /// </summary>
        public TKey Key { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateParentModelEventArgs{TKey}"/> class.
        /// </summary>
        /// <param name="key">The primary key of the specified parent data model.</param>
        /// <param name="type">The type of referential integrity action.</param>
        public UpdateParentModelEventArgs(TKey key, ReferentialIntegrityType type = ReferentialIntegrityType.CASCADE) : base(type)
        {
            Key = key;
        }
    }

    /// <summary>
    /// Represents a class that contains event data, when the specified parent data models have been updated.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public sealed class UpdateManyParentModelsEventArgs<TKey> : ModificationEventArgs
       where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// The primary keys of the parent data models.
        /// </summary>
        public List<TKey> Keys { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateManyParentModelsEventArgs{TKey}"/> class.
        /// </summary>
        /// <param name="keys">The primary keys of the specified parent data models.</param>
        /// <param name="type">The type of referential integrity action.</param>
        public UpdateManyParentModelsEventArgs(IEnumerable<TKey> keys, ReferentialIntegrityType type = ReferentialIntegrityType.CASCADE) : base(type)
        {
            Keys = new List<TKey>(keys);
        }
    }
}