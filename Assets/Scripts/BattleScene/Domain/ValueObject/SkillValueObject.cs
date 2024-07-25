using System.Collections.Immutable;
using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public class SkillValueObject
    {
        public SkillCode SkillCode { get; }
        public int TechnicalPoint { get; }
        public ImmutableList<BodyPartCode> DependencyList { get; }
        public Range Range { get; }
        public ImmutableList<AilmentValueObject> AilmentList { get; }
        public ImmutableList<BuffValueObject> BuffList { get; }

        public SkillValueObject(
            SkillCode skillCode,
            Range range,
            int technicalPoint = 0,
            ImmutableList<BodyPartCode> dependencyList = null,
            ImmutableList<AilmentValueObject> ailmentList = null,
            ImmutableList<BuffValueObject> buffList = null)
        {
            SkillCode = skillCode;
            Range = range;
            TechnicalPoint = technicalPoint;
            DependencyList = dependencyList ?? ImmutableList<BodyPartCode>.Empty;
            AilmentList = ailmentList ?? ImmutableList<AilmentValueObject>.Empty;
            BuffList = buffList ?? ImmutableList<BuffValueObject>.Empty;
        }
    }
}