using System;
using xmisc.backbone.identity.concretes.infrastructure;
using xmisc.backbone.identity.contracts.generators;

namespace xmisc.backbone.identity.concretes.generators
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
