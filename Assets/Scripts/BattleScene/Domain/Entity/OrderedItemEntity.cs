using BattleScene.Domain.Id;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.Entity
{
    public class OrderedItemEntity
    {
        public OrderedItemEntity(
            OrderNumber orderNumber,
            IOrderedItem orderedItem)
        {
            OrderNumber = orderNumber;
            OrderedItem = orderedItem;
        }

        public OrderNumber OrderNumber { get; }
        public IOrderedItem OrderedItem { get; }
    }
}