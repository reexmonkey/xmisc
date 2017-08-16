using System;
using reexmonkey.xmisc.backbone.identifiers.concretes.infrastructure;
using reexmonkey.xmisc.backbone.identifiers.contracts.generators;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.generators
{
    public class FpiGenerator : IKeyGenerator<Fpi>
    {
        private readonly Func<Fpi> ctor;

        public FpiGenerator(Func<Fpi> ctor)
        {
            this.ctor = ctor ?? throw new ArgumentNullException(nameof(ctor));
        }

        public Fpi GetNullKey() => Fpi.NullFpi;

        public Fpi GetNext() => ctor();
    }
}
