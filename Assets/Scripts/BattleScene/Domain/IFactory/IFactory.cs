using System.Collections.Immutable;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.IFactory
{
    public interface IFactory<TItem, in TId>
        where TItem : IUniqueItem<TId>
    {
        public ImmutableList<TItem> Create();
        public TItem Create(TId id);
    }
}