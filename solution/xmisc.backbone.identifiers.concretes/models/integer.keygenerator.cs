using reexmonkey.xmisc.backbone.identifiers.concretes.infrastructure;
using System;
using System.Security.Cryptography;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.models
{
    /// <summary>
    /// Represents a provider that produces random unique Int32 identifiers.
    /// </summary>
    public class RandomIntegerKeyGenerator : NumberKeyGeneratorBase<int>
    {
        private readonly RandomNumberGenerator generator;

        /// <summary>
        /// Creates a new instance of the <see cref="RandomIntegerKeyGenerator"/> class.
        /// </summary>
        public RandomIntegerKeyGenerator()
        {
            generator = RandomNumberGenerator.Create();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomIntegerKeyGenerator"/> with a <see cref="RandomNumberGenerator"/> instance.
        /// </summary>
        /// <param name="generator">The number generator that provides cryptographic strong numbers.</param>
        public RandomIntegerKeyGenerator(RandomNumberGenerator generator)
        {
            this.generator = generator ?? throw new ArgumentNullException(nameof(generator));
        }

        /// <summary>
        /// Generates the next unique Int32 identifier.
        /// </summary>
        /// <returns>The generated unique Int32 identifier.</returns>
        public override int GetNext()
        {
            var buffer = new byte[4];
            generator.GetBytes(buffer);
            return BitConverter.ToInt32(buffer, 0);
        }
    }

    /// <summary>
    /// Represents a provider that produces random unique Int32 identifiers.
    /// </summary>
    public class SequentialntegerKeyGenerator : NumberKeyGeneratorBase<int>
    {
        private int counter;

        /// <summary>
        /// Creates a new instance of the <see cref="SequentialntegerKeyGenerator"/> class.
        /// </summary>
        public SequentialntegerKeyGenerator() => counter = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="SequentialntegerKeyGenerator"/> with a seed value;
        /// </summary>
        public SequentialntegerKeyGenerator(int seed) => counter = seed;

        /// <summary>
        /// Generates the next unique numeric identifier.
        /// </summary>
        /// <returns>The generated unique numeric identifier.</returns>
        public override int GetNext() => ++counter;

        /// <summary>
        /// Reseeds the key generator.
        /// <para/> Use with caution: reseeding the counter changes sequence of generated numbers and this may be undesired in certain cases.
        /// <para/> However calling this method is beneficial when the sequence generator needs to be initialized from an external source.
        /// </summary>
        /// <param name="seed"></param>
        public void Reseed(int seed) => counter = seed;

        /// <summary>
        /// Resets the counter of the generator.
        /// <para/> Use with caution: reseeding the counter changes sequence of generated numbers and this may be undesired in certain cases.
        /// </summary>
        public void Reset() => counter = 0;
    }
}
