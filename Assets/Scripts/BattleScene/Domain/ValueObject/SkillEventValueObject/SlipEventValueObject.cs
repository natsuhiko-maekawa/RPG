using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject.SkillEventValueObject.Interface;

namespace BattleScene.Domain.ValueObject.SkillEventValueObject
{
    public class SlipEventValueObject : ISkillEventValueObject, IFailable // 24 byte
    {
        public SkillEventCode SkillEventCode { get; }
        public SlipCode SlipCode { get; }
        public IReadOnlyList<CharacterEntity> TargetList { get; }
        public IReadOnlyList<CharacterEntity> ActualTargetList { get; }

        public SlipEventValueObject(
            SlipCode slipCode,
            IReadOnlyList<CharacterEntity> targetList,
            IReadOnlyList<CharacterEntity> actualTargetList)
        {
            SkillEventCode = SkillEventCode.Slip;
            SlipCode = slipCode;
            TargetList = targetList;
            ActualTargetList = actualTargetList;
        }
    }
}