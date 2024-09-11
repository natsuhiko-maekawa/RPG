using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public class SlipDamageValueObject
    {
        public SlipDamageValueObject(
            IList<CharacterId> targetIdList,
            SlipDamageCode slipDamageCode,
            ImmutableList<AttackValueObject> attackList)
        {
            TargetIdList = targetIdList.ToImmutableList();
            SlipDamageCode = slipDamageCode;
            AttackList = attackList;
        }

        public CharacterId ActorId { get; } = null;
        public ImmutableList<CharacterId> TargetIdList { get; }
        public SlipDamageCode SlipDamageCode { get; }
        public ImmutableList<AttackValueObject> AttackList { get; }
    }
}