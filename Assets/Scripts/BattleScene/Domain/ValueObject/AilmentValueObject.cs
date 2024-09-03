using System.Collections.Generic;
using System.Collections.Immutable;
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
            IList<CharacterId> targetIdList)
        {
            ActorId = actorId;
            SkillCode = skillCode;
            AilmentCode = ailmentCode;
            TargetIdList = targetIdList.ToImmutableList();
        }

        public AilmentValueObject(
            CharacterId actorId,
            SkillCode skillCode)
        {
            ActorId = actorId;
            SkillCode = skillCode;
            AilmentCode = AilmentCode.NoAilment;
            TargetIdList = ImmutableList<CharacterId>.Empty;
        }

        public AilmentCode AilmentCode { get; }
        public CharacterId ActorId { get; }
        public SkillCode SkillCode { get; }
        public ImmutableList<CharacterId> TargetIdList { get; }

        public bool Success()
        {
            return !TargetIdList.IsEmpty;
        }
    }
}