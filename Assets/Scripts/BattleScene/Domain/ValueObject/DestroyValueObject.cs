using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public class DestroyValueObject
    {
        public DestroyValueObject(
            CharacterId actorId,
            IReadOnlyList<CharacterId> targetIdList,
            IReadOnlyList<CharacterId> actualTargetIdList,
            SkillCode skillCode,
            BodyPartCode bodyPartCode,
            int destroyCount)
        {
            ActorId = actorId;
            TargetIdList = targetIdList;
            ActualTargetIdList = actualTargetIdList;
            SkillCode = skillCode;
            BodyPartCode = bodyPartCode;
            DestroyCount = destroyCount;
        }
        
        public CharacterId ActorId { get; }
        public IReadOnlyList<CharacterId> TargetIdList { get; }
        public IReadOnlyList<CharacterId> ActualTargetIdList { get; }
        public SkillCode SkillCode { get; }
        public BodyPartCode BodyPartCode { get; }
        public int DestroyCount { get; }
    }
}