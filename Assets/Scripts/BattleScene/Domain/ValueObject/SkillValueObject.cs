using System;
using System.Collections.Generic;
using BattleScene.Domain.Code;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.Domain.ValueObject
{
    public class SkillValueObject
    {
        public SkillCommonValueObject Common { get; }
        public IReadOnlyList<AilmentValueObject> AilmentParameterList { get; }
        public IReadOnlyList<BuffValueObject> BuffParameterList { get; }
        public IReadOnlyList<CureValueObject> CureParameterList { get; }
        public IReadOnlyList<DamageValueObject> DamageParameterList { get; }
        public IReadOnlyList<DestroyValueObject> DestroyedParameterList { get; }
        public IReadOnlyList<EnhanceValueObject> EnhanceParameterList { get; }
        public IReadOnlyList<RecoveryValueObject> ResetParameterList { get; }
        public IReadOnlyList<RestoreValueObject> RestoreParameterList { get; }
        public IReadOnlyList<SlipValueObject> SlipParameterList { get; }

        public SkillValueObject(
            SkillCode skillCode,
            Range range,
            MessageCode attackMessageCode,
            int technicalPoint,
            IReadOnlyList<BodyPartCode> dependencyList,
            IReadOnlyList<AilmentValueObject>? ailmentList = null,
            IReadOnlyList<BuffValueObject>? buffList = null,
            IReadOnlyList<CureValueObject>? cureList = null,
            IReadOnlyList<DamageValueObject>? damageList = null,
            IReadOnlyList<DestroyValueObject>? destroyedPartList = null,
            IReadOnlyList<EnhanceValueObject>? enhanceList = null,
            IReadOnlyList<RecoveryValueObject>? resetParameterList = null,
            IReadOnlyList<RestoreValueObject>? restoreParameterList = null,
            IReadOnlyList<SlipValueObject>? slipParameterList = null)
        {
            Common = new SkillCommonValueObject(
                skillCode: skillCode,
                technicalPoint: technicalPoint,
                dependencyList: dependencyList,
                range: range,
                attackMessageCode: attackMessageCode);
            AilmentParameterList = ailmentList ?? Array.Empty<AilmentValueObject>();
            BuffParameterList = buffList ?? Array.Empty<BuffValueObject>();
            CureParameterList = cureList ?? Array.Empty<CureValueObject>();
            DamageParameterList = damageList ?? Array.Empty<DamageValueObject>();
            DestroyedParameterList = destroyedPartList ?? Array.Empty<DestroyValueObject>();
            EnhanceParameterList = enhanceList ?? Array.Empty<EnhanceValueObject>();
            ResetParameterList = resetParameterList ?? Array.Empty<RecoveryValueObject>();
            RestoreParameterList = restoreParameterList ?? Array.Empty<RestoreValueObject>();
            SlipParameterList = slipParameterList ?? Array.Empty<SlipValueObject>();
        }
    }
}