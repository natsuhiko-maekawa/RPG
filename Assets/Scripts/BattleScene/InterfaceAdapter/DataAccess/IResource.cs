using System.Collections.Immutable;

namespace BattleScene.InterfaceAdapter.DataAccess
{
    public interface IResource<out TItem, in TKey>
    {
        public TItem Get(TKey key);
    }
}