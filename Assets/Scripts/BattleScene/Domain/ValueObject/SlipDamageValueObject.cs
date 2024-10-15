using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public class SlipDamageValueObject : PrimeSkillValueObject
    {
        public SlipDamageValueObject(
            SlipCode slipCode,
            IReadOnlyList<CharacterId> targetIdList,
            IReadOnlyList<AttackValueObject> attackList)
        {
            SlipCode = slipCode;
            TargetIdList = targetIdList;
            ActualTargetIdList = TargetIdList;
            AttackList = attackList;
        }
    }
}