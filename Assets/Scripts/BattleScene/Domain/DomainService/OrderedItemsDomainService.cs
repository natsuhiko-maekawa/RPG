using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.DomainService
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