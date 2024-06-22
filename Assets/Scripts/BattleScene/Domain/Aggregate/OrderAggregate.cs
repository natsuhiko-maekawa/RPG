// using System.Collections.Immutable;
// using BattleScene.Domain.Entity;
// using BattleScene.Domain.Id;
//
// namespace BattleScene.Domain.Aggregate
// {
//     public class OrderAggregate : BaseEntity<OrderAggregate, OrderId>
//     {
//         public OrderAggregate(
//             OrderId orderId,
//             ImmutableList<OrderedItemEntity> orderedItemList)
//         {
//             Id = orderId;
//             OrderedItemList = orderedItemList;
//         }
//
//         public override OrderId Id { get; }
//         public ImmutableList<OrderedItemEntity> OrderedItemList { get; }
//     }
// }