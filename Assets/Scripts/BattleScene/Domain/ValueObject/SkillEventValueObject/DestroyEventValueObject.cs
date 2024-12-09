using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject.SkillEventValueObject.Interface;

namespace BattleScene.Domain.ValueObject.SkillEventValueObject
{
    public class DestroyEventValueObject : ISkillEventValueObject // 16 byte
    {
        public SkillEventCode SkillEventCode { get; }
        public IReadOnlyList<CharacterId> TargetIdList { get; }
        public IReadOnlyList<CharacterId> ActualTargetIdList => TargetIdList;
        public BodyPartCode DestroyedPart { get; }
        public byte DestroyCount { get; }

        public DestroyEventValueObject(
            IReadOnlyList<CharacterId> targetIdList,
            BodyPartCode destroyedPart,
            byte destroyCount = 1)
        {
            SkillEventCode = SkillEventCode.Destroy;
            TargetIdList = targetIdList;
            DestroyedPart = destroyedPart;
            DestroyCount = destroyCount;
        }
    }
}