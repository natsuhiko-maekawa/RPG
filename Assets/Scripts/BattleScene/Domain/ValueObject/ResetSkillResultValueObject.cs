using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.ValueObject
{
    public class ResetSkillResultValueObject : ISkillResult
    {
        public CharacterId ActorId { get; }
        public SkillCode SkillCode { get; }
        public ImmutableList<AilmentCode> AilmentCodeList { get; }
        public ImmutableList<CharacterId> TargetIdList { get; }

        public ResetSkillResultValueObject(
            CharacterId actorId, 
            SkillCode skillCode, 
            ImmutableList<AilmentCode> ailmentCodeList,
            ImmutableList<CharacterId> targetIdList)
        {
            ActorId = actorId;
            SkillCode = skillCode;
            AilmentCodeList = ailmentCodeList;
            TargetIdList = targetIdList;
        }

        public bool Success()
        {
            throw new System.NotImplementedException();
        }
    }
}