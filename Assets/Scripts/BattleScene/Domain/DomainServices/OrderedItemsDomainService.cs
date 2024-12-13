using System.Linq;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.Ids;

namespace BattleScene.Domain.DomainServices
{
    public class OrderedItemsDomainService
    {
        private readonly IRepository<OrderedItemEntity, OrderedItemId> _orderedItemRepository;

        public OrderedItemsDomainService(
            IRepository<OrderedItemEntity, OrderedItemId> orderedItemRepository)
        {
            _orderedItemRepository = orderedItemRepository;
        }

        public OrderedItemEntity First()
        {
            return _orderedItemRepository.Get()
                .OrderBy(x => x.Order)
                .First();
        }
    }
}