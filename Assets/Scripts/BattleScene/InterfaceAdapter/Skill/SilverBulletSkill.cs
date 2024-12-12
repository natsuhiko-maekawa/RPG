﻿using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.BaseClass;
using BattleScene.InterfaceAdapter.SkillComponents;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     シルバーバレット
    /// </summary>
    public class SilverBulletSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.SilverBullet;
        public override int TechnicalPoint { get; } = 7;
        public override Range Range { get; } = Range.Solo;
        public override IReadOnlyList<BodyPartCode> DependencyList { get; } = new[] { BodyPartCode.Arm };
        public override MessageCode AttackMessageCode { get; } = MessageCode.SkillMessage;

        public override IReadOnlyList<BaseDamage> DamageList { get; }
            = new[] { new ConstantDamage() };
    }
}