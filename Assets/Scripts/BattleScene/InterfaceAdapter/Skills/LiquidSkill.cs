using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.SkillComponents;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;
using BattleScene.InterfaceAdapter.Skills.BaseClass;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.InterfaceAdapter.Skills
{
    /// <summary>
    ///     粘液
    /// </summary>
    public class LiquidSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Liquid;
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode { get; } = MessageCode.InvasiveMouthMessage;

        public override IReadOnlyList<BaseDamage> DamageList { get; }
            = new[] { new BasicDamage() };

        public override IReadOnlyList<BaseSlip> SlipList { get; }
            = new[] { new Suffocation() };
    }
}