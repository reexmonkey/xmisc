using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.generators
{
    public class RandomLongKeyGenerator : NumericKeyGeneratorBase<long>
    {
        private readonly RandomNumberGenerator generator;

        public RandomLongKeyGenerator()
        {
            generator = RandomNumberGenerator.Create();
        }

        public RandomLongKeyGenerator(RandomNumberGenerator generator)
        {
            this.generator = generator ?? throw new ArgumentNullException(nameof(generator));
        }

        public override long GetNext()
        {
            var buffer = new byte[8];
            generator.GetBytes(buffer);
            return BitConverter.ToInt32(buffer, 0);
        }
    }


    public class SequentialLongKeyGenerator : ResuableNumericKeyGenerator<long>
    {
        private long seed;
        private readonly Queue<long> pool;

        public SequentialLongKeyGenerator() : this(0)
        {
        }

        public SequentialLongKeyGenerator(long seed)
        {
            this.seed = seed;
            pool = new Queue<long>();
        }
        public override long GetNext() => pool.Any() ? pool.Dequeue() : ++seed;

        public override void Reuse(long value)
        {
            if (!pool.Contains(value)) pool.Enqueue(value);
        }

        public override void Reset() => pool.Clear();
    }
}
