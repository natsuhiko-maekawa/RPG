using System.Linq;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.DomainService
{
    public class OrderedItemsDomainService
    {
        private readonly ICollection<OrderedItemEntity, OrderedItemId> _orderedItemCollection;

        public OrderedItemsDomainService(
            ICollection<OrderedItemEntity, OrderedItemId> orderedItemCollection)
        {
            _orderedItemCollection = orderedItemCollection;
        }

        public OrderedItemEntity First()
        {
            return _orderedItemCollection.Get()
                .OrderBy(x => x.Order)
                .First();
        }
    }
}