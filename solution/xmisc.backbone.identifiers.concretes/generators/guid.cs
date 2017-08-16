using System;
using reexmonkey.xmisc.backbone.identifiers.contracts.generators;
using reexmonkey.xmisc.backbone.identifiers.contracts.infrastructure;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.generators
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
