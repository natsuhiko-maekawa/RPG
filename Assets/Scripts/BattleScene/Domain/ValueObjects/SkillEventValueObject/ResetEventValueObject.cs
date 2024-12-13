using System;
using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects.SkillEventValueObject.Interface;

namespace BattleScene.Domain.ValueObjects.SkillEventValueObject
{
    public class ResetEventValueObject : ISkillEventValueObject // 40 byte
    {
        public SkillEventCode SkillEventCode { get; }
        public IReadOnlyList<CharacterEntity> TargetList { get; }
        public IReadOnlyList<AilmentCode> ResetAilmentCodeList { get; }
        public IReadOnlyList<BodyPartCode> ResetBodyPartCodeList { get; }
        public IReadOnlyList<SlipCode> ResetSlipCodeList { get; }

        public ResetEventValueObject(
            IReadOnlyList<CharacterEntity> targetList,
            IReadOnlyList<AilmentCode>? resetAilmentCodeList, 
            IReadOnlyList<BodyPartCode>? resetBodyPartCodeList, 
            IReadOnlyList<SlipCode>? resetSlipCodeList)
        {
            SkillEventCode = SkillEventCode.Reset;
            TargetList = targetList;
            ResetAilmentCodeList = resetAilmentCodeList ?? Array.Empty<AilmentCode>();
            ResetBodyPartCodeList = resetBodyPartCodeList ?? Array.Empty<BodyPartCode>();
            ResetSlipCodeList = resetSlipCodeList ?? Array.Empty<SlipCode>();
        }
    }
}