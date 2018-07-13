using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.models
{
    /// <summary>
    /// Represents a provider that produces random unique Int64 identifiers.
    /// </summary>
    public class RandomLongKeyGenerator : NumberKeyGeneratorBase<long>
    {
        private readonly RandomNumberGenerator generator;

        /// <summary>
        /// Creates a new instance of the <see cref="RandomLongKeyGenerator"/> class.
        /// </summary>
        public RandomLongKeyGenerator()
        {
            generator = RandomNumberGenerator.Create();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomLongKeyGenerator"/> with a <see cref="RandomNumberGenerator"/> instance.
        /// </summary>
        /// <param name="generator">The number generator that provides cryptographic strong numbers.</param>
        public RandomLongKeyGenerator(RandomNumberGenerator generator)
        {
            this.generator = generator ?? throw new ArgumentNullException(nameof(generator));
        }

        /// <summary>
        /// Generates the next unique Int64 identifier.
        /// </summary>
        /// <returns>The generated unique Int64 identifier.</returns>
        public override long GetNext()
        {
            var buffer = new byte[8];
            generator.GetBytes(buffer);
            return BitConverter.ToInt64(buffer, 0);
        }
    }


    /// <summary>
    /// Represents a provider that produces random unique UInt64 identifiers.
    /// </summary>
    public class SequentiaLongKeyGenerator : NumberKeyGeneratorBase<long>
    {
        private long counter;

        /// <summary>
        /// Creates a new instance of the <see cref="SequentiaLongKeyGenerator"/> class.
        /// </summary>
        public SequentiaLongKeyGenerator() => counter = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="SequentiaLongKeyGenerator"/> with a seed value;
        /// </summary>
        public SequentiaLongKeyGenerator(long seed) => this.counter = seed;

        /// <summary>
        /// Generates the next unique numeric identifier.
        /// </summary>
        /// <returns>The generated unique numeric identifier.</returns>
        public override long GetNext() => ++counter;

        /// <summary>
        /// Resets the counter of the generator.
        /// </summary>
        public void Reset() => counter = 0;
    }
}
