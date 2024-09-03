using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Skill;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;
using VContainer;
using static BattleScene.Domain.Code.SkillCode;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory
{
    public class SkillFactory : IFactory<SkillValueObject, SkillCode>
    {
        private readonly IObjectResolver _container;

        public SkillFactory(
            IObjectResolver container)
        {
            _container = container;
        }

        public SkillValueObject Create(SkillCode skillCode)
        {
            var skill = Resolve(skillCode);
            // TODO: 命名をやり直す
            var ailmentList = CreateAilmentValueObject(skill.AilmentList);
            var damageList = CreateDamageValueObject(skill.DamageList);
            var buffParameterList = CreateBuffParameterList(skill.BuffList);
            var restoreParameterList = CreateRestoreParameterList(skill.RestoreList);
            return new SkillValueObject(
                skillCode: skillCode,
                range: skill.Range,
                messageCode: skill.AttackMessageCode,
                ailmentList: ailmentList,
                buffList: buffParameterList,
                damageList: damageList);
        }

        private AbstractSkill Resolve(SkillCode skillCode)
        {
            return skillCode switch
            {
                #region Resolve Skills.
                Afterimage => _container.Resolve<AfterimageSkill>(),
                Attack => _container.Resolve<AttackSkill>(),
                Bite => _container.Resolve<BiteSkill>(),
                BiteOff => _container.Resolve<BiteOffSkill>(),
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
                Punch => _container.Resolve<PunchSkill>(),
                PutScythe => _container.Resolve<PutScytheSkill>(),
                Raikiri => _container.Resolve<RaikiriSkill>(),
                RandomShots => _container.Resolve<RandomShotsSkill>(),
                Shichishitou => _container.Resolve<ShichishitouSkill>(),
                SilverBullet => _container.Resolve<SilverBulletSkill>(),
                SmokeBomb => _container.Resolve<SmokeBombSkill>(),
                StarShell => _container.Resolve<StarShellSkill>(),
                Stringer => _container.Resolve<StringerSkill>(),
                TaserGun => _container.Resolve<TaserGunSkill>(),
                Utsusemi => _container.Resolve<UtsusemiSkill>(),
                Wabisuke => _container.Resolve<WabisukeSkill>(),
                #endregion
                _ => throw new ArgumentOutOfRangeException(nameof(skillCode), skillCode, null)
            };
        }

        private ImmutableList<AilmentValueObject> CreateAilmentValueObject(IList<AbstractAilment> ailmentList)
        {
            if (ailmentList == null) return ImmutableList<AilmentValueObject>.Empty;
            return ailmentList
                .Select(x => new AilmentValueObject(
                    AilmentCode: x.AilmentCode,
                    LuckRate: x.LuckRate))
                .ToImmutableList();
        }

        private ImmutableList<BuffParameterValueObject> CreateBuffParameterList(IList<AbstractBuff> buffList)
        {
            return buffList
                .Select(x => new BuffParameterValueObject(
                    BuffCode: x.BuffCode,
                    Rate: x.Rate,
                    Turn: x.Turn,
                    LifetimeCode: x.LifetimeCode))
                .ToImmutableList();
        }

        private ImmutableList<DamageParameterValueObject> CreateDamageValueObject(IList<AbstractDamage> damageList)
        {
            if (damageList == null) return ImmutableList<DamageParameterValueObject>.Empty;
            return damageList
                .Select(x => new DamageParameterValueObject(
                    AttackNumber: x.AttackNumber,
                    DamageRate: x.DamageRate,
                    HitRate: x.HitRate,
                    MatAttrCode: x.MatAttrCode,
                    DamageExpressionCode: x.DamageExpressionCode,
                    HitEvaluationCode: x.HitEvaluationCode,
                    AttacksWeakPointEvaluationCode: x.AttacksWeakPointEvaluationCode))
                .ToImmutableList();
        }

        private ImmutableList<RestoreParameterValueObject> CreateRestoreParameterList(
            IList<AbstractRestore> restoreList)
        {
            return restoreList
                .Select(x => new RestoreParameterValueObject(
                    TechnicalPoint: x.TechnicalPoint))
                .ToImmutableList();
        }
    }
}