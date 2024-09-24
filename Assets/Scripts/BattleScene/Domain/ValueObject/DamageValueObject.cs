using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public class DamageValueObject : PrimeSkillValueObject
    {
        public DamageValueObject(
            SkillCode skillCode,
            CharacterId actorId,
            IReadOnlyList<AttackValueObject> attackList)
        {
            SkillCode = skillCode;
            ActorId = actorId;
            TargetIdList = attackList
                .Select(x => x.TargetId)
                .Distinct()
                .ToList();
            ActualTargetIdList = TargetIdList;
            AttackList = attackList;
        }
    }
}