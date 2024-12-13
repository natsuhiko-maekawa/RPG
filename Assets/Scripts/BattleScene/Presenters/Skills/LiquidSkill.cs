using System.Collections.Generic;
using BattleScene.Domain.Codes;
using BattleScene.Presenters.SkillComponents;
using BattleScene.Presenters.SkillComponents.BaseClass;
using BattleScene.Presenters.Skills.BaseClass;
using Range = BattleScene.Domain.Codes.Range;

namespace BattleScene.Presenters.Skills
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