namespace reexmonkey.xmisc.core.io.infrastructure
{
    public abstract class CloneableBase<TSource>
    {
        public abstract TSource Clone(bool recursive);
    }
}