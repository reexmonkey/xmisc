using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace xmisc.backbone.repositories.contracts.foundation
{
    public interface IHydrateEntityRepository<in TKey, TModel>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
    {
        TModel Hydrate(TModel model);

        IEnumerable<TModel> HydrateAll(IEnumerable<TModel> models);

        TModel Dehydrate(TModel model);

        IEnumerable<TModel> DehydrateAll(IEnumerable<TModel> models);

        Task<TModel> HydrateAsync(TModel model);

        Task<IEnumerable<TModel>> HydrateAllAsync(IEnumerable<TModel> models);

        Task<TModel> DehydrateAsync(TModel model);

        Task<IEnumerable<TModel>> DehydrateAllAsync(IEnumerable<TModel> models);

    }
}
