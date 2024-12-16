namespace BattleScene.Domain.DataAccesses
{
    public interface IFactory<out TItem>
    {
        public TItem Create();
    }

    public interface IFactory<out TItem, in TKey>
    {
        public TItem Create(TKey key);
    }
}