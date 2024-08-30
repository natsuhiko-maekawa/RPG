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
        public SkillCommonValueObject SkillCommon { get; }
        public ImmutableList<AilmentValueObject> AilmentList { get; }
        public ImmutableList<BuffValueObject> BuffList { get; }
        public ImmutableList<DamageParameterValueObject> DamageList { get; }

        public SkillValueObject(
            SkillCode skillCode,
            Range range,
            MessageCode messageCode,
            int technicalPoint = 0,
            ImmutableList<BodyPartCode> dependencyList = null,
            ImmutableList<AilmentValueObject> ailmentList = null,
            ImmutableList<BuffValueObject> buffList = null,
            ImmutableList<DamageParameterValueObject> damageList = null)
        {
            SkillCode = skillCode;
            Range = range;
            TechnicalPoint = technicalPoint;
            DependencyList = dependencyList ?? ImmutableList<BodyPartCode>.Empty;
            SkillCommon = new SkillCommonValueObject(
                skillCode: skillCode,
                technicalPoint: technicalPoint,
                dependencyList: dependencyList,
                range: range,
                messageCode: messageCode);
            AilmentList = ailmentList ?? ImmutableList<AilmentValueObject>.Empty;
            BuffList = buffList ?? ImmutableList<BuffValueObject>.Empty;
            DamageList = damageList ?? ImmutableList<DamageParameterValueObject>.Empty;
        }
    }
}