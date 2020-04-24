using SimpleMigrations;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Transactions;

namespace reexmonkey.xmisc.backbone.migration.simple.migrations.migrations
{
    /// <summary>
    /// Represents a base class for migrations.
    /// </summary>
    public abstract class MigrationBase : IMigration<DbConnection>
    {
        /// <summary>
        /// Gets or sets the database to be used by this migration
        /// </summary>
        protected DbConnection Connection { get; set; }

        /// <summary>
        /// Gets or sets the logger to be used by this migration
        /// </summary>
        protected IMigrationLogger Logger { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether calls to <see cref="Execute(string, Func{string, DbConnection, DbCommand}, IEnumerable{DbParameter}, int?)"/> should be run inside of a transaction.
        /// </summary>
        /// <remarks>
        /// If this is false, <see cref="Transaction"/> will not be set.
        /// </remarks>
        protected virtual bool UseTransaction { get; set; } = true;

        /// <summary>
        /// Invoked when this migration should migrate up
        /// </summary>
        protected abstract void Down();

        /// <summary>
        /// Invoked when this migration should migrate down
        /// </summary>
        protected abstract void Up();

        /// <summary>
        /// Execute and log an SQL query (which returns no data)
        /// </summary>
        /// <param name="sql">SQL to execute</param>
        /// <param name="cmdFunc">The lambda function to create the SQL command object.</param>
        /// <param name="parameters">Parameters for the SQL command.</param>
        /// <param name="commandTimeout">The command timeout to use (in seconds), or null to use the default from your ADO.NET provider</param>
        public void Execute(string sql, Func<string, DbConnection, DbCommand> cmdFunc, IEnumerable<DbParameter> parameters,  int? commandTimeout = null)
        {
            if (Logger != null) Logger.LogSql(sql);
            using (var command =  cmdFunc(sql, Connection))
            {
                command.Parameters.Clear();
                command.Parameters.AddRange(parameters.ToArray());
                if (commandTimeout.HasValue) command.CommandTimeout = commandTimeout.Value;
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Run the migration in the given direction, using the given connection and logger
        /// </summary>
        /// <param name="data">Data used by the migration</param>
        /// <remarks>The migration should use a transaction if appropriate</remarks>
        public void RunMigration(MigrationRunData<DbConnection> data)
        {
            Connection = data.Connection;
            Logger = data.Logger;

            if (UseTransaction) RunMigrationWithTransaction(data);
            else RunMigrationWithoutTransaction(data);
        }

        private void RunMigrationWithoutTransaction(MigrationRunData<DbConnection> data)
        {
            if (data.Direction == MigrationDirection.Up) Up();
            else Down();
        }

        private void RunMigrationWithTransaction(MigrationRunData<DbConnection> data)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Suppress))
            {
                RunMigrationWithoutTransaction(data);
                scope.Complete();
            }
        }
    }
}
