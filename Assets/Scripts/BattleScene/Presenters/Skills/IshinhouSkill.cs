﻿using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents;
using BattleScene.Presenters.SkillComponents.BaseClass;
using BattleScene.Presenters.Skills.BaseClass;

namespace BattleScene.Presenters.Skills
{
    /// <summary>
    ///     医心方
    /// </summary>
    public class IshinhouSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Ishinhou;
        public override int TechnicalPoint { get; } = 3;
        public override Range Range { get; } = Range.Oneself;
        public override MessageCode AttackMessageCode { get; } = MessageCode.ResetAilmentMessage;

        public override IReadOnlyList<BaseRecovery> RecoveryList { get; }
            = new[] { new Ishinhou() };
    }
}