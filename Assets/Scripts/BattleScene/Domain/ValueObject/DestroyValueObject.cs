using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public class DestroyValueObject : PrimeSkillValueObject
    {
        public DestroyValueObject(
            BodyPartCode bodyPartCode,
            SkillCode skillCode,
            CharacterId actorId,
            IReadOnlyList<CharacterId> targetIdList,
            IReadOnlyList<CharacterId> actualTargetIdList,
            int destroyCount)
        {
            BodyPartCode = bodyPartCode;
            SkillCode = skillCode;
            ActorId = actorId;
            TargetIdList = targetIdList;
            ActualTargetIdList = actualTargetIdList;
            DestroyCount = destroyCount;
        }
    }
}