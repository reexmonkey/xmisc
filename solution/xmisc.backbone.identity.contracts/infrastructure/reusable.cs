namespace xmisc.backbone.identity.contracts.infrastructure
{
    public interface IReusable<in TValue>
    {
        void Reuse(TValue value);

        void Reset();

    }
}
