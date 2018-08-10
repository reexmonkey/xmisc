using System;
using System.Data;
using System.Data.Common;

namespace reexmonkey.xmisc.backbone.migration.simple.migrations.providers
{
    /// <summary>
    /// Class which can read from / write to a version table in an MSSQL database without the usage of advisory locks.
    /// </summary>
    /// <remarks>
    /// Although MSSQL supports advisory locks, this provider is useful in simple scenarios where concurrent migrators are not needed.
    /// <para /> Please see Simple.Migrations (https://github.com/canton7/Simple.Migrations)
    /// <para /> Credits and acknowledgments to canton7 (https://github.com/canton7/Simple.Migrations)
    /// </remarks>
    public class SqlServerDatabaseProvider : DatabaseProviderBase
    {
        /// <summary>
        /// Gets or sets the schema name used to store the version table.
        /// </summary>
        public string SchemaName { get; set; } = "dbo";

        /// <summary>
        /// Controls whether or not to try and create the schema if it does not exist.
        /// </summary>
        /// <remarks>
        /// If this is set to false then no schema is created. It is the user's responsibility to create the schema
        /// (if necessary) with the correct name and permissions before running the <see cref="SimpleMigrations.SimpleMigrator"/>. This may be
        /// required if the user which Simple.Migrator is running as does not have the correct permissions to check whether the
        /// schema has been created.
        /// </remarks>
        public bool CreateSchema { get; set; } = true;

        /// <summary>
        /// Initialises a new instance of the <see cref="SqlServerDatabaseProvider"/> class
        /// </summary>
        /// <param name="connection">Connection to use to run migrations. The caller is responsible for closing this.</param>
        public SqlServerDatabaseProvider(DbConnection connection) : base(connection)
        {
            MaxDescriptionLength = 256;
        }

        /// <summary>
        /// Returns SQL to create the schema
        /// </summary>
        /// <returns>SQL to create the schema</returns>
        protected override string CreateSchemaSql()
        {
            return CreateSchema ? $@"IF NOT EXISTS (select * from sys.schemas WHERE name ='{SchemaName}') EXECUTE ('CREATE SCHEMA [{SchemaName}]');" : String.Empty;
        }

        /// <summary>
        /// Returns SQL to create the version table
        /// </summary>
        /// <returns>SQL to create the version table</returns>
        protected override string CreateVersionTableSql()
        {
            return $@"IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[{SchemaName}].[{TableName}]') AND type in (N'U'))
                BEGIN
                CREATE TABLE [{SchemaName}].[{TableName}](
                    [Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
                    [Version] [bigint] NOT NULL,
                    [AppliedOn] [datetime] NOT NULL,
                    [Description] [nvarchar]({MaxDescriptionLength}) NOT NULL,
                )
                END;";
        }

        /// <summary>
        /// Returns SQL to fetch the current version from the version table
        /// </summary>
        /// <returns>SQL to fetch the current version from the version table</returns>
        protected override string GetCurrentVersionSql()
        {
            return $@"SELECT TOP 1 [Version] FROM [{SchemaName}].[{TableName}] ORDER BY [Id] desc;";
        }

        /// <summary>
        /// Returns SQL to update the current version in the version table
        /// </summary>
        /// <returns>SQL to update the current version in the version table</returns>
        protected override string GetSetVersionSql()
        {
            return $@"INSERT INTO [{SchemaName}].[{TableName}] ([Version], [AppliedOn], [Description]) VALUES (@Version, GETDATE(), @Description);";
        }
    }
}
