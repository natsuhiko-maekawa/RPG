namespace BattleScene.InterfaceAdapter.DataAccess
{
    public interface IResource<out TItem, in TId>
    {
        public TItem Get(TId id);
    }
}