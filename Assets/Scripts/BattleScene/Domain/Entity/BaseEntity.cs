namespace BattleScene.Domain.Entity
{
    public abstract class BaseEntity<TId>
    {
        public abstract TId Id { get; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var entity = (BaseEntity<TId>)obj;
            return Equals(Id, entity.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}