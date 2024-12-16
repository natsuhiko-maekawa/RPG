using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents;
using BattleScene.Presenters.SkillComponents.BaseClass;
using BattleScene.Presenters.Skills.BaseClass;

namespace BattleScene.Presenters.Skills
{
    /// <summary>
    ///     ファーストエイド
    /// </summary>
    public class FirstAidSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.FirstAid;
        public override int TechnicalPoint { get; } = 3;
        public override Range Range { get; } = Range.Oneself;
        public override MessageCode AttackMessageCode { get; } = MessageCode.RecoverPartMessage;

        public override IReadOnlyList<BaseRecovery> RecoveryList { get; }
            = new[] { new FirstAid() };
    }
}