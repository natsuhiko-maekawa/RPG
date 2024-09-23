using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.PrimeSkill;
using BattleScene.InterfaceAdapter.PrimeSkill.BaseClass;
using BattleScene.InterfaceAdapter.Skill.BaseClass;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     力溜め
    /// </summary>
    internal class MusterStrengthSkill : BaseSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.MusterStrength;
        public override int TechnicalPoint { get; } = 3;
        public override Range Range { get; } = Range.Oneself;
        public override PlayerImageCode PlayerImageCode { get; } = PlayerImageCode.Katana;
        public override MessageCode Description { get; } = MessageCode.MusterStrengthDescription;
        public override MessageCode AttackMessageCode { get; } = MessageCode.BuffMessage;

        public override ImmutableList<BaseBuff> BuffList { get; }
            = ImmutableList.Create<BaseBuff>(new MusterStrength());
    }
}