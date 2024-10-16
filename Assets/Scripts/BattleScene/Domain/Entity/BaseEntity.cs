namespace BattleScene.Domain.Entity
{
    public abstract class BaseEntity<TId>
    {
        public abstract TId Id { get; }
    }
}