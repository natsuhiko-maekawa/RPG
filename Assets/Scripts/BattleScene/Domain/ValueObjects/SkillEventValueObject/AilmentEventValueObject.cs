using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects.SkillEventValueObject.Interface;

namespace BattleScene.Domain.ValueObjects.SkillEventValueObject
{
    public class AilmentEventValueObject : ISkillEventValueObject, IFailable // 24 byte
    {
        public SkillEventCode SkillEventCode { get; }
        public AilmentCode AilmentCode { get; }
        public IReadOnlyList<CharacterEntity> TargetList { get; }
        public IReadOnlyList<CharacterEntity> ActualTargetList { get; }

        public AilmentEventValueObject(
            AilmentCode ailmentCode,
            IReadOnlyList<CharacterEntity> targetList,
            IReadOnlyList<CharacterEntity> actualTargetList)
        {
            SkillEventCode = SkillEventCode.Ailment;
            AilmentCode = ailmentCode;
            TargetList = targetList;
            ActualTargetList = actualTargetList;
        }
    }
}