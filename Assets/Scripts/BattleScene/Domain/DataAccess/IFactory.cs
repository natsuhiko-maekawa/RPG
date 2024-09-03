namespace BattleScene.Domain.DataAccess
{
    public interface IFactory<out TItem, in TKey>
    {
        public TItem Create(TKey key);
    }
}