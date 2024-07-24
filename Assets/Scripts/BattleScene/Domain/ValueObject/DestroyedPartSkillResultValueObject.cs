using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.Domain.OldId;

namespace BattleScene.Domain.ValueObject
{
    public class DestroyedPartSkillResultValueObject : ISkillResult
    {
        public DestroyedPartSkillResultValueObject(
            CharacterId characterId,
            BodyPartCode bodyPartCode,
            int destroyedNumber)
        {
            CharacterId = characterId;
            BodyPartCode = bodyPartCode;
            DestroyedNumber = destroyedNumber;
        }

        public CharacterId CharacterId { get; }
        public BodyPartCode BodyPartCode { get; }
        public int DestroyedNumber { get; }

        public CharacterId ActorId { get; }
        public SkillCode SkillCode { get; }
        public ImmutableList<CharacterId> TargetIdList { get; }

        public bool Success()
        {
            return !TargetIdList.IsEmpty;
        }
    }
}