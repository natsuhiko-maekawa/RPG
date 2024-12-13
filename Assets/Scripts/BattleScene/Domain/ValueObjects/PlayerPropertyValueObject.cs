using System.Collections.Generic;
using BattleScene.Domain.Codes;

namespace BattleScene.Domain.ValueObjects
{
    public class PlayerPropertyValueObject
    {
        public PlayerPropertyValueObject(
            int technicalPoint,
            IReadOnlyList<SkillCode> fatalitySkillList)
        {
            TechnicalPoint = technicalPoint;
            FatalitySkillList = fatalitySkillList;
        }

        public int TechnicalPoint { get; }
        public IReadOnlyList<SkillCode> FatalitySkillList { get; }
    }
}