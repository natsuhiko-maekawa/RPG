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

        public SkillValueObject Create(SkillCode skillCode)
        {
            var skill = Resolve(skillCode);
            var ailmentList = CreateAilmentValueObject(skill.AilmentList);
            return new SkillValueObject(
                skillCode: skillCode,
                range: skill.Range,
                ailmentList: ailmentList,
                buffList: null);
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
                    LuckRate: x.LuckRate))
                .ToImmutableList();
        }
    }
}