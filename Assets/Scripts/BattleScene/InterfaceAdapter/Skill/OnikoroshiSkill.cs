using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.BaseClass;
using BattleScene.InterfaceAdapter.SkillComponents;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     鬼殺し
    /// </summary>
    public class OnikoroshiSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Onikoroshi;
        public override IReadOnlyList<BodyPartCode> DependencyList { get; } = new[] { BodyPartCode.Arm };
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode { get; } = MessageCode.OnikoroshiMessage;

        public override IReadOnlyList<BaseAilment> AilmentList { get; }
            = new[] { new Confusion() };
    }
}