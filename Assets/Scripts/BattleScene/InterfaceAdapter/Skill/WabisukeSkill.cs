using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;
using BattleScene.InterfaceAdapter.Skill.PrimeSkill;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     侘助
    /// </summary>
    internal class WabisukeSkill : AbstractSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Wabisuke;
        public override int TechnicalPoint { get; } = 10;
        public override ImmutableList<BodyPartCode> DependencyList { get; } = ImmutableList.Create(BodyPartCode.Arm);
        public override Range Range { get; } = Range.Solo;
        public override PlayerImageCode PlayerImageCode { get; } = PlayerImageCode.Katana;
        public override MessageCode Description { get; } = MessageCode.WabisukeDescription;
        public override MessageCode AttackMessageCode { get; } = MessageCode.AttackMessage;

        public override ImmutableList<AbstractDamage> DamageList { get; }
            = ImmutableList.Create<AbstractDamage>(new BasicDamage());

        public override ImmutableList<AbstractBuff> BuffList { get; }
            = ImmutableList.Create<AbstractBuff>(new Wabisuke());
    }
}