namespace reexmonkey.xmisc.backbone.identifiers.contracts.infrastructure
{
    public interface IReusable<in TValue>
    {
        void Reuse(TValue value);

        void Reset();

    }
}
