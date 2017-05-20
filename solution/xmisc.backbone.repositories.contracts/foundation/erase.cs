using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace xmisc.backbone.repositories.contracts.foundation
{
    public interface IEraseModelRepository<in TKey, in TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        void EraseAll(TKey key);

        void EraseAll(IEnumerable<TKey> keys);

        void EraseAll(TModel entity);

        void EraseAll(IEnumerable<TModel> models);

        Task EraseAsync(TKey key);

        Task EraseAllAsync(IEnumerable<TKey> keys);

        Task EraseAsync(TModel entity);

        Task EraseAllAsync(IEnumerable<TModel> models);
    }
}
