using System;
using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;
using BattleScene.InterfaceAdapter.Skill;
using BattleScene.InterfaceAdapter.Skill.BaseClass;
using VContainer;
using static BattleScene.Domain.Code.SkillCode;

namespace BattleScene.InterfaceAdapter
{
    public class SkillFactory : IFactory<SkillValueObject, SkillCode>
    {
        private readonly IObjectResolver _container;

        public SkillFactory(
            IObjectResolver container)
        {
            _container = container;
        }

        public SkillValueObject Create(SkillCode key)
        {
            var skill = Resolve(key);
            var ailmentList = CreateAilmentParameterList(skill.AilmentList);
            var buffParameterList = CreateBuffParameterList(skill.BuffList);
            var damageList = CreateDamageParameterList(skill.DamageList);
            var destroyPartList = CreateDestroyParameterList(skill.DestroyList);
            var resetParameterList = CreateResetParameterList(skill.ResetList);
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
                damageList: damageList,
                destroyedPartList: destroyPartList,
                resetParameterList: resetParameterList,
                restoreParameterList: restoreParameterList,
                slipParameterList: slipParameterList);
        }

        private BaseSkill Resolve(SkillCode skillCode)
        {
            return skillCode switch
            {
                #region Resolve Skills.

                Afterimage => _container.Resolve<AfterimageSkill>(),
                Attack => _container.Resolve<AttackSkill>(),
                Bite => _container.Resolve<BiteSkill>(),
                BiteOff => _container.Resolve<BiteOffSkill>(),
                Bleeding => _container.Resolve<BleedingSkill>(),
                Burning => _container.Resolve<BurningSkill>(),
                Confusion => _container.Resolve<ConfusionSkill>(),
                CutUp => _container.Resolve<CutUpSkill>(),
                Defence => _container.Resolve<DefenceSkill>(),
                FieldRation => _container.Resolve<FieldRationSkill>(),
                FirstAid => _container.Resolve<FirstAidSkill>(),
                FlameThrow => _container.Resolve<FlameThrowSkill>(),
                Honzougaku => _container.Resolve<HonzougakuSkill>(),
                Ishinhou => _container.Resolve<IshinhouSkill>(),
                Kuchiyose => _container.Resolve<KuchiyoseSkill>(),
                Kyoukasuigetsu => _container.Resolve<KyoukasuigetsuSkill>(),
                Liquid => _container.Resolve<LiquidSkill>(),
                Murasame => _container.Resolve<MurasameSkill>(),
                MusterStrength => _container.Resolve<MusterStrengthSkill>(),
                Nadegiri => _container.Resolve<NadegiriSkill>(),
                NumbLiquid => _container.Resolve<NumbLiquidSkill>(),
                Onikoroshi => _container.Resolve<OnikoroshiSkill>(),
                Paralysis => _container.Resolve<ParalysisSkill>(),
                Poisoning => _container.Resolve<PoisoningSkill>(),
                Punch => _container.Resolve<PunchSkill>(),
                PutScythe => _container.Resolve<PutScytheSkill>(),
                Raikiri => _container.Resolve<RaikiriSkill>(),
                RandomShots => _container.Resolve<RandomShotsSkill>(),
                Shichishitou => _container.Resolve<ShichishitouSkill>(),
                SilverBullet => _container.Resolve<SilverBulletSkill>(),
                SmokeBomb => _container.Resolve<SmokeBombSkill>(),
                StarShell => _container.Resolve<StarShellSkill>(),
                Stringer => _container.Resolve<StringerSkill>(),
                Suffocation => _container.Resolve<SuffocationSkill>(),
                TaserGun => _container.Resolve<TaserGunSkill>(),
                Utsusemi => _container.Resolve<UtsusemiSkill>(),
                Wabisuke => _container.Resolve<WabisukeSkill>(),

                #endregion

                _ => throw new ArgumentOutOfRangeException(nameof(skillCode), skillCode, null)
            };
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

        private IReadOnlyList<ResetParameterValueObject> CreateResetParameterList(IReadOnlyList<BaseReset> resetList)
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