using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.DomainService
{
    public class OrderedItemsDomainService
    {
        private readonly IRepository<OrderedItemEntity, OrderId> _orderedItemRepository;

        public OrderedItemsDomainService(
            IRepository<OrderedItemEntity, OrderId> orderedItemRepository)
        {
            _orderedItemRepository = orderedItemRepository;
        }

        public OrderedItemEntity First()
        {
            return _orderedItemRepository.Select()
                .OrderBy(x => x.OrderNumber)
                .First();
        }
    }
}