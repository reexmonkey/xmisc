using reexjungle.xmisc.foundation.concretes;
using reexjungle.xmisc.foundation.contracts;
using reexjungle.xmisc.infrastructure.contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace reexjungle.xmisc.infrastructure.concretes.operations
{
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
        public Guid GetNextKey()
        {
            return pool.Empty() ? pool.Dequeue() : Guid.NewGuid();
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
        public string GetNextKey()
        {
            var guid = keygen.GetNextKey();

            //get next available non-empty Guid
            while (guid == Guid.Empty) guid = keygen.GetNextKey();
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
        /// <param name="keygen">The Guid generator used for generating the underlying Guids</param>
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
        public int GetNextKey()
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
        public long GetNextKey()
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
        private readonly IKeyGenerator<string> keygen;
        private readonly Queue<Fpi> pool;

        /// <summary>
        /// Produces the next key
        /// </summary>
        /// <returns>The next available key</returns>
        public Fpi GetNextKey()
        {
            var tokens = keygen.GetNextKey().Split('-');
            return !pool.NullOrEmpty() ? pool.Dequeue() :
                new Fpi(ApprovalStatus.None, tokens.Last(), "PRODUCT", "DESCRIPTION", "EN");
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
            keygen.Reset();
            pool.Clear();
        }

        /// <summary>
        ///Constructor
        /// </summary>
        /// <param name="authorKeygen">Generator of author names for the FPI generator.
        /// If none is provided, the default <see cref="GuidStringKeyGenerator"/> is used.
        ///  </param>
        public FpiKeyGenerator(IKeyGenerator<string> authorKeygen = null)
        {
            keygen = authorKeygen ?? new GuidStringKeyGenerator(false);
            pool = new Queue<Fpi>();
        }
    }
}