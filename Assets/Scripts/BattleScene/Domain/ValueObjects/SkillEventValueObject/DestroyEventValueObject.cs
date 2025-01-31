using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects.SkillEventValueObject.Interface;

namespace BattleScene.Domain.ValueObjects.SkillEventValueObject
{
    public class DestroyEventValueObject : ISkillEventValueObject, IFailable // 24 byte
    {
        public SkillEventCode SkillEventCode { get; }
        public IReadOnlyList<CharacterEntity> TargetList { get; }
        public IReadOnlyList<CharacterEntity> ActualTargetList { get; }
        public BodyPartCode DestroyedPart { get; }
        public byte DestroyCount { get; }

        public DestroyEventValueObject(
            IReadOnlyList<CharacterEntity> targetList,
            IReadOnlyList<CharacterEntity> actualTargetList,
            BodyPartCode destroyedPart,
            byte destroyCount = 1)
        {
            SkillEventCode = SkillEventCode.Destroy;
            TargetList = targetList;
            ActualTargetList = actualTargetList;
            DestroyedPart = destroyedPart;
            DestroyCount = destroyCount;
        }
    }
}