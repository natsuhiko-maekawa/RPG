using System;
using System.Collections.Generic;
using BattleScene.Domain.Code;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.Domain.ValueObject
{
    public class SkillValueObject
    {
        public SkillCommonValueObject Common { get; }
        public IReadOnlyList<AilmentValueObject> AilmentList { get; }
        public IReadOnlyList<BuffValueObject> BuffList { get; }
        public IReadOnlyList<CureValueObject> CureList { get; }
        public IReadOnlyList<DamageValueObject> DamageList { get; }
        public IReadOnlyList<DestroyValueObject> DestroyList { get; }
        public IReadOnlyList<EnhanceValueObject> EnhanceList { get; }
        public IReadOnlyList<RecoveryValueObject> RecoveryList { get; }
        public IReadOnlyList<RestoreValueObject> RestoreList { get; }
        public IReadOnlyList<SlipValueObject> SlipList { get; }

        public SkillValueObject(
            SkillCode skillCode,
            Range range,
            bool isAutoTarget,
            MessageCode attackMessageCode,
            int technicalPoint,
            IReadOnlyList<BodyPartCode> dependencyList,
            IReadOnlyList<AilmentValueObject>? ailmentList = null,
            IReadOnlyList<BuffValueObject>? buffList = null,
            IReadOnlyList<CureValueObject>? cureList = null,
            IReadOnlyList<DamageValueObject>? damageList = null,
            IReadOnlyList<DestroyValueObject>? destroyList = null,
            IReadOnlyList<EnhanceValueObject>? enhanceList = null,
            IReadOnlyList<RecoveryValueObject>? recoveryList = null,
            IReadOnlyList<RestoreValueObject>? restoreList = null,
            IReadOnlyList<SlipValueObject>? slipList = null)
        {
            Common = new SkillCommonValueObject(
                SkillCode: skillCode,
                TechnicalPoint: technicalPoint,
                DependencyList: dependencyList,
                Range: range,
                IsAutoTarget: isAutoTarget,
                // TODO: Fatalityスキルの場合、trueを設定するよう修正すること。
                IsFatality: false,
                AttackMessageCode: attackMessageCode);
            AilmentList = ailmentList ?? Array.Empty<AilmentValueObject>();
            BuffList = buffList ?? Array.Empty<BuffValueObject>();
            CureList = cureList ?? Array.Empty<CureValueObject>();
            DamageList = damageList ?? Array.Empty<DamageValueObject>();
            DestroyList = destroyList ?? Array.Empty<DestroyValueObject>();
            EnhanceList = enhanceList ?? Array.Empty<EnhanceValueObject>();
            RecoveryList = recoveryList ?? Array.Empty<RecoveryValueObject>();
            RestoreList = restoreList ?? Array.Empty<RestoreValueObject>();
            SlipList = slipList ?? Array.Empty<SlipValueObject>();
        }
    }
}