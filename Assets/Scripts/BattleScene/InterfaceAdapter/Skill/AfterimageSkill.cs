using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;
using BattleScene.InterfaceAdapter.Skill.PrimeSkill;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     残像
    /// </summary>
    internal class AfterimageSkill : AbstractSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Afterimage;
        public override Range Range { get; } = Range.Oneself;
        public override MessageCode AttackMessageCode { get; } = MessageCode.AfterimageMessage;
        public override ImmutableList<AbstractBuff> BuffList { get; }
            = ImmutableList.Create<AbstractBuff>(new AfterImage());
    }
}