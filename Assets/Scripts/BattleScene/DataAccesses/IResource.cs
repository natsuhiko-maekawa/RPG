using System.Collections.Generic;

namespace BattleScene.DataAccesses
{
    public interface IResource<out TItem>
    {
        public TItem Get();
    }

    public interface IResource<TItem, in TKey>
    {
        public TItem Get(TKey key);

        // QUESTION: この実装のように、アロケーションを起こさないためにはリストは戻り値で返却せず引数で受け渡しすべきか
        public void Get(List<TItem> itemList);
    }

    public interface IResource<out TItem, in TKey1, in TKey2>
    {
        public TItem Get(TKey1 key1);
        public TItem Get(TKey2 key2);
    }
}