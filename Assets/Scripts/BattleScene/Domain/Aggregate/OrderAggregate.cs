using System.Collections.Immutable;
using BattleScene.Domain.Entity;

namespace BattleScene.Domain.Aggregate
{
    public class OrderAggregate
    {
        public OrderAggregate(
            ImmutableList<OrderedItemEntity> orderedItemList)
        {
            OrderedItemList = orderedItemList;
        }

        public ImmutableList<OrderedItemEntity> OrderedItemList { get; }
    }
}