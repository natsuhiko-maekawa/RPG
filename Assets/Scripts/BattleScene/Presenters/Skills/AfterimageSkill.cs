using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents;
using BattleScene.Presenters.SkillComponents.BaseClass;
using BattleScene.Presenters.Skills.BaseClass;

namespace BattleScene.Presenters.Skills
{
    /// <summary>
    ///     残像
    /// </summary>
    public class AfterimageSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Afterimage;
        public override Range Range { get; } = Range.Oneself;
        public override MessageCode AttackMessageCode { get; } = MessageCode.AfterimageMessage;

        public override IReadOnlyList<BaseBuff> BuffList { get; }
            = new[] { new AfterImage() };
    }
}