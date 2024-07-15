using System;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.Interface;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.DomainService
{
    public class OrderedItemsDomainService
    {
        private readonly IRepository<OrderedItemEntity, OrderNumber> _orderedItemRepository;

        public OrderedItemsDomainService(
            IRepository<OrderedItemEntity, OrderNumber> orderedItemRepository)
        {
            _orderedItemRepository = orderedItemRepository;
        }

        [Obsolete]
        public IOrderedItem FirstItem()
        {
            throw new NotImplementedException();
        }

        public OrderedItemEntity First()
        {
            return _orderedItemRepository.Select()
                .First();
        }

        [Obsolete]
        public CharacterId FirstCharacterId()
        {
            throw new NotImplementedException();
        }

        [Obsolete]
        public AilmentCode FirstAilmentCode()
        {
            throw new NotImplementedException();
        }

        [Obsolete]
        public SlipDamageCode FirstSlipDamageCode()
        {
            throw new NotImplementedException();
        }
    }
}