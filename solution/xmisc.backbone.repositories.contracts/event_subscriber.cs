using reexmonkey.xmisc.backbone.repositories.contracts.helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies a subscriber of deletion events.
    /// </summary>
    /// <typeparam name="TKey">The type of primary key used by a parent data model.</typeparam>
    public interface IDeletionEventsSubscriber<TKey>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// Handles the event triggered when a parent data model is deleted.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The deletion event data.</param>
        void OnParentModelDeleted(object? sender, DeleteParentModelEventArgs<TKey> e);

        /// <summary>
        /// Handles the event triggered when many parent data models are deleted.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The deletion event data.</param>
        void OnManyParentModelsDeleted(object? sender, DeleteManyParentModelsEventArgs<TKey> e);
    }

    /// <summary>
    /// Specifies a subscriber of update events.
    /// </summary>
    /// <typeparam name="TKey">The type of primary key used by a parent data model.</typeparam>
    public interface IUpdateEventsSubscriber<TKey>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        /// <summary>
        /// Handles the event triggered when a parent data model is updated.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The update event data.</param>
        void OnParentModelDeleted(object? sender, UpdateParentModelEventArgs<TKey> e);

        /// <summary>
        /// Handles the event triggered when many parent data models are updated.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The update event data.</param>
        void OnManyParentModelsDeleted(object? sender, UpdateManyParentModelsEventArgs<TKey> e);
    }
}