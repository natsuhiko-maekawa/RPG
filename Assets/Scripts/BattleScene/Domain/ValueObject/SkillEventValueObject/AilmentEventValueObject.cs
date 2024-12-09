using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject.SkillEventValueObject.Interface;

namespace BattleScene.Domain.ValueObject.SkillEventValueObject
{
    public class AilmentEventValueObject : ISkillEventValueObject // 24 byte
    {
        public SkillEventCode SkillEventCode { get; }
        public AilmentCode AilmentCode { get; }
        public IReadOnlyList<CharacterId> TargetIdList { get; }
        public IReadOnlyList<CharacterId> ActualTargetIdList { get; }

        public AilmentEventValueObject(
            AilmentCode ailmentCode,
            IReadOnlyList<CharacterId> targetIdList,
            IReadOnlyList<CharacterId> actualTargetIdList)
        {
            SkillEventCode = SkillEventCode.Ailment;
            AilmentCode = ailmentCode;
            TargetIdList = targetIdList;
            ActualTargetIdList = actualTargetIdList;
        }
    }
}