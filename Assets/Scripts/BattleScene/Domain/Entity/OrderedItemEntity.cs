using BattleScene.Domain.Id;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.Entity
{
    public class OrderedItemEntity
    {
        public OrderNumber OrderNumber { get; }
        public IOrderedItem OrderedItem { get; }

        public OrderedItemEntity(
            OrderNumber orderNumber,
            IOrderedItem orderedItem)
        {
            OrderNumber = orderNumber;
            OrderedItem = orderedItem;
        }
    }
}