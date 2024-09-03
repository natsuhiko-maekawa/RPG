using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public class RestoreValueObject
    {
        public CharacterId ActorId { get; }
        public SkillCode SkillCode { get; }
        public ImmutableList<CharacterId> TargetIdList { get; }
        public int TechnicalPoint { get; }

        public RestoreValueObject(
            CharacterId actorId,
            SkillCode skillCode,
            IList<CharacterId> targetIdList,
            int technicalPoint)
        {
            ActorId = actorId;
            SkillCode = skillCode;
            TargetIdList = targetIdList.ToImmutableList();
            TechnicalPoint = technicalPoint;
        }
    }
}