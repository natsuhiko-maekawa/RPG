using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public class CureValueObject
    {
        public CureValueObject(
            CharacterId actorId,
            SkillCode skillCode,
            IReadOnlyList<RecoveryValueObject> cureList)
        {
            ActorId = actorId;
            SkillCode = skillCode;
            TargetIdList = cureList
                .Select(x => x.TargetId)
                .ToList();
            CureList = cureList;
        }

        public IReadOnlyList<RecoveryValueObject> CureList { get; }
        public CharacterId ActorId { get; }
        public SkillCode SkillCode { get; }
        public IReadOnlyList<CharacterId> TargetIdList { get; }

        public bool Success()
        {
            return CureList
                .Any(x => x.Amount > 0);
        }
    }
}