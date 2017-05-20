using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace xmisc.backbone.repositories.contracts.foundation
{
    public interface IIgnoreEntityRepository<in TKey, in TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        void Ignore(TKey key);

        void IgnoreAll(IEnumerable<TKey> keys);

        void Ignore(TModel model);

        void IgnoreAll(IEnumerable<TModel> models);

        Task IgnoreAsync(TKey key);

        Task IgnoreAllAsync(IEnumerable<TKey> keys);

        Task IgnoreAsync(TModel model);

        Task IgnoreAllAsync(IEnumerable<TModel> models);
    }
}
