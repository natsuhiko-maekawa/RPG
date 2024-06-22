using BattleScene.Domain.Id;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.Entity
{
    public class OrderedItemEntity : BaseEntity<OrderedItemEntity, OrderNumber>
    {
        public OrderedItemEntity(
            OrderNumber orderNumber,
            IOrderedItem orderedItem)
        {
            Id = orderNumber;
            OrderedItem = orderedItem;
        }

        public override OrderNumber Id { get; }
        public IOrderedItem OrderedItem { get; }
    }
}