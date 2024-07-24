using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;
using BattleScene.InterfaceAdapter.Skill.SkillElement;
using Range = BattleScene.Domain.Code.Range;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     痺れる粘液
    /// </summary>
    internal class NumbLiquidSkill : AbstractSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.NumbLiquid;
        public override Range Range { get; } = Range.Solo;
        public override MessageCode AttackMessageCode { get; } = MessageCode.NumbLiquidMessage;

        public override ImmutableList<AbstractDamage> DamageList { get; }
            = ImmutableList.Create<AbstractDamage>(new BasicDamage());

        public override ImmutableList<AbstractAilment> AilmentList { get; }
            = ImmutableList.Create<AbstractAilment>(new Paralysis());
    }
}