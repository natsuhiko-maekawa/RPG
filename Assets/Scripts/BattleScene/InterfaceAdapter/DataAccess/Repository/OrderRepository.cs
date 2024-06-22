// using BattleScene.Domain.Aggregate;
// using BattleScene.Domain.IRepository;
//
// namespace BattleScene.InterfaceAdapter.DataAccess.Repository
// {
//     public class OrderRepository : IOrderRepository
//     {
//         private OrderAggregate _order;
//
//         OrderAggregate IOrderRepository.Select()
//         {
//             return _order;
//         }
//
//         public void Update(OrderAggregate order)
//         {
//             _order = order;
//         }
//     }
// }