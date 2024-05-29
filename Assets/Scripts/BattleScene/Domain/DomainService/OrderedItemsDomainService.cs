using System;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.Interface;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.DomainService
{
    public class OrderedItemsDomainService
    {
        private readonly IOrderRepository _orderRepository;

        public IOrderedItem FirstItem()
        {
            return _orderRepository.Select().OrderedItemList
                .OrderBy(x => x.OrderNumber)
                .First()
                .OrderedItem;
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