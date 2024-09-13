namespace BattleScene.InterfaceAdapter.DataAccess
{
    public interface IResource<out TItem>
    {
        public TItem Get();
    }
    
    public interface IResource<out TItem, in TKey>
    {
        public TItem Get(TKey key);
    }
}