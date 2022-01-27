using reexmonkey.xmisc.backbone.identifiers.contracts.models;
using System;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.models
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
        /// <param name="ctor">The constructor that defines the mechanism of creating a FPI. </param>
        public FpiKeyGenerator(Func<Fpi> ctor)
        {
            this.ctor = ctor ?? throw new ArgumentNullException(nameof(ctor));
        }

        /// <summary>
        /// Gets the default FPI for all types of objects.
        /// </summary>
        /// <returns>The default fingerprint for all types of objects.</returns>
        public Fpi GetNullKey() => Fpi.NullFpi;

        /// <summary>
        /// Generates the next FPI.
        /// </summary>
        /// <returns>The generated FPI.</returns>
        public Fpi GetNext() => ctor();
    }
}
