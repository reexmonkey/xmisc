using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace xmisc.backbone.repositories.contracts.foundation
{
    public interface IReadEntityRepository<in TKey, TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        TModel Find(TKey key);

        TModel Find(Func<TModel, bool> predicate);

        IEnumerable<TModel> FindAll(IEnumerable<TKey> key, int? skip = null, int? take = null);

        IEnumerable<TModel> FindAll(Func<TModel, bool> predicate, int? skip = null, int? take = null);

        IEnumerable<TModel> Get(int? skip = null, int? take = null);

        Task<TModel> FindAsync(TKey key);

        Task<IEnumerable<TModel>> FindAllAsync(IEnumerable<TKey> keys, int? skip = null, int? take = null);

        IEnumerable<Task<TModel>> FindAllAsync(Func<TModel, bool> predicate, int? skip = null, int? take = null);

        Task<IEnumerable<TModel>> GetAsync(int? skip = null, int? take = null);

    }
}
