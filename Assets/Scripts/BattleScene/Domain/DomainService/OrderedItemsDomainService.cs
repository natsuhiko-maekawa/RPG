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

        public IOrderedItem FirstItem()
        {
            return _orderedItemRepository.Select()
                .OrderBy(x => x.Id)
                .FirstOrDefault()
                ?.OrderedItem;
        }

        public CharacterId FirstCharacterId()
        {
            if (FirstItem() is not OrderedCharacterValueObject orderedCharacter)
                throw new InvalidOperationException();
            return orderedCharacter.CharacterId;
        }

        public AilmentCode FirstAilmentCode()
        {
            if (FirstItem() is not OrderedAilmentValueObject orderedAilment)
                throw new InvalidOperationException();
            return orderedAilment.AilmentCode;
        }

        public SlipDamageCode FirstSlipDamageCode()
        {
            if (FirstItem() is not OrderedSlipDamageValueObject orderedSlipDamage)
                throw new InvalidOperationException();
            return orderedSlipDamage.SlipDamageCode;
        }
    }
}