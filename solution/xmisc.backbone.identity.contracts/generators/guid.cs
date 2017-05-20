using System;

namespace xmisc.backbone.identity.contracts.generators
{
    public abstract class GuidKeyGeneratorBase : IKeyGenerator<Guid>
    {
        public abstract Guid GetNext();
    }

    public abstract class SeedableGuidKeyGeneratorBase : GuidKeyGeneratorBase, ISeedableKeyGenerator<Guid>
    {
        public abstract Guid GetNext(Guid seed);
    }

    public abstract class GuidFingerprintGeneratorBase : IFingerprintGenerator<Guid>
    {
        public abstract Guid GetFingerprint<TModel>(TModel model);
    }
}