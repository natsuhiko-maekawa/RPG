﻿using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.BaseClass;
using BattleScene.InterfaceAdapter.SkillComponents;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     スモークボム
    /// </summary>
    public class SmokeBombSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.SmokeBomb;
        public override int TechnicalPoint { get; } = 5;
        public override Range Range { get; } = Range.Line;
        public override IReadOnlyList<BodyPartCode> DependencyList { get; } = new[] { BodyPartCode.Arm };
        public override MessageCode AttackMessageCode { get; } = MessageCode.SkillMessage;

        public override IReadOnlyList<BaseAilment> AilmentList { get; }
            = new[] { new EnemyBlind() };
    }
}