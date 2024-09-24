using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public class AilmentValueObject : PrimeSkillValueObject
    {
        public AilmentValueObject(
            AilmentCode ailmentCode,
            SkillCode skillCode,
            CharacterId actorId,
            IReadOnlyList<CharacterId> targetIdList,
            IReadOnlyList<CharacterId> actualTargetIdList)
        {
            AilmentCode = ailmentCode;
            SkillCode = skillCode;
            ActorId = actorId;
            TargetIdList = targetIdList;
            ActualTargetIdList = actualTargetIdList;
        }
    }
}