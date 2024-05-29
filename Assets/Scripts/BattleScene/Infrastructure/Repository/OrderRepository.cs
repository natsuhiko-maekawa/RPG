using BattleScene.Domain.Aggregate;
using BattleScene.Domain.IRepository;

namespace BattleScene.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private OrderAggregate _order;

        OrderAggregate IOrderRepository.Select()
        {
            throw new System.NotImplementedException();
        }

        public void Update(OrderAggregate order)
        {
            throw new System.NotImplementedException();
        }
    }
}