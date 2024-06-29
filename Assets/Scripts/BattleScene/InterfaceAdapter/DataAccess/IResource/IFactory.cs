using System.Collections.Immutable;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;

namespace BattleScene.InterfaceAdapter.DataAccess.IResource
{
    public interface IFactory<TItem, in TId>
        where TItem : IUniqueItem<TId>
    {
        public ImmutableList<TItem> Create();
        public TItem Create(TId id);
    }
}