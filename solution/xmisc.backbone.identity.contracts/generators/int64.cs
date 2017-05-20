using xmisc.backbone.identity.contracts.infrastructure;

namespace xmisc.backbone.identity.contracts.generators
{
    public abstract class Integer64KeyGeneratorBase : IKeyGenerator<long>, ISeedableKeyGenerator<long>
    {
        public abstract long GetNext();

        public abstract long GetNext(long seed);
    }

    public abstract class ReusableKeyInteger64KeyGeneratorBase : Integer64KeyGeneratorBase, IReusable<long>
    {
        public abstract void Reuse(long key);

        public abstract void Reset();
    }
}