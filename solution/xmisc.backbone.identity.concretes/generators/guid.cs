using System;
using xmisc.backbone.identity.contracts.generators;
using xmisc.backbone.identity.contracts.infrastructure;

namespace xmisc.backbone.identity.concretes.generators
{
    public class RandomGuidKeyGenerator : IKeyGenerator<Guid>
    {
        public Guid GetNullKey() => Guid.Empty;

        public Guid GetNext() => Guid.NewGuid();
    }

    public class SequentialGuidKeyGenerator : IKeyGenerator<SequentialGuid>
    {
        public SequentialGuid GetNullKey() => SequentialGuid.Empty;

        public SequentialGuid GetNext() => SequentialGuid.NewGuid();
    }
}
