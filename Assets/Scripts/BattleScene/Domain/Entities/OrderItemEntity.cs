using System.Diagnostics.CodeAnalysis;
using BattleScene.Domain.Codes;
using BattleScene.Domain.Ids;

namespace BattleScene.Domain.Entities
{
    public class OrderItemEntity : BaseEntity<OrderItemId>
    {
        public OrderItemEntity(
            OrderItemId orderItemId,
            int order)
        {
            Id = orderItemId;
            Order = order;
        }

        public override OrderItemId Id { get; }
        public int Order { get; }
        private CharacterEntity? _actor;
        private AilmentCode _ailmentCode;
        private SlipCode _slipCode;
        public ActorType ActorType { get; private set; }

        public void SetOrderItem(ActorInTurn actorInTurn)
        {
            _actor = actorInTurn.Actor;
            _ailmentCode = actorInTurn.AilmentCode;
            _slipCode = actorInTurn.SlipCode;
            ActorType = actorInTurn.ActorType;
        }

        public bool TryGetActor([NotNullWhen(true)] out CharacterEntity? actor)
        {
            actor = _actor;
            return _actor is not null;
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
            var str = $@"OrderItemEntity
  Order         : {Order},
  CharacterId   : {_actor?.ToString() ?? "NoCharacter"},
  AilmentCode   : {_ailmentCode},
  SlipDamageCode: {_slipCode},
  OrderItemType : {ActorType}";
            return str;
        }
    }

    public struct ActorInTurn // 16 byte
    {
        public ActorType ActorType { get; }
        public CharacterEntity? Actor { get; }
        public AilmentCode AilmentCode { get; }
        public SlipCode SlipCode { get; }

        public ActorInTurn(CharacterEntity actor)
        {
            ActorType = ActorType.Actor;
            Actor = actor;
            AilmentCode = AilmentCode.NoAilment;
            SlipCode = SlipCode.NoSlip;
        }

        public ActorInTurn(AilmentCode ailmentCode)
        {
            ActorType = ActorType.Ailment;
            Actor = null;
            AilmentCode = ailmentCode;
            SlipCode = SlipCode.NoSlip;
        }

        public ActorInTurn(SlipCode slipCode)
        {
            ActorType = ActorType.Slip;
            Actor = null;
            AilmentCode = AilmentCode.NoAilment;
            SlipCode = slipCode;
        }
    }

    public enum ActorType : byte
    {
        Actor,
        Ailment,
        Slip
    }
}