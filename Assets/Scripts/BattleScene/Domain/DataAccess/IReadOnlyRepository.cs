using System.Collections.Immutable;
using BattleScene.Domain.Entity;

namespace BattleScene.Domain.DataAccess
{
    public interface IReadOnlyRepository<TEntity, in TId> 
        where TEntity : BaseEntity<TEntity, TId>
    {
        public TEntity Select(TId id);
        public ImmutableList<TEntity> Select();
    }
}