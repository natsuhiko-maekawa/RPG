using System.Collections.Generic;
using BattleScene.Domain.Entity;

namespace BattleScene.Domain.DataAccess
{
    public interface ICollection<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        public TEntity Get(TId id);
        public IReadOnlyList<TEntity> Get();
        public void Add(TEntity entity);
        public void Add(IReadOnlyList<TEntity> entityList);
        public void Remove();
        public void Remove(TId id);
        public void Remove(IReadOnlyList<TId> idList);
    }
}