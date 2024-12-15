using System.Linq;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.Ids;

namespace BattleScene.Domain.DomainServices
{
    public class OrderItemsDomainService
    {
        private readonly IRepository<OrderItemEntity, OrderItemId> _orderItemRepository;

        public OrderItemsDomainService(
            IRepository<OrderItemEntity, OrderItemId> orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public OrderItemEntity First()
        {
            return _orderItemRepository.Get()
                .OrderBy(x => x.Order)
                .First();
        }
    }
}