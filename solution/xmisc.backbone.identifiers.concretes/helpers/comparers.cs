using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.helpers
{
    /// <summary>
    /// Represents a comparer that compares two values of <see cref="Guid"/> values according to the sort rules of SQL Server for GUIDs.
    /// </summary>
    public class GuidForSqlServerComparer : IComparer<Guid>
    {
        /// <summary>
        /// Defines a method to compare two <see cref="Guid"/> values according to the sort rules of SQL Server for GUIDs.
        /// </summary>
        /// <param name="x">The first GUID to compare.</param>
        /// <param name="y">The second GUID to compare.</param>
        /// <returns>The result from the comparison.</returns>
        public int Compare(Guid x, Guid y)
        {
            var left = new SqlGuid(x);
            var right = new SqlGuid(y);
            return left.CompareTo(right);
        }
    }

    /// <summary>
    /// Represents a comparer that compares two values of <see cref="SequentialGuid"/> values according to the sort rules of SQL Server for GUIDs.
    /// </summary>
    public class SequentialGuidForSqlServerComparer : IComparer<SequentialGuid>
    {
        /// <summary>
        /// Defines a method to compare two <see cref="SequentialGuid"/> values according to the sort rules of SQL Server for GUIDs.
        /// </summary>
        /// <param name="x">The first GUID to compare.</param>
        /// <param name="y">The second GUID to compare.</param>
        /// <returns>The result from the comparison.</returns>
        public int Compare(SequentialGuid x, SequentialGuid y)
        {
            var left = new SqlGuid(x);
            var right = new SqlGuid(y);
            return left.CompareTo(right);
        }
    }

    /// <summary>
    /// Represents a comparer that compares two values of <see cref="Md5Guid"/> values according to the sort rules of SQL Server for GUIDs.
    /// </summary>
    public class Md5GuidForSqlServerComparer : IComparer<Md5Guid>
    {
        /// <summary>
        /// Defines a method to compare two <see cref="Md5Guid"/> values according to the sort rules of SQL Server for GUIDs.
        /// </summary>
        /// <param name="x">The first GUID to compare.</param>
        /// <param name="y">The second GUID to compare.</param>
        /// <returns>The result from the comparison.</returns>
        public int Compare(Md5Guid x, Md5Guid y)
        {
            var left = new SqlGuid(x);
            var right = new SqlGuid(y);
            return left.CompareTo(right);
        }
    }

    /// <summary>
    /// Represents a comparer that compares two values of <see cref="Sha1Guid"/> values according to the sort rules of SQL Server for GUIDs.
    /// </summary>
    public class Sha1GuidForSqlServerComparer : IComparer<Sha1Guid>
    {
        /// <summary>
        /// Defines a method to compare two <see cref="Sha1Guid"/> values according to the sort rules of SQL Server for GUIDs.
        /// </summary>
        /// <param name="x">The first GUID to compare.</param>
        /// <param name="y">The second GUID to compare.</param>
        /// <returns>The result from the comparison.</returns>
        public int Compare(Sha1Guid x, Sha1Guid y)
        {
            var left = new SqlGuid(x);
            var right = new SqlGuid(y);
            return left.CompareTo(right);
        }
    }
}