using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.Domain.ValueObject
{
    public class SkillValueObject
    {
        public SkillCommonValueObject SkillCommon { get; }
        public ImmutableList<AilmentParameterValueObject> AilmentParameterList { get; }
        public ImmutableList<BuffParameterValueObject> BuffParameterList { get; }
        public ImmutableList<DamageParameterValueObject> DamageParameterList { get; }
        public ImmutableList<DestroyParameterValueObject> DestroyedParameterList { get; }
        public ImmutableList<RestoreParameterValueObject> RestoreParameterList { get; }
        public ImmutableList<SlipParameterValueObject> SlipParameterList { get; }

        public SkillValueObject(
            SkillCode skillCode,
            Range range,
            MessageCode attackMessageCode,
            int technicalPoint,
            ImmutableList<BodyPartCode> dependencyList,
            ImmutableList<AilmentParameterValueObject> ailmentList = null,
            ImmutableList<BuffParameterValueObject> buffList = null,
            ImmutableList<DamageParameterValueObject> damageList = null,
            ImmutableList<DestroyParameterValueObject> destroyedPartList = null,
            ImmutableList<RestoreParameterValueObject> restoreParameterList = null,
            IList<SlipParameterValueObject> slipParameterList = null)
        {
            SkillCommon = new SkillCommonValueObject(
                skillCode: skillCode,
                technicalPoint: technicalPoint,
                dependencyList: dependencyList,
                range: range,
                attackMessageCode: attackMessageCode);
            AilmentParameterList = ailmentList ?? ImmutableList<AilmentParameterValueObject>.Empty;
            BuffParameterList = buffList ?? ImmutableList<BuffParameterValueObject>.Empty;
            DamageParameterList = damageList ?? ImmutableList<DamageParameterValueObject>.Empty;
            DestroyedParameterList = destroyedPartList ?? ImmutableList<DestroyParameterValueObject>.Empty;
            RestoreParameterList = restoreParameterList ?? ImmutableList<RestoreParameterValueObject>.Empty;
            SlipParameterList = slipParameterList?.ToImmutableList() ?? ImmutableList<SlipParameterValueObject>.Empty;
        }
    }
}