using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.BaseClass;
using BattleScene.InterfaceAdapter.SkillComponents;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     本草学
    /// </summary>
    public class HonzougakuSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Honzougaku;
        public override int TechnicalPoint { get; } = 3;
        public override Range Range { get; } = Range.Oneself;
        public override MessageCode AttackMessageCode { get; } = MessageCode.ResetAilmentMessage;

        public override IReadOnlyList<BaseRecovery> RecoveryList { get; }
            = new[] { new Honzougaku() };
    }
}