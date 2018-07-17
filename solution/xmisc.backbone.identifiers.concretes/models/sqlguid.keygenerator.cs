using reexmonkey.xmisc.backbone.identifiers.concretes.extensions;
using reexmonkey.xmisc.backbone.identifiers.contracts.extensions;
using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using System;
using System.Data.SqlTypes;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.models
{

    /// <summary>
    /// Represents a provider that produces globally unique identifiers (GUIDs) to be stored in or retrieved from a SQL Server database.
    /// </summary>
    public class SqlGuidKeyGenerator : IKeyGenerator<Guid>
    {
        /// <summary>
        /// Gets the default globally unique identifier (GUID) that can be stored in or retrieved from a SQL Server database.
        /// </summary>
        /// <returns>The default GUID.</returns>
        public Guid GetNullKey() => (Guid)SqlGuid.Null;

        /// <summary>
        /// Generates the next globally unique identifier (GUID) to be stored in or retrieved from SQL Server databases.
        /// </summary>
        /// <returns>The generated GUID.</returns>
        public Guid GetNext() => (Guid)SequentialGuid.NewGuid().AsSqlGuid();
    }

}