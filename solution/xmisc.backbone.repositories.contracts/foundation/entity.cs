using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using xmisc.backbone.repositories.contracts.infrastucture;

namespace xmisc.backbone.repositories.contracts.foundation
{
    public interface IEntityRepository<TKey, in TModel, TEntity>
        where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        where TEntity: EntityBase<TKey, TModel>
    {
        
        TEntity Create(TModel model);

        TEntity Create(IEnumerable<TModel> models);


        Task<TEntity> CreateAsync(TModel model);
        
        Task<IEnumerable<TEntity>> CreateAsync(IEnumerable<TModel> models);
    }
}
