using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace xmisc.backbone.repositories.contracts.foundation
{
    public interface IPersistEntityRepository<in TKey, in TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        where TModel: new()

    {
        void Persist(TModel model);

        void PersistAll(IEnumerable<TModel> models);

        Task PersistAsync(TModel model);

        Task PersistAllAsync(IEnumerable<TModel> models);

    }
}
