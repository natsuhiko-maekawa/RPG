using BattleScene.Domain.Interface;

namespace BattleScene.Domain.Entity
{
    public abstract class BaseEntity<TEntity, TId>
        where TEntity : BaseEntity<TEntity, TId>
        where TId : IId
    {
        public abstract TId Id { get; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var entity = (TEntity)obj;
            return Equals(Id, entity.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}