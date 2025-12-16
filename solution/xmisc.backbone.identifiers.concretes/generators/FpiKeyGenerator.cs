using reexmonkey.xmisc.backbone.identifiers.concretes.models;
using reexmonkey.xmisc.backbone.identifiers.contracts.generators;
using System;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.generators
{
    /// <summary>
    /// Represents a provider that produces Formal Public Identifiers (FPIs).
    /// </summary>
    public class FpiKeyGenerator : IKeyGenerator<Fpi>
    {
        private readonly Func<Fpi> ctor;

        /// <summary>
        /// Initializes a new instance of the <see cref="FpiKeyGenerator"/> class with a lambda constructor function.
        /// </summary>
        /// <param name="ctor">The constructor that defines the mechanism of creating a Fpi. </param>
        public FpiKeyGenerator(Func<Fpi> ctor)
        {
            this.ctor = ctor ?? throw new ArgumentNullException(nameof(ctor));
        }

        /// <summary>
        /// Gets the default Fpi for all types of objects.
        /// </summary>
        /// <returns>The default fingerprint for all types of objects.</returns>
        public Fpi GetDefaultKey() => Fpi.Empty;

        /// <summary>
        /// Generates the next Fpi.
        /// </summary>
        /// <returns>The generated Fpi.</returns>
        public Fpi GetNext() => ctor();
    }
}