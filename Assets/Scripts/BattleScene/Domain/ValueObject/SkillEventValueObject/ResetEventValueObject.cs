using System;
using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject.SkillEventValueObject.Interface;

namespace BattleScene.Domain.ValueObject.SkillEventValueObject
{
    public class ResetEventValueObject : ISkillEventValueObject // 40 byte
    {
        public SkillEventCode SkillEventCode { get; }
        public IReadOnlyList<CharacterId> TargetIdList { get; }
        public IReadOnlyList<CharacterId> ActualTargetIdList => TargetIdList;
        public IReadOnlyList<AilmentCode> ResetAilmentCodeList { get; }
        public IReadOnlyList<BodyPartCode> ResetBodyPartCodeList { get; }
        public IReadOnlyList<SlipCode> ResetSlipCodeList { get; }

        public ResetEventValueObject(
            IReadOnlyList<CharacterId> targetIdList,
            IReadOnlyList<AilmentCode>? resetAilmentCodeList, 
            IReadOnlyList<BodyPartCode>? resetBodyPartCodeList, 
            IReadOnlyList<SlipCode>? resetSlipCodeList)
        {
            SkillEventCode = SkillEventCode.Reset;
            TargetIdList = targetIdList;
            ResetAilmentCodeList = resetAilmentCodeList ?? Array.Empty<AilmentCode>();
            ResetBodyPartCodeList = resetBodyPartCodeList ?? Array.Empty<BodyPartCode>();
            ResetSlipCodeList = resetSlipCodeList ?? Array.Empty<SlipCode>();
        }
    }
}