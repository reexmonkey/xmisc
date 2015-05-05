namespace reexjungle.xmisc.infrastructure.contracts
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPaginated<T>
        where T : struct
    {
        T? Page { get; set; }

        T? Size { get; set; }
    }
}