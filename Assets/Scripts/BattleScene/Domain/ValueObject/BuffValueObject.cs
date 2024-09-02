using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.ValueObject
{
    public class BuffValueObject : ISkillResult
    {
        public BuffValueObject(
            CharacterId actorId,
            SkillCode skillCode,
            BuffCode buffCode,
            IList<CharacterId> targetIdList)
        {
            ActorId = actorId;
            SkillCode = skillCode;
            BuffCode = buffCode;
            TargetIdList = targetIdList.ToImmutableList();
        }

        public BuffValueObject(
            CharacterId actorId,
            SkillCode skillCode)
        {
            ActorId = actorId;
            SkillCode = skillCode;
            BuffCode = BuffCode.NoBuff;
            TargetIdList = ImmutableList<CharacterId>.Empty;
        }

        public BuffCode BuffCode { get; }
        public CharacterId ActorId { get; }
        public SkillCode SkillCode { get; }
        public ImmutableList<CharacterId> TargetIdList { get; }

        public bool Success()
        {
            return !TargetIdList.IsEmpty;
        }
    }
}