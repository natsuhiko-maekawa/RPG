using System.Diagnostics.CodeAnalysis;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

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
            SlipCode = orderedItem.SlipCode;
            OrderedItemType = orderedItem.OrderedItemType;
        }

        public override OrderId Id { get; }
        public int OrderNumber { get; }
        private CharacterId? CharacterId { get; }
        private AilmentCode AilmentCode { get; }
        private SlipCode SlipCode { get; }
        public OrderedItemType OrderedItemType { get; }

        public bool TryGetCharacterId([MaybeNullWhen(false)] out CharacterId characterId)
        {
            characterId = CharacterId;
            return CharacterId != null;
        }

        public bool TryGetAilmentCode(out AilmentCode ailmentCode)
        {
            ailmentCode = AilmentCode;
            return AilmentCode != AilmentCode.NoAilment;
        }

        public bool TryGetSlipDamageCode(out SlipCode slipCode)
        {
            slipCode = SlipCode;
            return SlipCode != SlipCode.NoSlip;
        }

        public override string ToString()
        {
            var str = $@"Id: {Id},
OrderNumber: {OrderNumber},
CharacterId: {CharacterId},
AilmentCode: {AilmentCode},
SlipDamageCode: {SlipCode},
OrderedItemType: {OrderedItemType}";
            return str;
        }
    }

    public class OrderedItem
    {
        public OrderedItemType OrderedItemType { get; }
        public CharacterId? CharacterId { get; }
        public AilmentCode AilmentCode { get; } = AilmentCode.NoAilment;
        public SlipCode SlipCode { get; } = SlipCode.NoSlip;

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

        public OrderedItem(SlipCode slipCode)
        {
            SlipCode = slipCode;
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