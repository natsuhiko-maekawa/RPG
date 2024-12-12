using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.BaseClass;
using BattleScene.InterfaceAdapter.SkillComponents;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     七支刀
    /// </summary>
    public class ShichishitouSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Shichishitou;
        public override int TechnicalPoint { get; } = 5;
        public override Range Range { get; } = Range.Solo;
        public override IReadOnlyList<BodyPartCode> DependencyList { get; } = new[] { BodyPartCode.Arm };
        public override MessageCode AttackMessageCode { get; } = MessageCode.SkillMessage;

        public override IReadOnlyList<BaseDamage> DamageList { get; }
            = new[] { new Shichishitou() };
    }
}