using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.SkillComponents;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;
using BattleScene.InterfaceAdapter.Skills.BaseClass;

namespace BattleScene.InterfaceAdapter.Skills
{
    /// <summary>
    ///     スターシェル
    /// </summary>
    public class StarShellSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.StarShell;
        public override int TechnicalPoint { get; } = 3;
        public override Range Range { get; } = Range.Oneself;
        public override IReadOnlyList<BodyPartCode> DependencyList { get; } = new[] { BodyPartCode.Arm };
        public override MessageCode AttackMessageCode { get; } = MessageCode.SkillMessage;

        public override IReadOnlyList<BaseBuff> BuffList { get; }
            = new[] { new StarShell() };
    }
}