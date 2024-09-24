using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Entity;

namespace BattleScene.Domain.DataAccess
{
    public interface IRepository<TEntity, TId> 
        where TEntity : BaseEntity<TId>
    {
        public TEntity Select(TId id);
        public ImmutableList<TEntity> Select();
        public void Update(TEntity entity);
        public void Update(IReadOnlyList<TEntity> entityList);
        public void Delete();
        public void Delete(TId id);
        public void Delete(IReadOnlyList<TId> idList);
    }
}