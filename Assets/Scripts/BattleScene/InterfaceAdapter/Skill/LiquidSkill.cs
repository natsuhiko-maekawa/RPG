using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;
using BattleScene.InterfaceAdapter.Skill.PrimeSkill;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     粘液
    /// </summary>
    internal class LiquidSkill : AbstractSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Liquid;
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode { get; } = MessageCode.InvasiveMouthMessage;

        public override ImmutableList<AbstractDamage> DamageList { get; }
            = ImmutableList.Create<AbstractDamage>(new BasicDamage());

        public override ImmutableList<AbstractSlipDamage> SlipDamageList { get; }
            = ImmutableList.Create<AbstractSlipDamage>(new SuffocationSkill());
    }
}