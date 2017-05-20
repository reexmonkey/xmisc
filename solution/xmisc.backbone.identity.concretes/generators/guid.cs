using System;
using xmisc.backbone.identity.concretes.extensions;
using xmisc.backbone.identity.contracts.generators;

namespace xmisc.backbone.identity.concretes.generators
{
    public class RandomGuidKeyGenerator : GuidKeyGeneratorBase
    {
        public override Guid GetNext() => Guid.NewGuid();
    }


    public class SequentialGuidKeyGenerator : GuidKeyGeneratorBase
    {
        public override Guid GetNext() => Guid.NewGuid().AsVersion1Guid();
    }
}
