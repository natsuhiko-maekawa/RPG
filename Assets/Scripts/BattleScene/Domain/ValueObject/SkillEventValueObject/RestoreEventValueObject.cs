using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.ValueObject.SkillEventValueObject.Interface;

namespace BattleScene.Domain.ValueObject.SkillEventValueObject
{
    public class RestoreEventValueObject : ISkillEventValueObject // 16 byte
    {
        public SkillEventCode SkillEventCode { get; }
        public int TechnicalPoint { get; }
        public IReadOnlyList<CharacterEntity> TargetList { get; }

        public RestoreEventValueObject(
            int technicalPoint,
            IReadOnlyList<CharacterEntity> targetList)
        {
            SkillEventCode = SkillEventCode.Restore;
            TechnicalPoint = technicalPoint;
            TargetList = targetList;
        }
    }
}