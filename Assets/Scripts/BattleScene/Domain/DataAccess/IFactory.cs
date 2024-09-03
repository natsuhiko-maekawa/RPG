namespace BattleScene.Domain.DataAccess
{
    public interface IFactory<out TItem, in TId>
    {
        public TItem Create(TId id);
    }
}