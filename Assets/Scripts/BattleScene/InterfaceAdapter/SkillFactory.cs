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

        private IReadOnlyList<AilmentParameterValueObject> CreateAilmentParameterList(
            IReadOnlyList<BaseAilment> ailmentList)
        {
            return ailmentList
                .Select(x => new AilmentParameterValueObject(
                    AilmentCode: x.AilmentCode,
                    LuckRate: x.LuckRate))
                .ToList();
        }

        private IReadOnlyList<BuffParameterValueObject> CreateBuffParameterList(IReadOnlyList<BaseBuff> buffList)
        {
            return buffList
                .Select(x => new BuffParameterValueObject(
                    BuffCode: x.BuffCode,
                    Rate: x.Rate,
                    Turn: x.Turn,
                    LifetimeCode: x.LifetimeCode))
                .ToList();
        }

        private IReadOnlyList<CureParameterValueObject> CreateCureList(IReadOnlyList<BaseCure> cureList)
            => cureList
                .Select(x => new CureParameterValueObject(
                    CureExpressionCode: x.CureExpressionCode,
                    Rate: x.Rate))
                .ToList();

        private IReadOnlyList<DamageParameterValueObject> CreateDamageParameterList(
            IReadOnlyList<BaseDamage> damageList)
        {
            return damageList
                .Select(x => new DamageParameterValueObject(
                    AttackNumber: x.AttackNumber,
                    DamageRate: x.DamageRate,
                    HitRate: x.HitRate,
                    MatAttrCode: x.MatAttrCode,
                    DamageExpressionCode: x.DamageExpressionCode,
                    HitEvaluationCode: x.HitEvaluationCode,
                    AttacksWeakPointEvaluationCode: x.AttacksWeakPointEvaluationCode))
                .ToList();
        }

        private IReadOnlyList<DestroyParameterValueObject> CreateDestroyParameterList(
            IReadOnlyList<BaseDestroy> destroyPartList)
        {
            return destroyPartList
                .Select(x => new DestroyParameterValueObject(
                    BodyPartCode: x.BodyPartCode,
                    LuckRate: x.LuckRate,
                    Count: x.Count))
                .ToList();
        }

        private IReadOnlyList<EnhanceParameterValueObject> CreateEnhanceList(IReadOnlyList<BaseEnhance> enhanceList) =>
            enhanceList
                .Select(x => new EnhanceParameterValueObject(
                    EnhanceCode: x.EnhanceCode,
                    Turn: x.Turn,
                    LifetimeCode: x.LifetimeCode))
                .ToList();

        private IReadOnlyList<ResetParameterValueObject> CreateResetParameterList(IReadOnlyList<BaseRecovery> resetList)
            => resetList.Select(x => new ResetParameterValueObject(
                    AilmentCodeList: x.AilmentCodeList,
                    SlipCodeList: x.SlipCodeList,
                    BodyPartCodeList: x.BodyPartCodeList))
                .ToList();

        private IReadOnlyList<RestoreParameterValueObject> CreateRestoreParameterList(
            IReadOnlyList<BaseRestore> restoreList)
        {
            return restoreList
                .Select(x => new RestoreParameterValueObject(
                    TechnicalPoint: x.TechnicalPoint))
                .ToList();
        }

        private IReadOnlyList<SlipParameterValueObject> CreateSlipParameterList(IReadOnlyList<BaseSlip> slipList)
        {
            return slipList
                .Select(x => new SlipParameterValueObject(
                    SlipCode: x.SlipCode,
                    DamageRate: x.DamageRate,
                    DamageExpressionCode: DamageExpressionCode.Slip,
                    LuckRate: x.LuckRate))
                .ToList();
        }
    }
}