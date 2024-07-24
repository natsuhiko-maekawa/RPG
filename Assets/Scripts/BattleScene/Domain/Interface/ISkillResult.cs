using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.OldId;

namespace BattleScene.Domain.Interface
{
    public interface ISkillResult : IResult
    {
        public CharacterId ActorId { get; }
        public SkillCode SkillCode { get; }
        public ImmutableList<CharacterId> TargetIdList { get; }
        public bool Success();
    }
}