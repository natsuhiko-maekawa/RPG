using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.SkillComponents;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;
using BattleScene.InterfaceAdapter.Skills.BaseClass;

namespace BattleScene.InterfaceAdapter.Skills
{
    /// <summary>
    ///     侘助
    /// </summary>
    public class WabisukeSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Wabisuke;
        public override int TechnicalPoint { get; } = 10;
        public override IReadOnlyList<BodyPartCode> DependencyList { get; } = new[] { BodyPartCode.Arm };
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode { get; } = MessageCode.SkillMessage;

        public override IReadOnlyList<BaseDamage> DamageList { get; }
            = new[] { new BasicDamage() };

        public override IReadOnlyList<BaseBuff> BuffList { get; }
            = new[] { new Wabisuke() };
    }
}