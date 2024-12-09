using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject.SkillEventValueObject.Interface;

namespace BattleScene.Domain.ValueObject.SkillEventValueObject
{
    public class RestoreEventValueObject : ISkillEventValueObject // 16 byte
    {
        public SkillEventCode SkillEventCode { get; }
        public int TechnicalPoint { get; }
        public IReadOnlyList<CharacterId> TargetIdList { get; }
        public IReadOnlyList<CharacterId> ActualTargetIdList => TargetIdList;

        public RestoreEventValueObject(
            int technicalPoint,
            IReadOnlyList<CharacterId> targetIdList)
        {
            SkillEventCode = SkillEventCode.Restore;
            TechnicalPoint = technicalPoint;
            TargetIdList = targetIdList;
        }
    }
}