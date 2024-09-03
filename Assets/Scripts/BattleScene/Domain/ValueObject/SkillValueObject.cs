using System;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.Domain.ValueObject
{
    public class SkillValueObject
    {
        [Obsolete]
        public SkillCode SkillCode { get; }
        public int TechnicalPoint { get; }
        public ImmutableList<BodyPartCode> DependencyList { get; }
        public Range Range { get; }
        public SkillCommonValueObject SkillCommon { get; }
        public ImmutableList<AilmentParameterValueObject> AilmentParameterList { get; }
        public ImmutableList<BuffParameterValueObject> BuffParameterList { get; }
        public ImmutableList<DamageParameterValueObject> DamageParameterList { get; }
        public ImmutableList<DestroyedParameterValueObject> DestroyedParameterList { get; }
        public ImmutableList<RestoreParameterValueObject> RestoreParameterList { get; }

        public SkillValueObject(
            SkillCode skillCode,
            Range range,
            MessageCode messageCode,
            int technicalPoint = 0,
            ImmutableList<BodyPartCode> dependencyList = null,
            ImmutableList<AilmentParameterValueObject> ailmentList = null,
            ImmutableList<BuffParameterValueObject> buffList = null,
            ImmutableList<DamageParameterValueObject> damageList = null,
            ImmutableList<DestroyedParameterValueObject> destroyedPartList = null,
            ImmutableList<RestoreParameterValueObject> restoreParameterList = null)
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
            AilmentParameterList = ailmentList ?? ImmutableList<AilmentParameterValueObject>.Empty;
            BuffParameterList = buffList ?? ImmutableList<BuffParameterValueObject>.Empty;
            DamageParameterList = damageList ?? ImmutableList<DamageParameterValueObject>.Empty;
            DestroyedParameterList = destroyedPartList ?? ImmutableList<DestroyedParameterValueObject>.Empty;
            RestoreParameterList = restoreParameterList ?? ImmutableList<RestoreParameterValueObject>.Empty;
        }
    }
}