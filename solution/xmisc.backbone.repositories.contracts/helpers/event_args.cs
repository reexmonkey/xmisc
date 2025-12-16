namespace reexmonkey.xmisc.backbone.repositories.contracts.helpers
{
    /// <summary>
    /// Specifies a class that contains event data, when one or more parent data models have been modified.
    /// </summary>
    /// <remarks>
    /// Initializes the event argument with the type of referential integrity action to execute.
    /// </remarks>
    /// <param name="type">The type of referential integrity action.</param>
    public abstract class ModificationEventArgs(ReferentialIntegrityType type) : EventArgs
    {
        /// <summary>
        /// Determines the type of action to execute when the parent data model of a dependent (child) data model is deleted or updated.
        /// </summary>
        public ReferentialIntegrityType Type { get; } = type;
    }

    /// <summary>
    /// Represents a class that contains event data, when the specified parent data model has been deleted.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <remarks>
    /// Initializes a new instance of the <see cref="DeleteParentModelEventArgs{TKey}"/> class.
    /// </remarks>
    /// <param name="key">The primary key of the parent data model.</param>
    /// <param name="type">The type of referential integrity action.</param>
    public sealed class DeleteParentModelEventArgs<TKey>(TKey key, ReferentialIntegrityType type = ReferentialIntegrityType.CASCADE) : ModificationEventArgs(type)
       where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// The primary key of the specified parent data model.
        /// </summary>
        public TKey Key { get; } = key;
    }

    /// <summary>
    /// Represents a class that contains event data, when the specified parent data models have been deleted.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <remarks>
    /// Initializes a new instance of the <see cref="DeleteParentModelEventArgs{TKey}"/> class.
    /// </remarks>
    /// <param name="keys">The primary keys of the parent data models.</param>
    /// <param name="type">The type of referential integrity action.</param>
    public sealed class DeleteManyParentModelsEventArgs<TKey>(IEnumerable<TKey> keys, ReferentialIntegrityType type = ReferentialIntegrityType.CASCADE) : ModificationEventArgs(type)
       where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// The primary keys of the specified parent data models.
        /// </summary>
        public List<TKey> Keys { get; } = new List<TKey>(keys);
    }

    /// <summary>
    /// Represents a class that contains event data, when the specified parent data model has been updated.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <remarks>
    /// Initializes a new instance of the <see cref="UpdateParentModelEventArgs{TKey}"/> class.
    /// </remarks>
    /// <param name="key">The primary key of the specified parent data model.</param>
    /// <param name="type">The type of referential integrity action.</param>
    public sealed class UpdateParentModelEventArgs<TKey>(TKey key, ReferentialIntegrityType type = ReferentialIntegrityType.CASCADE) : ModificationEventArgs(type)
       where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// The primary key of the parent data model.
        /// </summary>
        public TKey Key { get; } = key;
    }

    /// <summary>
    /// Represents a class that contains event data, when the specified parent data models have been updated.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <remarks>
    /// Initializes a new instance of the <see cref="UpdateManyParentModelsEventArgs{TKey}"/> class.
    /// </remarks>
    /// <param name="keys">The primary keys of the specified parent data models.</param>
    /// <param name="type">The type of referential integrity action.</param>
    public sealed class UpdateManyParentModelsEventArgs<TKey>(IEnumerable<TKey> keys, ReferentialIntegrityType type = ReferentialIntegrityType.CASCADE) : ModificationEventArgs(type)
       where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// The primary keys of the parent data models.
        /// </summary>
        public List<TKey> Keys { get; } = new List<TKey>(keys);
    }
}