namespace BattleScene.DataAccess
{
    public interface IResource<out TItem>
    {
        public TItem Get();
    }

    public interface IResource<out TItem, in TKey>
    {
        public TItem Get(TKey key);
    }

    public interface IResource<out TItem, in TKey1, in TKey2>
    {
        public TItem Get(TKey1 key1);
        public TItem Get(TKey2 key2);
    }
}