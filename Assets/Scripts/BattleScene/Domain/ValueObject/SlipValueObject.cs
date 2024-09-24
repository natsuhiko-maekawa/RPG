using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public class SlipValueObject : PrimeSkillValueObject
    {
        public SlipValueObject(
            SlipDamageCode slipDamageCode,
            SkillCode skillCode,
            CharacterId actorId,
            IReadOnlyList<CharacterId> targetIdList,
            IReadOnlyList<CharacterId> actualTargetIdList)
        {
            SlipDamageCode = slipDamageCode;
            SkillCode = skillCode;
            ActorId = actorId;
            TargetIdList = targetIdList;
            ActualTargetIdList = actualTargetIdList;
        }
    }
}