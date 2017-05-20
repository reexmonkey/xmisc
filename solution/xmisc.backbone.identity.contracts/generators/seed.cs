namespace xmisc.backbone.identity.contracts.generators
{
    public interface ISeedableKeyGenerator<TKey>
    {
        TKey GetNext(TKey seed);
    }
}