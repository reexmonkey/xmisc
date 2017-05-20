using xmisc.backbone.identity.contracts.infrastructure;

namespace xmisc.backbone.identity.contracts.generators
{
    public abstract class IntegerKeyGeneratorBase : IKeyGenerator<int>, ISeedableKeyGenerator<int>
    {
        public abstract int GetNext();

        public abstract int GetNext(int seed);
    }

    public abstract class ReusableKeyIntegerKeyGeneratorBase : IntegerKeyGeneratorBase, IReusable<int>
    {
        public abstract void Reuse(int value);

        public abstract void Reset();
    }
}