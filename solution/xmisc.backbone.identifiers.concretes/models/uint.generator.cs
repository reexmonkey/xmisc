using System;
using System.Security.Cryptography;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.models
{
    /// <summary>
    /// Represents a provider that produces random unique UInt64 identifiers.
    /// </summary>
    public class RandomUIntKeyGenerator : NumberKeyGeneratorBase<uint>
    {
        private readonly RandomNumberGenerator generator;

        /// <summary>
        /// Creates a new instance of the <see cref="RandomUIntKeyGenerator"/> class.
        /// </summary>
        public RandomUIntKeyGenerator()
        {
            generator = RandomNumberGenerator.Create();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomUIntKeyGenerator"/> with a <see cref="RandomNumberGenerator"/> instance.
        /// </summary>
        /// <param name="generator">The number generator that provides cryptographic strong numbers.</param>
        public RandomUIntKeyGenerator(RandomNumberGenerator generator)
        {
            this.generator = generator ?? throw new ArgumentNullException(nameof(generator));
        }

        /// <summary>
        /// Generates the next unique UInt64 identifier.
        /// </summary>
        /// <returns>The generated unique UInt64 identifier.</returns>
        public override uint GetNext()
        {
            var buffer = new byte[4];
            generator.GetBytes(buffer);
            return BitConverter.ToUInt32(buffer, 0);
        }
    }

    /// <summary>
    /// Represents a provider that produces random unique UInt64 identifiers.
    /// </summary>
    public class SequentiaUIntKeyGenerator : NumberKeyGeneratorBase<uint>
    {
        private uint counter;

        /// <summary>
        /// Creates a new instance of the <see cref="SequentiaUIntKeyGenerator"/> class.
        /// </summary>
        public SequentiaUIntKeyGenerator() => counter = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="SequentiaUIntKeyGenerator"/> with a seed value;
        /// </summary>
        public SequentiaUIntKeyGenerator(uint seed) => this.counter = seed;

        /// <summary>
        /// Generates the next unique numeric identifier.
        /// </summary>
        /// <returns>The generated unique numeric identifier.</returns>
        public override uint GetNext() => ++counter;

        /// <summary>
        /// Resets the counter of the generator.
        /// </summary>
        public void Reset() => counter = 0;
    }
}
