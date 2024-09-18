using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using JetBrains.Annotations;

namespace BattleScene.Domain.Entity
{
    public class OrderedItemEntity : BaseEntity<OrderId>
    {
        public OrderedItemEntity(
            OrderId orderId,
            int orderNumber,
            OrderedItem orderedItem)
        {
            Id = orderId;
            OrderNumber = orderNumber;
            CharacterId = orderedItem.CharacterId;
            AilmentCode = orderedItem.AilmentCode;
            SlipDamageCode = orderedItem.SlipDamageCode;
            OrderedItemType = orderedItem.OrderedItemType;
        }

        public override OrderId Id { get; }
        public int OrderNumber { get; }
        [CanBeNull] private CharacterId CharacterId { get; }
        private AilmentCode AilmentCode { get; }
        private SlipDamageCode SlipDamageCode { get; }
        public OrderedItemType OrderedItemType { get; }
        
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
        public OrderedItemType OrderedItemType { get; }
        [CanBeNull] public CharacterId CharacterId { get; }
        public AilmentCode AilmentCode { get; } = AilmentCode.NoAilment;
        public SlipDamageCode SlipDamageCode { get; } = SlipDamageCode.NoSlipDamage;

        public OrderedItem(CharacterId characterId)
        {
            CharacterId = characterId;
            OrderedItemType = OrderedItemType.Character;
        }

        public OrderedItem(AilmentCode ailmentCode)
        {
            AilmentCode = ailmentCode;
            OrderedItemType = OrderedItemType.Ailment;
        }

        public OrderedItem(SlipDamageCode slipDamageCode)
        {
            SlipDamageCode = slipDamageCode;
            OrderedItemType = OrderedItemType.Slip;
        }
    }

    public enum OrderedItemType
    {
        Character,
        Ailment,
        Slip
    }
}