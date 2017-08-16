using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.generators
{
    public class RandomUnsignedIntegerKeyGenerator : NumericKeyGeneratorBase<ulong>
    {
        private readonly RandomNumberGenerator generator;

        public RandomUnsignedIntegerKeyGenerator()
        {
            generator = RandomNumberGenerator.Create();
        }

        public RandomUnsignedIntegerKeyGenerator(RandomNumberGenerator generator)
        {
            this.generator = generator ?? throw new ArgumentNullException(nameof(generator));
        }

        public override ulong GetNext()
        {
            var buffer = new byte[4];
            generator.GetBytes(buffer);
            return BitConverter.ToUInt64(buffer, 0);
        }
    }


    public class SequentialUnsignedIntegerKeyGenerator : ResuableNumericKeyGenerator<ulong>
    {
        private ulong seed;
        private readonly Queue<ulong> pool;

        public SequentialUnsignedIntegerKeyGenerator() : this(0)
        {
        }

        public SequentialUnsignedIntegerKeyGenerator(ulong seed)
        {
            this.seed = seed;
            pool = new Queue<ulong>();
        }
        public override ulong GetNext() => pool.Any() ? pool.Dequeue() : ++seed;

        public override void Reuse(ulong value)
        {
            if (!pool.Contains(value)) pool.Enqueue(value);
        }

        public override void Reset() => pool.Clear();
    }
}
