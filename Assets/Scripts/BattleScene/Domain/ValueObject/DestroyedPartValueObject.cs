using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.ValueObject
{
    public class DestroyedPartValueObject
    {
        public DestroyedPartValueObject(
            CharacterId actorId,
            IList<CharacterId> targetIdList,
            SkillCode skillCode,
            BodyPartCode bodyPartCode,
            int destroyCount)
        {
            CharacterId = actorId;
            ActorId = actorId;
            TargetIdList = targetIdList.ToImmutableList();
            SkillCode = skillCode;
            BodyPartCode = bodyPartCode;
            DestroyCount = destroyCount;
        }
        
        public CharacterId ActorId { get; }
        public ImmutableList<CharacterId> TargetIdList { get; }
        public SkillCode SkillCode { get; }
        public BodyPartCode BodyPartCode { get; }
        public int DestroyCount { get; }
        
        [Obsolete]
        public CharacterId CharacterId { get; }

        public bool Success()
        {
            return !TargetIdList.IsEmpty;
        }
    }
}