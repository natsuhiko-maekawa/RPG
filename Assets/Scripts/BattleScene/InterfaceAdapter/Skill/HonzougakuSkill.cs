using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;
using BattleScene.InterfaceAdapter.Skill.SkillElement;

namespace BattleScene.InterfaceAdapter.Skill
{
    /// <summary>
    ///     本草学
    /// </summary>
    internal class HonzougakuSkill : AbstractSkill
    {
        public override SkillCode SkillCode { get; } = SkillCode.Honzougaku;
        public override int TechnicalPoint { get; } = 3;
        public override Range Range { get; } = Range.Oneself;
        public override PlayerImageCode PlayerImageCode { get; } = PlayerImageCode.Katana;
        public override MessageCode Description { get; } = MessageCode.HonzougakuDescription;
        public override MessageCode AttackMessageCode { get; } = MessageCode.RemoveAilmentsMessage;

        public override ImmutableList<AbstractReset> ResetList { get; } 
            = ImmutableList.Create<AbstractReset>(new Honzougaku());
    }
}