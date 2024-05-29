using System.Collections.Immutable;
using BattleScene.Domain.Entity;

namespace BattleScene.Domain.Aggregate
{
    public class OrderAggregate
    {
        public ImmutableList<OrderedItemEntity> OrderedItemList { get; }
        
        public OrderAggregate(
            ImmutableList<OrderedItemEntity> orderedItemList)
        {
            OrderedItemList = orderedItemList;
        }
    }
}