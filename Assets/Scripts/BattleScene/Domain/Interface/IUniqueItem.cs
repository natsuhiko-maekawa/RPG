namespace BattleScene.InterfaceAdapter.DataAccess.Factory.Dto
{
    public interface IUniqueItem<out TId>
    {
        public TId Id { get; }
    }
}