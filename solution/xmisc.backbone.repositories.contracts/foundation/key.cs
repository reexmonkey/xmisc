using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace xmisc.backbone.repositories.contracts.foundation
{
    public interface IKeyRepository<TKey>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>

    {
        IEnumerable<TKey> GetKeys(int? skip = null, int? take = null);

        bool HasKey(TKey key);

        bool HasKeys(IEnumerable<TKey> keys, bool strict = true);

        Task<bool> HasKeyAsync(TKey key);

        Task<bool> HasKeysAsync(IEnumerable<TKey> keys, bool strict = true);

        Task<IEnumerable<TKey>> GetKeysAsync(int? skip = null, int? take = null);


    }
}
