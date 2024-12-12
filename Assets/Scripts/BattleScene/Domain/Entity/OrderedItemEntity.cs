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
        private CharacterEntity? _actor;
        private AilmentCode _ailmentCode;
        private SlipCode _slipCode;
        public ActorType ActorType { get; private set; }

        public void SetOrderedItem(ActorInTurn actorInTurn)
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
            var str = $@"OrderedItemEntity
  OrderNumber     : {Order},
  CharacterId     : {_actor?.ToString() ?? "NoCharacter"},
  AilmentCode     : {_ailmentCode},
  SlipDamageCode  : {_slipCode},
  OrderedItemType : {ActorType}";
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