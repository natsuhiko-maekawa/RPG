using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public class SlipDamageValueObject : PrimeSkillValueObject
    {
        public SlipDamageValueObject(
            SlipDamageCode slipDamageCode,
            IReadOnlyList<CharacterId> targetIdList,
            IReadOnlyList<AttackValueObject> attackList)
        {
            SlipDamageCode = slipDamageCode;
            TargetIdList = targetIdList;
            AttackList = attackList;
        }
    }
}