﻿using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;
using BattleScene.InterfaceAdapter.Skill.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     スターシェル
    /// </summary>
    public class StarShellSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.StarShell;
        public override int TechnicalPoint { get; } = 3;
        public override Range Range { get; } = Range.Oneself;
        public override IReadOnlyList<BodyPartCode> DependencyList { get; } = new [] { BodyPartCode.Arm };
        public override MessageCode AttackMessageCode { get; } = MessageCode.BuffMessage;

        public override IReadOnlyList<BaseBuff> BuffList { get; }
            = new [] { new StarShell() };
    }
}