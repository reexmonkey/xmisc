using reexjungle.xmisc.foundation.concretes;
using reexjungle.xmisc.foundation.contracts;
using reexjungle.xmisc.infrastructure.contracts;
using System;
using System.Collections.Generic;

namespace reexjungle.xmisc.infrastructure.concretes.operations
{
    /// <summary>
    /// Represents a key generator of Sequential Globally Unique Identifiers (GUIDs)
    /// </summary>
    public class SequentialGuidKeyGenerator : IKeyGenerator<Guid>
    {
        private readonly Queue<Guid> pool;

        /// <summary>
        /// Generates a key.
        /// </summary>
        /// <returns>
        /// The generated key.
        /// </returns>
        public Guid GetNext()
        {
            return !pool.Empty() ? pool.Dequeue() : SequentialGuid.NewGuid();
        }

        /// <summary>
        /// Recycles a used key for reuse purposes.
        /// </summary>
        /// <param name="key">The key to be recycled.</param>
        public void Recapture(Guid key)
        {
            if (!pool.Contains(key)) pool.Enqueue(key);
        }

        /// <summary>
        /// Re-initializes the key generator.
        /// </summary>
        public void Reset()
        {
            pool.Clear();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SequentialGuidKeyGenerator()
        {
            pool = new Queue<Guid>();
        }
    }

    /// <summary>
    /// Represents a key generator of Globally Unique Identifiers (GUIDs)
    /// </summary>
    public class GuidKeyGenerator : IKeyGenerator<Guid>
    {
        private readonly Queue<Guid> pool;

        /// <summary>
        /// Produces the next key
        /// </summary>
        /// <returns>The next available key</returns>
        public Guid GetNext()
        {
            return !pool.Empty() ? pool.Dequeue() : Guid.NewGuid();
        }

        /// <summary>
        /// Recapture a key for Reuse purposes
        /// </summary>
        /// <param name="key">The key that shall later be reused</param>
        public void Recapture(Guid key)
        {
            if (!pool.Contains(key)) pool.Enqueue(key);
        }

        /// <summary>
        /// Reinitializes the key generator.
        /// </summary>
        public void Reset()
        {
            pool.Clear();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public GuidKeyGenerator()
        {
            pool = new Queue<Guid>();
        }
    }

    /// <summary>
    /// Represents a key generator for producing Globally Unique Identifiers (GUIDs) as strings
    /// </summary>
    public class GuidStringKeyGenerator : IKeyGenerator<string>
    {
        private readonly IKeyGenerator<Guid> keygen;
        private readonly bool compact;

        /// <summary>
        /// Produces the next key
        /// </summary>
        /// <returns>The next available key</returns>
        public string GetNext()
        {
            var guid = keygen.GetNext();

            //get next available non-empty Guid
            while (guid == Guid.Empty) guid = keygen.GetNext();
            var key = guid.ToString();

            return compact ? key.Replace("-", string.Empty) : key;
        }

        /// <summary>
        /// Recaptures a key for re-use purposes
        /// </summary>
        /// <param name="key">The key that shall later be reused</param>
        public void Recapture(string key)
        {
            keygen.Recapture(new Guid(key));
        }

        /// <summary>
        /// Reinitializes the key generator.
        /// </summary>
        public void Reset()
        {
            keygen.Reset();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="compact">Should the blocks of the generated Guid be separated by hyphens?</param>
        /// <param name="keygen">The generator used for generating the underlying Guids</param>
        public GuidStringKeyGenerator(bool compact = true, IKeyGenerator<Guid> keygen = null)
        {
            this.keygen = keygen ?? new GuidKeyGenerator();
            this.compact = compact;
        }
    }

    /// <summary>
    /// Represents a key generator for producing integral keys
    /// </summary>
    public class NumericKeyGenerator : IKeyGenerator<int>
    {
        private int counter;
        private readonly Queue<int> pool;

        /// <summary>
        /// Produces the next key
        /// </summary>
        /// <returns>The next available key</returns>
        public int GetNext()
        {
            return (!pool.NullOrEmpty()) ? pool.Dequeue() : ++counter;
        }

        /// <summary>
        /// Recapture a key for Reuse purposes
        /// </summary>
        /// <param name="key">The key that shall later be reused</param>
        public void Recapture(int key)
        {
            if (!pool.Contains(key)) pool.Enqueue(key);
        }

        /// <summary>
        /// Reinitializes the key generator.
        /// </summary>
        public void Reset()
        {
            counter = 0;
            pool.Clear();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public NumericKeyGenerator()
        {
            counter = 0;
            pool = new Queue<int>();
        }
    }

    /// <summary>
    ///  Represents a key generator for producing 64-bit integral keys
    /// </summary>
    public class LongNumericKeyGenerator : IKeyGenerator<long>
    {
        private long counter;
        private readonly Queue<long> pool;

        /// <summary>
        /// Produces the next key
        /// </summary>
        /// <returns>The next available key</returns>
        public long GetNext()
        {
            return (!pool.NullOrEmpty()) ? pool.Dequeue() : ++counter;
        }

        /// <summary>
        /// Recapture a key for Reuse purposes
        /// </summary>
        /// <param name="key">The key that shall later be reused</param>
        public void Recapture(long key)
        {
            if (!pool.Contains(key)) pool.Enqueue(key);
        }

        /// <summary>
        /// Reinitializes the key generator.
        /// </summary>
        public void Reset()
        {
            counter = 0;
            pool.Clear();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public LongNumericKeyGenerator()
        {
            counter = 0;
            pool = new Queue<long>();
        }
    }

    /// <summary>
    /// Generates FPI keys
    /// </summary>
    public class FpiKeyGenerator : IKeyGenerator<Fpi>
    {
        private readonly IGenerator<ApprovalStatus> statusGenerator;
        private readonly IGenerator<string> authorGenerator;
        private readonly IGenerator<string> productGenerator;
        private readonly IGenerator<string> descriptionGenerator;
        private readonly IGenerator<string> languageGenerator;
        private readonly IGenerator<string> referenceGenerator;
        private readonly Queue<Fpi> pool;

        /// <summary>
        /// Produces the next key
        /// </summary>
        /// <returns>The next available key</returns>
        public Fpi GetNext()
        {
            return !pool.Empty() ? pool.Dequeue() :
                 new Fpi(statusGenerator.GetNext(), authorGenerator.GetNext(),
                    productGenerator.GetNext(),
                    descriptionGenerator.GetNext(),
                    languageGenerator.GetNext(),
                    referenceGenerator != null ? referenceGenerator.GetNext() : null);
        }

        /// <summary>
        /// Recaptures a key for reuse purposes
        /// </summary>
        /// <param name="key">The key that shall later be reused</param>
        public void Recapture(Fpi key)
        {
            if (!pool.Contains(key)) pool.Enqueue(key);
        }

        /// <summary>
        /// Reinitializes the key generator.
        /// </summary>
        public void Reset()
        {

            pool.Clear();
        }

        ///  <summary>
        /// Constructor
        ///  </summary>
        /// <param name="statusGenerator">Approval Status generator</param>
        /// <param name="authorGenerator">Author generator</param>
        /// <param name="productGenerator">Product class generator</param>
        /// <param name="descriptionGenerator">Description generator</param>
        /// <param name="languageGenerator">Lanaguage generator</param>
        /// <param name="referenceGenerator">Reference generator</param>
        public FpiKeyGenerator(
            IGenerator<ApprovalStatus> statusGenerator,
            IGenerator<string> authorGenerator,
            IGenerator<string> productGenerator,
            IGenerator<string> descriptionGenerator,
            IGenerator<string> languageGenerator,
            IGenerator<string> referenceGenerator = null)
        {
            statusGenerator.ThrowIfNull("statusGenerator");
            authorGenerator.ThrowIfNull("statusGenerator");
            productGenerator.ThrowIfNull("productGenerator");
            descriptionGenerator.ThrowIfNull("descriptionGenerator");
            languageGenerator.ThrowIfNull("languageGenerator");

            this.statusGenerator = statusGenerator;
            this.authorGenerator = authorGenerator;
            this.productGenerator = productGenerator;
            this.descriptionGenerator = descriptionGenerator;
            this.languageGenerator = languageGenerator;
            this.referenceGenerator = referenceGenerator;

            pool = new Queue<Fpi>();
        }
    }

    /// <summary>
    /// Represents a content generator
    /// </summary>
    /// <typeparam name="TValue">The type of content to generate</typeparam>
    public class ContentGenerator<TValue> : IGenerator<TValue>
    {
        private readonly Func<TValue> rule;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentGenerator&lt;Tvalue&gt;"/> class.
        /// </summary>
        /// <param name="rule"></param>
        public ContentGenerator(Func<TValue> rule)
        {
            if (rule == null) throw new ArgumentNullException("rule");
            this.rule = rule;
        }

        /// <summary>
        /// Generates a value and returns it.
        /// </summary>
        /// <returns>The next generated value.</returns>
        public TValue GetNext()
        {
            return (rule != null) 
                ? rule()
                : default(TValue);
        }
    }
}