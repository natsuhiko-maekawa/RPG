using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.BaseClass;
using BattleScene.InterfaceAdapter.SkillComponents;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
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