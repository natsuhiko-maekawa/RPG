using System.Collections.Immutable;
using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public class SkillCommonValueObject
    {
        public SkillCode SkillCode { get; }
        public int TechnicalPoint { get; }
        public ImmutableList<BodyPartCode> DependencyList { get; }
        public Range Range { get; }
        public MessageCode AttackMessageCode { get; }
        
        public SkillCommonValueObject(
            SkillCode skillCode,
            int technicalPoint,
            ImmutableList<BodyPartCode> dependencyList,
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