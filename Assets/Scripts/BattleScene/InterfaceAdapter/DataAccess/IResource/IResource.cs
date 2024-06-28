using System.Collections.Immutable;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;

namespace BattleScene.InterfaceAdapter.DataAccess.IResource
{
    public interface IResource<TItem, in TId>
        where TItem : IListScriptableObjectItem<TId>
    {
        public ImmutableList<TItem> Select();
        public TItem Select(TId id);
    }
}