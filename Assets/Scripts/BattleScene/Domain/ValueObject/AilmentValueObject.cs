using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public class AilmentValueObject
    {
        public AilmentValueObject(
            CharacterId actorId,
            SkillCode skillCode,
            AilmentCode ailmentCode,
            IReadOnlyList<CharacterId> targetIdList,
            IReadOnlyList<CharacterId> actualTargetIdList)
        {
            ActorId = actorId;
            SkillCode = skillCode;
            AilmentCode = ailmentCode;
            TargetIdList = targetIdList;
            ActualTargetIdList = actualTargetIdList;
        }

        public AilmentCode AilmentCode { get; }
        public CharacterId ActorId { get; }
        public SkillCode SkillCode { get; }
        public IReadOnlyList<CharacterId> TargetIdList { get; }
        public IReadOnlyList<CharacterId> ActualTargetIdList { get; }
    }
}