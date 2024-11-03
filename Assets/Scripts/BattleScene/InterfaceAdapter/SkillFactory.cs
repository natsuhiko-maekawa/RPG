using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;

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
            var ailmentList = CreateAilmentParameterList(skill.AilmentList);
            var buffParameterList = CreateBuffParameterList(skill.BuffList);
            var cureList = CreateCureList(skill.CureList);
            var damageList = CreateDamageParameterList(skill.DamageList);
            var destroyPartList = CreateDestroyParameterList(skill.DestroyList);
            var enhanceList = CreateEnhanceList(skill.EnhanceList);
            var recoveryParameterList = CreateResetParameterList(skill.RecoveryList);
            var restoreParameterList = CreateRestoreParameterList(skill.RestoreList);
            var slipParameterList = CreateSlipParameterList(skill.SlipList);
            return new SkillValueObject(
                skillCode: key,
                range: skill.Range,
                attackMessageCode: skill.AttackMessageCode,
                technicalPoint: skill.TechnicalPoint,
                dependencyList: skill.DependencyList,
                ailmentList: ailmentList,
                buffList: buffParameterList,
                cureList: cureList,
                damageList: damageList,
                destroyedPartList: destroyPartList,
                enhanceList: enhanceList,
                resetParameterList: recoveryParameterList,
                restoreParameterList: restoreParameterList,
                slipParameterList: slipParameterList);
        }

        private IReadOnlyList<AilmentValueObject> CreateAilmentParameterList(
            IReadOnlyList<BaseAilment> ailmentList)
        {
            return ailmentList
                .Select(x => new AilmentValueObject(
                    AilmentCode: x.AilmentCode,
                    LuckRate: x.LuckRate))
                .ToList();
        }

        private IReadOnlyList<BuffValueObject> CreateBuffParameterList(IReadOnlyList<BaseBuff> buffList)
        {
            return buffList
                .Select(x => new BuffValueObject(
                    BuffCode: x.BuffCode,
                    Rate: x.Rate,
                    Turn: x.Turn,
                    LifetimeCode: x.LifetimeCode))
                .ToList();
        }

        private IReadOnlyList<CureValueObject> CreateCureList(IReadOnlyList<BaseCure> cureList)
            => cureList
                .Select(x => new CureValueObject(
                    CureExpressionCode: x.CureExpressionCode,
                    Rate: x.Rate))
                .ToList();

        private IReadOnlyList<DamageValueObject> CreateDamageParameterList(
            IReadOnlyList<BaseDamage> damageList)
        {
            return damageList
                .Select(x => new DamageValueObject(
                    AttackNumber: x.AttackNumber,
                    DamageRate: x.DamageRate,
                    HitRate: x.HitRate,
                    MatAttrCode: x.MatAttrCode,
                    DamageExpressionCode: x.DamageExpressionCode,
                    HitEvaluationCode: x.HitEvaluationCode,
                    AttacksWeakPointEvaluationCode: x.AttacksWeakPointEvaluationCode))
                .ToList();
        }

        private IReadOnlyList<DestroyValueObject> CreateDestroyParameterList(
            IReadOnlyList<BaseDestroy> destroyPartList)
        {
            return destroyPartList
                .Select(x => new DestroyValueObject(
                    BodyPartCode: x.BodyPartCode,
                    LuckRate: x.LuckRate,
                    Count: x.Count))
                .ToList();
        }

        private IReadOnlyList<EnhanceValueObject> CreateEnhanceList(IReadOnlyList<BaseEnhance> enhanceList) =>
            enhanceList
                .Select(x => new EnhanceValueObject(
                    EnhanceCode: x.EnhanceCode,
                    Turn: x.Turn,
                    LifetimeCode: x.LifetimeCode))
                .ToList();

        private IReadOnlyList<RecoveryValueObject> CreateResetParameterList(IReadOnlyList<BaseRecovery> resetList)
            => resetList.Select(x => new RecoveryValueObject(
                    AilmentCodeList: x.AilmentCodeList,
                    SlipCodeList: x.SlipCodeList,
                    BodyPartCodeList: x.BodyPartCodeList))
                .ToList();

        private IReadOnlyList<RestoreValueObject> CreateRestoreParameterList(
            IReadOnlyList<BaseRestore> restoreList)
        {
            return restoreList
                .Select(x => new RestoreValueObject(
                    TechnicalPoint: x.TechnicalPoint))
                .ToList();
        }

        private IReadOnlyList<SlipValueObject> CreateSlipParameterList(IReadOnlyList<BaseSlip> slipList)
        {
            return slipList
                .Select(x => new SlipValueObject(
                    SlipCode: x.SlipCode,
                    DamageRate: x.DamageRate,
                    DamageExpressionCode: DamageExpressionCode.Slip,
                    LuckRate: x.LuckRate))
                .ToList();
        }
    }
}