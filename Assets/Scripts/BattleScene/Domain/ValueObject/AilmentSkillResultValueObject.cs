using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.ValueObject
{
    public class AilmentSkillResultValueObject : ISkillResult
    {
        public CharacterId ActorId { get; }
        public SkillCode SkillCode { get; }
        public AilmentCode AilmentCode { get; }
        public ImmutableList<CharacterId> TargetIdList { get; }

        public AilmentSkillResultValueObject(
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

        public AilmentSkillResultValueObject(
            CharacterId actorId,
            SkillCode skillCode)
        {
            ActorId = actorId;
            SkillCode = skillCode;
            AilmentCode = AilmentCode.NoAilment;
            TargetIdList = ImmutableList<CharacterId>.Empty;
        }
        
        public bool Success()
        {
            return !TargetIdList.IsEmpty;
        }
    }
}