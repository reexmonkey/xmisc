namespace reexjungle.xmisc.infrastructure.concretes.operations
{
    /// <summary>
    /// Represents a data storage type
    /// </summary>
    public enum StorageType
    {
        /// <summary>
        /// Relational Database Management System
        /// </summary>
        rdbms,

        /// <summary>
        /// No-SQL
        /// </summary>
        nosql,

        /// <summary>
        /// In-Memory
        /// </summary>
        memory,

        /// <summary>
        ///
        /// </summary>
        host,

        /// <summary>
        ///
        /// </summary>
        unknown
    }

    public enum HostType
    {
        azure,
        amazonws
    }

    public enum OrmType
    {
        mysql,
        postgresql,
        sqlserver,
        sqlite
    }

    public enum NoSqlType
    {
        mongodb,
        couchdb,
        ravendb,
        redis
    }
}