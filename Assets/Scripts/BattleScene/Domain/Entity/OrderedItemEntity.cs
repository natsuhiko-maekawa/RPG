using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using JetBrains.Annotations;

namespace BattleScene.Domain.Entity
{
    public class OrderedItemEntity : BaseEntity<OrderedItemEntity, OrderNumber>
    {
        public OrderedItemEntity(
            OrderNumber orderNumber,
            OrderedItem orderedItem)
        {
            Id = orderNumber;
            CharacterId = orderedItem.CharacterId;
            AilmentCode = orderedItem.AilmentCode;
            SlipDamageCode = orderedItem.SlipDamageCode;
        }

        public override OrderNumber Id { get; }
        [CanBeNull] private CharacterId CharacterId { get; }
        private AilmentCode AilmentCode { get; }
        private SlipDamageCode SlipDamageCode { get; }
        
        public CharacterId GetCharacterId()
        {
            if (CharacterId == null)
                throw new InvalidOperationException();
            return CharacterId;
        }

        public AilmentCode GetAilmentCode()
        {
            if (AilmentCode == AilmentCode.NoAilment)
                throw new InvalidOperationException();
            return AilmentCode;
        }

        public SlipDamageCode GetSlipDamageCode()
        {
            if (SlipDamageCode == SlipDamageCode.NoSlipDamage)
                throw new InvalidOperationException();
            return SlipDamageCode;
        }

        public bool TryGetCharacterId(out CharacterId characterId)
        {
            characterId = CharacterId;
            return CharacterId != null;
        }

        public bool TryGetAilmentCode(out AilmentCode ailmentCode)
        {
            ailmentCode = AilmentCode;
            return AilmentCode != AilmentCode.NoAilment;
        }

        public bool TryGetSlipDamageCode(out SlipDamageCode slipDamageCode)
        {
            slipDamageCode = SlipDamageCode;
            return SlipDamageCode != SlipDamageCode.NoSlipDamage;
        }
    }
    
    public class OrderedItem
    {
        [CanBeNull] public CharacterId CharacterId { get; }
        public AilmentCode AilmentCode { get; } = AilmentCode.NoAilment;
        public SlipDamageCode SlipDamageCode { get; } = SlipDamageCode.NoSlipDamage;

        public OrderedItem(CharacterId characterId)
        {
            CharacterId = characterId;
        }

        public OrderedItem(AilmentCode ailmentCode)
        {
            AilmentCode = ailmentCode;
        }

        public OrderedItem(SlipDamageCode slipDamageCode)
        {
            SlipDamageCode = slipDamageCode;
        }
    }
}