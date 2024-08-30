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
        public MessageCode MessageCode { get; }
        
        public SkillCommonValueObject(
            SkillCode skillCode,
            int technicalPoint,
            ImmutableList<BodyPartCode> dependencyList,
            Range range,
            MessageCode messageCode)
        {
            SkillCode = skillCode;
            TechnicalPoint = technicalPoint;
            DependencyList = dependencyList;
            Range = range;
            MessageCode = messageCode;
        }
    }
}