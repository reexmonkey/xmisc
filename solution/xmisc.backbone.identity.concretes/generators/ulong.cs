using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace xmisc.backbone.identity.concretes.generators
{
    public class RandomUnsignedLongKeyGenerator : NumericKeyGeneratorBase<ulong>
    {
        private readonly RandomNumberGenerator generator;

        public RandomUnsignedLongKeyGenerator()
        {
            generator = new RNGCryptoServiceProvider();
        }

        public RandomUnsignedLongKeyGenerator(RandomNumberGenerator generator)
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


    public class SequentialUnsignedLongKeyGenerator : ResuableNumericKeyGenerator<ulong>
    {
        private ulong seed;
        private readonly Queue<ulong> pool;

        public SequentialUnsignedLongKeyGenerator() : this(0)
        {
        }

        public SequentialUnsignedLongKeyGenerator(ulong seed)
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
