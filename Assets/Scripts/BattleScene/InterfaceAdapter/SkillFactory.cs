using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter
{
    public class SkillFactory : IFactory<SkillValueObject, SkillCode>
    {
        private readonly SkillServiceLocator _skillServiceLocator;

        public SkillFactory(
            SkillServiceLocator skillServiceLocator)
        {
            _skillServiceLocator = skillServiceLocator;
        }

        public SkillValueObject Create(SkillCode key)
        {
            var skill = _skillServiceLocator.Resolve(key);
            var ailmentList = CreateAilmentList(skill.AilmentList);
            var buffList = CreateBuffList(skill.BuffList);
            var cureList = CreateCureList(skill.CureList);
            var damageList = CreateDamageList(skill.DamageList);
            var destroyPartList = CreateDestroyList(skill.DestroyList);
            var enhanceList = CreateEnhanceList(skill.EnhanceList);
            var recoveryList = CreateRecoveryList(skill.RecoveryList);
            var restoreList = CreateRestoreList(skill.RestoreList);
            var slipList = CreateSlipList(skill.SlipList);
            return new SkillValueObject(
                skillCode: key,
                range: skill.Range,
                isAutoTarget: skill.IsAutoTarget,
                attackMessageCode: skill.AttackMessageCode,
                technicalPoint: skill.TechnicalPoint,
                dependencyList: skill.DependencyList,
                ailmentList: ailmentList,
                buffList: buffList,
                cureList: cureList,
                damageList: damageList,
                destroyList: destroyPartList,
                enhanceList: enhanceList,
                recoveryList: recoveryList,
                restoreList: restoreList,
                slipList: slipList);
        }

        private IReadOnlyList<AilmentValueObject> CreateAilmentList(IReadOnlyList<BaseAilment> ailmentList) =>
            ailmentList
                .Select(x => new AilmentValueObject(
                    AilmentCode: x.AilmentCode,
                    LuckRate: x.LuckRate))
                .ToArray();

        private IReadOnlyList<BuffValueObject> CreateBuffList(IReadOnlyList<BaseBuff> buffList) =>
            buffList
                .Select(x => new BuffValueObject(
                    BuffCode: x.BuffCode,
                    Rate: x.Rate,
                    Turn: x.Turn,
                    LifetimeCode: x.LifetimeCode))
                .ToArray();

        private IReadOnlyList<CureValueObject> CreateCureList(IReadOnlyList<BaseCure> cureList) =>
            cureList
                .Select(x => new CureValueObject(
                    CureExpressionCode: x.CureExpressionCode,
                    Rate: x.Rate))
                .ToArray();

        private IReadOnlyList<DamageValueObject> CreateDamageList(IReadOnlyList<BaseDamage> damageList) =>
            damageList
                .Select(x => new DamageValueObject(
                    attackNumber: x.AttackNumber,
                    damageRate: x.DamageRate,
                    hitRate: x.HitRate,
                    matAttrCode: x.MatAttrCode,
                    damageExpressionCode: x.DamageExpressionCode,
                    hitEvaluationCode: x.HitEvaluationCode,
                    attacksWeakPointEvaluationCode: x.AttacksWeakPointEvaluationCode))
                .ToArray();

        private IReadOnlyList<DestroyValueObject> CreateDestroyList(IReadOnlyList<BaseDestroy> destroyPartList) =>
            destroyPartList
                .Select(x => new DestroyValueObject(
                    BodyPartCode: x.BodyPartCode,
                    LuckRate: x.LuckRate,
                    Count: x.Count))
                .ToArray();

        private IReadOnlyList<EnhanceValueObject> CreateEnhanceList(IReadOnlyList<BaseEnhance> enhanceList) =>
            enhanceList
                .Select(x => new EnhanceValueObject(
                    EnhanceCode: x.EnhanceCode,
                    Turn: x.Turn,
                    LifetimeCode: x.LifetimeCode))
                .ToArray();

        private IReadOnlyList<RecoveryValueObject> CreateRecoveryList(IReadOnlyList<BaseRecovery> resetList) => 
            resetList.Select(x => new RecoveryValueObject(
                    AilmentCodeList: x.AilmentCodeList,
                    SlipCodeList: x.SlipCodeList,
                    BodyPartCodeList: x.BodyPartCodeList))
                .ToArray();

        private IReadOnlyList<RestoreValueObject> CreateRestoreList(IReadOnlyList<BaseRestore> restoreList) =>
            restoreList
                .Select(x => new RestoreValueObject(
                    TechnicalPoint: x.TechnicalPoint))
                .ToArray();

        private IReadOnlyList<SlipValueObject> CreateSlipList(IReadOnlyList<BaseSlip> slipList) =>
            slipList
                .Select(x => new SlipValueObject(
                    SlipCode: x.SlipCode,
                    DamageRate: x.DamageRate,
                    DamageExpressionCode: DamageExpressionCode.Slip,
                    LuckRate: x.LuckRate))
                .ToArray();
    }
}