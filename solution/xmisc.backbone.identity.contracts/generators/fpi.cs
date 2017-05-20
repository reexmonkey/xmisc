using xmisc.backbone.identity.contracts.infrastructure;

namespace xmisc.backbone.identity.contracts.generators
{
    public abstract class FpiKeyGeneratorBase : IKeyGenerator<FpiBase>
    {
        public abstract FpiBase GetNext();
    }
}