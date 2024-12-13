using System;
using BattleScene.Domain.Codes;
using BattleScene.InterfaceAdapter.Skills;
using BattleScene.InterfaceAdapter.Skills.BaseClass;
using VContainer;
using static BattleScene.Domain.Codes.SkillCode;

namespace BattleScene.InterfaceAdapter
{
    public class SkillServiceLocator
    {
        private readonly IObjectResolver _container;

        public SkillServiceLocator(
            IObjectResolver container)
        {
            _container = container;
        }

        public BaseSkill Resolve(SkillCode skillCode)
        {
            return skillCode switch
            {
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
                Metronome => _container.Resolve<MetronomeSkill>(),
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
                _ => throw new ArgumentOutOfRangeException(nameof(skillCode), skillCode, null)
            };
        }
    }
}