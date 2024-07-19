namespace BattleScene.Domain.IFactory
{
    public interface IFactory<out TItem, in TId>
    {
        public TItem Create(TId id);
    }
}