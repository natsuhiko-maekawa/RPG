using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents;
using BattleScene.Presenters.SkillComponents.BaseClass;
using BattleScene.Presenters.Skills.BaseClass;

namespace BattleScene.Presenters.Skills
{
    /// <summary>
    ///     力溜め
    /// </summary>
    public class MusterStrengthSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.MusterStrength;
        public override int TechnicalPoint { get; } = 3;
        public override Range Range { get; } = Range.Oneself;
        public override MessageCode AttackMessageCode { get; } = MessageCode.SkillMessage;

        public override IReadOnlyList<BaseBuff> BuffList { get; }
            = new[] { new MusterStrength() };
    }
}