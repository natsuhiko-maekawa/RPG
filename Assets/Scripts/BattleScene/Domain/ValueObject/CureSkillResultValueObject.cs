using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.ValueObject
{
    public class CureSkillResultValueObject : ISkillResult
    {
        public CharacterId ActorId { get; }
        public SkillCode SkillCode { get; }
        public ImmutableList<CharacterId> TargetIdList { get; }
        public ImmutableList<CureValueObject> CureList { get; }

        public CureSkillResultValueObject(
            CharacterId actorId, 
            SkillCode skillCode, 
            ImmutableList<CureValueObject> cureList)
        {
            ActorId = actorId;
            SkillCode = skillCode;
            TargetIdList = cureList
                .Select(x => x.TargetId)
                .ToImmutableList();
            CureList = cureList;
        }

        public bool Success()
        {
            return CureList
                .Any(x => x.Amount > 0);
        }
    }
}