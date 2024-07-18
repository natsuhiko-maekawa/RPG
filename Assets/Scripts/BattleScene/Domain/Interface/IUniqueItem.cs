namespace BattleScene.Domain.Interface
{
    public interface IUniqueItem<out TId>
    {
        public TId Id { get; }
    }
}