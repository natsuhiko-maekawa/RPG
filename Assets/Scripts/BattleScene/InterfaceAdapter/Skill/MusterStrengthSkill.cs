using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;
using BattleScene.InterfaceAdapter.Skill.PrimeSkill;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     力溜め
    /// </summary>
    internal class MusterStrengthSkill : AbstractSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.MusterStrength;
        public override int TechnicalPoint { get; } = 3;
        public override Range Range { get; } = Range.Oneself;
        public override PlayerImageCode PlayerImageCode { get; } = PlayerImageCode.Katana;
        public override MessageCode Description { get; } = MessageCode.MusterStrengthDescription;
        public override MessageCode AttackMessageCode { get; } = MessageCode.BuffMessage;

        public override ImmutableList<AbstractBuff> BuffList { get; }
            = ImmutableList.Create<AbstractBuff>(new MusterStrength());
    }
}