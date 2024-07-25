using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;
using BattleScene.InterfaceAdapter.Skill.SkillElement;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     毒針
    /// </summary>
    internal class StringerSkill : AbstractSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Stringer;
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode { get; } = MessageCode.StringerMessage;

        public override ImmutableList<AbstractDamage> DamageList { get; }
            = ImmutableList.Create<AbstractDamage>(new BasicDamage());

        public override ImmutableList<AbstractSlipDamage> SlipDamageList { get; }
            = ImmutableList.Create<AbstractSlipDamage>(new PoisoningSkill());
    }
}