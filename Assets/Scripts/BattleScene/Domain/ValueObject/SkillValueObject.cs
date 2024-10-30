using System.Collections.Generic;
using BattleScene.Domain.Code;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.Domain.ValueObject
{
    public class SkillValueObject
    {
        public SkillCommonValueObject Common { get; }
        public IReadOnlyList<AilmentParameterValueObject> AilmentParameterList { get; }
        public IReadOnlyList<BuffParameterValueObject> BuffParameterList { get; }
        public IReadOnlyList<DamageParameterValueObject> DamageParameterList { get; }
        public IReadOnlyList<DestroyParameterValueObject> DestroyedParameterList { get; }
        public IReadOnlyList<ResetParameterValueObject> ResetParameterList { get; }
        public IReadOnlyList<RestoreParameterValueObject> RestoreParameterList { get; }
        public IReadOnlyList<SlipParameterValueObject> SlipParameterList { get; }

        public SkillValueObject(
            SkillCode skillCode,
            Range range,
            MessageCode attackMessageCode,
            int technicalPoint,
            IReadOnlyList<BodyPartCode> dependencyList,
            IReadOnlyList<AilmentParameterValueObject>? ailmentList = null,
            IReadOnlyList<BuffParameterValueObject>? buffList = null,
            IReadOnlyList<DamageParameterValueObject>? damageList = null,
            IReadOnlyList<DestroyParameterValueObject>? destroyedPartList = null,
            IReadOnlyList<ResetParameterValueObject>? resetParameterList = null,
            IReadOnlyList<RestoreParameterValueObject>? restoreParameterList = null,
            IReadOnlyList<SlipParameterValueObject>? slipParameterList = null)
        {
            Common = new SkillCommonValueObject(
                skillCode: skillCode,
                technicalPoint: technicalPoint,
                dependencyList: dependencyList,
                range: range,
                attackMessageCode: attackMessageCode);
            AilmentParameterList = ailmentList ?? new List<AilmentParameterValueObject>();
            BuffParameterList = buffList ?? new List<BuffParameterValueObject>();
            DamageParameterList = damageList ?? new List<DamageParameterValueObject>();
            DestroyedParameterList = destroyedPartList ?? new List<DestroyParameterValueObject>();
            ResetParameterList = resetParameterList ?? new List<ResetParameterValueObject>();
            RestoreParameterList = restoreParameterList ?? new List<RestoreParameterValueObject>();
            SlipParameterList = slipParameterList ?? new List<SlipParameterValueObject>();
        }
    }
}