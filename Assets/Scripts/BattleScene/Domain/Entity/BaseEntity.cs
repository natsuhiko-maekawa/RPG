namespace BattleScene.Domain.Entity
{
    public abstract class BaseEntity<TId> where TId : notnull
    {
        public abstract TId Id { get; }

        public override bool Equals(object? obj)
        {
            if (obj is not BaseEntity<TId> entity) return false;
            return Equals(Id, entity.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}