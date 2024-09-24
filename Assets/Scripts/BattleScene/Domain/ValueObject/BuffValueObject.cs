using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public class BuffValueObject : PrimeSkillValueObject
    {
        public BuffValueObject(
            BuffCode buffCode,
            SkillCode skillCode,
            CharacterId actorId,
            IReadOnlyList<CharacterId> targetIdList,
            float rate,
            int turn,
            LifetimeCode lifetimeCode)
        {
            BuffCode = buffCode;
            SkillCode = skillCode;
            ActorId = actorId;
            TargetIdList = targetIdList;
            ActualTargetIdList = targetIdList;
            Rate = rate;
            Turn = turn;
            LifetimeCode = lifetimeCode;
        }
    }
}