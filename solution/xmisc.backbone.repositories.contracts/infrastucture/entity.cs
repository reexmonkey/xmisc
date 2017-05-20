using System;
using System.Collections.Generic;

namespace xmisc.backbone.repositories.contracts.infrastucture
{
    /// <summary>
    /// Provides a domain entity that encapsulates a given model.
    /// </summary>
    /// <typeparam name="TKey">The type of identity of the entity.</typeparam>
    /// <typeparam name="TModel">The type of model that is encapsulated.</typeparam>
    public abstract class EntityBase<TKey, TModel>: IEquatable<EntityBase<TKey, TModel>>
        where TKey: IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// Gets the identity value of the entity.
        /// </summary>
        public TKey Key { get; }

        /// <summary>
        /// Gets the encapsulated model of the entity.
        /// </summary>
        public TModel Model { get; }

        /// <summary>
        /// Gets or sets if the entity should be ignored.
        /// </summary>
        public bool Ignored { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityBase{TKey,TModel}"/> class.
        /// </summary>
        /// <param name="key">The identity value of the entity.</param>
        /// <param name="model">The model of the entity.</param>
        protected EntityBase(TKey key, TModel model)
        {
            Key = key;
            Model = model;
        }

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(EntityBase<TKey, TModel> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Key.Equals(other.Key);
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
        /// <param name="obj">The object to compare with the current object. </param>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((EntityBase<TKey, TModel>) obj);
        }

        /// <summary>Serves as the default hash function. </summary>
        /// <returns>A hash code for the current object.</returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }

        public static bool operator ==(EntityBase<TKey, TModel> left, EntityBase<TKey, TModel> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EntityBase<TKey, TModel> left, EntityBase<TKey, TModel> right)
        {
            return !Equals(left, right);
        }
    }
}
