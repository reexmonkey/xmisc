using reexmonkey.xmisc.backbone.repositories.contracts.helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies a publisher of deletion events.
    /// </summary>
    /// <typeparam name="TKey">The type of primary key used by a parent data model.</typeparam>
    public interface IDeletionEventsPublisher<TKey>
       where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// The event to fire, when a parent data model is deleted.
        /// </summary>
        event EventHandler<DeleteParentModelEventArgs<TKey>>? ParentModelDeleted;

        /// <summary>
        /// The event to fire, when many parent data models are deleted.
        /// </summary>
        event EventHandler<DeleteManyParentModelsEventArgs<TKey>>? ManyParentModelsDeleted;
    }

    /// <summary>
    /// Specifies a publisher of update events.
    /// </summary>
    /// <typeparam name="TKey">The type of primary key used by a parent data model.</typeparam>
    public interface IUpdateEventsPublisher<TKey>
       where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// The event to fire, when a parent data model is deleted.
        /// </summary>
        event EventHandler<UpdateParentModelEventArgs<TKey>>? ParentModelDeleted;

        /// <summary>
        /// The event to fire, when many parent data models are deleted.
        /// </summary>
        event EventHandler<UpdateManyParentModelsEventArgs<TKey>>? ManyParentModelsDeleted;
    }
}