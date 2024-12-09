using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject.SkillEventValueObject.Interface;

namespace BattleScene.Domain.ValueObject.SkillEventValueObject
{
    public class SlipEventValueObject : ISkillEventValueObject // 24 byte
    {
        public SkillEventCode SkillEventCode { get; }
        public SlipCode SlipCode { get; }
        public IReadOnlyList<CharacterId> TargetIdList { get; }
        public IReadOnlyList<CharacterId> ActualTargetIdList { get; }

        public SlipEventValueObject(
            SlipCode slipCode,
            IReadOnlyList<CharacterId> targetIdList,
            IReadOnlyList<CharacterId> actualTargetIdList)
        {
            SkillEventCode = SkillEventCode.Slip;
            SlipCode = slipCode;
            TargetIdList = targetIdList;
            ActualTargetIdList = actualTargetIdList;
        }
    }
}