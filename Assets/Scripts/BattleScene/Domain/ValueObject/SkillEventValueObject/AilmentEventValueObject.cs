using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject.SkillEventValueObject.Interface;

namespace BattleScene.Domain.ValueObject.SkillEventValueObject
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