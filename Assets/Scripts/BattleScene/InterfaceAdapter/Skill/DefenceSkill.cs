using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;
using BattleScene.InterfaceAdapter.Skill.PrimeSkill;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     防御
    /// </summary>
    internal class DefenceSkill : AbstractSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Defence;
        public override Range Range { get; } = Range.Oneself;
        public override PlayerImageCode PlayerImageCode { get; } = PlayerImageCode.Defence;
        public override MessageCode AttackMessageCode { get; } = MessageCode.DefenceMessage;

        public override ImmutableList<AbstractBuff> BuffList { get; }
            = ImmutableList.Create<AbstractBuff>(new Defence());

        public override ImmutableList<AbstractRestore> RestoreList { get; }
            = ImmutableList.Create<AbstractRestore>(new BasicRestore());
    }
}