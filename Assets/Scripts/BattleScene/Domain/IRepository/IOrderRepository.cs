using BattleScene.Domain.Aggregate;

namespace BattleScene.Domain.IRepository
{
    public interface IOrderRepository
    {
        public OrderAggregate Select();
        public void Update(OrderAggregate order);
    }
}