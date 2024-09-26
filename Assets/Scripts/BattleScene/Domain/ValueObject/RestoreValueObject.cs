using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public class RestoreValueObject : PrimeSkillValueObject
    {
        public RestoreValueObject(
            SkillCode skillCode,
            CharacterId actorId,
            IReadOnlyList<CharacterId> targetIdList,
            int technicalPoint)
        {
            SkillCode = skillCode;
            ActorId = actorId;
            TargetIdList = targetIdList;
            ActualTargetIdList = TargetIdList;
            TechnicalPoint = technicalPoint;
        }
    }
}