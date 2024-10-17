using System.Collections.Generic;
using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public class SkillCommonValueObject
    {
        public SkillCode SkillCode { get; }
        public int TechnicalPoint { get; }
        public IReadOnlyList<BodyPartCode> DependencyList { get; }
        public Range Range { get; }
        public MessageCode AttackMessageCode { get; }

        public SkillCommonValueObject(
            SkillCode skillCode,
            int technicalPoint,
            IReadOnlyList<BodyPartCode> dependencyList,
            Range range,
            MessageCode attackMessageCode)
        {
            SkillCode = skillCode;
            TechnicalPoint = technicalPoint;
            DependencyList = dependencyList;
            Range = range;
            AttackMessageCode = attackMessageCode;
        }
    }
}