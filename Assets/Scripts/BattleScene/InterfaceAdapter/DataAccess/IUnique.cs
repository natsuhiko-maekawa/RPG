namespace BattleScene.InterfaceAdapter.DataAccess
{
    public interface IUnique<out TId>
    {
        public TId Key { get; }
    }
}