using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Domain.Entities;
using BattleScene.Domain.ValueObjects.SkillEventValueObject.Interface;

namespace BattleScene.Domain.ValueObjects.SkillEventValueObject
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