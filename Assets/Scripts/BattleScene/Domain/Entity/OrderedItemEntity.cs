using System.Diagnostics.CodeAnalysis;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.Entity
{
    public class OrderedItemEntity : BaseEntity<OrderedItemId>
    {
        public OrderedItemEntity(
            OrderedItemId orderedItemId,
            int order)
        {
            Id = orderedItemId;
            Order = order;
        }

        public override OrderedItemId Id { get; }
        public int Order { get; }
        private CharacterId? _characterId;
        private AilmentCode _ailmentCode;
        private SlipCode _slipCode;
        public OrderedItemType OrderedItemType { get; private set; }

        public void SetOrderedItem(OrderedItem orderedItem)
        {
            _characterId = orderedItem.CharacterId;
            _ailmentCode = orderedItem.AilmentCode;
            _slipCode = orderedItem.SlipCode;
            OrderedItemType = orderedItem.OrderedItemType;
        }

        public bool TryGetCharacterId([NotNullWhen(true)] out CharacterId? characterId)
        {
            characterId = _characterId;
            return _characterId != null;
        }

        public bool TryGetAilmentCode(out AilmentCode ailmentCode)
        {
            ailmentCode = _ailmentCode;
            return _ailmentCode != AilmentCode.NoAilment;
        }

        public bool TryGetSlipCode(out SlipCode slipCode)
        {
            slipCode = _slipCode;
            return _slipCode != SlipCode.NoSlip;
        }

        public override string ToString()
        {
            var str = $@"OrderedItemEntity
  OrderNumber     : {Order},
  CharacterId     : {_characterId?.ToString() ?? "NoCharacter"},
  AilmentCode     : {_ailmentCode},
  SlipDamageCode  : {_slipCode},
  OrderedItemType : {OrderedItemType}";
            return str;
        }
    }

    public struct OrderedItem
    {
        public OrderedItemType OrderedItemType { get; }
        public CharacterId? CharacterId { get; }
        public AilmentCode AilmentCode { get; }
        public SlipCode SlipCode { get; }

        public OrderedItem(CharacterId characterId)
        {
            OrderedItemType = OrderedItemType.Character;
            CharacterId = characterId;
            AilmentCode = AilmentCode.NoAilment;
            SlipCode = SlipCode.NoSlip;
        }

        public OrderedItem(AilmentCode ailmentCode)
        {
            OrderedItemType = OrderedItemType.Ailment;
            CharacterId = null;
            AilmentCode = ailmentCode;
            SlipCode = SlipCode.NoSlip;
        }

        public OrderedItem(SlipCode slipCode)
        {
            OrderedItemType = OrderedItemType.Slip;
            CharacterId = null;
            AilmentCode = AilmentCode.NoAilment;
            SlipCode = slipCode;
        }
    }

    public enum OrderedItemType : byte
    {
        Character,
        Ailment,
        Slip
    }
}