﻿using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;
using BattleScene.InterfaceAdapter.Skill.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     鬼殺し
    /// </summary>
    public class OnikoroshiSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Onikoroshi;
        public override IReadOnlyList<BodyPartCode> DependencyList { get; } = new [] { BodyPartCode.Arm };
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode { get; } = MessageCode.OnikoroshiMessage;

        public override IReadOnlyList<BaseAilment> AilmentList { get; }
            = new [] { new Confusion() };
    }
}