using SimpleMigrations;
using System;
using System.Data;
using System.Data.Common;

namespace reexmonkey.xmisc.backbone.migration.simple.migrations.providers
{
    /// <summary>
    /// Custom database provider which acts by maintaining a table of applied versions.
    /// </summary>
    /// <remarks>
    /// Methods on this class are called according to a strict sequence.
    /// 
    /// When <see cref="SimpleMigrator{TConnection, TMigrationBase}.Load"/> is called:
    ///     1. <see cref="EnsurePrerequisitesCreatedAndGetCurrentVersion()"/> is invoked
    /// 
    /// 
    /// When <see cref="SimpleMigrator{TConnection, TMigrationBase}.MigrateTo(long)"/> or 
    /// <see cref="SimpleMigrator{TConnection, TMigrationBase}.Baseline(long)"/> is called:
    ///     1. <see cref="BeginOperation"/>  is called.
    ///     2. <see cref="GetCurrentVersion()"/> is called.
    ///     3. <see cref="UpdateVersion(long, long, string)"/>is called (potentially multiple times)
    ///     4. <see cref="GetCurrentVersion()"/> is called.
    ///     5. <see cref="EndOperation"/>is called.
    ///        
    /// Although MSSQL supports advisory locks, this provider is useful in simple scenarios where concurrent migrators are not needed.
    /// <para /> Credits and acknowledgments to canton7 (https://github.com/canton7/Simple.Migrations/blob/master/src/Simple.Migrations/DatabaseProvider/DatabaseProviderBase.cs)
    /// </remarks>
    public abstract class CustomDatabaseProviderBase : IDatabaseProvider<DbConnection>
    {
        /// <summary>
        /// Gets the connection used for all database operations
        /// </summary>
        protected DbConnection Connection { get; }

        /// <summary>
        /// Initialises a new instance of the <see cref="CustomDatabaseProviderBase"/> class
        /// </summary>
        /// <param name="connection">Database connection to use for all operations.</param>
        protected CustomDatabaseProviderBase(DbConnection connection)
        {
            Connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        /// <summary>
        /// Called when <see cref="SimpleMigrator{TConnection, TMigrationBase}.MigrateTo(long)"/> or <see cref="SimpleMigrator{TConnection, TMigrationBase}.Baseline(long)"/>
        /// is invoked, before any migrations are run. This should create connections and/or transactions and/or locks if necessary,
        /// and return the connection for the migrations to use.
        /// </summary>
        /// <returns>Connection for the migrations to use</returns>
        public DbConnection BeginOperation()
        {
            if (Connection.State != ConnectionState.Open) Connection.Open();
            return Connection;
        }

        /// <summary>
        /// If > 0, specifies the maximum length of the 'Description' field. Descriptions longer will be truncated
        /// </summary>
        /// <remarks>
        /// Database providers which put a maximum length on the Description field should set this to that length
        /// </remarks>
        protected int MaxDescriptionLength { get; set; }

        /// <summary>
        /// Cleans up any connections and/or transactions and/or locks created by <see cref="BeginOperation"/>
        /// </summary>
        /// <remarks>
        /// This is always paired with a call to <see cref="BeginOperation"/>: it is called exactly once for every time that
        /// <see cref="BeginOperation"/> is called.
        /// </remarks>
        public void EndOperation()
        {
            if (Connection.State == ConnectionState.Open) Connection.Close();
        }

        /// <summary>
        /// Should return 'CREATE TABLE IF NOT EXISTS', or similar
        /// </summary>
        protected abstract string CreateVersionTableSql();

        /// <summary>
        /// Should return 'CREATE SCHEMA IF NOT EXISTS', or similar
        /// </summary>
        /// <remarks>
        /// Don't override if the database has no concept of schemas
        /// </remarks>
        protected abstract string CreateSchemaSql();

        /// <summary>
        /// Should return SQL which selects a single long value - the current version - or 0/NULL if there is no current version
        /// </summary>
        protected abstract string GetCurrentVersionSql();

        /// <summary>
        /// Returns SQL which upgrades to a particular version.
        /// </summary>
        /// <remarks>
        /// The following parameters may be used:
        ///  - @Version - the long version to set
        ///  - @Description - the description of the version
        ///  - @OldVersion - the long version being migrated from
        /// </remarks>
        protected abstract string GetSetVersionSql();

        /// <summary>
        /// Helper method which ensures that the schema and VersionInfo table are created, and fetches the current version from it,
        /// using the given connection and transaction.
        /// </summary>
        /// <param name="connection">Connection to use</param>
        /// <param name="transaction">Transaction to use</param>
        /// <returns>The current version, or 0</returns>
        protected long EnsurePrerequisitesCreatedAndGetCurrentVersion(DbConnection connection, DbTransaction transaction = null)
        {
            var sql = CreateSchemaSql();
            if (!string.IsNullOrEmpty(sql) && !string.IsNullOrWhiteSpace(sql))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Transaction = transaction;
                    command.ExecuteNonQuery();
                }
            }

            using (var command = connection.CreateCommand())
            {
                command.CommandText = CreateVersionTableSql();
                command.Transaction = transaction;
                command.ExecuteNonQuery();
            }

            return GetCurrentVersion(connection, transaction);
        }

        /// <summary>
        /// Ensures that the schema (if appropriate) and version table are created, and returns the current version.
        /// </summary>
        /// <remarks>
        /// This is not surrounded by calls to <see cref="BeginOperation"/> or <see cref="EndOperation"/>, so
        /// it should do whatever locking is appropriate to guard against concurrent migrators.
        ///
        /// If the version table is empty, this should return 0.
        /// </remarks>
        /// <returns>The current version, or 0</returns>
        protected virtual long GetCurrentVersion(DbConnection connection, DbTransaction transaction = null)
        {
            long version = 0;
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = GetCurrentVersionSql();
                    if (transaction != null) command.Transaction = transaction;
                    var result = command.ExecuteScalar();
                    if (result != null) version = Convert.ToInt64(result);
                }
            }
            catch (Exception)
            {
                throw new MigrationException("Database Provider returns a value for the current version which isn't a long");
            }

            return version;
        }

        /// <summary>
        /// Ensures that the schema (if appropriate) and version table are created, and returns the current version.
        /// </summary>
        /// <remarks>
        /// This is not surrounded by calls to <see cref="BeginOperation"/> or <see cref="EndOperation"/>, so
        /// it should do whatever locking is appropriate to guard against concurrent migrators.
        ///
        /// If the version table is empty, this should return 0.
        /// </remarks>
        /// <returns>The current version, or 0</returns>
        public long EnsurePrerequisitesCreatedAndGetCurrentVersion() => EnsurePrerequisitesCreatedAndGetCurrentVersion(Connection);

        /// <summary>
        /// Fetch the current database schema version, or 0.
        /// </summary>
        /// <remarks>
        /// This method is always invoked after a call to <see cref="BeginOperation"/>, but before a call to
        /// <see cref="EndOperation"/>. Therefore it may use a connection created by <see cref="BeginOperation"/>.
        ///
        /// If this databases uses locking on the VersionInfo table to guard against concurrent migrators, this
        /// method should use the connection that lock was acquired on.
        /// </remarks>
        /// <returns>The current database schema version, or 0</returns>
        public long GetCurrentVersion() => GetCurrentVersion(Connection);

        /// <summary>
        /// Update the VersionInfo table to indicate that the given migration was successfully applied.
        /// </summary>
        /// <remarks>
        /// This is always invoked after a call to <see cref="BeginOperation"/> but before a call to <see cref="EndOperation"/>,
        /// Therefore it may use a connection created by <see cref="BeginOperation"/>.
        /// </remarks>
        /// <param name="oldVersion">The previous version of the database schema</param>
        /// <param name="newVersion">The version of the new database schema</param>
        /// <param name="newDescription">The description of the migration which was applied</param>
        public void UpdateVersion(long oldVersion, long newVersion, string newDescription) => UpdateVersion(oldVersion, newVersion, newDescription, Connection);

        /// <summary>
        /// Helper method to update the VersionInfo table to indicate that the given migration was successfully applied,
        /// using the given connectoin and transaction.
        /// </summary>
        /// <param name="oldVersion">The previous version of the database schema</param>
        /// <param name="newVersion">The version of the new database schema</param>
        /// <param name="newDescription">The description of the migration which was applied</param>
        /// <param name="connection">Connection to use</param>
        /// <param name="transaction">Transaction to use, may be null</param>
        protected virtual void UpdateVersion(long oldVersion, long newVersion, string newDescription, DbConnection connection, DbTransaction transaction = null)
        {
            if (MaxDescriptionLength > 0 && newDescription.Length > MaxDescriptionLength)
            {
                newDescription = newDescription.Substring(0, MaxDescriptionLength - 3) + "...";
            }

            using (var command = connection.CreateCommand())
            {
                if (transaction != null) command.Transaction = transaction;
                command.CommandText = GetSetVersionSql();

                var versionParam = command.CreateParameter();
                versionParam.ParameterName = "Version";
                versionParam.Value = newVersion;
                command.Parameters.Add(versionParam);

                var oldVersionParam = command.CreateParameter();
                oldVersionParam.ParameterName = "OldVersion";
                oldVersionParam.Value = oldVersion;
                command.Parameters.Add(oldVersionParam);

                var nameParam = command.CreateParameter();
                nameParam.ParameterName = "Description";
                nameParam.Value = newDescription;
                command.Parameters.Add(nameParam);

                command.ExecuteNonQuery();
            }
        }
    }
}
