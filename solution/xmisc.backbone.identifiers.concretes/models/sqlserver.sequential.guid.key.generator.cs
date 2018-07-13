using reexmonkey.xmisc.backbone.identifiers.contracts.extensions;
using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using System;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.models
{

    /// <summary>
    /// Represents a provider that produces SQL Server compatible time-based global unique identifiers (version 1).
    /// </summary>
    public class SqlServerSequentialGuidKeyGenerator : IKeyGenerator<SequentialGuid>
    {
        /// <summary>
        /// Gets the default time-based globally unique identifier.
        /// </summary>
        /// <returns>The default unique identifier.</returns>
        public SequentialGuid GetNullKey() => SequentialGuid.Empty;

        /// <summary>
        /// Generates the next unique randomly or pseudo-randomly generated (version 1) global unique identifier.
        /// </summary>
        /// <returns>The generated global unique identifier.</returns>
        public SequentialGuid GetNext() => SequentialGuid.NewGuid().AsSqlServerSequentialGuid();
    }

}