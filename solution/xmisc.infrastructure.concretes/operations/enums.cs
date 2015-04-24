namespace reexjungle.xmisc.infrastructure.concretes.operations
{
    /// <summary>
    ///
    /// </summary>
    public enum StorageType
    {
        rdbms,
        nosql,
        memory,
        host,
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