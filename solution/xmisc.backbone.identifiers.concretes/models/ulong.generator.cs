using reexmonkey.xmisc.backbone.identifiers.concretes.infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.models
{
    /// <summary>
    /// Represents a provider that produces random unique UInt64 identifiers.
    /// </summary>
    public class RandomULongKeyGenerator : NumberKeyGeneratorBase<ulong>
    {
        private readonly RandomNumberGenerator generator;

        /// <summary>
        /// Creates a new instance of the <see cref="RandomULongKeyGenerator"/> class.
        /// </summary>
        public RandomULongKeyGenerator()
        {
            generator = RandomNumberGenerator.Create();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomULongKeyGenerator"/> with a <see cref="RandomNumberGenerator"/> instance.
        /// </summary>
        /// <param name="generator">The number generator that provides cryptographic strong numbers.</param>
        public RandomULongKeyGenerator(RandomNumberGenerator generator)
        {
            this.generator = generator ?? throw new ArgumentNullException(nameof(generator));
        }

        /// <summary>
        /// Generates the next unique UInt64 identifier.
        /// </summary>
        /// <returns>The generated unique UInt64 identifier.</returns>
        public override ulong GetNext()
        {
            var buffer = new byte[8];
            generator.GetBytes(buffer);
            return BitConverter.ToUInt64(buffer, 0);
        }
    }


    /// <summary>
    /// Represents a provider that produces random unique UInt64 identifiers.
    /// </summary>
    public class SequentialULongKeyGenerator : NumberKeyGeneratorBase<long>
    {
        private long counter;

        /// <summary>
        /// Creates a new instance of the <see cref="SequentialULongKeyGenerator"/> class.
        /// </summary>
        public SequentialULongKeyGenerator() => counter = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="SequentialULongKeyGenerator"/> with a seed value;
        /// </summary>
        public SequentialULongKeyGenerator(long seed) => this.counter = seed;

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
