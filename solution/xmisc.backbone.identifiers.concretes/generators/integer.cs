using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace reexmonkey.xmisc.backbone.identifiers.concretes.generators
{
    public class RandomIntegerKeyGenerator : NumericKeyGeneratorBase<int>
    {
        private readonly RandomNumberGenerator generator;

        public RandomIntegerKeyGenerator()
        {
            generator = RandomNumberGenerator.Create();
        }

        public RandomIntegerKeyGenerator(RandomNumberGenerator generator)
        {
            this.generator = generator ?? throw new ArgumentNullException(nameof(generator));
        }

        public override int GetNext()
        {
            var buffer = new byte[4];
            generator.GetBytes(buffer);
            return BitConverter.ToInt32(buffer, 0);
        }
    }


    public class SequentialIntegerKeyGenerator : ResuableNumericKeyGenerator<int>
    {
        private int seed;
        private readonly Queue<int> pool;

        public SequentialIntegerKeyGenerator() : this(0)
        {
        }

        public SequentialIntegerKeyGenerator(int seed)
        {
            this.seed = seed;
            pool = new Queue<int>();
        }
        public override int GetNext() => pool.Any() ? pool.Dequeue() : ++seed;

        public override void Reuse(int value)
        {
            if (!pool.Contains(value)) pool.Enqueue(value);
        }

        public override void Reset() => pool.Clear();
    }
}
