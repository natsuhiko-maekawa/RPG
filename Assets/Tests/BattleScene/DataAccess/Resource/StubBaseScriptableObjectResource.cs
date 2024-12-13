using System.Collections.Generic;
using System.Linq;
using BattleScene.DataAccesses;

namespace Tests.BattleScene.DataAccess.Resource
{
    public class StubBaseScriptableObjectResource<TItem, TId> : IResource<TItem, TId> where TItem : IUnique<TId>
    {
        private readonly List<TItem> _itemList;

        public StubBaseScriptableObjectResource(
            IReadOnlyList<TItem> itemList)
        {
            _itemList = itemList.ToList();
        }

        public TItem Get(TId key)
        {
            return _itemList.First(x => Equals(x.Key, key));
        }

        public void Get(List<TItem> itemList)
        {
            itemList.Clear();
            itemList.AddRange(_itemList);
        }
    }
}