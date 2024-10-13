using System.Collections.Generic;
using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
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